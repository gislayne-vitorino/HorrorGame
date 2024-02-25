using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{

  [SerializeField] Animator animator;
  public string InteractionPrompt => "Clique para abrir a caixa";

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
    animator.SetBool("isOpen", true);
    return true;
  }
}
