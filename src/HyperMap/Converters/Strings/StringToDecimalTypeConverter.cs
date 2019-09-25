namespace HyperMap.Converters.Strings
{
    public class StringToDecimalTypeConverter : TypeConverter<string, decimal>
    {
        public override decimal Convert(string source)
        {
            return decimal.TryParse(source, out var result) ? result : 0;
        }
    }
}
