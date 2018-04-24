using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSystem : MonoBehaviour {

	// Deck Stuff
	List<int> cards;
	public int deckSize;
	public int uniqueCards;
	// Generating my own probablity
	private int[] randyValues = { 
		0,0,       // Jump
		1,1,1,1,1, // Forward
		2,2,       // Backward
		3,		   // JumpBackwards
		4,		   // JumpForwards
	};
	// Hand
	public int handCount;
	private int counter;

	public GameObject dropZone;
	public GameObject handZone;
	// Cards
	GameObject card;
	GameObject Jump;
	GameObject Forward;
	GameObject Backward;
	GameObject JumpBackward;
	GameObject JumpForward;
	// Other
	private bool drewCard;
	private bool givenCard;
	public Button drawButton;
	private int cardsInHand;
	//

	void Start() {
		Shuffle();
		handCount = 0;
		counter = 0;
		givenCard = false;
		dropZone = GameObject.Find ("PlayCardArea");
		handZone = GameObject.Find ("PlayerHand");
		Jump = Resources.Load ("Card_J", typeof(GameObject)) as GameObject;
		Forward = Resources.Load ("Card_F", typeof(GameObject)) as GameObject;
		Backward = Resources.Load ("Card_B", typeof(GameObject)) as GameObject;
		JumpForward = Resources.Load ("Card_JF", typeof(GameObject)) as GameObject;
		JumpBackward = Resources.Load ("Card_JB", typeof(GameObject)) as GameObject;
		drawButton = GameObject.Find ("drawButton").GetComponent<UnityEngine.UI.Button> ();
		drawButton.onClick.AddListener(delegate {
			if (HandCount() < 5 && !PlayerMovement.endedTurn && !drewCard){
				Draw(1);
				deckSize--;
				drewCard = true;
			}
		});
		FillHand();
	}

	void Awake() {
	}

	// Update is called once per frame
	void Update () {
		//
		if (PlayerMovement.endedTurn && !givenCard) {
			// If player has 4 or less cards, draw one card;
			if (HandCount() < 5) {
				Debug.Log ("Player's new turn, give a card");
				Draw(1);
				deckSize--;
			}
		} else if (!PlayerMovement.endedTurn && givenCard) {
			givenCard = !givenCard;
		}
		if (PlayerMovement.endedTurn && drewCard) {
			drewCard = false;
		}
		GameObject.Find ("cardsLeft").GetComponent<Text> ().text = deckSize.ToString ();
	}

	public void Draw(int c) {
		for (int i = 0; i < c; i++) {
			if (cards [counter] == 0) {
				card = Instantiate (Jump, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 1) {
				card = Instantiate (Forward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 2) {
				card = Instantiate (Backward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 3) {
				card = Instantiate (JumpBackward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 4) {
				card = Instantiate (JumpForward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			}
			card.transform.SetParent (GameObject.Find ("PlayerHand").transform);
			counter++;
		}
		givenCard = true;
	}

	public void FillHand() {
		for (int i=5; i>handCount; i--) {
			if (cards [counter] == 0) {
				card = Instantiate (Jump, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 1) {
				card = Instantiate (Forward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 2) {
				card = Instantiate (Backward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 3) {
				card = Instantiate (JumpBackward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			} else if (cards [counter] == 4) {
				card = Instantiate (JumpForward, new Vector3 (0, 0, 0), Quaternion.identity) as GameObject;
			}
			card.transform.SetParent(GameObject.Find ("PlayerHand").transform);
			counter++;
			deckSize--;
			//handCount++;
		}
	}

	public int HandCount() {
		int totalCards = 0;
		totalCards += dropZone.transform.childCount;
		totalCards += handZone.transform.childCount;
		cardsInHand = totalCards;
		Debug.Log (cardsInHand);
			//GameObject.Find ("PlayerHand").transform.childCount;
		return cardsInHand;
	}

	public IEnumerable<int> GetCards()
	{
		foreach (int i in cards) {
			yield return i;
		}
	}

	public void Shuffle() {
		if (cards == null) {
			cards = new List<int> ();
		} else {
			cards.Clear ();
		}
		for (int i = 0; i < deckSize; i++) {
			int randy = randyValues [Random.Range (0, randyValues.Length)];
			cards.Add (randy);
		}
		int count = cards.Count;
		while (count > 1) {
			count--;
			int rand = Random.Range (0, count + 1);
			int temp = cards[rand];
			cards [rand] = cards [count];
			cards [count] = temp;
		}
	}
}
