namespace HyperMap.Converters.Strings
{
    public class StringToBooleanTypeConverter : TypeConverter<string, bool>
    {
        public override bool Convert(string source)
        {
            return bool.TryParse(source, out var result) && result;
        }
    }
}
