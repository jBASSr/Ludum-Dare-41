using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardContent : MonoBehaviour {

	public CardObject card;
	public Text cardName;
	public Text cardDesc;
	public Text cardCost;
	public Text cardType;
	public Text cardMoves;
	public Image cardArt;

	// Use this for initialization
	void Start () {
		cardName.text = card.name;
		cardDesc.text = card.desc;
		cardCost.text = card.cost.ToString();
		//cardType.text = card.type;
		//cardMoves.text = card.moves.ToString();
		cardArt.sprite = card.artwork;
	}
}