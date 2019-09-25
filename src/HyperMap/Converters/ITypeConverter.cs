namespace HyperMap.Converters
{
    public interface ITypeConverter<in TSource, out TTarget>
    {
        IMappingFactory MappingFactory { get; set; }
        TTarget Convert(TSource from);
    }

    public abstract class TypeConverter<TSource, TTarget> : ITypeConverter<TSource, TTarget>
    {
        public IMappingFactory MappingFactory { get; set; }
        
        public abstract TTarget Convert(TSource from);
    }
}
