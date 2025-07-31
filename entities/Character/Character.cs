using Godot;

public partial class Character : Node2D
{
    private Vector2 Velocity = new(0, 0);

    public override void _Process(double delta)
    {
        HandleInput();


        Position = CalculatePosition(Velocity, delta);
    }

    private void HandleInput()
    {
        if (Input.IsActionPressed("move_right"))
        {
            Move(new Vector2(1, 0));
        }
        if (Input.IsActionPressed("move_left"))
        {
            Move(new Vector2(-1, 0));
        }
        if (Input.IsActionPressed("move_up"))
        {
            Move(new Vector2(0, -1));
        }
        if (Input.IsActionPressed("move_down"))
        {
            Move(new Vector2(0, 1));
        }
    }

    public override void _Draw()
    {
        base._Draw();
        GD.Print("Character position: ", Position);

        // Move the "Body" Polygon2D to match the Character's Position
        var body = GetNodeOrNull<Polygon2D>("CharacterBody2D/Body");
        if (body != null)
        {
            body.Position = Position;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        GD.Print("Character ready");
    }

    private Vector2 CalculatePosition(Vector2 direction, double delta)
    {
        var newPosition = Position + direction * (float)delta * 100;
        return newPosition;
    }

    private Vector2 CalculateVelocity(Vector2 direction)
    {
        var newVelocity = Velocity + direction;
        return newVelocity;
    }

    public void Move(Vector2 direction)
    {
        Velocity += direction;
        Velocity = Velocity.Normalized() * 1;
        GD.Print("Velocity: ", Velocity);
    }
}
