using Godot;

public partial class Projectile : CharacterBody2D
{
    protected Vector2 Direction = new(0, 0);
    protected float Speed = 150;
    protected float Damage = 1;
    protected float Lifetime = 1; // seconds

    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public void Initialize(Vector2 direction, float speed, float damage, float lifetime)
    {
        Direction = direction;
        Speed = speed;
        Damage = damage;
        Lifetime = lifetime;
    }
}