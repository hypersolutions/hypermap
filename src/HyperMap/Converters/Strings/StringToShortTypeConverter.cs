namespace HyperMap.Converters.Strings
{
    public class StringToShortTypeConverter : TypeConverter<string, short>
    {
        public override short Convert(string source)
        {
            return (short) (short.TryParse(source, out var result) ? result : 0);
        }
    }
}
