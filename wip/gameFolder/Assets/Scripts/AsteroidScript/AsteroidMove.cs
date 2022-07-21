using UnityEngine;
using System.Collections;

public class AsteroidMove : MonoBehaviour {

	//Maximal speed
	private float AsteroidSpeed;
	//Tumble/rotationSpeed
	private float tumble;
	//Randomized value for random movement
	private float randomize;
	//RigidBody of the Asteroids
	private Rigidbody2D _rigid;
	//Can this Asteroids Move?
	private bool cannotMove;

	//Use this for initialization
	void Start () {
		//Set the Asteroids speed
		AsteroidSpeed = 10;
		//Rotation tumble value
		tumble = 15;
	}
	void Awake (){
		//there should always be a nxtLvlCheck
		if(NextLevelChecker.current != null){
			//Check if player got in Contact with NXTlvlCheck, stop the Asteroids
			cannotMove = NextLevelChecker.current.AsteroidStopper ();
		}

		//Make a random float every time a asteroid is spawned
		randomize = Random.Range(-2.5f,2.5f);
		//Make reference with the Rigidbody2D component
		_rigid = GetComponentInParent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!cannotMove && NextLevelChecker.current != null){
			//Move the Asteroids
			_rigid.velocity = transform.right * (AsteroidSpeed - randomize);
			//Rotate Asteroids and give angular velocity
			_rigid.angularVelocity = (Random.value * randomize) * (Random.value * tumble);
		}


	}
}
