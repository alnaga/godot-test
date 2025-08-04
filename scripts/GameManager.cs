using Godot;

/// <summary>
/// Utility class for accessing global game systems from anywhere
/// </summary>
public static class GameManager
{
    /// </summary>
    public static Settings GetSettings(Node fromNode)
    {
        return fromNode.GetTree().GetFirstNodeInGroup("settings") as Settings;
    }

    /// <summary>
    /// Toggle debug mode from any node
    /// </summary>
    public static void ToggleDebug(Node fromNode)
    {
        var settings = GetSettings(fromNode);
        settings?.ToggleDebug();
    }

    /// <summary>
    /// Check if debug mode is enabled from any node
    /// </summary>
    public static bool IsDebugEnabled(Node fromNode)
    {
        var settings = GetSettings(fromNode);
        return settings?.IsDebug ?? false;
    }
}