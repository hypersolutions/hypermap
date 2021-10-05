// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace HyperMap.DependencyInjection.UnitTests.Support
{
    public class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasName => Name != null;
    }
}
