using System.Linq;
using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using Shouldly;
using Xunit;

namespace HyperMap.Integration.Tests
{
    public class SourceToTargetCollectionMapTests
    {
        [Fact]
        public void WithEnumerable()
        {
            var factory = MappingBuilder.DiscoverIn<Customer>().BuildFactory();
            var mapper = factory.Create<Customer, CustomerEnumView>();
            var source = new Customer
            {
                Name = "Homer Simpson",
                Orders = new[]
                {
                    new Order
                    {
                        Id = 101, Description = "Slippers", Quantity = 1
                    },
                    new Order
                    {
                        Id = 201, Description = "Socks", Quantity = 3
                    }
                }
            };

            var target = mapper.Map(source);
            
            target.Name.ShouldBe(source.Name);
            var firstSourceOrder = source.Orders.First();
            var firstTargetOrder = target.Orders.First();
            firstTargetOrder.Id.ShouldBe(firstSourceOrder.Id);
            firstTargetOrder.Description.ShouldBe(firstSourceOrder.Description);
            firstTargetOrder.Quantity.ShouldBe(firstSourceOrder.Quantity);
            var secondSourceOrder = source.Orders.First();
            var secondTargetOrder = target.Orders.First();
            secondTargetOrder.Id.ShouldBe(secondSourceOrder.Id);
            secondTargetOrder.Description.ShouldBe(secondSourceOrder.Description);
            secondTargetOrder.Quantity.ShouldBe(secondSourceOrder.Quantity);
        }
        
        [Fact]
        public void WithList()
        {
            var factory = MappingBuilder.DiscoverIn<Customer>().BuildFactory();
            var mapper = factory.Create<Customer, CustomerListView>();
            var source = new Customer
            {
                Name = "Homer Simpson",
                Orders = new[]
                {
                    new Order
                    {
                        Id = 101, Description = "Slippers", Quantity = 1
                    },
                    new Order
                    {
                        Id = 201, Description = "Socks", Quantity = 3
                    }
                }
            };

            var target = mapper.Map(source);
            
            target.Name.ShouldBe(source.Name);
            var firstSourceOrder = source.Orders.First();
            var firstTargetOrder = target.Orders.First();
            firstTargetOrder.Id.ShouldBe(firstSourceOrder.Id);
            firstTargetOrder.Description.ShouldBe(firstSourceOrder.Description);
            firstTargetOrder.Quantity.ShouldBe(firstSourceOrder.Quantity);
            var secondSourceOrder = source.Orders.First();
            var secondTargetOrder = target.Orders.First();
            secondTargetOrder.Id.ShouldBe(secondSourceOrder.Id);
            secondTargetOrder.Description.ShouldBe(secondSourceOrder.Description);
            secondTargetOrder.Quantity.ShouldBe(secondSourceOrder.Quantity);
        }
    }
}
