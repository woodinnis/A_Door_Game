using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 4.0f;
	public float gravity = 20.0f;

	public int karma = 0;

	private Vector3 rotation = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;


	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController> ();

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

		// If the player clicks on the door
		if(Input.GetMouseButtonDown(0))
		{
			doorEnter();
		}
	}

	// Deactivate any elements tagged as pickups when they are collided with
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pickup")) 
		{
			other.gameObject.SetActive(false);
			karma++;
		}
	}

	// Resets the player position to zero and loads a new level
	void doorEnter()
	{
		transform.position = Vector3.zero;
		if(karma < 1)
			Application.LoadLevel ("_Room1");
		else
			Application.LoadLevel ("_Room2");
	}
}
