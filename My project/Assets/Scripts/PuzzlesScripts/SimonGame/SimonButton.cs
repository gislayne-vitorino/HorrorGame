using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonButton : MonoBehaviour
{
  SimonGameController simonGame;
  [SerializeField] int buttonId;

    void Start (){
      simonGame = GameObject.Find("SimonGameManager").GetComponent<SimonGameController>();
    }

    public void wasClicked (){
      simonGame.addPlayerMove(buttonId);
    }
}
