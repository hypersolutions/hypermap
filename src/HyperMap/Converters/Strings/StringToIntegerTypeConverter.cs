namespace HyperMap.Converters.Strings
{
    public class StringToIntegerTypeConverter : TypeConverter<string, int>
    {
        public override int Convert(string source)
        {
            return int.TryParse(source, out var result) ? result : 0;
        }
    }
}
