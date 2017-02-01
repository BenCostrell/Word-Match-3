using UnityEngine;
using System.Collections;

public class ShapeController : MonoBehaviour {

	public string shape;
	public Color color;
	public ShapeManager shapeManager;

	// Use this for initialization
	void Awake () {
		shapeManager = GameObject.FindGameObjectWithTag ("ShapeManager").GetComponent<ShapeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void InitializeProperties(string newShape, Color newColor){
		shape = newShape;
		color = newColor;
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		sr.sprite = shapeManager.GetSpriteByName (newShape);
		sr.color = newColor;
	}
}
