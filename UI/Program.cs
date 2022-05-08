using TerrEditor.Application;

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
        var workingPlace = new WorkingPlace();
        var form = new MainForm(workingPlace);
        form.Text = "Territory Editor";
        Application.Run(form);
    }
}