using System;

namespace HyperMap.Converters.Enums
{
    public class EnumToStringTypeConverter<TSource> : TypeConverter<TSource, string> 
        where TSource : struct, IConvertible  
    {
        public override string Convert(TSource from)
        {
            if (!typeof(TSource).IsEnum) 
                throw new ArgumentException("TSource must be an enum type.");
            
            return from.ToString();
        }
    }
}
