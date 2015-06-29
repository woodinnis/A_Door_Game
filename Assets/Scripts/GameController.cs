using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform[] pickup;
	public int[] KarmaLevel;
	public int spawnDistance;
	public int totalPickups = 0;

	public Transform player;
	public Terrain world;

	private int karma = 0;

	private int pickupCount = 0;
	private int pickupIndex = 0;

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
		Instantiate(player,pStart,Quaternion.identity);

		pickupCount = Random.Range(5,9);

		for(int i = 0; i < pickupCount; i++)
		{
			AddPickups();
		}
	}

	void Update()
	{
		if(totalPickups < pickupCount)
		{
			AddPickups();
		}
	}
	// Loads a final room according to current Karma level
	public void doorEnter()
	{
		if(karma < KarmaLevel[1])
			Application.LoadLevel ("_Room1");
		else if(karma < KarmaLevel[2])
			Application.LoadLevel ("_Room2");
		else if(karma < KarmaLevel[3])
			Application.LoadLevel ("_Room3");
	}

	// Adjust karma level
	public void AddKarma(int newKarma)
	{
		karma += newKarma;
		if (karma < KarmaLevel[0])
			karma = KarmaLevel[0];
	}

	public int GetKarma()
	{
		return karma;
	}

	public void AddPickups()
	{
		float mX = Random.Range (pX - spawnDistance, pX + spawnDistance);
		float mZ = Random.Range (pZ - spawnDistance, pZ + spawnDistance);
		float mY = world.terrainData.GetHeight ((int)mX, (int)mZ);
		
		pickupIndex = Random.Range(0,pickup.Length);
		Transform me = pickup[pickupIndex];
		Transform here = (Transform)Instantiate(me, new Vector3(mX, mY + 1, mZ), Quaternion.identity);
		
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
		
		totalPickups++;
	}
}
