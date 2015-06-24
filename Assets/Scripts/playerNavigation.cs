﻿using UnityEngine;
using System.Collections;

public class playerNavigation : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 4.0f;
	public float gravity = 20.0f;

	public float rotation = 5.0f;

	private Vector3 moveDirection = Vector3.zero;

	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController> ();

		moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= speed;

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		rotation += Input.GetAxis ("Mouse X");
		transform.eulerAngles = new Vector3 (0, rotation, 0);
	}
}
