using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{   
    public GameObject footstep;
    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKey("w")){
            footsteps();
        }

        if(Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s") ){
            footsteps();
        }  

        if(Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("s") ){
            StopFootsteps();
        } 

        if(Input.GetKey("w")){
            footsteps();
        }
        if(Input.GetKeyUp("w")){
            StopFootsteps();
        }



    }

    void footsteps(){
        footstep.SetActive(true);
    }

    void StopFootsteps(){
        footstep.SetActive(false);
    }
}
