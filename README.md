# HyperMap

## Getting Started

You can find this package via NuGet: **HyperMap**

### Overview

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


