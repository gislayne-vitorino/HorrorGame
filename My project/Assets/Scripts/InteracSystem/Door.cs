using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
  public string InteractionPrompt => "A porta está trancada";

  public void disableInteraction()
  {
    throw new System.NotImplementedException();
  }

  public void enableInteraction()
  {
    throw new System.NotImplementedException();
  }

  public bool Interact(Interactor interactor)
  {
    throw new System.NotImplementedException();
  }
}
