using Godot;

/// <summary>
/// Script for a settings node, which should exist on the global level in the scene tree. 
/// </summary>
public partial class Settings : Node
{
    public bool IsDebug = false;

    public override void _Process(double delta)
    {
        if (!IsDebug)
        {
            return;
        }

        GD.Print("Debug: ", IsDebug);
    }

    public void ToggleDebug()
    {
        IsDebug = !IsDebug;
    }
}