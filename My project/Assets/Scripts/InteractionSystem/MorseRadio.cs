using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MorseRadio : MonoBehaviour, IInteractable
{
  [SerializeField] private string _prompt;
  private MorseCodePlayer morsePlayer;

  private bool isOn = false;

  public void Awake(){
      GameObject radio = transform.gameObject;
      morsePlayer = radio.GetComponent<MorseCodePlayer>();
    }

  public string InteractionPrompt => _prompt;

  public bool Interact(Interactor interactor)
  {
    if (isOn)
      disableInteraction();
    else
      enableInteraction();
    
    return true;
  }

  public void enableInteraction()
  {
    morsePlayer.enabled = true;
    isOn = true;
  }

  public void disableInteraction()
  {
    morsePlayer.enabled = false;
    isOn = false;
  }

}
