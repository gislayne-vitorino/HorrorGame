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
    
    private bool characterVisible = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GetRequest("http://127.0.0.1:5000/emotion"));
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
            HideCharacter();
        }else{
            ShowCharacter();
        }
    }
     void HideCharacter()
    {
        // Implemente o código para esconder o personagem
        characterVisible = false;
        gameObject.SetActive(false);
    }
    void ShowCharacter()
    {
        // Implemente o código para mostrar o personagem
        characterVisible = true;
        gameObject.SetActive(true);
    }
}
