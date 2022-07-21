using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlanetAlert : MonoBehaviour {

	//Get different types of EnemyShips
	[SerializeField] private List<GameObject> enemyShips;
	//Maximal amount to pool
	[SerializeField] private int maxEnemys = 10;
	//Maximal amount in level
	[SerializeField] private int enemyCount = 5;
	//Spawn.x=spawnWait, Spawn.y=StartWait, Spawn.z=WaveWait
	[SerializeField] private Vector3 spawn = new Vector3(2f, 3f, 5f);
	//Make the EnemyShip Pool
	private List<GameObject> enemysPool;
	//Make a single ref to EnemyShip for pool
	private GameObject enemy;
	//Target to spawn enemys at
	private Transform target;
	//The Alert Icon on the planet
	private SpriteRenderer alertIcon;
	//The transformation of Icon
	private Transform alertIconTrans;
	//Is the planet on Alert?
	private bool onAlert;
	//Is the planet already Spawning?
	private bool spawning;
	//Get current scene for type of enemy
	private int crrntScene;
    //audio
    private AudioSource source;
	[SerializeField] private AudioClip alarmSound;
	[SerializeField] private AudioClip enemySpawnSound;

    void Awake() {
		//Get the sound components
        source = GetComponent<AudioSource>();
		//Get the current scene number
		crrntScene = SceneManager.GetActiveScene ().buildIndex;
		//If the currentscene is not a possible scene
		if(crrntScene<0||crrntScene>6){
			//Just set it to 0
			crrntScene = 0;
		}
    }

    // Use this for initialization
    void Start () {
		//SET THE POOLINGLIST, AND ADD INACTIVE GAMEOBJECTS
		enemysPool = new List<GameObject> ();
		for(int i=0; i<enemyShips.Count;i++){
			enemy = enemyShips [i];
			for(int p=0; p<maxEnemys; p++){
				GameObject obj = (GameObject)Instantiate (enemy);
				obj.SetActive (false);
				enemysPool.Add (obj);
			}
		}
		//Get the alert icon
		alertIcon = GetComponentInChildren<SpriteRenderer> ();
		alertIconTrans = alertIcon.transform;
		//Disable the AlertIcon
		alertIcon.enabled = false;
		//Planets transform is the target to spawn at
		target = transform;
		//Planet is not on alert and not spawning
		onAlert = false;
		spawning = false;
	}

	void Update(){
		//Rotate the AlertIcon for animation
		alertIconTrans.Rotate (0,0,45*Time.deltaTime);
	}

	void OnTriggerEnter2D (Collider2D other) {
		//If the player got inside the Planets Border
		if (other.CompareTag("Player")) {
			//The planet is on alert
			onAlert = true;
			//Play the alert sound
            source.PlayOneShot(alarmSound,0.5f);
			//DIALOG for UI
			uiDialogAvatar.current.AddDialogAvatar(crrntScene-1, 4f);
			uiDialogTxt.current.AddDialogTxt ("It seems like we have an intruder!", 5f);
        }
	}

	void OnTriggerStay2D (Collider2D other){
		//If the player stay within the planets border
		if(other.CompareTag("Player") && onAlert && !spawning){
			//Show the alert Icon
			alertIcon.enabled = true;
			//if planet is not yet spawning, start the spawning
			if(!spawning){
                StartCoroutine ("Spawn");
            }
		}
	}

	//Spawning routine
    public IEnumerator Spawn () {
		//The planet is spawning
		spawning = true;
		//Wait for the spawnWait
		yield return new WaitForSeconds (spawn.x);
		//show UI DIALOG
		uiDialogAvatar.current.AddDialogAvatar(crrntScene-1, 3f);
		uiDialogTxt.current.AddDialogTxt ("SHOOT HIM DOWN!", 3f);
		//Keep on looping
        while (true) {         
			//Loop through EnemyTypes
            for (int i = 0; i<enemyCount; i++) {
				//Make the spanwposition equal to the target
				Vector2 spawnPosition = new Vector2 (target.position.x, target.position.y);
				//And the rotation equal to its own
				Quaternion spawnRotation = Quaternion.identity;
				//INT P AS IN POOL
				for(int p=0; p<enemysPool.Count; p++) {
					//Check if enemy is not already in level
					if (!enemysPool[p].activeInHierarchy) {
						//play the spawning sound
                        source.PlayOneShot(enemySpawnSound, 0.8f);
						//Give this enemy the position
                        enemysPool [p].transform.position = spawnPosition;
						enemysPool [p].transform.rotation = spawnRotation;
						//Set the enemy to active
						enemysPool [p].SetActive (true);
						break;
					}
				}
				//Wait for a sec before next spawn
				yield return new WaitForSeconds (spawn.y);
			}
			//Wait after spawn wave
			yield return new WaitForSeconds (spawn.z);
		}
	}

	void OnTriggerExit2D (Collider2D other){
		//if the player left the planets border
		if (other.CompareTag ("Player")) {
			//Stop the alarm sounds
            this.source.Stop();
			//planet is nolonger on alert
            onAlert = false;
			//UI DIALOG
			uiDialogAvatar.current.AddDialogAvatar(crrntScene-1, 3f);
			uiDialogTxt.current.AddDialogTxt ("Yeah you better run!", 3f);
			//Disable the alert Icon
			alertIcon.enabled = false;
			//Stop the spawning
			StopCoroutine ("Spawn");
			if(spawning){
				StopCoroutine ("Spawn");
				spawning = false;
			}
        }
	}
}

