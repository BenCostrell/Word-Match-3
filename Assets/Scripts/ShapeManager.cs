using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShapeManager : MonoBehaviour {

	public Sprite circleSprite;
	public Sprite squareSprite;
	public Sprite triangleSprite;

	public GameObject shapePrefab;

	public Vector3 nextSpawnLocation;
	public int nextGridXValue;

	public GameObject[,] shapeGrid;
	public int[] numObjectsInEachColumn;

	public GameObject dropIndicator;

	// Use this for initialization
	void Start () {
		InitializeShapeGrid ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void InitializeShapeGrid(){
		shapeGrid = new GameObject[5, 5];
		nextSpawnLocation = new Vector3 (20, -13);
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

	public void CreateShape(string newShape, string newColor, GameObject player){
		GameObject newShapeObject = Instantiate (shapePrefab, nextSpawnLocation, Quaternion.identity) as GameObject;
		int colNum = nextGridXValue;
		int rowNum = numObjectsInEachColumn [colNum];

		newShapeObject.GetComponent<ShapeController> ().InitializeProperties (newShape, newColor, colNum, rowNum);
		shapeGrid [colNum, rowNum] = newShapeObject;
		UpdateObjectCount ();

		nextGridXValue = (nextGridXValue + 1) % 5;
		nextSpawnLocation = new Vector3 (20 + nextGridXValue * 5, -13 + numObjectsInEachColumn [nextGridXValue] * 5);
		dropIndicator.transform.position = new Vector3 (nextSpawnLocation.x, dropIndicator.transform.position.y);
	}

	public GameObject GetObjectInGrid(string shape, string color){
		GameObject objectToReturn = null;
		bool foundObject = false;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++){
				if (shapeGrid [i, j] != null) {
					GameObject shapeObject = shapeGrid [i, j];
					if ((shapeObject.GetComponent<ShapeController> ().shape == shape) && 
						(shapeObject.GetComponent<ShapeController> ().color == color)) {
						objectToReturn = shapeObject;
						foundObject = true;
					}
				}
				if (foundObject) {
					break;
				}
			}
			if (foundObject) {
				break;
			}
		}
		return objectToReturn;
	}

	void UpdateObjectCount(){
		for (int i = 0; i < 5; i++) {
			int count = 0;
			for (int j = 0; j < 5; j++) {
				if (shapeGrid [i, j] != null) {
					count += 1;
				}
			}
			numObjectsInEachColumn [i] = count;
		}
	}

	public void CheckForMatches(GameObject player){
		HashSet<GameObject> objectsInMatches = new HashSet<GameObject> ();
		for (int i = 0; i < 5; i++){
			for (int j = 0; j < 3; j++) {
				if ((shapeGrid [i, j] != null) && (shapeGrid [i, j + 1] != null) && (shapeGrid [i, j + 2])) {
					GameObject shape1 = shapeGrid [i, j];
					GameObject shape2 = shapeGrid [i, j + 1];
					GameObject shape3 = shapeGrid [i, j + 2];
					if (IsMatch (shape1, shape2, shape3)) {
						objectsInMatches.Add (shape1);
						objectsInMatches.Add (shape2);
						objectsInMatches.Add (shape3);
					}
				}
			}
		}
		for (int k = 0; k < 3; k++){
			for (int l = 0; l < 5; l++) {
				if ((shapeGrid [k, l] != null) && (shapeGrid [k + 1, l] != null) && (shapeGrid [k + 2, l] != null)) {
					GameObject shape1 = shapeGrid [k, l];
					GameObject shape2 = shapeGrid [k + 1, l];
					GameObject shape3 = shapeGrid [k + 2, l];
					if (IsMatch (shape1, shape2, shape3)) {
						objectsInMatches.Add (shape1);
						objectsInMatches.Add (shape2);
						objectsInMatches.Add (shape3);
					}
				}
			}
		}
		bool areThereMatches = false;
		if (objectsInMatches.Count > 0) {
			areThereMatches = true;
		}
		foreach (GameObject shape in objectsInMatches) {
			shape.GetComponent<ShapeController> ().DestroyAndScore (player);
		}
		UpdateGrid ();
		if (areThereMatches) {
			CheckForMatches (player);
		}
	}

	bool IsMatch(GameObject shape1, GameObject shape2, GameObject shape3){
		ShapeController shapeCont1 = shape1.GetComponent<ShapeController>();
		ShapeController shapeCont2 = shape2.GetComponent<ShapeController>();
		ShapeController shapeCont3 = shape3.GetComponent<ShapeController>();

		if (((shapeCont1.shape == shapeCont2.shape) && (shapeCont2.shape == shapeCont3.shape)) ||
		    ((shapeCont1.color == shapeCont2.color) && (shapeCont2.color == shapeCont3.color))) {
			return true;
		} else {
			return false;
		}
	}

	public void UpdateGrid(){
		UpdateObjectCount ();
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 4; j++) {
				if ((shapeGrid [i, j] == null) && (shapeGrid [i, j + 1] != null)){
					Debug.Log ("shifting");
					GameObject shiftedObject = shapeGrid [i, j + 1];
					shapeGrid [i, j + 1] = null;
					shiftedObject.transform.position = new Vector3 (20 + (i * 5), -13 + (j * 5));
					shiftedObject.GetComponent<ShapeController> ().col = i;
					shiftedObject.GetComponent<ShapeController> ().row = j;
					shapeGrid [i, j] = shiftedObject;
				}
			}
		}
	}

}
