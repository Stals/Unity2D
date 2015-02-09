using UnityEngine;
using System.Collections;

public class SphereDemoManager : MonoBehaviour 
{
	float m_ExplosiveForce = 1.0f;
	float m_ImplosiveForce = 1.0f;
	float m_ForceRadius = 1.0f;

	float m_Red = 0.0f;
	float m_Green = 0.0f;
	float m_Blue = 255.0f;
	bool m_RandomiseColor = false;

	VectorGrid m_VectorGrid;
	GUIStyle m_BoxStyle = null;

	Color m_StartColor = Color.red;
	Color m_TargetColor = Color.blue;
	float m_ColorInterp;

	void Start()
	{
		Application.targetFrameRate = 60;
		m_VectorGrid = GameObject.Find("VectorGrid").GetComponent<VectorGrid>();
	}

	void OnGUI () 
	{
		if(m_BoxStyle == null)
		{
			m_BoxStyle = new GUIStyle( GUI.skin.box );
			m_BoxStyle.normal.background = MakeTex( 2, 2, new Color( 1.0f, 1.0f, 1.0f, 0.2f ) );
		}
		
		GUI.Box(new Rect(10, 10, 230, 100), "", m_BoxStyle);

		GUI.Label(new Rect (80, 20, 200, 20), "Instructions");
		GUI.Label(new Rect (20, 50, 250, 20), "Left Click - Shoot Object");

		if(GUI.Button(new Rect(20,80,200,20), "Reset")) 
		{
			m_VectorGrid.Reset();
		}
	}

	private Texture2D MakeTex(int width, int height, Color col)
	{
		Color[] pix = new Color[width * height];
		
		for(int i = 0; i < pix.Length; i++)
		{
			pix[i] = col;	
		}
		
		Texture2D result = new Texture2D( width, height );
		result.SetPixels(pix);
		result.Apply();
		return result;
	}
}
