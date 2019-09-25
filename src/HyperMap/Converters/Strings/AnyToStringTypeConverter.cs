namespace HyperMap.Converters.Strings
{
    public class AnyToStringTypeConverter<T> : TypeConverter<T, string>
    {
        public override string Convert(T source)
        {
            return source?.ToString();
        }
    }
}
