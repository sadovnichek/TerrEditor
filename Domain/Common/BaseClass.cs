namespace TerrEditor.Domain.Common
{
    public interface ILandscapeObject : ISize, IPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string Path { get; set; }

        public Category ObjectType { get; }
        public string ObjectName { get; }

        //возможно содержит общие методы 
    }
}
