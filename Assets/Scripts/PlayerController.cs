using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 4.0f;
	public float gravity = 20.0f;
	
	private CharacterController controller;

	private Vector3 rotation = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;
	private GameObject door;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	void OnLevelWasLoaded(int Level)
	{
		// Reset Player position when loading a final room
		if (Level > 0)
			transform.position = Vector3.zero;
	}

	void Start()
	{
		controller = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		 

		// Basic Movement
		float moveHoriz = Input.GetAxis ("Horizontal");
		float moveVert = Input.GetAxis ("Vertical");

		Vector3 move = new Vector3(moveHoriz, 0 , moveVert);

		moveDirection = transform.TransformDirection (move);
		moveDirection *= speed;

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		// Rotate player view to follow the mouse
		rotation.x += Input.GetAxis ("Mouse X");
		rotation.y -= Input.GetAxis ("Mouse Y");
		transform.eulerAngles = new Vector3(rotation.y,rotation.x,0);
	}
}