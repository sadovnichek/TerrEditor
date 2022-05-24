namespace UI;

public interface IMouseMethods
{
    void Mouse_Down(object sender, MouseEventArgs e);
    void Mouse_Up(object sender, MouseEventArgs e);
    void Move_Mouse(object sender, MouseEventArgs e);
    void Drag_Over(object sender, DragEventArgs e);
    void Drag_Drop(object sender, DragEventArgs e);
    void Drag_Enter(object sender, DragEventArgs e);
    void OnPictureBoxClick(object sender, EventArgs eventArgs);
}