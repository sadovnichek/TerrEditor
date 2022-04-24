using System;

namespace TerrEditor.Domain
{
    public interface LandscapeObject : ISize, IPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string Path { get; set; }

        public Category ObjectType { get; }
        public string ObjectName { get; }

        //возможно содержит общие методы 
    }
}
