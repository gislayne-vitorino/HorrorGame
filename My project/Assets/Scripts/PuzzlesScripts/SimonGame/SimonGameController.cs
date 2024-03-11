using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimonGameController : MonoBehaviour
{
    private List<int> taskList = new List<int>();
    private List<int> playerList = new List<int>();
    private bool playerHaveMoved = false;
    private bool coroutineSequenceEnded = true;
    private GameObject[] allButtons;

    public float timer = 0f;
    [SerializeField] private int initRounds;
    [SerializeField] private int totalRounds;

    //!Isso nao deveria estar aqui, deveria criar outro script para isso
    [SerializeField] Renderer sequenceShower;
    [SerializeField] List<Material> materialsList;

    void Start (){
      initGame();
      allButtons = GameObject.FindGameObjectsWithTag("SimonButton");
    }

    void Update (){
      if (playerHaveMoved){
        StopCoroutine(showSequence());
        if (isSequenceCorrect()){
          playerHaveMoved = false;
          timer = 0f;
          if (totalRounds == playerList.Count){
            winGame();
          }else if (taskList.Count == playerList.Count){
            SetUpNextRound();
          }
        }
        else{
          loseGame();
          playerHaveMoved = false;
          //sendSequenceToMonster(taskList);
        }
      }

      if (coroutineSequenceEnded)
        timer += Time.deltaTime;

      if (timer >= 5.0f){
        StartCoroutine(showSequence());
        timer = 0f;
      }

    }

    private void initGame(){
      for (int i = 0; i < initRounds; i++){
        addRound();
      }
      StartCoroutine(showSequence());
    }

    private void addRound(){
      taskList.Add(Random.Range(0,4));
    }

    public void addPlayerMove(int buttonId){
      playerList.Add(buttonId);
      Debug.Log(buttonId);
      playerHaveMoved = true;
    }

    private bool isSequenceCorrect(){
      for (int i = 0; i < playerList.Count; i++)
        if (playerList[i] != taskList[i]){
          return false;
        }
      return true;
    }

    private void SetUpNextRound(){
      playerList.Clear();
      addRound();
      StartCoroutine(showSequence());
    }

    private void winGame(){     
      foreach(GameObject button in allButtons){
        Button script = button.GetComponent<Button>();
        script.disableInteraction();
      }
      disableSimonGame();
    }

    private void loseGame(){
      playerList.Clear();
      taskList.Clear();
      initGame();
    }

    //!!Isso nao deveria estar aqui, deveria criar outro script para isso
    private IEnumerator showSequence(){
      coroutineSequenceEnded = false;
      foreach(var buttonId in taskList) {
        sequenceShower.material = materialsList[buttonId+1];
        yield return new WaitForSeconds(1f);
        sequenceShower.material = materialsList[0];
        yield return new WaitForSeconds(.75f);
      }
      sequenceShower.material = materialsList[0];
      coroutineSequenceEnded = true;
    }

    private void disableSimonGame(){
      GameObject button = transform.gameObject;
      button.GetComponent<SimonGameController>().enabled = false;
    }
}
