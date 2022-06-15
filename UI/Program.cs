using TerrEditor.Application;
using TerrEditor.Domain;
using Microsoft.Extensions.DependencyInjection;

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
        try
        {
            Application.Run(form);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IWorkSpace, WorkSpace>();
        services.AddSingleton<MouseMethods>();
        services.AddSingleton<PanelEventRepository>();
        services.AddSingleton<SaveLoadService>();
        services.AddScoped<MainForm>();
    }
}