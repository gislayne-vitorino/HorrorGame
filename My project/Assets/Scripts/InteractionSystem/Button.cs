using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private SimonButton simonButtonScript;
    private GameObject button;
    private Button buttonScript;

    public void Awake(){
      button = transform.gameObject;
      buttonScript = button.GetComponent<Button>();
      simonButtonScript = button.GetComponent<SimonButton>();
    }

    public string InteractionPrompt => _prompt;

    public bool Interact (Interactor interactor){
      simonButtonScript.wasClicked();
      return true;
    }

    public void enableInteraction(){
      int newLayer = LayerMask.NameToLayer("InteractableObject");
      button.layer = newLayer;
      buttonScript.enabled = true;
    }

    public void disableInteraction(){
      int newLayer = LayerMask.NameToLayer("Scenario");
      button.layer = newLayer;
      buttonScript.enabled = false;
    }
}
