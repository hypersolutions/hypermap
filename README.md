# HyperMap

## Getting Started

You can find this packages via NuGet: 


[**Hyper.Map**](https://www.nuget.org/packages/Hyper.Map)

[**Hyper.Map.DependencyInjection**](https://www.nuget.org/packages/Hyper.Map.DependencyInjection)

**Note** that _Hyper.Map.DependencyInjection_ provides support for the _Microsoft.Extensions.DependencyInjection_ NuGet package so you can automatically register _all_ _IMapper_ instances with its IoC.

## Overview

Why create HyperMap. This provides a unique solution to the common pattern of copying object data around - something that is a very common, tedious pattern in code.

There are frameworks that already do this but _HyperMap_ utilises the _Roslyn_ compiler to generate code at runtime to give you the same performance benefits as if you wrote the code yourself.

### Your First Map Class

Given the following simple source and target classes:

```
public class Address
{
    public int HouseNumber { get; set; }
    public string Street { get; set; }
    public string Town { get; set; }
}

public class AddressView
{
    public string HouseNumber { get; set; }
    public string Street { get; set; }
    public string Town { get; set; }
}
```

you define a map class between the two in the direction of the mapping:

```
public sealed class AddressToAddressViewMap : MapBase<Address, AddressView>
{
    public AddressToAddressViewMap()
    {
        For(p => p.HouseNumber).MapTo(p => p.HouseNumber).Using<AnyToStringTypeConverter<int>>();
    }
}
```

Matching names and types are _automatically_ mapped for you. You can also specify a specific converter to use (as above) if required.

And that's it. Your first mapping has been defined.

### Building the Maps

To compile and build your mappings, you need to use the _MappingBuilder_ class:

```
 var factory = MappingBuilder.DiscoverIn<Address>().BuildFactory();
```

This allows you to select multiple assemblies to _discover_ your mappings in by calling _AndDiscoverIn_ in the chain. Once complete, call the _BuildFactory_ method.

This returns an instance of the _IMappingFactory_ which you can use to inject directly into your classes and _resolve_ mappings. Alternatively, you can add all of your mappings into your IoC of choice. The _Create_ method on the _MappingFactory_ returns instances of the _IMapper<TSource, TTarget>_ interface.

**Note** that the factory caches instances of mappings in a thread-safe manner as all mapping classes generated are stateless and can be reused.

You can also provide _options_ for the compiler via the _BuildFactory_ method mentioned above. By default the options use _HyperMap.Custom.Mappings_ as its namespace but you can override this:

```
var options = new CompilerOptions{AssemblyName = "MyNamespace"};
var factory = MappingBuilder.DiscoverIn<Address>().BuildFactory(options);
```

### Resolving Mappers

The _MappingFactory_ allows you to resolve instances of the _IMapper<TSource, TTarget>_ from the underlying loaded assembly. As stated above, these are stateless and cached within the _MappingFactory_ class. You can resolve a map class by:

```
var factory = MappingBuilder.DiscoverIn<Address>().BuildFactory();
var mapper = factory.Create<Address, AddressView>();
```

**Note** that if a mapper is not found then it will return null instead of throwing an exception.

Given a source class then you can map to the target:

```
var source = new Address {HouseNumber = 742, Street = "Evergreen Terrace", Town = "Springfield"};
var target = mapper.Map(source);
```

### Converters

There is a large set of built-in converters to handle common conversion situations but each converter implements the _IConverterType<TSource, TTarget>_ type:

```
public class SingleLineAddressConverter : ITypeConverter<Address, string>
{
    public IMappingFactory MappingFactory { get; set; }

    public string Convert(Address from)
    {
        return $"{from.HouseNumber} {from.Street}, {from.Town}";
    }
}
```

The above is an example of a converter that takes a source object _Address_ and converts it to a single-line string. The resulting map class would look like:

```
public sealed class AddressToSingleLineAddressViewMap : MapBase<Address, SingleLineAddressView>
{
    public AddressToSingleLineAddressViewMap()
    {
        For(p => p).MapTo(p => p.Display).Using<SingleLineAddressConverter>();
    }
}
```

Also note the _MappingFactory_ property. This allows you to resolve other mappings to use in your converter. For example given the following source and target classes:

```
public class Customer
{
    public string Name { get; set; }
    public Order MostRecentOrder { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}

public class CustomerView
{
    public string Name { get; set; }
    public OrderView MostRecentOrder { get; set; }
}

public class OrderView
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}

```

You can create maps such as:

```
public sealed class CustomerToCustomerViewMap : MapBase<Customer, CustomerView>
{
    public CustomerToCustomerViewMap()
    {
        For(p => p.MostRecentOrder).MapTo(p => p.MostRecentOrder).UsingFactory<Order, OrderView>();
    }
}

public sealed class OrderToOrderViewMap : MapBase<Order, OrderView>
{
    public OrderToOrderViewMap()
    {
        
    }
}
```

**There** is also a base class _TypeConverter_ that you can inherit from which provides an override for the Convert method.

### Collections

The basic set of collection mappings are supported via a set of _converters_. Given the following classes you wish to map between:

```
public class Customer
{
    public string Name { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}

public class CustomerListView
{
    public string Name { get; set; }
    public List<OrderView> Orders { get; set; }
}

public class OrderView
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}
```

You can create a map between an IEnumerable source and List target collection like:

```
public sealed class CustomerToCustomerListViewMap : MapBase<Customer, CustomerListView>
{
    public CustomerToCustomerListViewMap()
    {
        For(p => p.Orders).MapTo(p => p.Orders).Using<EnumerableToListTypeConverter<Order, OrderView>>();
    }
}
```

Other supported collections are:

* **EnumerableTypeConverter** which is IEnumerable to IEnumerable
* **ListToEnumerableTypeConverter** which is from a List to IEnumerable
* **ListTypeConverter** which is List to List

There is also a base class so you can create your own converters by extending _EnumerableTypeConverterBase_:

```
public sealed class ListTypeConverter<TSource, TTarget> 
    : EnumerableTypeConverterBase<TSource, TTarget>, ITypeConverter<List<TSource>, List<TTarget>>
    where TSource : class where TTarget : class
{
    public IMappingFactory MappingFactory { get; set; }
    
    public List<TTarget> Convert(List<TSource> from)
    {
        return ConvertInternal(from, MappingFactory)?.ToList();
    }
}
```

### Microsoft Extensions Dependency Injection Support

If you install the _Hyper.Map.DependencyInjection_ NuGet package, it will add an extension method to the _IMappingFactory_ interface to register all the instances with it.

As previously stated, all the _IMapper_ instances are stateless so the extension adds each as a _singleton_ into the _ServiceCollection_:

```
var services = new ServiceCollection();

var factory = MappingBuilder.DiscoverIn<Address>().BuildFactory().RegisterWith(services);
```

You can inject the _IMapper<TSource, TTarget>_ into any classes within your code as normal instead of the _IMappingFactory_.

**Note** that the _IMappingFactory_ has a method called _GetAll_ which you can use to get all instances and add into any of your own IoC implementations. You can get its interface type:

```
var services = new ServiceCollection();
var factory = MappingBuilder.DiscoverIn<Address>().BuildFactory();

foreach (var mappingInstance in factory.GetAll())
{
    var mappingInterfaceType = mappingInstance.GetType().GetInterfaces().First();
    services.AddSingleton(mappingInterfaceType, mappingInstance);
}
```

**Where** you can replace _services_ with your own IoC. 

## Developer Notes

### Building and Publishing

From the root, to build, run:

```bash
dotnet build --configuration Release
```

To run all the unit and integration tests, run:

```bash
dotnet test --no-build --configuration Release
```

To create the packages, run (**optional** as the build generates the packages):
 
```bash
cd src/HyperMap
dotnet pack --no-build --configuration Release

cd src/HyperMap.DependencyInjection
dotnet pack --no-build --configuration Release
```

To publish the packages to the nuget feed on nuget.org:

```bash
dotnet nuget push ./bin/Release/Hyper.Map.2.0.0.nupkg -k [THE API KEY] -s https://api.nuget.org/v3/index.json

dotnet nuget push ./bin/Release/Hyper.Map.DependencyInjection.2.0.0.nupkg -k [THE API KEY] -s https://api.nuget.org/v3/index.json
```

## Links

* **GitFlow** https://datasift.github.io/gitflow/IntroducingGitFlow.html
