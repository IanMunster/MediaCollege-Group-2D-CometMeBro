using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float playerSpeed = 10f;
	private Rigidbody2D _rigid;
	private float moveX;
	private float moveY;

	// Use this for initialization
	void Start () {
		_rigid = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis ("Vertical");

		_rigid.velocity = new Vector2 (moveX * playerSpeed, moveY * playerSpeed);
	}
		
}
