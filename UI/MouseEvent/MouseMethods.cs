using TerrEditor.Application;
using TerrEditor.Domain;
using TerrEditor.Domain.Tools;
using TerrEditor.Infrastructure;

namespace UI.MouseEvent;

public class MouseMethods : IMouseMethods
{
    private Rectangle _dragBoxFromMouseDown;
    private Image _currentSelectedImage;
    private readonly IWorkSpace _workSpace;
    private readonly IWorkService _workService;

    public MouseMethods(IWorkSpace workSpace, IWorkService service)
    {
        _workSpace = workSpace;
        _workService = service;
    }
    
    public void Mouse_Down(object sender, MouseEventArgs e)
    {
        var dragSize = SystemInformation.DragSize;
        _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
            e.Y - (dragSize.Height / 2)), dragSize);
        switch (sender)
        {
            case Button clickedButton:
            {
                _currentSelectedImage = new Bitmap(clickedButton.Image);
                _workService.SetToolType(ToolType.None);
                break;
            }
            case ItemPictureBox clickedPictureBox:
            {
                _currentSelectedImage = new Bitmap(clickedPictureBox.Image);
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
            var item = new Item()
            {
                Image = _currentSelectedImage,
                Location = new Point(Cursor.Position.X - MainForm._panel.Location.X - _currentSelectedImage.Width / 2,
                    Cursor.Position.Y - MainForm._panel.Location.Y - _currentSelectedImage.Height / 2),
                Size = new Size(_currentSelectedImage.Width, _currentSelectedImage.Height)
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
        if (eventArgs is not MouseEventArgs) 
            return;
        _workSpace.Remove(pictureBox.Item);
        _workSpace.Add(_workService.DoAction(pictureBox.Item));
    }
}