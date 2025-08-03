using Godot;

public enum GameState
{
    MainMenu,
    Game,
    Pause,
    GameOver
}

/// <summary>
/// Utility class for accessing global game systems from anywhere
/// </summary>
public static class GameManager
{
    /// <summary>
    /// Gets the Main node from anywhere in the scene tree
    /// </summary>
    public static Main GetMain(Node fromNode)
    {
        return fromNode.GetTree().GetFirstNodeInGroup("main") as Main;
    }

    /// <summary>
    /// Gets the Settings node from anywhere in the scene tree
    /// </summary>
    public static Settings GetSettings(Node fromNode)
    {
        return fromNode.GetTree().GetFirstNodeInGroup("settings") as Settings;
    }

    /// <summary>
    /// Changes the game state from any node
    /// </summary>
    public static void SetGameState(Node fromNode, GameState newState)
    {
        var main = GetMain(fromNode);
        main?.SetGameState(newState);
    }

    /// <summary>
    /// Gets the current game state from any node
    /// </summary>
    public static GameState? GetGameState(Node fromNode)
    {
        var main = GetMain(fromNode);
        return main?.GetCurrentGameState();
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