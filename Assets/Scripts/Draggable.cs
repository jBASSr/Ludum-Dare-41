using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public Transform cardParent = null;
	public GameObject placeholder;
	public Transform placeholderParent;
	public int origIndex;
	//public List<string> moveQ = new List<string>();
	//public enum ActionType { MOVEMENT, ATTACK, ACTION };
	//public ActionType cardType = ActionType.MOVEMENT;
	void Awake() {
		//cardName = GetComponent<CardContent>().card.moves.ToString();
	}

	public void OnBeginDrag(PointerEventData pointerEvent) {
		//if (!PlayerMovement.endedTurn) {
			// Invisible spacer card
			placeholder = new GameObject ();
			placeholder.transform.SetParent (transform.parent);
			LayoutElement layout = placeholder.AddComponent<LayoutElement> ();
			layout.preferredHeight = this.GetComponent<LayoutElement> ().preferredHeight;
			layout.preferredWidth = this.GetComponent<LayoutElement> ().preferredWidth;
			layout.flexibleHeight = 0;
			layout.flexibleWidth = 0;
			placeholder.transform.SetSiblingIndex (this.transform.GetSiblingIndex());
			origIndex = placeholder.transform.GetSiblingIndex ();
			// Hold 
			cardParent = transform.parent;
			placeholderParent = cardParent;
			transform.SetParent (transform.parent.parent);
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
			//PlayArea[] slots = GameObject.FindObjectsOfType<PlayArea> ();
		//}
	}
	public void OnDrag(PointerEventData pointerEvent) {
		//Debug.Log (pointerEvent.position);
		//if (!PlayerMovement.endedTurn) {
			transform.position = Vector3.MoveTowards (transform.position, pointerEvent.position, 50.0f);
		
			//
			if (placeholder.transform.parent != placeholderParent) {
				placeholder.transform.SetParent (placeholderParent);
			}
			//
			int newSiblingIdx = placeholderParent.childCount;
			// INC through all cards in hand
			for (int i = 0; i < placeholderParent.childCount; i++) {
				// If my card is left of a card, update it's new sibling.
				if (this.transform.position.x < placeholderParent.GetChild (i).position.x) {
					newSiblingIdx = i;
					if (placeholder.transform.GetSiblingIndex () < newSiblingIdx) {
						newSiblingIdx--;
						break;
					}
				}
				placeholder.transform.SetSiblingIndex (newSiblingIdx);
			}
		//}
	}
	public void OnEndDrag(PointerEventData pointerEvent) {
		//if (!PlayerMovement.endedTurn) {
			//transform.parent = cardParent;
			transform.SetParent (cardParent);
			transform.SetSiblingIndex (placeholder.transform.GetSiblingIndex ());
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			Destroy (placeholder);
			// Now the fun part

			//
			// What am I on top of?
			//EventSystem.current.RaycastAll(pointerEvent)
		//}
	}
}
