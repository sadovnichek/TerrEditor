using System;

namespace TerrEditor.Domain
{
    public interface ISize
    {
        int Height { get; set; }
        int Width { get; set; }
    }
}