using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OldManController : MonoBehaviour {

	public float AdjustY;
	public float keepAway = 5;
	public TextAsset DialogueFile;
	public float timeToWait;
	public float walkSpeed;
	public float approachSpeed;

	private GameController game;
	private Transform player;
	private Camera camera;
	private string[] Dialogue;
	private int dialogueLength;
	private Text text;
	private bool isTalking = false;
	private bool gameStart = true;

	// Use this for initialization
	void Start () {
		
		// OldMan should Vector3.Lerp in to view //
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		game = gameControllerObject.GetComponent<GameController>();

		GameObject playerObject = GameObject.FindWithTag ("Player");
		player = playerObject.GetComponent<Transform>();

		GameObject cameraObject = GameObject.FindWithTag("MainCamera");
		camera = cameraObject.GetComponent<Camera>();

		GameObject textController = GameObject.FindWithTag("UI");
		text = textController.GetComponent<Text>();

		Dialogue = DialogueFile.text.Split ('\n');
		dialogueLength = Dialogue.Length;
	}

	void Update(){

		if(gameStart)
		{
			Vector3 StopHere = new Vector3(player.position.x + keepAway, player.position.y,player.position.z);
			transform.position = Vector3.Lerp(transform.position, StopHere, Time.deltaTime * approachSpeed);
		}

		if(game.oldManTalk)
		{
			gameStart = false;
			transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * walkSpeed);
		}
	}
	// Update is called once per frame
	void LateUpdate () {
		Vector3 pos = transform.position;
		pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustY);
		transform.position = pos;

		if(Vector3.Distance(pos,player.position) > camera.farClipPlane)
			Destroy (this);
	}

	void OnMouseEnter(){
		//for(int i = 0; i < dialogueLength; i++)
		//{
		if(!game.oldManTalk && !isTalking)
		{
			isTalking = true;
			StartCoroutine(Speek());
		}
			//text.text = Dialogue[i];
			//YieldInstruction WaitForSeconds(timeToWait);
		//}
	}

	IEnumerator Speek()
	{
		//while (true) {
		for(int i = 0; i < dialogueLength; i++)
		{
			//Debug.Log ("Green");
			text.text = Dialogue[i];
			yield return new WaitForSeconds(timeToWait);
		}
		text.text = ("");
		game.oldManTalk = true;
	}
}
