using Godot;
using System;

// TODO: Make Hadrian Pac-Man
/*
 * Make an Enemy entity, which:
 * - is an instance of Character
 * - calculates a path to the player using some algorithm and chases them
 * - when the algorithm is executing, call the MoveLeft, MoveRight etc methods every frame where the character has more than 2 (forward/back) possible directions
 * - the algorithm should be able to figure out the possible directions it can choose at any moment, the path to the player should be calculated based on the possible directions
 * - 
 */

public partial class Character : CharacterBody2D
{
    protected float Speed = 150;

    private Settings _settings;

    public override void _Ready()
    {
        base._Ready();

        _settings = GameManager.GetSettings(this);

        if (_settings != null && _settings.IsDebug)
        {
            GD.Print("Character node found: ", this != null);
        }
    }

    public override void _Process(double delta)
    {
        if (_settings != null && _settings.IsDebug)
        {
            GD.Print("Position: ", Position);
            GD.Print("Velocity: ", Velocity);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Vector2 motion = Velocity * (float)delta * Speed;
        var collision = MoveAndCollide(motion);

        if (collision != null)
        {
            HandleCollision(collision);
        }
    }

    public override void _Draw()
    {
        base._Draw();

        var body = GetNodeOrNull<Polygon2D>("CharacterBody2D/Body");
        if (body != null)
        {
            body.Position = Position;
        }
    }

    public void Move(Direction direction)
    {
        Velocity = GetDirectionVector(direction);
    }

    private static Vector2 GetDirectionVector(Direction direction) => direction switch
    {
        Direction.Left => new(-1, 0),
        Direction.Right => new(1, 0),
        Direction.Up => new(0, -1),
        Direction.Down => new(0, 1),
        _ => throw new ArgumentException($"Invalid direction: {direction}")
    };

    protected virtual void HandleCollision(KinematicCollision2D collision)
    {
        // Default behavior for CharacterBody2D collision
        var collider = collision.GetCollider();
        if (_settings != null && _settings.IsDebug)
        {
            GD.Print("Collided with: ", collider.GetType().Name, collider.GetInstanceId());
        }

        // Check if we collided with a RigidBody2D and push it
        if (collider is RigidBody2D rigidBody)
        {
            // Calculate push force based on our velocity and collision normal
            Vector2 pushDirection = -collision.GetNormal(); // Direction to push the other body
            float pushForce = Speed * 1; // Adjust this value to control push strength

            // Apply impulse to the RigidBody2D
            rigidBody.ApplyImpulse(pushDirection * pushForce, collision.GetPosition() - rigidBody.GlobalPosition);

            // Optionally reduce our velocity or bounce back
            Velocity = Velocity.Bounce(collision.GetNormal()) * 0.5f; // Bounce with some energy loss
        }
        else
        {
            // For other collision types (walls, etc.), stop movement
            Velocity = Vector2.Zero;
        }
    }
}
