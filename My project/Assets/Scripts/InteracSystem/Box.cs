using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{

  [SerializeField] Animator animator;
  public string InteractionPrompt => _prompt;

  [SerializeField] private string _prompt = "Aperte E para abrir a caixa";
  private MoveRuller padlockMoveRull;
  private PadLockPassword padlockPassword;

  private Inventory inventory;
  
  private GameObject box;

  [SerializeField] private Renderer key;

  private bool simonGameSolved = false;

  private bool padlockWasCracked = false;

  public void Start(){
    box = transform.gameObject;
    padlockMoveRull = FindObjectOfType<MoveRuller>();
    padlockPassword = FindObjectOfType<PadLockPassword>();

    GameObject player = GameObject.FindGameObjectWithTag("Player");
    inventory = player.GetComponent<Inventory>();
  }

  public void disableInteraction()
  {
    int newLayer = LayerMask.NameToLayer("Scenario");
    box.layer = newLayer;
  }

  public void enableInteraction()
  {
    int newLayer = LayerMask.NameToLayer("InteractableObject");
    box.layer = newLayer;
  }

  public bool Interact(Interactor interactor)
  {
    /*
    if (padlockWasCracked){
      animator.SetBool("isOpen", true);
    } else{
      GameManager.Instance.updateState(GameState.Focused);
      padlockMoveRull.enableInteraction();
    }
    */
    if (simonGameSolved){
      inventory.hasKey = true;
      key.enabled = false;
      disableInteraction();
      //Destroy Key
    } else {
      _prompt = "A caixa parece estar trancada";
    }
    
    return true;
  }

  internal void simonGameWasSolved()
  {
    animator.SetBool("isOpen", true);
    _prompt = "Aperte E para pegar a chave";
    simonGameSolved = true;
  }
}
