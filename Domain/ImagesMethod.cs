using System.Drawing;

namespace ImagesInteraction;

public class ImagesMethod
{
    public static Image ResizeImage(Image imgToResize, Size size)
    {
        return new Bitmap(imgToResize, size); // ”брать куда-то
    }
}