using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
  public static MenuManager Instance;
  [SerializeField] private GameObject winScreen;
  [SerializeField] private GameObject loseScreen;

  void Awake(){
    GameManager.OnGameStateChange += setUp;
  }
  public void setUp (GameState gameState){
    if (gameState == GameState.Victory)
    {
      loseScreen.SetActive(false);
      winScreen.SetActive(true);
    } else if (gameState == GameState.Lose){
      winScreen.SetActive(false);
      loseScreen.SetActive(true);
    }
  }
}
