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
        var form = new MainForm();
        form.Text = "Territory Editor";
        var canvas = new Canvas(form.Size);
        
        Application.Run(form);
    }
}