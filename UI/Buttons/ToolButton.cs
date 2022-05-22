﻿using TerrEditor.Domain.Tools;
using ImagesInteraction;

namespace UI.Buttons;
public sealed class ToolButton : UserButton
{
    private ToolType currentToolType;
    
    public ToolButton(Rectangle geometry, Image image, ToolType toolType) : base(geometry, "")
    {
        Image = ImagesMethod.ResizeImage(image, geometry.Size);
        BackColor = Color.White;
        currentToolType = toolType;
        Click += ToolHandler;
    }

    private void ToolHandler(object? sender, EventArgs e)
    {
        BackColor = (BackColor == Color.White) ? Color.Fuchsia : Color.White;
        Image = (BackColor == Color.Fuchsia)
            ? ImagesMethod.ResizeImage(Image, new Size(Image.Width - 15, Image.Height - 15))
            : ImagesMethod.ResizeImage(Image, new Size(Image.Width + 15, Image.Height + 15));
        MainForm._service.CurrentToolType = (MainForm._service.CurrentToolType != currentToolType)
            ? currentToolType : ToolType.None;
    }
}

