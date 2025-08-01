using Godot;
using System;

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

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public partial class Character : CharacterBody2D
{
    // CharacterBody2D already has a Velocity property!
    protected float Speed = 150;

    public override void _Ready()
    {
        base._Ready();
        
        // CharacterBody2D collision detection is automatic!
        // No setup needed - CollisionPolygon2D will work automatically
    }

    public override void _Process(double delta)
    {
        // Movement now handled in _PhysicsProcess
        
        var settings = GetNodeOrNull<Settings>("Settings");
        if (settings != null && settings.IsDebug)
        {
            GD.Print("Position: ", Position);
            GD.Print("Velocity: ", Velocity);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        // Now we can use CharacterBody2D's built-in collision detection!
        Vector2 motion = Velocity * (float)delta * Speed;
        var collision = MoveAndCollide(motion);
        
        if (collision != null)
        {
            var collider = collision.GetCollider();
            OnCollision(collider as Node2D);
            
            // Handle the collision response
            HandleCollision(collision);
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
        GD.Print("Collided with: ", collider.GetType().Name, collider.GetInstanceId());
        
        // Different collision responses you can choose from:
        
        // Option 1: Stop movement
        Velocity = Vector2.Zero;
        
        // Option 2: Bounce off (uncomment to use)
        // Velocity = Velocity.Bounce(collision.GetNormal());
        
        // Option 3: Slide along surface (uncomment to use)
        // Velocity = Velocity.Slide(collision.GetNormal());
    }

    // CharacterBody2D automatically handles CollisionPolygon2D collision detection!
    // No additional setup methods needed

    protected virtual void OnPickup(Collectible collectible)
    {
        GD.Print("Picked up: ", collectible.GetInstanceId());
    }

    protected virtual void OnCollision(Node2D other)
    {
        if (other is Collectible collectible)
        {
            OnPickup(collectible);
        }
    }
}
