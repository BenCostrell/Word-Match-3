using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject wordGenerator;
	public GameObject playerPrefab;
	public GameObject player1;
	public GameObject player2;


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
}
