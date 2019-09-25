using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HyperMap.Mapping
{
    public sealed class TargetMap<TTarget, TSourceMember>
    {
        private readonly MapInfo _map;

        internal TargetMap(MapInfo map)
        {
            _map = map;
        }
        
        public ConverterMap<TSourceMember, TMember> MapTo<TMember>(
            Expression<Func<TTarget, TMember>> mapExpression)
        {
            var targetMapItem = GetMapItem(mapExpression);
            _map.Target = targetMapItem;
            return new ConverterMap<TSourceMember, TMember>(_map);
        }
        
        private static MapItem GetMapItem(LambdaExpression mapExpression)
        {
            if (mapExpression.Body is MemberExpression body)
            {
                var property = (PropertyInfo)body.Member;
                
                if (!property.CanWrite)
                    throw new InvalidOperationException($"Cannot map to readonly property '{property.Name}'.");
                
                return new MapItem {Name = property.Name, Type = property.PropertyType};
            }
            
            return new MapItem {Type = mapExpression.Body.Type};
        }
    }
}
