using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    private Transform target;
    public Rigidbody rb;
    public float speed = 1f;
    private bool triggerStayEnabled = true;

    private void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update(){
         if(triggerStayEnabled==true){
              FollowPlayer();
            }
    }

    void FollowPlayer(){
           Vector3 pos = new Vector3(-3.768587f, -4.768372e-07f, -2.276852f);
            // Determina a próxima posição na direção de 'pos'
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

            // Move o objeto para a próxima posição
            rb.MovePosition(nextPosition);

            // Orienta o objeto na direção da posição de destino
            transform.LookAt(pos);
        }
        

    public void EnableTriggerStay()
    {
        triggerStayEnabled = true;
    }

    // Método para desabilitar o OnTriggerStay
    public void DisableTriggerStay()
    {
        triggerStayEnabled = false;
    }
}
