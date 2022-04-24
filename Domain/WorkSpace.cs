﻿namespace TerrEditor.Domain;

public static class WorkSpace
{
    public static List<Canvas> Canvases;
    public static Item CurrentObject;
    public static Tool CurrentTool;
    public static Canvas CurrentCanvas;

    static WorkSpace()
    {
        Canvases = new List<Canvas>();
        CurrentObject = default(Item);
        CurrentTool = default(Tool);
    }
    
    //методы
}