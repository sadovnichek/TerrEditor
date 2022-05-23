using TerrEditor.Infrastructure;
using UI.Buttons;

namespace UI;

public partial class MainForm : Form
{
    private void ConfigureItemButtons()
    {
        foreach (var value in _assets.ParsedDBInfo.Values)
        {
            itemsPanel.Controls.Add(new ItemButton(new Rectangle(0, 120, 100, 100), 
                value.Resize(new Size(75, 75)),
                _mouseMethods.Mouse_Down!, _mouseMethods.Mouse_Up!, _mouseMethods.Move_Mouse!));
        }
    }
    
    private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
        
    }
}