namespace HyperMap.Converters.Strings
{
    public class StringToFloatTypeConverter : TypeConverter<string, float>
    {
        public override float Convert(string source)
        {
            return float.TryParse(source, out var result) ? result : 0;
        }
    }
}
