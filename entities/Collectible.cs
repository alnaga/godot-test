using Godot;

public abstract partial class Collectible : AnimatableBody2D
{
    protected bool Collected = false;
    protected int ID = 0;

    public override void _Process(double delta)
    {
        base._Process(delta);

        var settings = GetNodeOrNull<Settings>("Settings");
        if (settings != null && settings.IsDebug)
        {
            GD.Print("Collectible with ID: ", ID, " collected: ", Collected);
        }
    }

    public void Collect()
    {
        Collected = true;
        QueueFree();
    }
}