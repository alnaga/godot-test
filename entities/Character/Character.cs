using Godot;

// TODO: Make Hadrian Pac-Man
/*
 * Make Character entity, which:
 * - when turning in any direction, sets a constant speed
 * - use normal MoveLeft, MoveRight etc methods to control movement
 * - if moving into a wall, stop moving
 * - if moving over a pickup, add to global score and change the status of the pickup to collected
 * Make a Player entity, which:
 * - is an instance of Character
 * - calls the MoveLeft, MoveRight etc methods when the player presses the corresponding keys
 * - has a score
 * - has a lives
 * - has a level
 * Make an Enemy entity, which:
 * - is an instance of Character
 * - calculates a path to the player using some algorithm and chases them
 * - when the algorithm is executing, call the MoveLeft, MoveRight etc methods every frame where the character has more than 2 (forward/back) possible directions
 * - the algorithm should be able to figure out the possible directions it can choose at any moment, the path to the player should be calculated based on the possible directions
 * - 
 */
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
