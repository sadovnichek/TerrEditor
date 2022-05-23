using System.Drawing;

namespace TerrEditor.Infrastructure;


public static class ImageExtentions
{
    public static Image ResizeImage(this Image imgToResize, Size size)
    {
        return new Bitmap(imgToResize, size);
    }
}