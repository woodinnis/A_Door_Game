using UnityEngine;
using System.Collections;

public class OldManController : MonoBehaviour {

	public float AdjustY;
	private GameController game;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		game = gameControllerObject.GetComponent<GameController>();

	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 pos = transform.position;
		pos.y = (Terrain.activeTerrain.SampleHeight(transform.position) + AdjustY);
		transform.position = pos;
	}

	void OnMouseEnter(){
		print ("HI SONNY!");
	}
	void OnMouseExit(){
		game.oldManTalk = true;	
	}
}
