using Godot;

public partial class Collectible : Area2D
{
    protected bool Collected = false;
    protected int Value = 1;

    public override void _Ready()
    {
        base._Ready();

        // // Set up the visual circle
        // var polygon = GetNode<Polygon2D>("Polygon2D");
        // CreateCirclePolygon(polygon, 7.0f, 32);

        // // Set up the collision shape to match
        // var collider = GetNode<CollisionPolygon2D>("Collider");
        // SetCircleCollisionPolygon(collider, 7.0f, 32);

        // Connect the body_entered signal
        BodyEntered += OnBodyEntered;
    }

    // private static Vector2[] GetCirclePoints(float radius, int segments = 32)
    // {
    //     var points = new Vector2[segments];
    //     for (int i = 0; i < segments; i++)
    //     {
    //         float angle = i * Mathf.Pi * 2 / segments;
    //         points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    //     }
    //     return points;
    // }

    // private static void CreateCirclePolygon(Polygon2D polygon, float radius, int segments = 32)
    // {
    //     var points = GetCirclePoints(radius, segments);
    //     polygon.Polygon = points;
    //     polygon.Color = Colors.Blue; // Use red for visibility
    // }

    // private static void SetCircleCollisionPolygon(CollisionPolygon2D collider, float radius, int segments = 32)
    // {
    //     var points = GetCirclePoints(radius, segments);
    //     collider.Polygon = points;
    // }

    private void OnBodyEntered(Node body)
    {
        if (body is Player player)
        {
            Collect(player);
        }
    }

    public void Collect(Player player)
    {
        Collected = true;
        player.Score += Value;
        QueueFree();
    }
}