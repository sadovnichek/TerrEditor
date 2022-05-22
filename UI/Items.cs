using ImagesInteraction;
using UI.Buttons;

namespace UI;

public partial class MainForm : Form
{
    private void ConfigureItemButtons()
    {
        foreach (var value in _assets.ParsedDBInfo.Values)
        {
            itemsPanel.Controls.Add(new ItemButton(new Rectangle(0, 120, 100, 100), 
                ImagesMethod.ResizeImage(value, new Size(75, 75)),
                Mouse_Down!, Mouse_Up!, Move_Mouse!));
        }
    }
    
    private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
        
    }
}