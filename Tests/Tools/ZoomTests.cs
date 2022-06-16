using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TerrEditor.Domain;
using Size = System.Drawing.Size;

namespace Tests.Tools;

public class ZoomTests
{
    [TestCase(100, 100, -100)]
    [TestCase(0, 100, -100)]
    [TestCase(0, 101, -100)]
    public void DoAction_WhenDeltaIsNegativeAndWidthOrHeightLessThanAbsOfDelta_ReturnsUnchangedItem(int width, int height, int delta)
    {
        var zoom = new Zoom();
        Zoom.delta = delta;
        var itemSize = new Size(width, height);
        var item = new Item(Point.Empty, itemSize);

        var newItem = zoom.DoAction(item);

        newItem.Size.Should().Be(itemSize);
    }

    [TestCase(-100, 0, 100)]
    [TestCase(-200, 101, 100)]
    public void DoAction_WhenDeltaIsPositiveAndAtLeastOneOfSizeComponentsIsLessThanOrEqualToNegDelta_ReturnsUnchangedItem(int width, int height, int delta)
    {
        var zoom = new Zoom();
        Zoom.delta = delta;
        var itemSize = new Size(width, height);
        var item = new Item(Point.Empty, itemSize);

        var newItem = zoom.DoAction(item);

        newItem.Size.Should().Be(itemSize);
    }

    [TestCase(0, 0, 100)]
    [TestCase(-1, -1, 100)]
    public void DoAction_WhenDeltaIsPositiveAndSizeComponentsAreGreaterThanNegDelta_ReturnsChangedItem(int width, int height, int delta)
    {
        var zoom = new Zoom();
        Zoom.delta = delta;
        var item = new Item(Point.Empty, new Size(width, height));

        var newItem = zoom.DoAction(item);

        newItem.Size.Should().Be(new Size(width + delta, height + delta));
    }
}