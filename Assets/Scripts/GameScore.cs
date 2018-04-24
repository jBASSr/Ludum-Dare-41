using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {
	public Text scoreUIText;
	static public int score;
	static public int totalScore;
	// Use this for initialization
	void Start () {
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		scoreUIText.GetComponent<Text> ().text = score.ToString("000000");		
	}
}
