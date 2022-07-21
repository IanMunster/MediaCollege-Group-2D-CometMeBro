using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyExplosionSupply : MonoBehaviour {

	//make ref to this
    public static EnemyExplosionSupply getExplosion;
	//The explosion to be pooled
	[SerializeField] private GameObject poolExpl;
	//Maximal amount to be pooled
	[SerializeField] private int pooledMax;
	//Can this explosion pool expand
	[SerializeField] private bool canExpand = true;
	//The explosion pool
	private List<GameObject> pooledExplosions;
	// Use this for initialization
	void Awake () {
		//make ref to this
        getExplosion = this;
		//Make new poolList
		pooledExplosions = new List<GameObject> ();
		//Add the max of explosion to pool and set to inactive
		for (int i = 0; i < pooledMax; i++) {
			GameObject explosion = (GameObject)Instantiate (poolExpl);
			//explosion.transform.parent;
			explosion.SetActive(false);
			pooledExplosions.Add (explosion);
		}
	}

	//Get an Explosion
	public GameObject GetPooledExplosion (){
		//Check if the explosion is not yet used
		for(int i=0; i<pooledExplosions.Count; i++){
			if(!pooledExplosions[i].activeInHierarchy){
           		//Give the explosion
                return pooledExplosions [i];
			}
		}
		//If there is no inactive explosion and this can expand, add new explosion to pool and give it back.
		if(canExpand){
			GameObject explosion = (GameObject)Instantiate (poolExpl);
			pooledExplosions.Add (explosion);
			return explosion;
		}
		//Else give nothing back
		return null;
	}
}
