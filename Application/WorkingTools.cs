using Microsoft.Extensions.DependencyInjection;
using TerrEditor.Domain;
using TerrEditor.Domain.Tools;
using UI.MouseEvent;

namespace TerrEditor.Application;

public class WorkingTools : IWorkingTools
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ServiceCollection _services;

    public WorkingTools(PanelEventRepository panelEventRepository)
    {
        _services = new ServiceCollection();
        _services.AddSingleton(new Eraser(panelEventRepository));
        _services.AddSingleton<Zoom>();
        _services.AddSingleton<Turner>();
        _serviceProvider = _services.BuildServiceProvider();
    }

    public ITool GetTool(ToolType type)
    {
        switch (type)
        {
            case ToolType.Eraser:
            {
                return _serviceProvider.GetRequiredService<Eraser>();
            }
            case ToolType.Zoom:
            {
                return _serviceProvider.GetRequiredService<Zoom>();
            }
            case ToolType.Turner:
            {
                return _serviceProvider.GetRequiredService<Turner>();
            }
            default:
            {
                throw new NotImplementedException($"Not service for {type}");
            }
        }
    }
}