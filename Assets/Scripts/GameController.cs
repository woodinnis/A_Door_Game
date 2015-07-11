using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// The Terrain.SampleHeight YAdjust LateUpdate function is being used by almost every controller
	// Creating a more globally accessable function here would be ideal

	public Transform[] pickup;
	public int[] KarmaLevel;
	public int PickupMin = 6;
	public int PickupMax = 10;
	public int totalPickups = 0;

	public Transform player;
	public Transform door;
	public Terrain world;

	private int karma = 0;

	private int pickupCount = 0;
	private int pickupIndex = 0;
	private float spawnDistance;

	private CharacterController characterController;
	private Camera gameCamera;

	private float pX,pZ,pY;

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Second;

		// Randomly generate a player spawn location
		pX = Random.Range (world.transform.position.x, world.terrainData.size.x);
		pZ = Random.Range (world.transform.position.z, world.terrainData.size.z);
		pY = world.terrainData.GetHeight ((int)pX, (int)pZ);

		// Spawn the player
		Vector3 pStart = new Vector3 (pX, pY + 1, pZ);
		Vector3 dStart = new Vector3 (0, 1, 0);
		Instantiate(player,pStart,Quaternion.identity);
		Instantiate (door, dStart, Quaternion.identity);

		// Assign Character component
		GameObject characterControllerObject = GameObject.FindWithTag ("Player");
		characterController = characterControllerObject.GetComponent<CharacterController> ();

		// Assign Camera Component
		GameObject gameCameraObject = GameObject.FindWithTag ("MainCamera");
		gameCamera = gameCameraObject.GetComponent<Camera> ();

		// Set number of total pickups
		pickupCount = Random.Range(PickupMin,PickupMax);
		spawnDistance = gameCamera.farClipPlane;

		// Spawn pickups
		for(int i = 0; i < pickupCount; i++)
		{
			AddPickups(characterController.transform);
		}
	}

	void Update()
	{
		// Check for missing pickups and respawn
		if(totalPickups < pickupCount)
		{
			AddPickups(characterController.transform);
		}
	}
	// Loads a final room according to current Karma level
	public void doorEnter()
	{
		if (karma < KarmaLevel [1])
			Application.LoadLevel ("_Room1");
		else if (karma >= KarmaLevel [1] && karma < KarmaLevel [2])
			Application.LoadLevel ("_Room2");
		else if (karma >= KarmaLevel [2] && karma < KarmaLevel [3])
			Application.LoadLevel ("_Room3");
		else if (karma >= KarmaLevel [3])
			Application.LoadLevel ("_Room4");
	}

	// Adjust karma level
	public void AddKarma(int newKarma)
	{
		karma += newKarma;
		if (karma < KarmaLevel[0])
			karma = KarmaLevel[0];

		print (karma);
	}

	public int GetKarma()
	{
		return karma;
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
