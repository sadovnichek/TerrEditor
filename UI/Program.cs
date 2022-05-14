using TerrEditor.Application;
using TerrEditor.Domain;

namespace UI;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        
        ApplicationConfiguration.Initialize();
        var workSpace = WorkSpace.GetInstance();
        var workingPlace = new WorkService(workSpace);
        var form = new MainForm(workingPlace);
        form.Text = @"Landscape Editor";
        Application.Run(form);
    }
}