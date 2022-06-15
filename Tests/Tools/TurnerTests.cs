using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TerrEditor.Domain;
using TerrEditor.Domain.Tools;

namespace Tests.Tools;

public class TurnerTests
{
    // [Test]
    // public void GetInstance__ReturnsSameInstance()
    // {
    //     var instance1 = Turner.GetInstance();
    //     var instance2 = Turner.GetInstance();
    //
    //     ReferenceEquals(instance1, instance2).Should().BeTrue();
    // }
    //
    // [TestCase(100, 100)]
    // [TestCase(1, 10)]
    // public void DoAction__SetsLocationAsHalfASize(int width, int height)
    // {
    //     var item = new Item(Point.Empty, new Size(width, height));
    //     var turner = Turner.GetInstance();
    //
    //     var newItem = turner.DoAction(item);
    //
    //     newItem.Location.Should().Be(new Point(width / 2, height / 2));
    // }
}