using Godot;

/// <summary>
/// Script for a settings node, which should exist on the global level in the scene tree. 
/// </summary>
public partial class Settings : Node
{
    public bool IsDebug = false;

    public override void _Ready()
    {
        // Add this node to the "settings" group for global access
        AddToGroup("settings");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Input.IsActionJustPressed("toggle_debug"))
        {
            ToggleDebug();
        }
    }

    public void ToggleDebug()
    {
        IsDebug = !IsDebug;
        GD.Print("Debug: ", IsDebug ? "enabled" : "disabled");
    }
}