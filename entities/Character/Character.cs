using Godot;

// TODO: Make Hadrian Pac-Man
public partial class Character : Node2D
{
    private Vector2 Velocity = new(0, 0);
    private bool IsDebug = true;

    public override void _Process(double delta)
    {
        HandleInput();
        UpdatePosition(delta);
        PrintDebug();
    }

    private void HandleInput()
    {
        if (Input.IsActionPressed("move_right"))
        {
            UpdateVelocity(new Vector2(1, 0));
        }
        if (Input.IsActionPressed("move_left"))
        {
            UpdateVelocity(new Vector2(-1, 0));
        }
        if (Input.IsActionPressed("move_up"))
        {
            UpdateVelocity(new Vector2(0, -1));
        }
        if (Input.IsActionPressed("move_down"))
        {
            UpdateVelocity(new Vector2(0, 1));
        }
    }

    public override void _Draw()
    {
        base._Draw();

        // Move the "Body" Polygon2D to match the Character's Position
        var body = GetNodeOrNull<Polygon2D>("CharacterBody2D/Body");
        if (body != null)
        {
            body.Position = Position;
        }
    }

    private void UpdatePosition(double delta)
    {
        Position += Velocity * (float)delta * 100;
    }

    private void UpdateVelocity(Vector2 direction)
    {
        Velocity += direction;
        Velocity = Velocity.Normalized() * 1;
    }

    private void PrintDebug()
    {
        if (!IsDebug)
        {
            return;
        }

        // GD.Print("Position: ", Position);
        // GD.Print("Velocity: ", Velocity);
        // GD.Print("IsDebug: ", IsDebug);
    }
}
