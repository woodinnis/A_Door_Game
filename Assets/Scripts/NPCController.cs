using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {

	public int SeeMe = 0;
	public int MeetMe = 0;

	private GameController gameController;

	// Use this for initialization
	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		gameController = gameControllerObject.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter()
	{
		gameController.AddKarma (SeeMe);
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButton(0))
		{
			gameController.AddKarma (MeetMe);
		}
	}
	
}
