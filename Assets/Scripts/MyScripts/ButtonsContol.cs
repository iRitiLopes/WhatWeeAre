using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonsContol : MonoBehaviour {

	// Use this for initialization

	GameObject gameData;

	void Start () {
		gameData = GameObject.Find ("GameData");
		
	}
		
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void ChangeScene(int indexNewScene)
    {
		//float fadeTime = GameObject.Find ("_GM").GetComponent<Fading>().BeginFade(1);
		//yield return new WaitForSeconds (fadeTime);
        SceneManager.LoadScene(indexNewScene);

    }
    
	public void setNewGame(){		

		for (int i = 0; i < 2; i++) {
			gameData.GetComponent<DataControl> ().SetLevel1Itens (false, i);
		}

		gameData.GetComponent<DataControl> ().Save ();
	}


}
