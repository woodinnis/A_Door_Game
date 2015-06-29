﻿using UnityEngine;
using System.Collections;

public class PickupDestroy : MonoBehaviour {

	public int buffer;	// Allow some extra range outside the clipping plane

	private CharacterController characterController;
	private GameController gameController;
	private Renderer pickupRenderer;
	private Camera GameCamera;

	// Use this for initialization
	void Start () {
		GameObject GameCameraObject = GameObject.FindWithTag ("MainCamera");
		GameObject CharacterControllerObject = GameObject.FindWithTag ("Player");
		GameObject GameControllerObject = GameObject.FindWithTag ("GameController");

		GameCamera = GameCameraObject.GetComponent<Camera>();
		characterController = CharacterControllerObject.GetComponent<CharacterController> ();
		gameController = GameControllerObject.GetComponent<GameController> ();

		pickupRenderer = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Transform Pplace = characterController.transform;
		if(!pickupRenderer.isVisible)
		{
			// If an object is outside the camera's view, remove it from the world
			if(transform.position.x > Pplace.position.x + (GameCamera.farClipPlane + buffer) ||
			   transform.position.x < Pplace.position.x - (GameCamera.farClipPlane + buffer) ||
			   transform.position.z > Pplace.position.z + (GameCamera.farClipPlane + buffer) ||
			   transform.position.z < Pplace.position.z - (GameCamera.farClipPlane + buffer))
			{
				Destroy(gameObject);
				gameController.totalPickups--;
			}
		}
	}
}