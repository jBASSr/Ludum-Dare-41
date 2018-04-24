using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Card", menuName = "Card")]
public class CardObject : ScriptableObject {
	// Visible card elements
	public new string name;
	public string desc;
	public Sprite artwork;
	// Values
	public int cost;
	public string moves;
}
