using UnityEngine;
using System.Collections;

public class MorseCodePlayer : MonoBehaviour {

  [SerializeField] private AudioSource source;
	[SerializeField] private AudioClip dotSound;
	[SerializeField] private AudioClip dashSound;
  [SerializeField] private int numberOfLetters = 4;

	public float spaceDelay;
	public float letterDelay;
  private bool morseCodeEnded = true;
  private string selectedWord;
  private float timer = 0.0f;
	
	private string[,] possibleWords = 
	{
    
    {//four letter words
      "--..----", //mito
      ".-.-..--.-",//alma
      "----....---", //odio
      "---.-......---", //olho
    },
    {//five letter words
    "-----.-.-.", //morte
    "---.-...-..----", //orfao
    "...-...-...----", //vilao
    "---..-...-...-.", //ouvir
    },
    {//six letter words
     "....---...-...-..", //hostil
     "..-...-..---.", //infame
     ".-..--.-.-.---.-.", //rancor
     "--.-.-.-...-." //mártir
    }
};

	void Start ()
	{
    spaceDelay = dotSound.length*14;
    letterDelay = dotSound.length*6;

    int correctSizeIndex = numberOfLetters - 4;
    int wordIndex = Random.Range(0,4);

    selectedWord = possibleWords[correctSizeIndex,wordIndex];
	}

  void Update (){
      if (morseCodeEnded)
        timer += Time.deltaTime;

      if (timer >= 5.0f){
        StartCoroutine(PlayMorseCodeMessage(selectedWord));
        timer = 0f;
      }
    }
  
	private IEnumerator PlayMorseCodeMessage(string message)
	{
    morseCodeEnded = false;
		
		foreach(char letter in message)
		{
			if (letter == ' ')
				yield return new WaitForSeconds(spaceDelay);
			else 
			{
				AudioClip sound;
        if (letter == '-')	
            sound = dashSound;
          else 
            sound = dotSound;
           
					GetComponent<AudioSource>().PlayOneShot(sound);
					yield return new WaitForSeconds(sound.length + letterDelay);
      }
    }   
    morseCodeEnded = true;
	}
   
}
