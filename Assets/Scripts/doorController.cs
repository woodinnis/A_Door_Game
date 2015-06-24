using UnityEngine;
using System.Collections;

public class doorController : MonoBehaviour {

	public float speed = 5.0f;
	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	private GameObject target;

	void Start()
	{
		target = GameObject.FindWithTag("Player");
	}
	// Update is called once per frame
	void Update () {
		CharacterController controller = GetComponent<CharacterController> ();

		// This causes the door to go flying off into infinity
		moveDirection = new Vector3(target.transform.localPosition.x, 0, target.transform.localPosition.z);//Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		moveDirection = transform.TransformDirection (moveDirection);
		moveDirection *= speed;
		
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

	}
}
