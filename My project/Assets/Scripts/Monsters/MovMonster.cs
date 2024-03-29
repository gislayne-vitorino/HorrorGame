using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMonster : MonoBehaviour
{
    private Transform target;
    public Rigidbody rb;
    public float speed = 5f;
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
            Vector3 targetPos = target.position;
            targetPos.y = 0;
            Vector3 pos = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            //Debug.Log (pos);
            rb.MovePosition(pos);
            transform.LookAt(targetPos);
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
