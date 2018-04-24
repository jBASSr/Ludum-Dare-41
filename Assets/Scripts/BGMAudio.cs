using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMAudio : MonoBehaviour {

	public AudioSource BGM;

	void Start() {
		if (FindObjectsOfType<BGMAudio>().Length > 1) {
			Destroy(gameObject);
		}
	}

	public void changeBGM(AudioClip music) {
		if (BGM.clip.name == music.name) {
			return;
		}
		BGM.Stop ();
		BGM.clip = music;
		BGM.Play ();
	}
}
