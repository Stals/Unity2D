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
    
    [SerializeField]
    PhysicsMaterial2D linePhysicsMaterial;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        updateConnection();
    }

	void updateConnection()
	{
        //clearDisplayedLine();
        
		// or line setactive - 
        if (pin_1.isSelected() && pin_2.isSelected())
        {
            if(line != null){
                
                line.points3[0] = pin_1.transform.localPosition;
                line.points3[1] = pin_2.transform.localPosition;
                
                line.Draw3D();
            }else{
                // create new line
                Vector3[] points = new Vector3[2];
                points[0] = pin_1.transform.localPosition;
                points[1] = pin_2.transform.localPosition;
    
    
                line = new VectorLine("LineRenderer", points, lineMaterial, 30.0f, Vectrosity.LineType.Continuous, Joins.Weld);
                line.collider = true;
                line.Draw3D();
                
                line.physicsMaterial = linePhysicsMaterial;
                Rigidbody2D r = line.vectorObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
                r.gravityScale = 0f;
                r.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                r.isKinematic = true;
                r.mass = 0;
            }
            
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
