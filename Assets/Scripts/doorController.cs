using UnityEngine;
using System.Collections;

public class doorController : MonoBehaviour {

	public float speed = 5.0f;
	public float gravity = 20.0f;

	public PlayerController PScript;

	public Color color1;
	public Color color2;

	private Vector3 moveDirection = Vector3.zero;
	private GameObject target;
	private Transform myTarget;
	private Transform myPosition;

	private Renderer rend;

	void Start()
	{
		target = GameObject.FindWithTag ("Player");		// Find the Player
		PScript = target.GetComponent<PlayerController> ();	// Assign the player's controller

		// Assign the renderer and starting color
		rend = GetComponent<Renderer>();
		rend.material.color = color1;
	}
	// Update is called once per frame
	void Update () 
	{
		CharacterController controller = GetComponent<CharacterController> ();

		// Follow the player
		Vector3 relativePos = target.transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;

		if ((Vector3.Distance (transform.position, target.transform.position) > 5)) 
		{
			transform.Translate (Vector3.forward);
		} 
		else 
		{
			moveDirection = Vector3.zero;
		}

		controller.Move (moveDirection * Time.deltaTime);

		// Adjust color based on player progress
		if (PScript.karma > 0) 
		{
			rend.material.color = color2;
		}
	}
}
