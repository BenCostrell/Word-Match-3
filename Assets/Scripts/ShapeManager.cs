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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitializeShapeGrid(){
		shapeGrid = new GameObject[5, 5];
	}

	public Sprite GetSpriteByName(string spriteName){
		Sprite spriteToReturn;
		if (spriteName == "circle") {
			spriteToReturn = circleSprite;
		}
		else if (spriteName == "square") {
			spriteToReturn = squareSprite;
		}
		else {
			spriteToReturn = triangleSprite;
		}
		return spriteToReturn;
	}

	public Color GetColorByName(string colorString){
		Color colorToReturn;
		if (colorString == "red") {
			colorToReturn = Color.red;
		} else if (colorString == "green") {
			colorToReturn = Color.green;
		} else {
			colorToReturn = Color.blue;
		}
		return colorToReturn;
	}

	public void CreateShape(string newShape, Color newColor){
		GameObject newShapeObject = Instantiate (shapePrefab, nextSpawnLocation, Quaternion.identity) as GameObject;
		newShapeObject.GetComponent<ShapeController> ().InitializeProperties (newShape, newColor);
	}
}
