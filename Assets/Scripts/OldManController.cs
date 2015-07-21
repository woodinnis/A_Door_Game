using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OldManController : MonoBehaviour {

	public float AdjustY;
	public TextAsset DialogueFile;
	public float timeToWait;

	private GameController game;
	private string[] Dialogue;
	private int dialogueLength;
	private Text text;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		game = gameControllerObject.GetComponent<GameController>();

		GameObject textController = GameObject.FindWithTag("UI");
		text = textController.GetComponent<Text>();

		Dialogue = DialogueFile.text.Split ('\n');
		dialogueLength = Dialogue.Length;
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
			StartCoroutine(Speek());
			//text.text = Dialogue[i];
			//YieldInstruction WaitForSeconds(timeToWait);
		//}
	}
	void OnMouseExit(){
		//game.oldManTalk = true;	
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
	}
}
