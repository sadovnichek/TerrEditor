using TerrEditor.Domain;
using Size = TerrEditor.Domain.Size;

namespace UI;

public class MouseMethods
{
    private Rectangle _dragBoxFromMouseDown;
    private Image _currentSelectedImage;
    private string _currentSelectedImageName = "";
    private readonly IWorkSpace _workSpace;
    private readonly ToolHandler _toolHandler;

    public MouseMethods(IWorkSpace workSpace, ToolHandler toolHandler)
    {
        _workSpace = workSpace;
        _toolHandler = toolHandler;
    }
    
    public void Mouse_Down(object sender, MouseEventArgs e)
    {
        var dragSize = SystemInformation.DragSize;
        _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
            e.Y - (dragSize.Height / 2)), dragSize);
        if (_toolHandler.ToolType != ToolType.None)
            return;
        switch (sender)
        {
            case Button clickedButton:
            {
                _currentSelectedImage = new Bitmap(clickedButton.Image);
                _currentSelectedImageName = clickedButton.Name;
                break;
            }
            case ItemPictureBox clickedPictureBox:
            {
                _currentSelectedImage = new Bitmap(clickedPictureBox.Image);
                _currentSelectedImageName = clickedPictureBox.Item.ImageName;
                break;
            }
            default:
                _dragBoxFromMouseDown = Rectangle.Empty;
                break;
        }
    }
    public void Mouse_Up(object sender, MouseEventArgs e)
    {
        _dragBoxFromMouseDown = Rectangle.Empty;
    }
    public void Move_Mouse(object sender, MouseEventArgs e)
    {
        switch (sender)
        {
            case Button clickedButton when e.Button == MouseButtons.Left:
            {
                if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    clickedButton.DoDragDrop(_currentSelectedImage, DragDropEffects.All);
                }
                break;
            }
            case ItemPictureBox pictureBox when e.Button == MouseButtons.Left:
            {
                if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    pictureBox.DoDragDrop(_currentSelectedImage, DragDropEffects.All);
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
            var location = new Point2D(Cursor.Position.X - MainForm._panel.Location.X - _currentSelectedImage.Width / 2,
                Cursor.Position.Y - MainForm._panel.Location.Y - _currentSelectedImage.Height / 2);
            var size = new Size(_currentSelectedImage.Width, _currentSelectedImage.Height);
            var imageName = _currentSelectedImageName;
            _workSpace.Add(new Item(location, size, imageName));
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
        if (eventArgs is not MouseEventArgs) 
            return;
        _toolHandler.DoAction(pictureBox);
    }
}