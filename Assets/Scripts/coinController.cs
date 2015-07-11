using UnityEngine;
using System.Collections;

public class coinController : MonoBehaviour {

	// Use this for initialization
	public float speed = 5.0f;
	public int effect = 0;
	public float AdjustSpawnY;

	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		gameController = gameControllerObject.GetComponent<GameController> ();
	}
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(speed * Time.deltaTime, 0, 0);
	}

	void LateUpdate() {
		Vector3 pos = transform.position;
		pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustSpawnY);
		transform.position = pos;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{	
			gameController.AddKarma(effect);

			Destroy(gameObject);
			gameController.totalPickups--;
		}
	}
}
