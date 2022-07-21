using UnityEngine;
using System.Collections;

public class AsteroidHealth : MonoBehaviour {

	//The total amount of health per Asteroid
	[SerializeField] private float asteroidHealth = 100;
	//Total amount of Damage Asteroids recieve per hit
	[SerializeField] private float dmgPerShot = 25;
	//Total amount of Points recieved per Killed asteroids
	[SerializeField] private int pointPerKill = 10;

	//If the asteroid collides
	void OnTriggerEnter2D(Collider2D other){
		//Collision with playerLazers
		if(other.CompareTag("PlayerLazer")){
			//Subtract the amount of DMG from health
			asteroidHealth -= dmgPerShot;
			//Set the playerLazer on Inactive
			other.gameObject.SetActive (false);
			//If the Asteroids has no health left
			if(asteroidHealth == 0){
				//GET A EXPLOSION
				GameObject asteroidExplosion = AsteroidExplosion.getExplosion.GetPooledExplosion();
				//Place the explosion on the asteroids position
				asteroidExplosion.transform.position = transform.position;
				asteroidExplosion.transform.rotation = transform.rotation;
				//Set the Explosion on Active (so it plays it particlesystem)
				asteroidExplosion.SetActive (true);
				//Set the Asteroid on Inactive
				gameObject.SetActive (false);
				//Reset the Asteroids health
				ResetHealth ();
				//Add the amount of score to UserInterface
				uiPlayerScore.current.AddScore (pointPerKill);
			}
		}
	}
	void ResetHealth (){
		//Set health back to 100;
		asteroidHealth = 100;
	}
}
