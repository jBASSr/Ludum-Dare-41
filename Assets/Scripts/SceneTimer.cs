using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTimer : MonoBehaviour
{
	int timer = 0;
	void Start()
	{
		StartCoroutine ("waitTimer");
	}
    // Update is called once per frame
    void Update()
    {
		if (timer > 3)
        {
            SceneManager.LoadScene("menu");
        }
    }

	private IEnumerator waitTimer() {
		while (true) {
			yield return new WaitForSeconds (1f);
			timer++;
		}
	}
}
