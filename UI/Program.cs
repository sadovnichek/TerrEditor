using TerrEditor.Application;
using TerrEditor.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace UI;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();
        ConfigureServices(services);
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var form = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(form);
        }
        
        //var workSpace = WorkSpace.GetInstance();
        //var workingPlace = new WorkService(workSpace);
        //var form = new MainForm(workingPlace);
        //Application.Run(form);
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddScoped<MainForm>();
        services.AddSingleton<IWorkSpace, WorkSpace>();
        services.AddSingleton<IWorkService, WorkService>();
    }
}