using FluentAssertions;
using NUnit.Framework;
using TerrEditor.Domain;
using UI.MouseEvent;

namespace Tests;

public class PanelEventRepositoryTests
{
    [Test]
    public void Get_WhenNothingHasBeenAdded_ReturnNull()
    {
        var panelEventRepository = new PanelEventRepository();

        var panelEvent = panelEventRepository.Get();

        panelEvent.Should().Be(null);
    }

    [Test]
    public void Get_WhenSomethingHasBeenAdded_ReturnLast()
    {
        var panelEventRepository = new PanelEventRepository();
        var @event = new PanelEvent(PanelEventType.Add, new Item());
        panelEventRepository.AddEvent(@event);

        var panelEvent = panelEventRepository.Get();

        panelEvent.Should().Be(@event);
    }

    [Test]
    public void IsEmpty_WhenNothingHasBeenAdded_ReturnTrue()
    {
        var panelEventRepository = new PanelEventRepository();

        panelEventRepository.IsEmpty.Should().BeTrue();
    }

    [Test]
    public void IsEmpty_WhenSomethingHasBeenAdded_ReturnFalse()
    {
        var panelEventRepository = new PanelEventRepository();
        var @event = new PanelEvent(PanelEventType.Add, new Item());
        panelEventRepository.AddEvent(@event);

        panelEventRepository.IsEmpty.Should().BeFalse();
    }

    [Test]
    public void IsEmpty_WhenSomethingHasBeenAddedAndThenGet_ReturnTrue()
    {
        var panelEventRepository = new PanelEventRepository();
        var @event = new PanelEvent(PanelEventType.Add, new Item());
        panelEventRepository.AddEvent(@event);
        panelEventRepository.Get();

        panelEventRepository.IsEmpty.Should().BeTrue();
    }
}