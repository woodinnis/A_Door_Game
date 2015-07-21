using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed = 6.0f;
	public float jumpSpeed = 4.0f;
	public float gravity = 20.0f;
	public float AdjustSpawnY;
	public Terrain world;

	private GameController game;
	private CharacterController controller;
	private GameObject oldMan;

	private Vector3 rotation = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;
	private GameObject door;

	private float pX,pZ,pY;
	private Vector3 pStart;
	private float stop = 0.0f;
	private float go;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);

		// Generate random world coordinates and et the player start position
		pX = Random.Range (transform.position.x, world.terrainData.size.x);
		pZ = Random.Range (transform.position.z, world.terrainData.size.z);
		pY = world.terrainData.GetHeight ((int)pX, (int)pZ);

		pStart = new Vector3 (pX, pY + 1, pZ);

		go = speed;
	}

	void OnLevelWasLoaded(int Level)
	{
		// Reset Player position when loading a final room
		if (Level > 0)
			transform.position = Vector3.zero;
	}

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		game = gameControllerObject.GetComponent<GameController>();

		controller = GetComponent<CharacterController> ();
		
		// Move the player to start
		//transform.position = pStart;
	}

	// Update is called once per frame
	void Update () {

		if (!game.oldManTalk)
			speed = stop;
		else
			speed = go;

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

	// Ensures player remains at or above the terrain height at the terrain X,Y position
	void LateUpdate(){
		if(!(Application.loadedLevel > 0))
		{
			Vector3 pos = transform.position;
			pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustSpawnY);
			transform.position = pos;
		}
	}
}