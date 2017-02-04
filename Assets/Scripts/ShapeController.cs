using UnityEngine;
using System.Collections;

public class ShapeController : MonoBehaviour {

	public string shape;
	public string color;
	public ShapeManager shapeManager;
	public GameManager gameManager;
	public int col;
	public int row;

	private bool singleDestruction;

	// Use this for initialization
	void Awake () {
		shapeManager = GameObject.FindGameObjectWithTag ("ShapeManager").GetComponent<ShapeManager> ();
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ();
		singleDestruction = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitializeProperties(string newShape, string newColor, int colNum, int rowNum){
		shape = newShape;
		color = newColor;
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.sprite = shapeManager.GetSpriteByName (newShape);
		sr.color = shapeManager.GetColorByName (newColor);
		col = colNum;
		row = rowNum;
	}

	public void DestroyAndScore(GameObject player, bool singleDest){
		singleDestruction = singleDest;
		iTween.ScaleFrom (gameObject, iTween.Hash ("scale", 1.5f * Vector3.one, "time", 1f, 
			"oncomplete", "FinishDestroyingShape", "oncompleteparams", player));
	}

	void FinishDestroyingShape(GameObject player){
		shapeManager.tweensRemaining -= 1;
		player.GetComponent<PlayerController> ().score += 1;
		Destroy (gameObject);
		shapeManager.shapeGrid [col, row] = null;
		shapeManager.UpdateGrid ();
		gameManager.UpdateScore ();
		if (singleDestruction) {
			singleDestruction = false;
			shapeManager.CheckForMatches (player);
		}
	}
}
