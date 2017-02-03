using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int playerNum;
	private Rigidbody2D rb;
	public float speed;
	public WordGenerator wordGenerator;
	public ShapeManager shapeManager;

	public string verb;
	public string adjective;
	public string noun;

	private TextMesh tm;

	public int score;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		wordGenerator = GameObject.FindGameObjectWithTag ("WordGenerator").GetComponent<WordGenerator>();
		shapeManager = GameObject.FindGameObjectWithTag ("ShapeManager").GetComponent<ShapeManager>();
		InitializeText ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}

	void InitializeText(){
		tm = GetComponent<TextMesh> ();
		ResetText ();
	}

	void ResetText(){
		verb = "[]";
		adjective = "[]";
		noun = "[]";
		UpdateText ();
	}

	void UpdateText(){
		tm.text = ProcessText (verb) + ProcessText (adjective) + ProcessText (noun);
		if (tm.text.Length == 3) {
			if (verb == "MAKE") {
				shapeManager.CreateShape (noun, adjective, gameObject);
				shapeManager.CheckForMatches (gameObject);
			} else if (verb == "DESTROY") {
				GameObject shapeToDestroy = shapeManager.GetObjectInGrid (noun, adjective);
				if (shapeToDestroy != null) {
					shapeToDestroy.GetComponent<ShapeController> ().DestroyAndScore (gameObject);
					shapeManager.UpdateGrid ();
					shapeManager.CheckForMatches (gameObject);
				}
			}

			ResetText ();
		}
	}

	string ProcessText(string text){
		string textToReturn;
		if (text == "[]") {
			textToReturn = text;
		} else {
			textToReturn = text [0].ToString();
		}
		return textToReturn;
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
				verb = wc.text;
			}
			else if (wc.partOfSpeech == "adjective") {
				adjective = wc.text;
			}
			else if (wc.partOfSpeech == "noun") {
				noun = wc.text;
			}
			UpdateText ();

			wordGenerator.ReplaceWord (collidedObject);
		}
	}
}
