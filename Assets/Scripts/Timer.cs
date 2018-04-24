using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	public Text timerUIText;
	public int time;
	private bool calc;
	// Use this for initialization
	void Start () {
		timerUIText.text = "000";
		time = 0;

		startTimer ();
	}
	
	// Update is called once per frame
	void Update () {
		timerUIText.GetComponent<Text> ().text = time.ToString("000");
		//countupTime ();
	}

	public void resetTimer() {
		time = 0;
	}

	public void startTimer() {
		StartCoroutine ("countupTime");
	}

	public void stopTimer() {
		StopCoroutine("countupTime");
	}

	public void zeroTimer() {
		StartCoroutine ("countdownTime");
	}

	private IEnumerator countdownTime() {
		while (true) {
			yield return new WaitForSeconds (0.01f);
			time--;
			if (time <= 0)
				StopCoroutine ("countdownTime");
		}
	}

	private IEnumerator countupTime() {
		while (true) {
			yield return new WaitForSeconds (1);
			time++;
		}
	}
}
