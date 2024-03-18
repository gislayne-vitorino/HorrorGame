using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
  [SerializeField] private AudioClip winSound;
  [SerializeField] private AudioClip loseSound;
  [SerializeField] private AudioClip GameSound;
  [SerializeField] private AudioSource audioSource;
    
  void Awake(){
    GameManager.OnGameStateChange += onStateChange;
  }

  private void onStateChange(GameState gameState)
  {
    if (gameState == GameState.Victory){
      audioSource.Stop();
      audioSource.PlayOneShot(winSound);
    } else if(gameState == GameState.Lose){
      audioSource.Stop();
      audioSource.PlayOneShot(loseSound);
    }
  }
}
