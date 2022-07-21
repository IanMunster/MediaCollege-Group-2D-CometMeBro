using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour {

	//Make a public ref to this.
	public static AsteroidSpawner current;
	//GameObject to be pooled
	private GameObject hazard;
	//All the different kind of Hazards
	[SerializeField] private List<GameObject> hazards;
	//The total Hazards Pool
	private List<GameObject> hazardPool;
	//The values at wich the Hazard
	private Vector2 spawnValues;
	//Maximal ammount of Asteroids in the level
	[SerializeField] private int hazardCount;
	//Time-out before the spawning
	[SerializeField] private float spawnWait;
	//Time-out before the next spawn
	[SerializeField] private float startWait;
	//Time-out after spawning
	[SerializeField] private float waveWait;
	//Maximal amount op Pooled Asteroids
	private int maxHazPool = 9;
	//Random int, to randomize type of hazard
	private int aLilRandom;
	// Use this for initialization
	void Awake () {
		//Make a reference to this
		current = this;
		//tweak position of the asteroid spawner
		spawnValues = transform.position;
		//SET THE POOLINGLIST, AND ADD INACTIVE GAMEOBJECTS
		hazardPool = new List<GameObject> ();		
		//Loop through all the asteroid types
		for(int i=0; i<hazards.Count;i++){
			//Get a hazard from list
			hazard = hazards [i];
			//Add all the asteroids to the pool
			for(int p=0; p<maxHazPool; p++){
				//Instatiate the Asteroid
				GameObject obj = (GameObject)Instantiate (hazard);
				//make the hazards child of the Spawner
				obj.transform.parent = transform;
				//Set the pooled hazard to inactive
				obj.SetActive (false);
				//Add the pooled hazard to the hazardslist.
				hazardPool.Add (obj);
			}
		}
		//Start the SpawnWave routine
		StartCoroutine (SpawnWave ());
	}

	//Routine to Spawn the asteroids in waves
	public IEnumerator SpawnWave() {
		//Wait until the startwait is exceeded
		yield return new WaitForSeconds (startWait);
		//Aslong as its true, loop
		while (true) {
			//Loop trough all the different hazards
			for (int i = 0; i < hazardCount; i++) {
				//GIVE THE HAZARD SPAWN POSITION & ROTATION
				Vector2 spawnPosition = new Vector2 (spawnValues.x, Random.Range (spawnValues.y - 20, spawnValues.y + 20));
				Quaternion spawnRotation = this.transform.rotation;
				/*INT P AS IN POOL*/
				aLilRandom = Random.Range (0, hazardPool.Count);
				//Loop through all the Pooled hazards
				for (int p = 0; p < hazardPool.Count; p++) {
					//If a hazard is not active in level
					if (!hazardPool [aLilRandom].activeInHierarchy) {
						//Give the hazard a new position and rotation
						hazardPool [aLilRandom].transform.position = spawnPosition;
						hazardPool [aLilRandom].transform.rotation = spawnRotation;
						//Set the asteroid to active
						hazardPool [aLilRandom].SetActive (true);
						break;
					}
				}
				//WAIT FOR THE SPAWNWAIT SECONDS
				yield return new WaitForSeconds (spawnWait);
			}
			//Wait after the spawnWave is completed
			yield return new WaitForSeconds (waveWait);
		}
	}

}
