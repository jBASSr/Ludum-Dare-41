using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetLevelNumber : MonoBehaviour {
	static public int levelNum;
	public int currentLevel;
	// Use this for initialization
	int timer = 0;
	void Start() {
		StartCoroutine ("waitTimer");
		GetComponent<Text> ().text = string.Format ("1-{0}", levelNum);
	}
	void Update()
	{
		
		if (timer > 2)
		{
			string levelName = string.Format ("level0{0}", levelNum);
			Debug.Log (levelName);
			SceneManager.LoadScene (levelName);
		}
	}
	private IEnumerator waitTimer() {
		while (true) {
			yield return new WaitForSeconds (1f);
			timer++;
		}
	}
}
