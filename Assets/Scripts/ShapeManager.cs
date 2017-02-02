using UnityEngine;
using System.Collections;

public class ShapeManager : MonoBehaviour {

	public Sprite circleSprite;
	public Sprite squareSprite;
	public Sprite triangleSprite;

	public GameObject shapePrefab;

	public Vector3 nextSpawnLocation;
	public int nextGridXValue;

	public GameObject[,] shapeGrid;
	public int[] numObjectsInEachColumn;

	// Use this for initialization
	void Start () {
		InitializeShapeGrid ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitializeShapeGrid(){
		shapeGrid = new GameObject[5, 6];
		nextSpawnLocation = new Vector3 (7, 4);
		nextGridXValue = 0;
		numObjectsInEachColumn = new int[5] { 0, 0, 0, 0, 0 };
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

		shapeGrid [nextGridXValue, numObjectsInEachColumn[nextGridXValue]] = newShapeObject;
		numObjectsInEachColumn [nextGridXValue] += 1;

		nextSpawnLocation += new Vector3 (1.6f, 0);
		nextGridXValue += 1;
		if (nextGridXValue > 4) {
			nextSpawnLocation = new Vector3 (7, 4);
			nextGridXValue = 0;
		}
	}
}
