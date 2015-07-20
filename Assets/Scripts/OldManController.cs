using UnityEngine;
using System.Collections;

public class OldManController : MonoBehaviour {

	public float AdjustY;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 pos = transform.position;
		pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustY);
		transform.position = pos;
	}
}
