using Godot;
using System;

public class Plot : Node2D, IInteractable
{
    public enum State {
        EMPTY,
        GROWING,
        MATURE
    }

    [Signal] delegate void OnSeedPlanted();
    [Signal] delegate void OnMature();
    [Signal] delegate void OnInteract();

    private Plot.State state;
    private int progress;
    private int growSpeed;
    private float tally;

    private StaticBody2D collider;
    private AnimatedSprite anime;

    public bool CanPlant { get { return state == Plot.State.EMPTY; } }
    public bool CanHarvest { get { return state == Plot.State.MATURE; } }

    public override void _Ready()
    {
        progress = 0;
        tally = 0f;
        growSpeed = 10;
        state = Plot.State.EMPTY;

        anime = GetNode<AnimatedSprite>("AnimatedSprite");

        collider = GetNode<StaticBody2D>("StaticBody2D");
        collider.Connect("input_event",this,nameof(Interact));
    }

    public void Interact(Kitty kitty)
    {
        switch (state)
        {
            case Plot.State.EMPTY:
                AttemptToPlant(kitty);
            break;
            case Plot.State.GROWING:
            break;
            case Plot.State.MATURE:
                AttemptToHarvest(kitty);
            break;
        }

        EmitSignal(nameof(OnInteract));
    }

    public void AttemptToPlant(Kitty kitty)
    {
        if ( kitty.Seeds == 0 ) 
        {
            GD.Print("Not enough seeds!");
            return;
        }

        state = Plot.State.GROWING;
        anime.Animation = "planted";
        progress = 0;
        kitty.Seeds--;
        GD.Print("Planted");
    }

    public void AttemptToHarvest(Kitty kitty)
    {
        state = Plot.State.EMPTY;
        progress = 0;
        anime.Animation = "empty";
        GD.Print("Harvested");
        kitty.Seeds += (int) Mathf.Round((float) GD.RandRange(1,2));
        kitty.Nip++;
    }

    public override void _Process(float delta)
    {
        if ( state == Plot.State.GROWING )
        {
            tally += delta;
            if ( tally >= 1.0f )
            {
                tally = 0f;
                progress += growSpeed;

                GD.Print($"state: {state.ToString()} Progress: {progress.ToString()} Tally: {tally.ToString()} ");

                if ( progress >= 100 )
                {
                    state = Plot.State.MATURE;
                    anime.Animation = "mature";
                    EmitSignal(nameof(OnMature));
                }
            }
        }
    }

}
