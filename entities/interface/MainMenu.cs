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

    private void StartGame()
    {
        GetTree().ChangeSceneToFile("res://entities/levels/Level.tscn");
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