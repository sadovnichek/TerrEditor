using Microsoft.Extensions.DependencyInjection;
using TerrEditor.Domain;
using TerrEditor.Domain.Tools;
using UI.MouseEvent;

namespace TerrEditor.Application;

public class WorkingTools : IWorkingTools
{
    private readonly ServiceProvider _serviceProvider;
    private readonly ServiceCollection _services;

    public WorkingTools(PanelEventRepository panelEventRepository)
    {
        _services = new ServiceCollection();
        _services.AddSingleton(new Eraser(panelEventRepository));
        _services.AddSingleton<Zoom>();
        _services.AddSingleton<Turner>();
    }

    public ITool GetTool(ToolType type)
    {
        using var serviceProvider = _services.BuildServiceProvider();
        switch (type)
        {
            case ToolType.Eraser:
            {
                return serviceProvider.GetRequiredService<Eraser>();
            }
            case ToolType.Zoom:
            {
                return serviceProvider.GetRequiredService<Zoom>();
            }
            case ToolType.Turner:
            {
                return serviceProvider.GetRequiredService<Turner>();
            }
            default:
            {
                throw new NotImplementedException($"Not service for {type}");
            }
        }
    }
}