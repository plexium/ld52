using Godot;
using System;

public class Main : Node2D
{
    private HUD hud;
    private Dialog dialog;
    private Kitty player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = GetNode<Kitty>("Porch/Kitty");

        hud = GetNode<HUD>("HUD");
        hud.Player = player;

        dialog = GetNode<Dialog>("HUD/TransactionUI");
        dialog.Player = player;
    }

    public void OpenDoor()
    {
        GetNode<Node2D>("Porch").Hide();
        GetNode<Node2D>("Door").Show();
        dialog.StartTransaction();
        dialog.Show();
    }

    public void CloseDoor()
    {
        GetNode<Node2D>("Porch").Show();
        GetNode<Node2D>("Door").Hide();
        dialog.Hide();
    }
}
