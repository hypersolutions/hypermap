// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace HyperMap.Tests.Support
{
    public class UserView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasName => Name != null;
    }
}
