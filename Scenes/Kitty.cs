using Godot;
using System;

public class Kitty : Node2D
{
    [Export] public int Speed = 400;
    [Export] public int Seeds;
    [Export] public int Nip;
    [Export] public int CatCoin;
    [Export] public int MarketShare;

    private Vector2 ScreenSize;
    private IInteractable onTopOf;

    public override void _Ready()
    {
        Seeds = 1;
        Nip = 0;
        CatCoin = 0;
        MarketShare = 0;
        ScreenSize = GetViewportRect().Size;
    }

    public override void _Process(float delta)
    {
        var velocity = Vector2.Zero;

        if ( Input.IsActionPressed("ui_right") ) velocity.x += 1;
        if ( Input.IsActionPressed("ui_up") ) velocity.y -= 1;
        if ( Input.IsActionPressed("ui_left") ) velocity.x -= 1;
        if ( Input.IsActionPressed("ui_down") ) velocity.y += 1;

        var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");

        if ( velocity.Length() > 0 ){
            velocity = velocity.Normalized() * Speed;
            //animatedSprite.Play();
        } else {
            //animatedSprite.Stop();
        }

        Position += velocity * delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.x,0,ScreenSize.x),
            y: Mathf.Clamp(Position.y,0,ScreenSize.y)
        );

        if ( velocity.x != 0 ){
//            animatedSprite.Animation = "walk";
//            animatedSprite.FlipV = false;
            animatedSprite.FlipH = velocity.x > 0;
        } else if ( velocity.y != 0 ){
//            animatedSprite.Animation = "up";
//            animatedSprite.FlipV = velocity.y > 0;
        }
    }

    public override void _Input(InputEvent e)
    {
        if ( !e.IsActionPressed("ui_select") ) return;

        if ( onTopOf == null ) return;

        onTopOf.Interact(this);
    }

    public void OnBodyEnter(PhysicsBody2D body)
    {
        if ( body.GetParent() is IInteractable )
        {
            onTopOf = body.GetParent() as IInteractable;
        }
    }

    public void OnBodyExit(PhysicsBody2D body)
    {
        onTopOf = null;
    }
}
