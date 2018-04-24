using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoomaController : MonoBehaviour {
	public LayerMask whatIsGround;
	public Transform groundCheckL;
	public Transform groundCheckR;
	public int moveSpeed;
	public float groundRadius = 0.005f;
	public bool movingRight;
	public bool endRight;
	public bool endLeft;
	public Vector3 MoveDir = Vector3.forward;
	// Use this for initialization
	void Start () {
		movingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
		endRight = Physics2D.OverlapCircle (groundCheckR.position, groundRadius, whatIsGround);
		endLeft = Physics2D.OverlapCircle (groundCheckL.position, groundRadius, whatIsGround);
		if (movingRight && !endRight) {
			MoveDir = new Vector3 (-1, 0, 0);
			movingRight = false;
		} else if (!movingRight && !endLeft) {
			MoveDir = new Vector3 (1, 0, 0);
			movingRight = true;
		}
		transform.Translate (MoveDir * Time.deltaTime * moveSpeed * .5f);
	}
}
