using UnityEngine;
using System.Collections;

public class VectorGridDemoFireObject : MonoBehaviour 
{
	public VectorGrid m_VectorGrid;
	public GameObject m_Object;
	public float m_Force = 1.0f;

	Color m_StartColor = Color.red;
	Color m_TargetColor = Color.blue;

	float m_ColorInterp;
	public float m_Red = 0.0f;
	public float m_Green = 0.0f;
	public float m_Blue = 255.0f;
	
	// Update is called once per frame
	void Update () 
	{	
		if(Input.GetMouseButtonDown(0))
		{
			GameObject newObject = GameObject.Instantiate(m_Object) as GameObject;
			newObject.transform.position = this.transform.position + this.transform.forward;
			newObject.GetComponent<Rigidbody>().AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * m_Force);
			newObject.GetComponent<VectorGridForce>().m_VectorGrid = m_VectorGrid;
			newObject.GetComponent<VectorGridForce>().m_Color = new Color(m_Red, m_Green, m_Blue);
			Destroy(newObject, 5.0f);
		}

		UpdateRandomColor();
	}

	void UpdateRandomColor()
	{
		m_ColorInterp += Time.deltaTime;
		
		if(m_ColorInterp > 1.0f)
		{
			m_ColorInterp -= 1.0f;
			m_StartColor = m_TargetColor;
			m_TargetColor = new Color(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f));
		}
		
		Color interpolatedColor = m_StartColor + ((m_TargetColor - m_StartColor) * m_ColorInterp);
		m_Red = interpolatedColor.r;
		m_Green = interpolatedColor.g;
		m_Blue = interpolatedColor.b;
	}

}
