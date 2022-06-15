using FluentAssertions;
using NUnit.Framework;
using TerrEditor.Domain;

namespace Tests.Tools;

public class EraserTests
{
    [Test]
    public void DoAction__AddsEventToRepository()
    {
        var panelEventRepository = new PanelEventRepository();
        var eraser = new Eraser(panelEventRepository);
        var item = new Item();

        eraser.DoAction(item);

        panelEventRepository.Get().Should().BeEquivalentTo(new PanelEvent(PanelEventType.Remove, item));
    }
}