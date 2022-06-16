using TerrEditor.Domain;
using Size = TerrEditor.Domain.Size;

namespace UI;

public class ToolHandler
{
    public ToolType ToolType = ToolType.None;
    private IWorkSpace _workSpace;

    public ToolHandler(IWorkSpace workSpace)
    {
        _workSpace = workSpace;
    }

    public void SetToolType(ToolType toolType)
    {
        ToolType = toolType;
    }

    public void DoAction(ItemPictureBox pictureBox)
    {
        if (ToolType == null)
            throw new Exception("Tool type was not set");
        switch (ToolType)
        {
            case ToolType.Eraser:
            {
                _workSpace.Remove(pictureBox.Item);
                break;
            }
            case ToolType.Zoom:
            {
                _workSpace.Remove(pictureBox.Item);
                var newItem = new Item(pictureBox.Item.Location, 
                    new Size(pictureBox.Item.Size.Width + 100, pictureBox.Item.Size.Height + 100), 
                    pictureBox.Item.ImageName);
                _workSpace.Add(newItem);
                break;
            }
            case ToolType.Turner:
            {
                _workSpace.RotateItem(pictureBox.Item);
                break;
            }
            default:
            {
                throw new Exception($"A tool {ToolType} was not realized");
            }
        }
    }
}