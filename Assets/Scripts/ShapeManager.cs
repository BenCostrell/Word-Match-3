using UnityEngine;
using System.Collections;

public class ShapeManager : MonoBehaviour {

	public Sprite circleSprite;
	public Sprite squareSprite;
	public Sprite triangleSprite;

	public GameObject shapePrefab;

	public Vector3 nextSpawnLocation;

	public GameObject[,] shapeGrid;

	// Use this for initialization
	void Start () {
		InitializeShapeGrid ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitializeShapeGrid(){
		shapeGrid = new GameObject[5, 5];
		nextSpawnLocation = new Vector3 (7, 4);
	}

	public Sprite GetSpriteByName(string spriteName){
		Sprite spriteToReturn;
		if (spriteName == "CIRCLE") {
			spriteToReturn = circleSprite;
		}
		else if (spriteName == "SQUARE") {
			spriteToReturn = squareSprite;
		}
		else {
			spriteToReturn = triangleSprite;
		}
		return spriteToReturn;
	}

	public Color GetColorByName(string colorString){
		Color colorToReturn;
		if (colorString == "RED") {
			colorToReturn = Color.red;
		} else if (colorString == "GREEN") {
			colorToReturn = Color.green;
		} else {
			colorToReturn = Color.blue;
		}
		return colorToReturn;
	}

	public void CreateShape(string newShape, string newColor){
		GameObject newShapeObject = Instantiate (shapePrefab, nextSpawnLocation, Quaternion.identity) as GameObject;
		newShapeObject.GetComponent<ShapeController> ().InitializeProperties (newShape, newColor);

		nextSpawnLocation += new Vector3 (1.6f, 0);
		if (nextSpawnLocation.x > 13.5f) {
			nextSpawnLocation = new Vector3 (7, 4);
		}
	}
}
