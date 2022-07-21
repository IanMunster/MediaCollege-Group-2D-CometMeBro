using UnityEngine;
using System.Collections;

public class Backfollow : MonoBehaviour {
	//The parralax speed
    private float parralax = 2f;

	// Update is called once per frame
	void Update () {
		//Get the Meshrenderer and material of Background
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
		//Get the offset of the material
        Vector2 offset = mat.mainTextureOffset;
		//Make the backgrounds offset to the localscale, divided by its position
        offset.y = transform.position.y / transform.localScale.y / parralax;
        offset.x = transform.position.x / transform.localScale.x / parralax;
        mat.mainTextureOffset = offset;
    }
}
