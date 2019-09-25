namespace HyperMap.Converters
{
    public class MappingFactoryTypeConverter<TSource, TTarget> : TypeConverter<TSource, TTarget>
        where TSource : class where TTarget : class
    {
        public override TTarget Convert(TSource source)
        {
            var mapper = MappingFactory.Create<TSource, TTarget>();
            return mapper?.Map(source);
        }
    }
}
