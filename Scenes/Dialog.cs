using Godot;
using System;

public class Dialog : Control
{

    private Button sellButton;
    private Button skipButton;
    private RichTextLabel message;
    public Kitty Player;

    [Signal] delegate void OnSoldNip();
    [Signal] delegate void OnSkip();
    [Signal] delegate void OnBusted();

    public override void _Ready()
    {
        message = GetNode<RichTextLabel>("Message");
        sellButton = GetNode<Button>("Sell");
        skipButton = GetNode<Button>("Skip");        
    }

    public void StartTransaction()
    {
        sellButton.Disabled = ( Player.Nip == 0 );
        message.Text = "Meow, gimmie sum of dat nipnip, cat...";
    }

    public void Sell()
    {
        StartTransaction();
        GD.Print("sell");
        Player.CatCoin += Player.Nip;
        Player.MarketShare += Player.Nip;
        Player.Nip = 0;

        if (Player.MarketShare >= 100 )
        {
            Player.MarketShare = 100;
            GetTree().ChangeScene("res://Scenes/GameOver.tscn");
        }

        EmitSignal(nameof(OnSoldNip));
    }

    public void Busted()
    {
        Player.Nip = 0;
        Player.Seeds = 1;
        Player.CatCoin = (int) Mathf.Floor((float) Player.CatCoin / 2f);
        Player.MarketShare = (int) Mathf.Floor((float) Player.MarketShare / 2f);
        EmitSignal(nameof(OnBusted));
    }

    public void Skip()
    {
        Player.MarketShare -= 2;
        if ( Player.MarketShare < 0 ) Player.MarketShare = 0;
        GD.Print("skip");
        EmitSignal(nameof(OnSkip));
    }
}