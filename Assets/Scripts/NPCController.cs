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
	
	// This function is giving frequent Object Reference errors.
	// It's possible that it's being triggered before all assets are in place
	void OnTriggerEnter()
	{
		gameController.AddKarma (SeeMe);
	}

	void OnMouseDown()
	{
		gameController.AddKarma (MeetMe);
	}
	
}
