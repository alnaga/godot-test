using Godot;

/// <summary>
/// Script for a settings node, which should exist on the global level in the scene tree. 
/// </summary>
// TODO: Look into autoloads (https://docs.godotengine.org/en/stable/tutorials/scripting/singletons_autoload.html) instead here, especially the thing with Instances
[GlobalClass]
public partial class Settings : Node
{
    [Export]
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