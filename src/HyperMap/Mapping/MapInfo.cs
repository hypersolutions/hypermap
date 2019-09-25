using System;

namespace HyperMap.Mapping
{
    public class MapInfo
    {
        internal MapItem Source { get; set; }
        internal MapItem Target { get; set; }
        internal Type Converter { get; set; }
    }
}
