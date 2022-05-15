using TerrEditor.Application;
using UI.Buttons;

namespace UI;

public partial class MainForm : Form
{
    private void ConfigureItemButtons()
    {
        itemsPanel.Controls.Add(new ItemButton(new Rectangle(0, 120, 100, 100), _chair, Mouse_Down!, Mouse_Up!, Move_Mouse!));
        itemsPanel.Controls.Add(new ItemButton(new Rectangle(0, 20, 100, 100), _tree, Mouse_Down!, Mouse_Up!, Move_Mouse!));
    }
    
    private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
        
    }
}