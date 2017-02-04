using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject wordGenerator;
	public GameObject playerPrefab;
	public GameObject player1;
	public GameObject player2;
	public GameObject score_P1;
	public GameObject score_P2;
	public GameObject actionNotification;


	// Use this for initialization
	void Awake () {
		InitializePlayers ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Reset")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void InitializePlayers(){
		player1 = Instantiate (playerPrefab, new Vector3 (0, -2, 0), Quaternion.identity) as GameObject;
		player2 = Instantiate (playerPrefab, new Vector3 (0, 2, 0), Quaternion.identity) as GameObject;

		player1.GetComponent<PlayerController> ().playerNum = 1;
		player2.GetComponent<PlayerController> ().playerNum = 2;

		wordGenerator.GetComponent<WordGenerator> ().player1 = player1;
		wordGenerator.GetComponent<WordGenerator> ().player2 = player2;
	}

	public void UpdateScore(){
		score_P1.GetComponent<Text> ().text = player1.GetComponent<PlayerController>().score.ToString();
		score_P2.GetComponent<Text> ().text = player2.GetComponent<PlayerController>().score.ToString();
	}

	public void ActivateActionNotification(int playerNum, string verb, string adjective, string noun, bool destructionAttemptedButDidntOccur){
		string destructionAttempt = "";
		if (destructionAttemptedButDidntOccur) {
			destructionAttempt = " TRIED TO ";
		}
		actionNotification.SetActive (true);
		actionNotification.GetComponent<Text>().text = "PLAYER " + playerNum + " " + destructionAttempt + 
			VerbToPastTense(verb, destructionAttemptedButDidntOccur) + " A " + adjective + " " + noun;
		iTween.ScaleFrom (actionNotification, iTween.Hash ("scale", 2f * Vector3.one, "time", 0.8f, 
			"oncomplete", "DeactivateActionNotification", "oncompletetarget", gameObject));
	}

	void DeactivateActionNotification(){
		actionNotification.SetActive (false);
	}

	string VerbToPastTense(string verb, bool failedAttempt){
		if (verb == "MAKE") {
			return "MADE";
		} else if (!failedAttempt) {
			return verb + "ED";
		} else {
			return verb;
		}
	}
}
