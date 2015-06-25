using UnityEngine;
using System.Collections;

public class coinRotator : MonoBehaviour {

	// Use this for initialization
	public float speed = 5.0f;
	//public float angle = 5.0f;
	
	// Update is called once per frame
	void Update () {
		//Quaternion rotation = Quaternion.Euler(0,angle,0);
		//transform.Rotate (Vector3.up, speed * Time.deltaTime);
		transform.Rotate(speed * Time.deltaTime, 0, 0);
	}
}
