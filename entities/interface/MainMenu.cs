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
        // If click on the screen, start game
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        // Use utility method to change game state
        GameManager.SetGameState(this, GameState.Game);
    }

    private void OnStartButtonPressed()
    {
        StartGame();
    }
}