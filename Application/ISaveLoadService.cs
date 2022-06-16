namespace TerrEditor.Application;

public interface ISaveLoadService
{
    void Save(object sender, EventArgs eventArgs);

    void Load(object sender, EventArgs eventArgs);
}