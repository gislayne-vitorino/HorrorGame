using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private int _layerNumber = 8;

    private RaycastHit hitObject;

     private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          
          if (Physics.Raycast(ray, out hitObject, 50, 1<<_layerNumber))
          {
            hitObject.transform.GetComponent<IInteractable>().Interact(this);
          }
        }       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawRay();
    }


    /*
    * Codigo para interacao com um player que se movimenta
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    private Collider[] _colliders = new Collider [5];
    private int _numFound;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
          var interactable = _colliders[0].GetComponent<IInteractable>();
          
          if (interactable != null && Keyboard.current.eKey.wasPressedThisFrame)
          {
            interactable.Interact(this);
          
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
    */
}
