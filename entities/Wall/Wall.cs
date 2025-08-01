using Godot;

// Utility class for shared rectangle geometry - can be used by any physics body type
public static class RectangleGeometry
{
    /// <summary>
    /// Creates 4 points for a rectangle centered at origin
    /// </summary>
    public static Vector2[] GetRectanglePoints(float width, float height)
    {
        float halfWidth = width / 2;
        float halfHeight = height / 2;
        
        return new Vector2[4]
        {
            new(-halfWidth, -halfHeight), // Top-left
            new(halfWidth, -halfHeight),  // Top-right  
            new(halfWidth, halfHeight),   // Bottom-right
            new(-halfWidth, halfHeight)   // Bottom-left
        };
    }

    /// <summary>
    /// Sets up visual rectangle using Polygon2D
    /// </summary>
    public static void SetupVisualRectangle(Polygon2D polygon, float width, float height, Color color)
    {
        polygon.Polygon = GetRectanglePoints(width, height);
        polygon.Color = color;
    }

    /// <summary>
    /// Sets up collision using CollisionShape2D with RectangleShape2D (more efficient than polygon)
    /// </summary>
    public static void SetupRectangleCollision(CollisionShape2D collisionShape, float width, float height)
    {
        var shape = new RectangleShape2D();
        shape.Size = new Vector2(width, height);
        collisionShape.Shape = shape;
    }
}

/// <summary>
/// Static wall - doesn't move, used for level boundaries, obstacles
/// Inherits from StaticBody2D which is optimized for non-moving collision objects
/// </summary>
public partial class Wall : StaticBody2D
{
    [Export] public float Width = 100f;
    [Export] public float Height = 100f;
    [Export] public Color WallColor = Colors.Gray;

    public override void _Ready()
    {
        base._Ready();

        // Get child nodes
        var polygon = GetNode<Polygon2D>("Polygon2D");
        var collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        // Use shared utility methods
        RectangleGeometry.SetupVisualRectangle(polygon, Width, Height, WallColor);
        RectangleGeometry.SetupRectangleCollision(collisionShape, Width, Height);
    }
}