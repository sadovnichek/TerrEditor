using TerrEditor.Application;
using TerrEditor.Domain;
using TerrEditor.Domain.Tools;
using TerrEditor.Infrastructure;

namespace UI.MouseEvent;

public class MouseMethods : IMouseMethods
{
    public Rectangle DragBoxFromMouseDown { get; private set; }
    public Image CurrentSelectedImage { get; private set; }
    private WorkSpace _workSpace;
    private WorkService _workService;

    public MouseMethods(IWorkSpace workSpace, IWorkService service)
    {
        _workSpace = workSpace as WorkSpace;
        _workService = service as WorkService;
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
            _workSpace.Add(item);
        }
    }
    public void Drag_Enter(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
    }
    public void OnPictureBoxClick(object sender, EventArgs eventArgs)
    {
        if (sender is not ItemPictureBox pictureBox) 
            return;
        if (eventArgs is not MouseEventArgs mouseEventArgs) 
            return;
        _workService.SetItem(pictureBox.Item);
        switch (_workService.CurrentToolType)
        {
            case ToolType.Eraser:
            {
                _workSpace.Remove(pictureBox.Item);
                break;
            }
            case ToolType.Zoom:
            {
                if ((mouseEventArgs.Button & MouseButtons.Left) != 0)
                    Zoom.delta = 40;
                else if ((mouseEventArgs.Button & MouseButtons.Right) != 0)
                    Zoom.delta = -40;
                pictureBox.Size = _workService.DoAction().Size;
                pictureBox.Image = pictureBox.Image.Resize(pictureBox.Size);
                break;
            }
            case ToolType.Turner:
            {
                /*var image = RotateImage(new Bitmap(pictureBox.Image),_service.DoAction().Location);
                var pb = CreatePictureBox(image);
                _panel.Controls.Remove(pictureBox);
                _panel.Controls.Add(pb);*/
                break;
            }
            case ToolType.Brush:
                break;
            case ToolType.Pipette:
                break;
            case ToolType.Highlighter:
                break;
        }
    }
}