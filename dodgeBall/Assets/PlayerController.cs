using UnityEngine;
using System.Collections;

using Vectrosity;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	PinController pin_1;

	[SerializeField]
	PinController pin_2;


    [SerializeField]
    Material lineMaterial;

    VectorLine line = null;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		updateConnection();
	}

	void updateConnection()
	{
		// or line setactive - 
        if (pin_1.isSelected() && pin_2.isSelected())
        {
            clearDisplayedLine();

            Vector3[] points = new Vector3[2];
            points[0] = pin_1.transform.localPosition;
            points[1] = pin_2.transform.localPosition;


            line = new VectorLine("LineRenderer", points, lineMaterial, 7.0f, Vectrosity.LineType.Continuous, Joins.Weld);
            line.collider = true;
            line.Draw3D();
            
        }
        else {
            clearDisplayedLine();
        }
	}


    void clearDisplayedLine()
    {
        if (line != null)
        {
            VectorLine.Destroy(ref line);
            line = null;
        }
    }
}
