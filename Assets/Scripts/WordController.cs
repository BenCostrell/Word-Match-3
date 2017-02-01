using UnityEngine;
using System.Collections;

public class WordController : MonoBehaviour {

	public string text;
	public string partOfSpeech;
	public float characterLength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetText(string textToSet, string pofSpeech){
		text = textToSet;
		partOfSpeech = pofSpeech;
		GetComponent<TextMesh> ().text = textToSet;
		BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
		boxCol.size = new Vector2 (characterLength * text.Length, boxCol.size.y);
	}
}
