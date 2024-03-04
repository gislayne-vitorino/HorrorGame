using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiResponse
{
    public string emotion;
}
public class EmotionRamReq : MonoBehaviour
{
    private float happinessTimer = 0f;
    private float happinessDuration = 2f;

    private float idleTimer = 0f;  
    private float idleDuration = 5f;
  
    private float requisitionTimer = 0f;
    private float requisitionDuration = 0.5f;

    private bool characterVisible = true;
    public Rigidbody rb;
    private MovMonster movMonster;
    private float moveSpeed = 0.1f; // Velocidade de movimento


    // Start is called before the first frame update
    void Start()
    {   
        movMonster = GetComponent<MovMonster>(); // Obtemos o componente ExampleScript associado ao GameObject
        movMonster.EnableTriggerStay();
        idleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //requisitionTimer += Time.deltaTime;
       // if(requisitionTimer >= requisitionDuration){
            StartCoroutine(GetRequest("http://127.0.0.1:5000/emotion"));
        //    requisitionTimer=0;
        //}
    }
    IEnumerator GetRequest(string url)  {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)){
            yield return webRequest.SendWebRequest();

            if(webRequest.isNetworkError){
                Debug.Log("error: " + webRequest.error);

            }else {
                Debug.Log(webRequest.downloadHandler.text);
                HandleApiResponse(webRequest.downloadHandler.text);
            }
        

        }
    }

    void HandleApiResponse(string response){
    
        ApiResponse apiResponse = JsonUtility.FromJson<ApiResponse>(response);
        if (apiResponse != null && apiResponse.emotion == "Happiness")
        {
            happinessTimer += Time.deltaTime;
            //Debug.Log(happinessTimer);
            if(happinessTimer >= happinessDuration){
                HideCharacter();
                idleTimer = 0f;
            }
        }else{
            idleTimer += Time.deltaTime;
            //Debug.Log(happinessTimer);
            happinessTimer = 0f;
            if(idleTimer >= idleDuration){
                movMonster.EnableTriggerStay();
            }

        }
    }
     void HideCharacter()
    {
        // Implemente o código para esconder o personagem
        characterVisible = false;
        movMonster.DisableTriggerStay();
        Debug.Log("mataaaaa");
        Vector3 pos =new Vector3(-3.768587f, -4.768372e-07f, -2.276852f);
        rb.MovePosition(Vector3.Lerp(rb.position, pos, moveSpeed * Time.fixedDeltaTime));
        
    }
    void ShowCharacter()
    {
        // Implemente o código para mostrar o personagem
        characterVisible = true;
        gameObject.SetActive(true);
    }
}