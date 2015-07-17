using UnityEngine;
using System.Collections;

public class InstantiationController : MonoBehaviour {

	// This script will be to optimize the GameController by taking all of the instatiate functions out of GameController.cs

	public Transform[] pickup;
	public int PickupMin = 6;
	public int PickupMax = 10;
	public int totalPickups = 0;

	private int pickupCount = 0;
	private int pickupIndex = 0;
	private float spawnDistance;
	
	public Transform door;
	public Terrain world;

	private CharacterController characterController;
	private Camera gameCamera;

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Second;

		// Assign Character component
		GameObject characterControllerObject = GameObject.FindWithTag ("Player");
		characterController = characterControllerObject.GetComponent<CharacterController> ();
		
		// Assign Camera Component
		GameObject gameCameraObject = GameObject.FindWithTag ("MainCamera");
		gameCamera = gameCameraObject.GetComponent<Camera> ();

		// Instatiate the door into the world
		Vector3 dStart = new Vector3 (0, 1, 0);
		Instantiate (door, dStart, Quaternion.identity);

		// Spawn pickups
		for(int i = 0; i < pickupCount; i++)
		{
			AddPickups(characterController.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {

		// Set number of total pickups
		pickupCount = Random.Range(PickupMin,PickupMax);
		spawnDistance = gameCamera.farClipPlane;

		// Check for missing pickups and respawn
		if(totalPickups < pickupCount)
		{
			AddPickups(characterController.transform);
		}
	}

	public void AddPickups(Transform P)
	{
		// Set random coordinates based on player position, and terrain height
		float mX = -1;
		float mZ = -1;
		while(mX < 0 || mX > world.terrainData.size.x)	// Ensure no spawn point is outside the terrain x-axis
			mX = Random.Range (P.position.x - spawnDistance, P.position.x + spawnDistance);
		
		while(mZ < 0 || mZ > world.terrainData.size.z)	// Ensure no spawn point is outside the terrain z-axis
			mZ = Random.Range (P.position.z - spawnDistance, P.position.z + spawnDistance);
		
		float mY = world.terrainData.GetHeight ((int)mX, (int)mZ);
		
		// Choose a random pickup from the available list and place it in the world
		pickupIndex = Random.Range(0,pickup.Length);
		Transform me = pickup[pickupIndex];
		Transform here = (Transform)Instantiate(me, new Vector3(mX, mY + 1, mZ), Quaternion.identity);
		
		// Set starting values according to each pickup
		if(here.CompareTag("Coin"))
		{
			coinController thing = here.GetComponent<coinController>();
			thing.effect = Random.Range(-1,2);
		}
		if(here.CompareTag("NPC"))
		{
			NPCController npc = here.GetComponent<NPCController>();
			npc.SeeMe = Random.Range(-5,5);
			npc.MeetMe = Random.Range(-10,10);
		}
		
		// Increment total pickups
		totalPickups++;
	}
}
