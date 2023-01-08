using Godot;
using System;

public class SceneManager : Node2D
{
 
    [Export] public PackedScene HomeScreen;
    [Export] public PackedScene MainScreen;
    [Export] public PackedScene GameOverScreen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ResetGame();
    }

    public void ResetGame(){
        GetTree().ChangeSceneTo(HomeScreen);        
    }

    public void StartGame(){
        GetTree().ChangeSceneTo(MainScreen);
    }

    public void GameOver(){
        GetTree().ChangeSceneTo(GameOverScreen);
        
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
