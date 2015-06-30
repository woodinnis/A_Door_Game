using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCController : MonoBehaviour {

	public int SeeMe = 0;
	public int MeetMe = 0;
	public Color[] NPCColor;
	public TextAsset GoodDFile;
	public TextAsset BadDFile;

	private GameController gameController;
	private Collider trigger;
	private Renderer rend;
	private Text text;

	private string[] GoodDialogue;
	private string[] BadDialogue;
	private int GLong;
	private int BLong;

	// Use this for initialization
	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		GameObject textObject = GameObject.FindWithTag ("UI");

		text = textObject.GetComponent<Text> ();
		gameController = gameControllerObject.GetComponent<GameController> ();

		trigger = GetComponent<Collider> ();
		rend = GetComponent<Renderer>();

		if(SeeMe < 0)
			rend.material.color = NPCColor[0];
		else if(SeeMe > 0)
			rend.material.color = NPCColor[1];
		else
			rend.material.color = NPCColor[2];

		GoodDialogue = GoodDFile.text.Split ('\n');
		BadDialogue = BadDFile.text.Split ('\n');
		GLong = GoodDialogue.Length;
		BLong = BadDialogue.Length;
	}

	// This function is giving frequent Object Reference errors.
	// It's possible that it's being triggered before all assets are in place
	void OnTriggerEnter()
	{
		gameController.AddKarma (SeeMe);
		Component.Destroy (trigger);
	}

	void OnMouseExit()
	{
		text.text = ("");
	}

	void OnMouseDown()
	{
		gameController.AddKarma (MeetMe);
		if (MeetMe > 0)
			text.text = GoodDialogue [Random.Range (0, GLong)];
		else
			text.text = BadDialogue [Random.Range (0, BLong)];
	}
	
}