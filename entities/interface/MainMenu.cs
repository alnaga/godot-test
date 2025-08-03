using Godot;

public partial class MainMenu : Control
{
    private Button playButton;
    private Button quitButton;

    public override void _Ready()
    {
        playButton = GetNode<Button>("VBoxContainer/PlayButton");
        quitButton = GetNode<Button>("VBoxContainer/QuitButton");

        playButton.Pressed += OnStartButtonPressed;
        quitButton.Pressed += OnQuitButtonPressed;
    }

    public override void _Input(InputEvent @event)
    {
        // If click on the screen, start game
        // if (@event is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
        // {
        //     StartGame();
        // }

        // if (@event is InputEventKey keyEvent && keyEvent.Keycode == Key.Escape)
        // {
        //     OnQuitButtonPressed();
        // }
    }

    private void StartGame()
    {
        // Use utility method to change game state
        GameManager.SetGameState(this, GameState.Game);
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }

    private void OnStartButtonPressed()
    {
        StartGame();
    }
}