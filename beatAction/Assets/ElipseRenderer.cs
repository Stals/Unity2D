using UnityEngine;
using System.Collections;
using Vectrosity;



public class ElipseRenderer : MonoBehaviour {

	public Material lineMaterial;
	public float xRadius = 120.0f;
	public float yRadius = 120.0f;
	public int segments = 60;
	public float pointRotation = 0.0f;
	
	VectorLine line;
	
	// Use this for initialization
	void Start () {
		// Make Vector2 array where the size is the number of segments plus one (since the first and last points must be the same)
		Vector2[] linePoints = new Vector2[segments+1];
		
		// Make a VectorLine object using the above points and a material as defined in the inspector, with a width of 3 pixels
		line = new VectorLine( "Line", linePoints, lineMaterial, 3.0f, Vectrosity.LineType.Continuous);

        // TODO remot - adde for testing
        Game.Instance.getBeatTicker().onBeat += onBeat;
        Game.Instance.getBeatTicker().onBigBeat += onBigBeat;
	}
	
	// Update is called once per frame
	void Update () {
		// Create an ellipse in the VectorLine object, where the origin is the center of the screen
		// If xRadius and yRadius are the same, you can use MakeCircleInLine instead, which needs just one radius value instead of two
		line.MakeEllipse( new Vector3(Screen.width/2, Screen.height/2), xRadius, yRadius, segments, pointRotation);
		
		// Draw the line
		line.Draw();

		//float f = Screen.width / 2;
	}

    void onBeat(){
        xRadius += 5;
        yRadius += 5;

        Debug.Log("onBeat");
    }

    void onBigBeat(){
        xRadius += 15;
        yRadius += 15;
        
        Debug.Log("onBigBeat");
    }
}
