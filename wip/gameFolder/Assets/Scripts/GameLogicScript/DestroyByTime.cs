using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	[SerializeField] private float lifeTime; // = 2

    void OnEnable () {
		//DESTROY AFTER LIFETIME
		Invoke ("Destroy", lifeTime);
	}

	void Destroy () {
		//SET THE OBJECT TO INACTIVE
		gameObject.SetActive (false);
    }

	void OnDisable () {
		//CHECK SO IT DOESNT DESTROY TWICE
		CancelInvoke ();
	}
}
