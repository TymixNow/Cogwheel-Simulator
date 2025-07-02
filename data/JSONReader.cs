using System;
using System.Text.Json;
using Godot;

public partial class JSONReader
{
    public static T Read<T>(string filePath)
    {
        using var file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
        string text = file.GetAsText();
        return JsonSerializer.Deserialize<T>(text);
    }
}
