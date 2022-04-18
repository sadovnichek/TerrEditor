using System;

namespace TerrEditor.Domain
{
    class LandscapeObject : ISize, IPicture
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public string Path { get; private set; }

        public string ObjectType;
        public string ObjectName;

        //возможно содержит общие методы 
    }
}
