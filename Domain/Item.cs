﻿using System.Drawing;

namespace TerrEditor.Domain;

public class Item
{
    public Guid Id => Guid.NewGuid();
    public Bitmap Image;
    public Point Location;
    public Rectangle Size;
    public string Name;
}