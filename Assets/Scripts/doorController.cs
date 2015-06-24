using UnityEngine;
using System.Collections;

public class doorController : MonoBehaviour {

	public float speed = 5.0f;
	public float gravity = 20.0f;

	private Vector3 moveDirection = Vector3.zero;
	private GameObject target;
	private Transform myTarget;
	private Transform myPosition;

	void Start()
	{
		target = GameObject.FindWithTag("Player");
	}
	// Update is called once per frame
	void Update () 
	{
		CharacterController controller = GetComponent<CharacterController> ();

		Vector3 relativePos = target.transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		transform.rotation = rotation;

		if ((Vector3.Distance (transform.position, target.transform.position) > 5))
		{
			transform.Translate(Vector3.forward);
		}
		else
		{
			//speed = 0;
			moveDirection = Vector3.zero;

		}

		//moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
	}
}
