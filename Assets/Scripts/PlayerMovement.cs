using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	//----------------------------------------
	// Player
	private Rigidbody2D rb;
	public float moveSpeed; // DEFAULT: 2.0f
	public float jumpForce; // DEFAULT: idk
    public float goombaForce;
    public int lives;
    public bool forward;
	private Animator anim;
	//private Vector3 position;
	public int moveCount;
	public int speedX;
	public int speedY;
	public bool isGrounded;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float groundRadius = 0.0001f;
	// Board
	public int currentLevel;
	public float gridSize; // DEFAULT: 3.0f
	private enum Orientation { xAxis, yAxis };
	// Game Settings
	public Button endTurnButton;
	public GameObject DropArea;
	public GameObject livesObj;
	//InputStack inputstack;
	public float turnTimer;
	public float actionTime;
	public bool isMoving;
	public bool isJumping;
	public float moveTime;
	public float SingleMoveTime;
	public float WaitTime;
	static public bool endedTurn;
	public int cardsInHand;
	//----------------------------------------
	// Use this for initialization
	void Start () {
		GetLevelNumber.levelNum++;
		//position = transform.position;
		currentLevel = SceneManager.GetActiveScene ().buildIndex;
		rb = GetComponent<Rigidbody2D> ();
		anim = GameObject.Find ("Sprite").GetComponent<Animator> ();
		anim.SetBool ("jumping", false);
		endTurnButton = GameObject.Find ("EndTurnButton").GetComponent<UnityEngine.UI.Button> ();
        endTurnButton.onClick.AddListener(EndTurn);
        DropArea = GameObject.Find ("TCG");
		livesObj = GameObject.Find ("LivesText");
		//inputstack = GetComponent<InputStack> ();
		groundCheck = transform.Find ("GroundCheck").transform;
		isMoving = false;
		isJumping = false;
		endedTurn = false;
        forward = true;
        lives = 3;
	}

	void FixedUpdate() {
		StepMovement();
		if (speedX > 0 || speedX < 0) {
			rb.velocity = new Vector2 (speedX * moveSpeed, rb.velocity.y);
		} else if (!isGrounded) {
			rb.velocity = new Vector2 (rb.velocity.x * 0.988f, rb.velocity.y);
		}
		if (speedX == 0 && speedY == 0 && isGrounded) {
			rb.velocity = new Vector2 (rb.velocity.x * 0.955f, rb.velocity.y);
		}
		Debug.Log (rb.velocity.x);
		if (Mathf.Abs(rb.velocity.x) < 0.05 && isGrounded) {
			rb.velocity = new Vector2 (0f, rb.velocity.y);
		}
		anim.SetFloat ("speed", Mathf.Abs (rb.velocity.x));
		//Debug.Log ("xVel:" + rb.velocity.x);
		if (speedY > 0 && isGrounded && !isJumping) {
			rb.AddForce (new Vector2 (0.0f, jumpForce));
			speedY = 0; // zero it earlier to prevent some crazy stuff when going through one-ways
			anim.SetBool ("jumping", true);
			isJumping = true;
            //FindObjectOfType<AudioManager>().Play("Jump");
            GameObject.Find("Audio").GetComponent<AudioManager>().Play("Jump");
        } else if (Mathf.Abs(rb.velocity.y) <= 0) {
				anim.SetBool ("jumping", false);
				isJumping = false;
		}
		if (rb.position.y < -10) {
            Death();
            //rb.position = new Vector2 (-3, 0);
		}
        if ((speedX > 0.0f && !forward) || (speedX < 0.0f && forward))
        {
            Flip();
        }
		livesObj.GetComponent<Text> ().text = lives.ToString ();
    }
	
	// Update is called once per frame
	void Update () {
		//speedX = Input.GetKey (KeyCode.A) ? -1 : Input.GetKey (KeyCode.D) ? 1 : 0;
		//speedY = Input.GetKey (KeyCode.S) ? -1 : Input.GetKey (KeyCode.W) ? 1 : 0;
		if (InputStack.get.HasKeys () && endedTurn && !isMoving) {
			string key = InputStack.get.GetNextKey ();
			Debug.Log ("Pressing: " + key);
			switch (key) {
			case "w":
				speedY = 1;
				isMoving = true;
				break;
			case "a":
				speedX = -1;
				isMoving = true;
				break;
			case "s":
				speedY = -1;
				isMoving = true;
				break;
			case "d":
				speedX = 1;
				isMoving = true;
				break;
			case "q":
				speedX = -1;
				speedY = 1;
				isMoving = true;
				break;
			case "e":
				speedX = 1;
				speedY = 1;
				isMoving = true;
				break;
			case "space":
				Debug.Log (key);
				break;
			}
		} else if (!InputStack.get.HasKeys() && endedTurn) {
			//Debug.Log ("No more user input");
			turnTimer += Time.deltaTime;
			if (turnTimer >= actionTime) {
				turnTimer = 0;
				endedTurn = !endedTurn;
				InputStack.storedInput = false;
				//DropArea.SetActive (true);
				if (!LevelEnd.endOfLevel)
					DropArea.GetComponent<CanvasGroup>().alpha = 1f;
			}
		}

		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

		/*
		 * D:
		 * 
		// Is the player currently moving?
		// Check if expected position equals actual position
		if (transform.position == position) {
			isMoving = false;
		} else {
			isMoving = true;
			moveCount++;
		}
		// User Input
		if (Input.GetKey (KeyCode.W) && !isMoving) {
			position += Vector3.up;
		}
		if (Input.GetKey (KeyCode.A) && !isMoving) {
			position += Vector3.left;
		}
		if (Input.GetKey (KeyCode.S) && !isMoving) {
			position += Vector3.down;
		}
		if (Input.GetKey (KeyCode.D) && !isMoving) {
			position += Vector3.right;
		}
		// Update player position
		transform.position = Vector3.MoveTowards(transform.position, 
												 position, 
												 Time.deltaTime * moveSpeed);
		*/
	}

	public void StepMovement() {
		if (isMoving) {
			moveTime += Time.deltaTime;
			if (moveTime >= SingleMoveTime) {
				//Debug.Log ("Move time over, onto the next one");
				//isMoving = false;
				//endedTurn = false;
				//moveTime = 0;
				speedX = 0;
				//speedY = 0;
				if (moveTime >= WaitTime) {
					isMoving = false;
					moveTime = 0f;
					speedY = 0;
				}
			}
		}
	}

    public void Flip()
    {
        transform.Rotate(Vector3.up, 180.0f, 0);
        forward = !forward;
    }

	private void EndTurn() {
		if (PlayArea.currentCost <= PlayArea.maxCost) {
			//Debug.Log ("Ended turn");
			//inputstack.endedTurn = true;
			endedTurn = true;
			/*
			int totalCards = 0;
			totalCards += dropZone.transform.childCount;
			totalCards += handZone.transform.childCount;
			cardsInHand = totalCards;
			*/
			GameObject.Find ("PlayCardArea").GetComponent<PlayArea> ().DeletePlayed ();
			//DropArea.SetActive (false);
			DropArea.GetComponent<CanvasGroup> ().alpha = 0f;
		}
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Death();
        } else if (collision.gameObject.tag == "EnemyTop")
        {
            rb.AddForce(new Vector2(0.0f, goombaForce));
        }
    }

    void Death()
    {
        // Reset Player Position to Start
        rb.position = new Vector2(-3, 0);
        rb.velocity = new Vector3(0, 0, 0);
		// Copyrighted Sounds!
		GameObject.Find("Audio").GetComponent<AudioManager>().Play("Death");
        // Reduce life counter
        lives--;
        if (lives < 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("gameover");
    }
}
