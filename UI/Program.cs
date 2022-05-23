using TerrEditor.Application;
using TerrEditor.Domain;
using Microsoft.Extensions.DependencyInjection;
using UI.MouseEvent;

namespace UI;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ApplicationConfiguration.Initialize();
        var services = new ServiceCollection();
        ConfigureServices(services);
        using var serviceProvider = services.BuildServiceProvider();
        var form = serviceProvider.GetRequiredService<MainForm>();
        Application.Run(form);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IWorkSpace, WorkSpace>();
        services.AddSingleton<IWorkService, WorkService>();
        services.AddSingleton<IWorkingTools, WorkingTools>();
        services.AddSingleton<IMouseMethods, MouseMethods>();
        services.AddSingleton<PanelEventRepository>();
        services.AddSingleton<SaveService>();
        services.AddScoped<MainForm>();
    }
}