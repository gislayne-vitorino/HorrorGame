using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrinciapal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string tagJogo;

    public void Jogar(){
        SceneManager.LoadScene(tagJogo);
    }

    public void SairdoJogo(){
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
