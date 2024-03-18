using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M2CollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se o objeto colidido é o jogador
        {
            GameManager.Instance.updateState(GameState.Lose); // Chama a função updateState do GameManager para definir o estado como "Lose"
        }
    }
}