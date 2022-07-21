using UnityEngine;
using System.Collections;

public class BoundaryWarning : MonoBehaviour {

	//Get the Player in the scene
	[SerializeField] private GameObject player;
	//Get the players lifes
	private PlayerLifes _lifes;
	//Did the player leave the boundary
	private bool playerLeft;

	// Use this for initialization
	void Awake () {
		//Find the player
		player = GameObject.FindWithTag ("Player");
		//Get his lifes
		_lifes = player.GetComponent<PlayerLifes> ();
		//Player is inside the boundary
		playerLeft = false;
	}

	void OnTriggerExit2D (Collider2D other){
		//IF PLAYER GETS TO CLOSE: GET A WARNING;
		if (other.CompareTag ("Player")) {
			//Display a warning in UI
			uiCenterTxt.current.AddCenterTxt ("WARNING! TURN BACK!", 10f);
			//Player left the boundary
			playerLeft = true;
			//Repeatedly check if player is still outside
			StartCoroutine (CheckPlayerReturns());
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		//IF PLAYER RETURNS: REMOVE WARNING;
		if(other.CompareTag("Player")){
			//Remove the UI warning
			uiCenterTxt.current.AddCenterTxt ("",0f);
			//Player is back in boundary
			playerLeft = false;
			//Stop the check
			StopCoroutine (CheckPlayerReturns());
		}
	}

	IEnumerator CheckPlayerReturns(){
		//IF PLAYER CONTINUES: KILL THE PLAYER
		while(playerLeft){
			//Wait for 8sec for player to return
			yield return new WaitForSeconds (8f);
			//If the player didnt return
			if (playerLeft) {
				//kill the player
				_lifes.LoseLife();
			}
		}
		//Stop the check
		StopCoroutine (CheckPlayerReturns());
		yield return null;
	}
}
