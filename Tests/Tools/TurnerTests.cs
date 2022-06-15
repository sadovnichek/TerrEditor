using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TerrEditor.Application;
using TerrEditor.Domain;
using TerrEditor.Domain.Tools;
using UI.MouseEvent;

namespace Tests.Tools;

public class TurnerTests
{
    private IWorkingTools _workingTools = new WorkingTools(new PanelEventRepository());

    [Test]
    public void TestSingleton()
    {
        var instance1 = _workingTools.GetTool(ToolType.Turner);
        var instance2 = _workingTools.GetTool(ToolType.Turner);
        ReferenceEquals(instance1, instance2).Should().BeTrue();
    }
}