namespace HyperMap.Converters.Strings
{
    public class StringToDoubleTypeConverter : TypeConverter<string, double>
    {
        public override double Convert(string source)
        {
            return double.TryParse(source, out var result) ? result : 0;
        }
    }
}
