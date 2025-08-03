using Godot;

public partial class Player : Character
{
    public long Score = 0;
    public int Lives = 3;
    public int Level = 0;

    private long _lastScore = 0;

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        // Debug: Print position to verify movement
        if (GameManager.IsDebugEnabled(this))
        {
            GD.Print("Player Position: ", Position);
            GD.Print("Player Velocity: ", Velocity);

            var camera = GetNode<Camera2D>("Camera2D");
            if (camera != null)
            {
                GD.Print("Camera Position: ", camera.Position);
            }
        }
        
        // Track previous score to detect changes
        if (_lastScore != Score)
        {
            GD.Print("Player score: ", Score);
            _lastScore = Score;
        }
    }
}