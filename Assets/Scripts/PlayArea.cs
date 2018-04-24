using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	// Probably a smarter way to do this but oh well.
	static public List<string> moveQ = new List<string>();
	int index;
	// Awful practice but running out of time. :(
	static public int currentCost = 0;
	static public int maxCost = 5;
	//public Draggable.ActionType cardType = Draggable.ActionType.MOVEMENT;

	void Update() {
		if (currentCost > maxCost) {
			GameObject.Find ("Warning").GetComponent<CanvasGroup> ().alpha = 1f;
		} else if (currentCost <= maxCost) {
			GameObject.Find ("Warning").GetComponent<CanvasGroup> ().alpha = 0f;
		}
		GameObject.Find ("Cost").GetComponent<Text> ().text = string.Format("{0}/5",currentCost.ToString());
	}

	public void DeletePlayed() {
		int i = 0;
		foreach (Transform child in GameObject.Find("PlayCardArea").transform) {
			Destroy(GameObject.Find("PlayCardArea").GetComponent<Transform>().GetChild(i++).gameObject);
		}
		currentCost = 0;
	}

	public void OnDrop(PointerEventData pointerData) {
		Draggable d = pointerData.pointerDrag.GetComponent<Draggable> ();
		if (d != null) {
			//Debug.Log ("Prev: " + d.cardParent);
			//Debug.Log ("New: " + this.transform);
			Transform prev = d.cardParent;
			d.cardParent = this.transform;
			index = d.placeholder.transform.GetSiblingIndex ();
			//Debug.Log ("index = " + index);
			if (this.transform.name == "PlayCardArea" && this.transform != prev) {
				// ACTION: We dropped a card in the 'to be played' zone.
				//Debug.Log (this.transform.name);
				moveQ.Insert (index, d.GetComponent<CardContent> ().card.moves);
				currentCost += d.GetComponent<CardContent> ().card.cost;
				Debug.Log ("Cost: " + currentCost + "/" + maxCost);
			} else if (this.transform.name == "PlayerHand" && this.transform != prev) {
				// ACTION: We removed a card from the 'to be played' zone and back into the hand.
				//Debug.Log (this.transform.name);
				//Debug.Log ("new index = " + index);
				//Debug.Log ("RemoveAt: " + d.origIndex);
				moveQ.RemoveAt (d.origIndex);
				currentCost -= d.GetComponent<CardContent> ().card.cost;
				Debug.Log ("Cost: " + currentCost + "/" + maxCost);
			} else if (this.transform == prev && this.transform.name == "PlayCardArea") {
				// ACTION: We swapped cards around in the 'to be played' zone.
				Debug.Log ("Swapping: " + d.origIndex + " with " + index);
				Debug.Log ("Removing: moveQ[" + d.origIndex + "] = " + moveQ [d.origIndex]);
				moveQ.RemoveAt (d.origIndex);
				moveQ.Insert(index, d.GetComponent<CardContent> ().card.moves);
				//moveQ.RemoveAt (d.origIndex + 1);
			}
		}
	}
	// Enter PlayArea Panel
	public void OnPointerEnter(PointerEventData pointerData) {
		if (pointerData.pointerDrag == null) {
			return;
		}
		Draggable d = pointerData.pointerDrag.GetComponent<Draggable> ();
		if (d != null) {
			d.placeholderParent = this.transform;
		}
	}
	// Exit PlayArea Panel
	public void OnPointerExit(PointerEventData pointerData) {
		if (pointerData.pointerDrag == null) {
			return;
		}
		Draggable d = pointerData.pointerDrag.GetComponent<Draggable> ();
		if (d != null && d.placeholderParent == this.transform) {
			d.placeholderParent = d.cardParent;
		}
	}
}
