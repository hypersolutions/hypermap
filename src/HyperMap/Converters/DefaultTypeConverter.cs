namespace HyperMap.Converters
{
    public class DefaultTypeConverter<T> : TypeConverter<T, T>
    {
        public override T Convert(T source)
        {
            return source;
        }
    }
}
