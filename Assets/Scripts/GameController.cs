using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Transform[] pickup;
	public int[] KarmaLevel;
	
	private int karma = 0;

	private int pickupCount = 0;
	private int pickupIndex = 0;

	// Use this for initialization
	void Start () {
		Random.seed = (int)System.DateTime.Now.Second;

		pickupCount = Random.Range(5,9);
		for(int i = 0; i < pickupCount; i++)
		{
			pickupIndex = Random.Range(0,pickup.Length);
			Transform me = pickup[pickupIndex];
			Transform here = (Transform)Instantiate(me, new Vector3(i * 5f, 0.5f, 10f), Quaternion.identity);

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
}
