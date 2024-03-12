using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

  private string _prompt = "Aperte E para abrir a Porta";
  public string InteractionPrompt => _prompt;
  private Inventory playerInventory;
  private GameObject door;
  private Animator animator;

  [SerializeField] private bool needKey = false;
  [SerializeField] private bool allwaysLocked = false;

  void Start(){
    door = transform.gameObject;
    playerInventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    animator = GetComponent<Animator>();
  }

  public void enableInteraction()
  {
    int newLayer = LayerMask.NameToLayer("InteractableObject");
    door.layer = newLayer;
  }

  public void disableInteraction()
  {
    int newLayer = LayerMask.NameToLayer("Scenario");
    door.layer = newLayer;
  }

  private void openDoor(){
    animator.SetBool("doorIsOpen", true);
    playerInventory.hasKey = false;
    disableInteraction();
  }
  private IEnumerator shakeDoor(){
    animator.SetBool("doorIsClosed", true);
    _prompt = "";
    yield return new WaitForSeconds(1.5f);
    if (allwaysLocked)
      _prompt = "A porta parece estar bloqueada pelo outro lado";
    else
      _prompt = "A porta está trancada";
    yield return new WaitForSeconds(1.5f);
    animator.SetBool("doorIsClosed", false);
    _prompt = "Aperte E para abrir a Porta";
  }

  public bool Interact(Interactor interactor)
  {
    if (needKey)
    {
      if (playerInventory.hasKey)
        openDoor();
      else
        StartCoroutine(shakeDoor());
    } 
    else if (allwaysLocked)
      StartCoroutine(shakeDoor());
    else
      openDoor();
    return true;
  }
}
