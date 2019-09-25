using HyperMap.Integration.Tests.Sources;
using HyperMap.Integration.Tests.Targets;
using Shouldly;
using Xunit;

namespace HyperMap.Integration.Tests
{
    public class NestedSourceToFlatTargetTests
    {
        [Fact]
        public void WithSourceAddress()
        {
            var factory = MappingBuilder.DiscoverIn<Employee>().BuildFactory();
            var mapper = factory.Create<Employee, EmployeeView>();
            var source = new Employee
            {
                FirstName = "Homer",
                LastName = "Simpson",
                Age = 40,
                IsMale = true,
                HomeAddress = new Address
                {
                    HouseNumber = 742,
                    Street = "Evergreen Terrace",
                    Town = "Springfield"
                }
            };

            var target = mapper.Map(source);
            
            target.FirstName.ShouldBe(source.FirstName);
            target.LastName.ShouldBe(source.LastName);
            target.Age.ShouldBe(source.Age);
            target.IsMale.ShouldBe(source.IsMale);
            target.HomeAddress.ShouldBe("742 Evergreen Terrace, Springfield");
        }
    }
}
