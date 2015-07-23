using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// The Terrain.SampleHeight YAdjust LateUpdate function is being used by almost every controller
	// Creating a more globally accessable function here would be ideal

	public int[] KarmaLevel;
	//public Canvas MainCanvas;
	//public Camera MainCamera;
	public bool oldManTalk = false;

	private float startTime;
	private float playTimer;

	private int karma = 0;

	// Use this for initialization
	void Start (){
		startTime = Time.time;
	}

	void FixedUpdate()
	{
		playTimer = Time.timeSinceLevelLoad;


		//MainCamera.rect.x = MainCanvas.transform.position.x;
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
}
