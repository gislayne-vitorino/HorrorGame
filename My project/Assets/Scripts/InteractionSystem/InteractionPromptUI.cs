using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    public void SetUp(string text){
      promptText.text = text;
      uiPanel.SetActive(true);
    }

    public void Close (){
      uiPanel.SetActive(false);
    }
}
