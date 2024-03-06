using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private int _layerNumber = 8;
    private InteractionPromptUI interactionPromptUI;
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f; 
    private Collider[] _colliders = new Collider [5];
    private int _numFound;

    void Start(){
      interactionPromptUI = GameObject.Find("Canvas").GetComponent<InteractionPromptUI>();
    }

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, 1<<_layerNumber);

        if (_numFound > 0)
        {
          var clickedObject = _colliders[0].GetComponent<IInteractable>();
          interactionPromptUI.SetUp(clickedObject.InteractionPrompt);
          
          if (clickedObject != null && Input.GetKeyDown(KeyCode.E))
            clickedObject.Interact(this);          
        }
        else
          interactionPromptUI.Close();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

/*
    *For player without legs movement (only camera)
    private int _layerNumber = 8;
    private RaycastHit hitObject;
    private InteractionPromptUI interactionPromptUI;

    void Start(){
      interactionPromptUI = GameObject.Find("Canvas").GetComponent<InteractionPromptUI>();
    }

     private void Update()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitObject, 50, 1<<_layerNumber))
        {
          IInteractable clickedObject = hitObject.transform.GetComponent<IInteractable>();
          interactionPromptUI.SetUp(clickedObject.InteractionPrompt);

          if (Input.GetMouseButtonDown(0))
            clickedObject.Interact(this);
        } 
        else
          interactionPromptUI.Close();
    }

*/

    
}
