using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	//speed of the star
	[SerializeField] private float speed; 
	//size of the star
	[SerializeField] private Vector2 size;
	//position of the star
	private Vector2 starPosition;

	// Use this for initialization
	void Awake () {
		//Give the stars random positions
		starPosition.x = Random.Range (1, 100);
		starPosition.y = Random.Range (1, 100);
		//Get stars from star generator
		GetaStar ();
	}
	// Update is called once per frame
	void FixedUpdate ()
    {
		//bottom left of screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		// top right point of screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        // Get the current position of the star
		Vector2 currPos = transform.position;
        // new position of the star
		starPosition = new Vector2(currPos.x, currPos.y + speed * Time.deltaTime);
        // update the position of the star
		transform.position = starPosition;
		//Change the starSize (could be multiplied by random
		transform.localScale = size*Random.Range(0.5f, 2f);
        // when the star goes outside the screen
		if(currPos.y < min.y || currPos.x < min.x || currPos.y > max.y|| currPos.x > max.x)
        {
			//Set the star to inactive
			this.gameObject.SetActive (false);
			//Give the star new random position
			starPosition = new Vector2(Random.Range(min.x, max.x), Random.Range(min.x, max.y));
        }
		//Keep getting stars
		GetaStar ();
    }
	// Set a Star in the Star Pool to active
	public void GetaStar(){
		GameObject star = StarGenerator.current.GetPooledStars();
		//unless it didnt get a star back
		if(star == null){
			return;
		} else{
			//Get a star, and give it position and rotation
			star.transform.position = starPosition;
			star.transform.rotation = this.transform.rotation;
			//Set the star to active
			star.SetActive(true);
		}
	}
}
