using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	static public bool endOfLevel;
	static public bool loadNextLevel;
	private bool countScore;
	public int timeScore;
	public int gameScore;
	int counter;

	float wait;
	Timer t;
	GameScore s;
	GameObject TCG;

	void Start() {
		t = GameObject.Find("TimeText").GetComponent<Timer> ();
		s = GameObject.Find("ScoreText").GetComponent<GameScore> ();
		TCG = GameObject.Find ("TCG");
		wait = 0f;
		counter = 0;
	}

	void Update() {
		//Debug.Log (endOfLevel);
		//Debug.Log (countScore);
		//Debug.Log (timeScore);
		if (endOfLevel) {
			if (countScore) {
				if (timeScore > 0) {
					
					StartCoroutine ("deductTimePenalty");
					//countScore = !countScore;
				}
				/*
				else {
					Debug.Log ("Done calculating score, moving on");
					loadNextLevel = true;

				}
				*/
			}
			if (loadNextLevel) {
				Debug.Log ("Loading next level");
				countScore = false;
				endOfLevel = false;
				loadNextLevel = false;
				TCG.GetComponent<CanvasGroup> ().alpha = 1f;
				// I hate hard coding but last second implementations!!!
				// Actual levels should be 1 to whatever value here (3 in this case);
				// Keep menu at 0.
				if (GetLevelNumber.levelNum < 5) {
					//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
					SceneManager.LoadScene("next_level");
				}
				//TCG.SetActive (true);
			}
		}
	}

	private IEnumerator deductTimePenalty() {
		while (true) {
			yield return new WaitForSeconds (1f);
			GameScore.score -= 10;
			Debug.Log ("Counter:" + counter + " TimeScore:" + timeScore);
			if (++counter >= timeScore) {
				StopCoroutine ("deductTimePenalty");
				timeScore = 0;
				loadNextLevel = true;
				Debug.Log ("Done calculating score, moving on");
				//countScore = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		if (c.tag == "Player" && !endOfLevel) {
			GameScore.score += 10000; // Score for beating the level
			// Save time and score
			timeScore = int.Parse(t.timerUIText.text);
			gameScore = int.Parse(s.scoreUIText.text);
			// Calculate 'final' score
			if ( (gameScore - timeScore) < 0 ) {
				gameScore = 0;
			}
			TCG.GetComponent<CanvasGroup> ().alpha = 0f;
				//.SetActive (false);
			t.stopTimer ();
			t.zeroTimer ();
			countScore = true;
			endOfLevel = true;
			Debug.Log ("Player reached the end");
		}
	}
}
