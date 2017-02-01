using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordGenerator : MonoBehaviour {

	public List<string> verbs;
	public List<string> adjectives;
	public List<string> nouns;

	public GameObject wordPrefab;
	public float minAcceptableDistance;
	public int numEachPos;

	private List<GameObject> wordList;

	// Use this for initialization
	void Start () {
		GenerateWords ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateWords(){
		wordList = new List<GameObject> ();
		for (int i = 0; i < numEachPos; i++) {
			GenerateWord (verbs);
			GenerateWord (adjectives);
			GenerateWord (nouns);
		}
	}

	public void GenerateWord(List<string> poSpeechArray){
		int wordIndex = Random.Range (1, poSpeechArray.Count);
		string wordText = poSpeechArray [wordIndex];
		Vector3 location = GenerateRandomLocation ();
		GameObject word = Instantiate (wordPrefab, location, Quaternion.identity) as GameObject;
		word.GetComponent<WordController> ().SetText (wordText, poSpeechArray [0]);
		wordList.Add (word);
	}

	Vector3 GenerateRandomLocation(){
		Vector3 location = new Vector3 (Random.Range (-3f, 3f), Random.Range (-4.5f, 4.5f), 0);
		bool accept = ValidateLocation (location);
		while (!accept) {
			location = new Vector3 (Random.Range (-3f, 3f), Random.Range (-4.5f, 4.5f), 0);
			accept = ValidateLocation (location);
		}
		return location;
	}

	bool ValidateLocation(Vector3 location){
		bool accept = true;
		if (wordList.Count > 0) {
			foreach (GameObject word in wordList) {
				if (Vector3.Distance (location, word.transform.position) < minAcceptableDistance) {
					accept = false;
					break;
				} 
			}
		}
		return accept;
	}

}
