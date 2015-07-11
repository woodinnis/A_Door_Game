﻿using UnityEngine;
using System.Collections;

public class doorController : MonoBehaviour {

	public float speed = 5.0f;
	public float gravity = 20.0f;
	public int distanceFromPlayer = 5;
	public float AdjustSpawnY;

	private GameController gameController;
	private CharacterController controller;
	
	public Color[] color;

	private Vector3 moveDirection = Vector3.zero;
	private GameObject target;
	private Transform myTarget;
	private Transform myPosition;

	private Renderer rend;

	void Start()
	{
		target = GameObject.FindWithTag ("Player");		// Find the Player
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		// Assign controllers
		gameController = gameControllerObject.GetComponent<GameController> ();
		controller = GetComponent<CharacterController> ();

		// Assign the renderer and starting color
		rend = GetComponent<Renderer>();
		rend.material.color = color[0];
	}
	// Update is called once per frame
	void Update () 
	{
		// Follow the player
		Vector3 relativePos = target.transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;

		int karma = gameController.GetKarma ();
		bool doorHasArrived = false;

		// Set the door to arrived and move towards the player when player karma goes above 0
		if (karma > 0 && doorHasArrived == false)
			doorHasArrived = true;

		// Move the door towards the player when door is set to Arrived
		if(doorHasArrived == true)
		{
			if ((Vector3.Distance (transform.position, target.transform.position) > distanceFromPlayer)) 
			{
				transform.Translate (Vector3.forward);
			} 
			else 
			{
				moveDirection = Vector3.zero;
			}
		}
		controller.Move (moveDirection * Time.deltaTime);

		// Adjust door color based on player progress
		ChangeDoorColor ();
	}

	// Ensures The Door remain at or above the terrain height at the terrain X,Y position
	void LateUpdate() {
		Vector3 pos = transform.position;
		pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustSpawnY);
		transform.position = pos;
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButton(0))
		{
			gameController.doorEnter ();
		}
	}

	// Adjust the door color based on the current Karma level
	void ChangeDoorColor()
	{
		int karma = gameController.GetKarma();
		int[] level = gameController.KarmaLevel;

		if (karma < level[1]) 
		{
			rend.material.color = color [0];
		} 
		else if (karma >= level[1] && karma < level[2])
		{
			rend.material.color = color [1];
		}
		else if (karma >= level[2] && karma < level[3])
		{
			rend.material.color = color[2];
		}
		else if (karma >= level[3])
		{
			rend.material.color = color[3];
		}
	}
}