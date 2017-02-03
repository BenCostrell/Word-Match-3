using UnityEngine;
using System.Collections;

public class ShapeController : MonoBehaviour {

	public string shape;
	public string color;
	public ShapeManager shapeManager;
	public int col;
	public int row;

	// Use this for initialization
	void Awake () {
		shapeManager = GameObject.FindGameObjectWithTag ("ShapeManager").GetComponent<ShapeManager> ();
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

	public void DestroyAndScore(GameObject player){
		player.GetComponent<PlayerController> ().score += 1;
		Destroy (gameObject);
		shapeManager.shapeGrid [col, row] = null;
	}
}
