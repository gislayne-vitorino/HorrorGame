using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public static event Action<GameState> OnGameStateChange;

  void Awake(){
    Instance = this;
  }

  void Start(){
    updateState(GameState.Playing);
  }

  public void updateState(GameState newState){

    switch (newState)
    {
      case GameState.Playing:
        handlePlayingState();
        break;
      case GameState.Focused:
        handleFocusedState();
        break;
      case GameState.Victory:
        handleVictoryState();
        break;
      case GameState.Lose:
        handleLoseState();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }
    OnGameStateChange?.Invoke(newState);
  }

  private void handlePlayingState(){

  }
  private void handleFocusedState(){
    
  }
  private void handleVictoryState(){
    
  }
  private void handleLoseState(){
    
  }

}

public enum GameState{
  Playing,
  Focused,
  Victory,
  Lose,
}
