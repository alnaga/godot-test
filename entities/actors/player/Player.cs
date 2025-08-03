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
        HandleInput();
        // Track previous score to detect changes
        if (_lastScore != Score)
        {
            GD.Print("Player score: ", Score);
            _lastScore = Score;
        }
    }

    private void HandleInput()
    {
        if (Input.IsActionPressed("move_right"))
        {
            Move(Direction.Right);
        }
        if (Input.IsActionPressed("move_left"))
        {
            Move(Direction.Left);
        }
        if (Input.IsActionPressed("move_up"))
        {
            Move(Direction.Up);
        }
        if (Input.IsActionPressed("move_down"))
        {
            Move(Direction.Down);
        }

        // If none of the above are pressed, stop moving
        if (!Input.IsActionPressed("move_right") && !Input.IsActionPressed("move_left") && !Input.IsActionPressed("move_up") && !Input.IsActionPressed("move_down"))
        {
            Velocity = Vector2.Zero;
        }
    }
}