using UnityEngine;
using System.Collections;

public class InteractDemoManager : MonoBehaviour 
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
	
	// Update is called once per frame
	void Update () 
	{
		bool modifierPressed = Input.GetKey(KeyCode.LeftCommand);
		Color color = new Color(m_Red/255.0f, m_Green/255.0f, m_Blue/255.0f, 1.0f);

		if(Input.mousePosition.x > 250)
		{
			if(Input.GetMouseButton(0) && !modifierPressed)
			{
				Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 worldPosition = screenRay.GetPoint(m_VectorGrid.transform.position.z - Camera.main.transform.position.z);
				worldPosition.z = m_VectorGrid.transform.position.z;
				m_VectorGrid.AddGridForce(worldPosition, m_ExplosiveForce * 0.1f, m_ForceRadius, color, true);
			}
			else if(Input.GetMouseButton(1) || (Input.GetMouseButton(0) && modifierPressed))
			{
				Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				Vector3 worldPosition = screenRay.GetPoint(m_VectorGrid.transform.position.z - Camera.main.transform.position.z);
				worldPosition.z = m_VectorGrid.transform.position.z;
				m_VectorGrid.AddGridForce(worldPosition, -m_ImplosiveForce * 0.1f, m_ForceRadius, color, true);
			}
		}

		if(m_RandomiseColor)
		{
			UpdateRandomColor();
		}
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
		m_Red = interpolatedColor.r * 255.0f;
		m_Green = interpolatedColor.g * 255.0f;
		m_Blue = interpolatedColor.b * 255.0f;
	}

	void OnGUI () 
	{
		if(m_BoxStyle == null)
		{
			m_BoxStyle = new GUIStyle( GUI.skin.box );
			m_BoxStyle.normal.background = MakeTex( 2, 2, new Color( 1.0f, 1.0f, 1.0f, 0.2f ) );
		}
		
		GUI.Box(new Rect(10, 10, 230, 500), "", m_BoxStyle);

		GUI.Label(new Rect (80, 20, 200, 20), "Instructions");
		GUI.Label(new Rect (20, 50, 250, 20), "Left Click - Apply Explosive Force");
		GUI.Label(new Rect (20, 70, 250, 20), "Right Click - Apply Implosive Force");

		int sliderOffset = 120;
		GUI.Label(new Rect (20, sliderOffset + 5, 100, 30), "Explosive Force");
		m_ExplosiveForce = GUI.HorizontalSlider (new Rect (120, sliderOffset + 10, 100, 30), m_ExplosiveForce, 0.0f, 10.0f);
		
		GUI.Label(new Rect (20, sliderOffset + 35, 100, 30), "Implosive Force");
		m_ImplosiveForce = GUI.HorizontalSlider (new Rect (120, sliderOffset + 40, 100, 30), m_ImplosiveForce, 0.0f, 10.0f);
		
		GUI.Label(new Rect (20, sliderOffset + 65, 100, 30), "Force Radius");
		m_ForceRadius = GUI.HorizontalSlider (new Rect (120, sliderOffset + 70, 100, 30), m_ForceRadius, 0.0f, 10.0f);

		Vector2 colorOffset = new Vector2(40, 250);
		GUI.Label(new Rect (colorOffset.x + 40, colorOffset.y, 100, 30), "Color");
		
		m_Red = 255.0f - GUI.VerticalSlider(new Rect (colorOffset.x + 20, colorOffset.y + 30, 30, 100), 255.0f - m_Red, 0.0f, 255.0f);
		m_Green = 255.0f - GUI.VerticalSlider(new Rect(colorOffset.x + 50, colorOffset.y + 30, 30, 100), 255.0f - m_Green, 0.0f, 255.0f);
		m_Blue = 255.0f - GUI.VerticalSlider(new Rect(colorOffset.x + 80, colorOffset.y + 30, 30, 100), 255.0f - m_Blue, 0.0f, 255.0f);
		
		GUI.Label(new Rect(colorOffset.x + 20, colorOffset.y + 140, 30, 30),"R");
		GUI.Label(new Rect(colorOffset.x + 50, colorOffset.y + 140, 30, 30),"G");
		GUI.Label(new Rect(colorOffset.x + 80, colorOffset.y + 140, 30, 30),"B");
		
		m_RandomiseColor = GUI.Toggle(new Rect(colorOffset.x + 20, colorOffset.y + 170, 200, 30), m_RandomiseColor, "Randomize");

		if(GUI.Button(new Rect(20,colorOffset.y + 230,200,20), "Reset")) 
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
