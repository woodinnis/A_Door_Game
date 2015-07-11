using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCController : MonoBehaviour {

	public int SeeMe = 0;
	public int MeetMe = 0;
	public Color[] NPCColor;
	public TextAsset GoodDFile;
	public TextAsset BadDFile;
	public float AdjustSpawnY;
	public float speed = 3.5f;

	private GameController gameController;
	private Collider trigger;
	private Renderer rend;
	private Text text;

	private bool isHere = false;
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

	void Update()
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();

		if (!isHere)
		{
			Vector3 goHere = new Vector3(0,0,0);

			while(Vector3.Distance(agent.destination > 10f))
				 goHere = GetMoveLocation();

			agent.destination = goHere;
			agent.speed = speed;
			isHere = true;
		}
		else if(transform.position == agent.destination)
		{
			isHere = false;
		}
	}
	// Ensures NPCs remain at or above the terrain height at the terrain X,Y position
	void LateUpdate() {
		Vector3 pos = transform.position;
		pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustSpawnY);
		transform.position = pos;
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
	Vector3 GetMoveLocation()
	{
		Terrain world = Terrain.activeTerrain;
		float pX = Random.Range (transform.position.x, world.terrainData.size.x);
		float pZ = Random.Range (transform.position.z, world.terrainData.size.z);
		Vector3 dest = new Vector3 (pX, 0, pZ);
		return dest;
	}
}