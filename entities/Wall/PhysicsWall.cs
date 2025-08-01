using Godot;

/// <summary>
/// Physics wall - can be pushed, falls with gravity, responds to forces
/// Inherits from RigidBody2D which handles physics automatically
/// Shares visual/collision code with static Wall via RectangleGeometry
/// </summary>
public partial class PhysicsWall : RigidBody2D
{
    [Export] public float Width = 50f;
    [Export] public float Height = 50f;
    [Export] public Color WallColor = Colors.Brown;

    public override void _Ready()
    {
        base._Ready();

        // Get child nodes (same structure as static Wall)
        var polygon = GetNode<Polygon2D>("Polygon2D");
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        // Use the SAME shared utility methods as static Wall!
        RectangleGeometry.SetupVisualRectangle(polygon, Width, Height, WallColor);
        RectangleGeometry.SetupRectangleCollision(collisionShape, Width, Height);

        // Physics-specific settings
        Mass = 1.0f; // Lighter walls are easier to push
        GravityScale = 1.0f; // Responds to gravity
    }
}