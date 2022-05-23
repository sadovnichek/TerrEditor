using TerrEditor.Domain;

namespace UI.MouseEvent;

public class MouseMethods : IMouseMethods
{
    public Rectangle DragBoxFromMouseDown { get; private set; }
    public Image CurrentSelectedImage { get; private set; }
    private WorkSpace _workSpace;
    private PanelEventRepository _panelEventRepository;
    
    public MouseMethods(IWorkSpace workSpace, PanelEventRepository panelEventRepository)
    {
        _workSpace = workSpace as WorkSpace;
        _panelEventRepository = panelEventRepository;
    }
    
    public void Mouse_Down(object sender, MouseEventArgs e)
    {
        var dragSize = SystemInformation.DragSize;
        DragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
            e.Y - (dragSize.Height / 2)), dragSize);
        switch (sender)
        {
            case Button clickedButton:
            {
                CurrentSelectedImage = new Bitmap(clickedButton.Image);
                break;
            }
            case ItemPictureBox clickedPictureBox:
            {
                CurrentSelectedImage = new Bitmap(clickedPictureBox.Image);
                break;
            }
            default:
                DragBoxFromMouseDown = Rectangle.Empty;
                break;
        }
    }
    public void Mouse_Up(object sender, MouseEventArgs e)
    {
        DragBoxFromMouseDown = Rectangle.Empty;
    }
    public void Move_Mouse(object sender, MouseEventArgs e)
    {
        switch (sender)
        {
            case Button clickedButton when e.Button == MouseButtons.Left:
            {
                if (DragBoxFromMouseDown != Rectangle.Empty && !DragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    clickedButton.DoDragDrop(CurrentSelectedImage, DragDropEffects.All);
                }
                break;
            }
            case ItemPictureBox pictureBox when e.Button == MouseButtons.Left:
            {
                if (DragBoxFromMouseDown != Rectangle.Empty && !DragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    pictureBox.DoDragDrop(CurrentSelectedImage, DragDropEffects.All);
                    _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Remove, pictureBox.Item));
                    _workSpace.Remove(pictureBox.Item);
                }
                break;
            }
        }
    }
    public void Drag_Over(object sender, DragEventArgs e)
    {
        e.Effect = (e.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move
            ? DragDropEffects.Move
            : DragDropEffects.None;
    }
    public void Drag_Drop(object sender, DragEventArgs e)
    {
        if (e.Effect == DragDropEffects.Move)
        {
            var item = new Item()
            {
                Image = CurrentSelectedImage,
                Location = new Point(Cursor.Position.X - MainForm._panel.Location.X - CurrentSelectedImage.Width / 2,
                    Cursor.Position.Y - MainForm._panel.Location.Y - CurrentSelectedImage.Height / 2),
                Size = new Size(CurrentSelectedImage.Width, CurrentSelectedImage.Height)
            };
            _panelEventRepository.AddEvent(new PanelEvent(PanelEventType.Add, item));
            _workSpace.Add(item);
        }
    }
    public void Drag_Enter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
    }
}