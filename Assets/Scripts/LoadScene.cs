using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    public Button startGame;

	void Start() {
		GetLevelNumber.levelNum = 1;
		startGame = GameObject.Find ("StartButton").GetComponent<UnityEngine.UI.Button> ();
		startGame.onClick.AddListener (NewGame);
	}

	void NewGame() {
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		SceneManager.LoadScene("next_level");
	}	
}
