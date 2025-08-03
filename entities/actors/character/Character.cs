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
    [Export] public float Speed = 500.0f;
    [Export] public float Acceleration = 2500.0f;
    [Export] public float Friction = 1200.0f;

    private Settings _settings;
    protected Vector2 _inputDirection = Vector2.Zero;

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
            GD.Print("Input Direction: ", _inputDirection);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        HandleMovementInput();
        ApplyMovement(delta);
        MoveAndSlide();
        HandleCollisions();
    }

    private void HandleMovementInput()
    {
        // Get input from action map (you'll need to set these up in Input Map)
        _inputDirection = Vector2.Zero;

        if (Input.IsActionPressed("move_left"))
            _inputDirection.X -= 1;
        if (Input.IsActionPressed("move_right"))
            _inputDirection.X += 1;
        if (Input.IsActionPressed("move_up"))
            _inputDirection.Y -= 1;
        if (Input.IsActionPressed("move_down"))
            _inputDirection.Y += 1;

        // Normalize diagonal movement so it's not faster
        _inputDirection = _inputDirection.Normalized();
    }

    private void ApplyMovement(double delta)
    {
        if (_inputDirection != Vector2.Zero)
        {
            // Immediately set velocity to target direction and speed
            Velocity = _inputDirection * Speed;
        }
        else
        {
            // Apply friction when no input
            Velocity = Velocity.MoveToward(Vector2.Zero, Friction * (float)delta);
        }
    }

    // Public method for AI or other scripts to control movement
    public void Move(Direction direction)
    {
        _inputDirection = GetDirectionVector(direction);
    }

    // Public method to set movement direction directly
    public void SetMovementDirection(Vector2 direction)
    {
        _inputDirection = direction.Normalized();
    }

    // Convenience methods for movement control
    public void StopMovement()
    {
        _inputDirection = Vector2.Zero;
        Velocity = Vector2.Zero;
    }

    public bool IsMoving()
    {
        return Velocity.LengthSquared() > 1.0f; // Small threshold to account for floating point precision
    }

    public Vector2 GetMovementDirection()
    {
        return _inputDirection;
    }

    // Get the current movement speed (actual velocity magnitude)
    public float GetCurrentSpeed()
    {
        return Velocity.Length();
    }

    private static Vector2 GetDirectionVector(Direction? direction) => direction switch
    {
        Direction.Left => new(-1, 0),
        Direction.Right => new(1, 0),
        Direction.Up => new(0, -1),
        Direction.Down => new(0, 1),
        _ => Vector2.Zero
    };

    // Called after MoveAndSlide to handle any collisions that occurred
    private void HandleCollisions()
    {
        // MoveAndSlide automatically handles collision detection and sliding
        // We can check for specific collision events here if needed
        
        for (int i = 0; i < GetSlideCollisionCount(); i++)
        {
            var collision = GetSlideCollision(i);
            var collider = collision.GetCollider();
            
            if (_settings != null && _settings.IsDebug)
            {
                GD.Print("Collided with: ", collider.GetType().Name);
            }

            // Handle specific collision types
            if (collider is RigidBody2D rigidBody)
            {
                PushRigidBody(rigidBody, collision);
            }
            else if (collider is Area2D area)
            {
                HandleAreaCollision(area);
            }
        }
    }

    private void PushRigidBody(RigidBody2D rigidBody, KinematicCollision2D collision)
    {
        // Calculate push force based on our velocity and collision normal
        Vector2 pushDirection = -collision.GetNormal();
        float pushForce = Velocity.Length() * 0.1f; // Adjust multiplier for push strength

        // Apply impulse to the RigidBody2D
        Vector2 impulse = pushDirection * pushForce;
        Vector2 contactPoint = collision.GetPosition() - rigidBody.GlobalPosition;
        
        rigidBody.ApplyImpulse(impulse, contactPoint);
    }

    protected virtual void HandleAreaCollision(Area2D area)
    {
        // Override in derived classes to handle specific area interactions
        // For example: collectibles, damage zones, triggers, etc.
        if (_settings != null && _settings.IsDebug)
        {
            GD.Print("Entered area: ", area.Name);
        }
    }
}
