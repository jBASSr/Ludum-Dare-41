using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStack : MonoBehaviour {
	static public bool storedInput;
	private int count;
	// SOURCE: https://forum.unity.com/threads/queuing-input-keys.4073/

	private static InputStack inputstack;
	public static InputStack get {
		get {
			if (!inputstack) {
				Debug.LogError ("No InputStack in scene!");
			}
			return inputstack;
		}
	}

	// What keys are we checking for?
	private string[] keys = new string[] {
		"w", "a", "s", "d", "space"
	};
	private Queue keyQueue;


	void Awake () {
		inputstack = this;
		keyQueue = new Queue ();
		//GameObject.Find ("PlayCardArea").GetComponent<PlayArea> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerMovement.endedTurn && !storedInput) {
			//Debug.Log ("Caught turn ended in InputStack class :)");
			count = 0;
			foreach (string key in keys) {
				if (count < PlayArea.moveQ.Count) {
					//Debug.Log ("Got key: " + PlayArea.moveQ [count]);
					keyQueue.Enqueue (PlayArea.moveQ[count]);
					count++;
				}
			}
			storedInput = true;
			PlayArea.moveQ.Clear ();
		}
		/*
		foreach (string key in keys) {
			if (Input.GetKeyDown (key)) {
				Debug.Log ("Got key: " + key);
				keyQueue.Enqueue (key);
			}
		}
		*/
	}

	public string GetNextKey() {
		if (keyQueue.Count > 0) {
			return keyQueue.Dequeue () as string;
		} else {
			return "";
		}
	}

	public bool HasKeys() {
		return keyQueue.Count > 0;
	}
}
