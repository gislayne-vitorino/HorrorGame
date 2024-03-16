using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Tobii.Gaming;
using UnityEngine;

public class ShowNoGazeDetection : MonoBehaviour
{

    public GameObject Indicator;
    private float idleTimer = 0f;
    public float idleDuration = 5f;
    private MovMonster movMonster;
    private ReturnToBase returnMonster;
    private float happinessTimer = 0f;
    public float happinessDuration = 2f;
    private bool characterVisible = true;

    private void Start()
    {
        idleTimer = 0;

        movMonster = GetComponent<MovMonster>();
        returnMonster = GetComponent<ReturnToBase>();
        movMonster.EnableTriggerStay();
        returnMonster.DisableTriggerStay();
    }

    void Update()
    {
        if (!TobiiAPI.GetGazePoint().IsRecent())
        {
            UnityEngine.Debug.Log(happinessTimer);
            if (Time.time - happinessTimer >= happinessDuration)
            {
                HideCharacter();
                idleTimer = 0f;
                idleTimer = Time.time;
            }
        }
        else
        {
            if (Time.time - idleTimer >= idleDuration)
            {
                happinessTimer = Time.time;
                movMonster.EnableTriggerStay();
                returnMonster.DisableTriggerStay();

            }
            UnityEngine.Debug.Log("Eyes opened");
        }
    }

    private void ShowGraphic(bool isVisible)
    {
        Indicator.SetActive(isVisible);
    }

    void HideCharacter()
    {
        // Implemente o c�digo para esconder o personagem
        characterVisible = false;
        movMonster.DisableTriggerStay();
        returnMonster.EnableTriggerStay();
        //Debug.Log("mataaaaa");
        //Vector3 pos =new Vector3(-3.768587f, -4.768372e-07f, -2.276852f);

        //rb.MovePosition(Vector3.Lerp(rb.position, pos, moveSpeed * Time.fixedDeltaTime));

    }
}
