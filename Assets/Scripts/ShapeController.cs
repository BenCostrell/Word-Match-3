using UnityEngine;
using System.Collections;

public class ShapeController : MonoBehaviour {

	public string shape;
	public string color;
	public ShapeManager shapeManager;

	// Use this for initialization
	void Awake () {
		shapeManager = GameObject.FindGameObjectWithTag ("ShapeManager").GetComponent<ShapeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitializeProperties(string newShape, string newColor){
		shape = newShape;
		color = newColor;
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.sprite = shapeManager.GetSpriteByName (newShape);
		sr.color = shapeManager.GetColorByName (newColor);
	}
}
