using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerNum;
	private Rigidbody2D rb;
	public float speed;
	public GameObject wordGenerator;

	public string verb;
	public string adjective;
	public string noun;

	private TextMesh tm;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		wordGenerator = GameObject.FindGameObjectWithTag ("WordGenerator");
		InitializeText ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}

	void InitializeText(){
		tm = GetComponent<TextMesh> ();
		verb = "[]";
		adjective = "[]";
		noun = "[]";
		UpdateText ();
	}

	void UpdateText(){
		tm.text = verb + adjective + noun;
	}
	void Move(){
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal_P" + playerNum), Input.GetAxis ("Vertical_P" + playerNum));
		rb.velocity = speed * direction;
	}

	void OnTriggerEnter2D(Collider2D collider){
		GameObject collidedObject = collider.gameObject;
		if (collidedObject.tag == "Word") {
			WordController wc = collidedObject.GetComponent<WordController> ();
			if (wc.partOfSpeech == "verb") {
				verb = wc.text [0].ToString();
			}
			else if (wc.partOfSpeech == "adjective") {
				adjective = wc.text [0].ToString();
			}
			else if (wc.partOfSpeech == "noun") {
				noun = wc.text [0].ToString();
			}
			UpdateText ();

			wordGenerator.GetComponent<WordGenerator>().ReplaceWord (collidedObject);
		}
	}
}
