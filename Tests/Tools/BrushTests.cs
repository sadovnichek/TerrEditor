using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TerrEditor.Domain;
using Brush = TerrEditor.Domain.Tools.Brush;
using Size = System.Drawing.Size;

namespace Tests.Tools;

public class BrushTests
{
    [Test]
    public void DoAction__ReturnSameItemUnchanged()
    {
        var item = new Item(new Point2D(1, 1), new TerrEditor.Domain.Size(10, 10));
        var brush = new Brush();

        var newItem = brush.DoAction(item);

        newItem.Size.Should().Be(item.Size);
        newItem.Location.Should().Be(item.Location);
        newItem.Image.Should().Be(item.Image);
    }
}