using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
  [SerializeField] Material originalMaterial;
  [SerializeField] Material highlightedMaterial;
  [SerializeField] Renderer rendererReference;
  [SerializeField] SimonGameController simonGame;
  [SerializeField] int buttonId;

    private IEnumerator ChangeColor (){
      rendererReference.material = highlightedMaterial;
      yield return new WaitForSeconds(0.5f);
      rendererReference.material = originalMaterial;
    }

    public void wasClicked (){
      simonGame.addPlayerMove(buttonId);
      StartCoroutine(ChangeColor());
    }
}
