using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovMonster : MonoBehaviour
{
    private Transform target;
    public Rigidbody rb;
    public float speed = 2f;

    private void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FollowPlayer(){
            Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            Debug.Log (pos);
            rb.MovePosition(pos);
            transform.LookAt(target);
        }
        
    void OnTriggerStay(Collider player){
      
            if(player.tag == "Player"){
              FollowPlayer();
            }
          }
}
