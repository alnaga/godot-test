using Godot;

public partial class Main : Node
{
    private GameState _gameState = GameState.MainMenu;
    private GameState _previousGameState;
    private Node _sceneContainer;
    private Node _currentScene;

    // Preload scenes for better performance
    private readonly PackedScene _mainMenuScene = GD.Load<PackedScene>("res://entities/interface/MainMenu.tscn");
    private readonly PackedScene _levelScene = GD.Load<PackedScene>("res://entities/levels/Level.tscn");

    public override void _Ready()
    {
        // Create scene container
        _sceneContainer = new Node();
        _sceneContainer.Name = "SceneContainer";
        AddChild(_sceneContainer);

        // Load initial scene
        ChangeGameState(GameState.MainMenu);
    }

    public override void _Process(double delta)
    {
        // Only change scene if state actually changed
        if (_gameState != _previousGameState)
        {
            ChangeGameState(_gameState);
            _previousGameState = _gameState;
        }
    }

    private void ChangeGameState(GameState newState)
    {
        // Remove current scene
        if (_currentScene != null)
        {
            _currentScene.QueueFree();
            _currentScene = null;
        }

        // Load new scene based on state
        switch (newState)
        {
            case GameState.MainMenu:
                _currentScene = _mainMenuScene.Instantiate();
                break;
            case GameState.Game:
                _currentScene = _levelScene.Instantiate();
                break;
            case GameState.Pause:
                // Could show pause overlay while keeping game scene
                break;
            case GameState.GameOver:
                // Load game over scene
                break;
        }

        if (_currentScene != null)
        {
            _sceneContainer.AddChild(_currentScene);
        }

        _gameState = newState;
    }

    // Public method to change game state from other scripts
    public void SetGameState(GameState newState) => _gameState = newState;

    // Public method to get current game state
    public GameState GetCurrentGameState() => _gameState;
}