using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using HyperMap.Converters;

namespace HyperMap.Mapping
{
    public abstract class MapBase
    {
        private readonly List<MapInfo> _mappings = new List<MapInfo>();
        
        protected MapBase(bool runAutoMappings)
        {
            if (runAutoMappings)
            {
                SetAutoMaps();
            }
        }
        
        internal abstract Type SourceType { get; }
        
        internal abstract Type TargetType { get; }

        internal IEnumerable<MapInfo> Mappings => _mappings;
        
        internal string Namespace { get; set; }
        
        private void SetAutoMaps()
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            
            var sourceMaps = SourceType.GetProperties(flags);
            var targetMaps = TargetType.GetProperties(flags);

            foreach (var sourceMap in sourceMaps)
            {
                var targetMap = targetMaps.FirstOrDefault(p =>
                    p.Name == sourceMap.Name && p.PropertyType == sourceMap.PropertyType);

                if (targetMap != null)
                {
                    var sourceMapItem = new MapItem {Name = sourceMap.Name, Type = sourceMap.PropertyType};
                    var mappingInfo = CreateMappingInfo(sourceMapItem);
                    var targetMapItem = new MapItem {Name = targetMap.Name, Type = targetMap.PropertyType};
                    mappingInfo.Target = targetMapItem;
                    var typeArgs = new []{targetMap.PropertyType};
                    var converterType = typeof(DefaultTypeConverter<>);
                    mappingInfo.Converter = converterType.MakeGenericType(typeArgs);
                }
            }
        }

        protected MapInfo CreateMappingInfo(MapItem mapItem)
        {
            if (mapItem.Name != null)
            {
                var existingMapper = _mappings.FirstOrDefault(m => m.Source.Name == mapItem.Name);

                if (existingMapper != null)
                    _mappings.Remove(existingMapper);
            }

            var mapInfo = new MapInfo {Source = mapItem};
            _mappings.Add(mapInfo);
            return mapInfo;
        }
    }
    
    public abstract class MapBase<TSource, TTarget> : MapBase where TSource : class where TTarget : class
    {
        protected MapBase(bool runAutoMappings = true) : base(runAutoMappings)
        {
        }
        
        internal override Type SourceType => typeof(TSource);
        
        internal override Type TargetType => typeof(TTarget);
        
        protected TargetMap<TTarget, TMember> For<TMember>(
            Expression<Func<TSource, TMember>> mapExpression)
        {
            var mapItem = GetMapItem(mapExpression);
            var mapping = CreateMappingInfo(mapItem);
            return new TargetMap<TTarget, TMember>(mapping);
        }
        
        private static MapItem GetMapItem(LambdaExpression mapExpression)
        {
            if (mapExpression.Body is MemberExpression body)
            {
                var property = (PropertyInfo)body.Member;
                return new MapItem {Name = property.Name, Type = property.PropertyType};
            }
            
            return new MapItem {Type = mapExpression.Body.Type};
        }
    }
}
