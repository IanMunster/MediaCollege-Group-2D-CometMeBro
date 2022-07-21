using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarGenerator : MonoBehaviour {

	//make a static ref to this script
	public static StarGenerator current;
	// refers to the prefab StarA
	[SerializeField] private GameObject starA;
	// maximum number of stars on scene
	[SerializeField] private int maxStars = 10;
	//the pool list of all the pooled stars
	private List<GameObject> pooledStars;

	void Awake(){
		//set ref. to this
		current = this;
	}
	// Use this for initialization
	void Start () {
        // botton-left of the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        // top-right of the screen
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        //Make a new Pool List
		pooledStars = new List<GameObject>();
		// loop to create stars
        for(int i= 0; i < maxStars; ++i) {
			//instantiate a new StarA as "star"
            GameObject star = (GameObject)Instantiate(starA);
			// set position of star (random x and y)
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
			// make star a child of StarGeneratorX
			star.transform.parent = transform;
			//Set the pooled star to inActive
			star.SetActive (false);
			//Add the star to the Star Pool
			pooledStars.Add (star);
        }
    }
	//Get a pooled star, if its not active
	public GameObject GetPooledStars(){
		for(int i=0; i<pooledStars.Count; i++){
			//If its not active
			if (!pooledStars[i].activeInHierarchy) {
				//give back the inactive gameobject
				return pooledStars[i];
			}
		} 
		//else give nothing back
		return null;
	}
}