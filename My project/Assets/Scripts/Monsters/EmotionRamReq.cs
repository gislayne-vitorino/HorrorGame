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
    private bool happinessFlag = true;
    private float idleTimer = 0f;  
    private float idleDuration = 10f;
  
    
    public float interval = 0.002f; // Intervalo em segundos
    private float lastExecutionTime = 0f;
    
    private List<int> taskList = new List<int> { 0, 1, 2, 3 };
    private List<string> emotionList = new List<string> { "Anger", "Sadness", "Happiness", "Surprise" };
    private int actualState = 0;
    private bool characterVisible = true;
    public Rigidbody rb;
    private MovMonster movMonster;
    private ReturnToBase returnMonster;

    private float moveSpeed = 0.1f; // Velocidade de movimento


    // Start is called before the first frame update
    void Start()
    {   
        movMonster = GetComponent<MovMonster>();
        returnMonster = GetComponent<ReturnToBase>(); // Obtemos o componente ExampleScript associado ao GameObject
        movMonster.EnableTriggerStay();
        returnMonster.DisableTriggerStay();

        idleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //requisitionTimer += Time.deltaTime;
        //if(requisitionTimer >= requisitionDuration){
         if (Time.time - lastExecutionTime >= interval)
        {
            // Executa o comando desejado
            StartCoroutine(GetRequest("http://127.0.0.1:5000/emotion"));
            // Atualiza o tempo da última execução
            lastExecutionTime = Time.time;
        }
            
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
        if (apiResponse != null && apiResponse.emotion == emotionList[taskList[(actualState%(taskList.Count))]])
        {   
    
                //Debug.Log("INICIO");
                //Debug.Log(string.Join(", ", taskList));
             
            //Debug.Log(emotionList);
            //Debug.Log(taskList);
            
            Debug.Log(emotionList[taskList[(actualState%(taskList.Count))]]);

            //happinessTimer += Time.deltaTime;
            Debug.Log(happinessTimer);
            Debug.Log(Time.time);
             Debug.Log(happinessFlag);
            if(Time.time - happinessTimer >= happinessDuration && happinessFlag){
                actualState+=1;
                HideCharacter();
                idleTimer = 0f;
                happinessFlag = false;
                idleTimer = Time.time;
            }
        }else{
            //idleTimer += Time.deltaTime;
            //Debug.Log(happinessTimer);
            //happinessTimer = 0f;
            happinessTimer=Time.time;
            if(Time.time - idleTimer >= idleDuration){
                happinessFlag = true;
                movMonster.EnableTriggerStay();
                returnMonster.DisableTriggerStay();

            }

        }
    }
     void HideCharacter()
    {
        // Implemente o código para esconder o personagem
        characterVisible = false;
        movMonster.DisableTriggerStay();
        returnMonster.EnableTriggerStay();
        //Debug.Log("mataaaaa");
        //Vector3 pos =new Vector3(-3.768587f, -4.768372e-07f, -2.276852f);
        
        //rb.MovePosition(Vector3.Lerp(rb.position, pos, moveSpeed * Time.fixedDeltaTime));
        
    }
    void ShowCharacter()
    {
        // Implemente o código para mostrar o personagem
        characterVisible = true;
        gameObject.SetActive(true);
    }

    public void sendSequenceToMonster(List<int> newList)
    {
        // Implemente o código para mostrar o personagem

        taskList= newList;
    }
}