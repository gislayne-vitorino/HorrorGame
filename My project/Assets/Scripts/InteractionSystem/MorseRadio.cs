using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MorseRadio : MonoBehaviour, IInteractable
{
  private string _prompt;
  [SerializeField] private string onPrompt;
  [SerializeField] private string offPrompt;
  [SerializeField] private bool isOn = false;
  private MorseCodePlayer morsePlayer;

  public void Awake(){
      GameObject radio = transform.gameObject;
      morsePlayer = radio.GetComponent<MorseCodePlayer>();

      if (isOn)
        _prompt = onPrompt;
      else 
        _prompt = offPrompt;
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
    _prompt = onPrompt;
  }

  public void disableInteraction()
  {
    morsePlayer.enabled = false;
    morsePlayer.StopAllCoroutines();
    morsePlayer.resetAfterStopCoroutines();
    isOn = false;
    _prompt = offPrompt;
  }

}
