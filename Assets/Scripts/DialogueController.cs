using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueController : MonoBehaviour {

	public TextAsset[] DialogueFile;

	private string[][] Dialogue;
	private Text text;
	private int DialogueField;
	private int DialogueLength;

	// Use this for initialization
	void Start () {
		// Assign Text field
		GameObject textController = GameObject.FindWithTag("UI");
		text = textController.GetComponent<Text>();

		TextAsset TempFile;
		// Retrieve Dialogue text from files
		//DialogueField = Dialogue.Length;

		for(int i = 0; i < Dialogue.Length; i++)
		{
			//TempFile = DialogueFile[i];
			//string[] tempString = TempFile.text.Split('\n');
			//Dialogue[i] = tempString[i];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SayRandom(int i){
		DialogueLength = Dialogue.Length;
		string[] speech = Dialogue[i];
		//print(speech);
		text.text = speech [Random.Range (0, DialogueLength)];
	}
}
