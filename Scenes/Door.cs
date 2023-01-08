using Godot;
using System;

public class Door : Node2D, IInteractable
{
    [Export] public float Pop;    

    [Signal] delegate void OnAnswerDoor();

    private bool buyer;

    public override void _Ready()
    {
        Pop = 0.1f;
        buyer = false;
    }

    public void Interact(Kitty player)
    {
        AnswerDoor();
    }

    public void CheckDoor()
    {
        if ( buyer ){
            GetNode<AnimatedSprite>("AnimatedSprite").Animation = "knock";
            GetNode<AnimatedSprite>("AnimatedSprite").Play();
        } else {
            if ( GD.RandRange(0f,1f) <= Pop ){
                buyer = true;
            }
        }
    }

    public void AnswerDoor()
    {
        buyer = false;        
        EmitSignal(nameof(OnAnswerDoor));
        GetNode<AnimatedSprite>("AnimatedSprite").Animation = "default";
    }
}
