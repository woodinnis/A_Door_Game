using UnityEngine;
using System.Collections;

public class coinController : MonoBehaviour {

	// Use this for initialization
	public float speed = 5.0f;
	public int effect = 0;

	void Start()
	{
		//Random.seed = (int)System.DateTime.Now.Second;
		//effect = (int)Random.Range (-1, 1);
	}
	// Update is called once per frame
	void Update () 
	{

		transform.Rotate(speed * Time.deltaTime, 0, 0);
	}
}
