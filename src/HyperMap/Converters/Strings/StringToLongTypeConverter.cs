namespace HyperMap.Converters.Strings
{
    public class StringToLongTypeConverter : TypeConverter<string, long>
    {
        public override long Convert(string source)
        {
            return long.TryParse(source, out var result) ? result : 0;
        }
    }
}
