using Godot;
using System;

public class HUD : CanvasLayer
{
    private RichTextLabel seedCount;
    private RichTextLabel nipCount;
    private RichTextLabel catCoin;
    private RichTextLabel marketShare;

    public Kitty Player;

    public override void _Ready()
    {
        seedCount = GetNode<RichTextLabel>("SeedCount");
        nipCount = GetNode<RichTextLabel>("NipCount");
        catCoin = GetNode<RichTextLabel>("CatCoin");
        marketShare = GetNode<RichTextLabel>("Share");
    }

    public override void _Process(float delta)
    {
        nipCount.Text = "Nip : " + Player.Nip.ToString();
        seedCount.Text = "Seeds : " + Player.Seeds.ToString();
        catCoin.Text = "RatTails : $" + Player.CatCoin.ToString();
        marketShare.Text = "Market Share : " + Player.MarketShare.ToString() + "%";
    }
}
