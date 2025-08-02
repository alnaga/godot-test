using Godot;

public partial class MainMenu : Control
{
    public override void _Ready()
    {
        // Example: Connect a start button if you have one
        // GetNode<Button>("StartButton").Pressed += OnStartButtonPressed;
    }

    public override void _Input(InputEvent @event)
    {
        // Example: Press Enter to start game
        if (@event.IsActionPressed("start_game"))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        // Find the Main node and change game state
        var main = GetTree().GetFirstNodeInGroup("main") as Main;
        if (main != null)
        {
            main.SetGameState(GameState.Game);
        }
    }

    private void OnStartButtonPressed()
    {
        StartGame();
    }
}