using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	//public int cardMoves;
	//public Draggable.ActionType cardType = Draggable.ActionType.MOVEMENT;

	public void OnDrop(PointerEventData pointerData) {
		Draggable d = pointerData.pointerDrag.GetComponent<Draggable> ();
		if (d != null) {
			d.cardParent = this.transform;
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
