using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OldManController : MonoBehaviour {

	public float AdjustY;
	public TextAsset DialogueFile;
	public float timeToWait;
	public float walkSpeed;

	private GameController game;
	private string[] Dialogue;
	private int dialogueLength;
	private Text text;
	private bool isTalking = false;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		game = gameControllerObject.GetComponent<GameController>();

		GameObject textController = GameObject.FindWithTag("UI");
		text = textController.GetComponent<Text>();

		Dialogue = DialogueFile.text.Split ('\n');
		dialogueLength = Dialogue.Length;
	}

	void Update(){

		if(game.oldManTalk)
		{
			transform.position = Vector3.Lerp(transform.position,Vector3.zero, Time.deltaTime * walkSpeed);
		}
	}
	// Update is called once per frame
	void LateUpdate () {
		Vector3 pos = transform.position;
		pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustY);
		transform.position = pos;
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
