﻿namespace TerrEditor.Domain;

public class Highlight
{
    public string Name = "Highlight";

    public void HighlightAndCopy(Item item)
    {
        CurrentObject = item;
        //buffer.add(o) ??
    }
}