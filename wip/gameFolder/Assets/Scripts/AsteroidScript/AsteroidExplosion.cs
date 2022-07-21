using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidExplosion : MonoBehaviour {

	//Set a public refrence to the ExplosionsSupply
	public static AsteroidExplosion getExplosion;
	//Gameobject to be Pooled
	[SerializeField] private GameObject poolExpl;
	//Maximal amount to be pooled
	[SerializeField] private int poolMax;
	//Can this explosion pool expand if needed?
	[SerializeField] private bool canExpand = true;
	//The explosion Gameobject pool
	private List<GameObject> pooledExplosions;

	// Use this for initialization
	void Awake () {
		//Make the static ref to this
		getExplosion = this;
		//Make a new Gameobject pool
		pooledExplosions = new List<GameObject> ();
		//Put a total of the PoolMax in de pool
		for(int i=0; i<poolMax; i++){
			//Temporary GameObject to store the PooledObj
			GameObject ASexplosion = (GameObject)Instantiate (poolExpl);
			//Make this the Asteroids parent
			ASexplosion.transform.parent = transform;
			//Set the Pooled GameObject to inactive
			ASexplosion.SetActive(false);
			//Add the Pooled GameObject to the Pool
			pooledExplosions.Add (ASexplosion);
		}
	}

	//This is to get an Explosion
	public GameObject GetPooledExplosion (){
		//Go through the length of the Pool
		for(int i=0; i<pooledExplosions.Count; i++){
			//If the Explosion is currently not active
			if(!pooledExplosions[i].activeInHierarchy){
				//Return the inactive GameObject
				return pooledExplosions [i];
			}
		}
		//If this pool can Expand
		if(canExpand){
			//Instatiate a new Temp GameObject
			GameObject ASexplosion = (GameObject)Instantiate (poolExpl);
			//Add the TempGameObject to the Pool
			pooledExplosions.Add (ASexplosion);
			//Return the new Explosion
			return ASexplosion;
		}
		//Else if it cant expand and there is no explosion In-active, return nothin
		return null;
	}
}