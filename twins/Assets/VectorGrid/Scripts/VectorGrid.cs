using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif 

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Main vector grid class. Controls a set of grid points which can be affected by movement of their
/// neighbors (via springs connecting them) and also by external forces directly applying velocity changes
/// </summary>
[ExecuteInEditMode()]
[RequireComponent (typeof(MeshFilter), (typeof(MeshRenderer)))]
[AddComponentMenu("Vector Grid/Vector Grid")]
public class VectorGrid : MonoBehaviour 
{
	// How many grid points make up the grid in each direction
	public int m_GridWidth = 16;
	public int m_GridHeight = 16;

	// How far apart each grid point is from the rest
	public float m_GridScale = 0.1f;

	// Grid points can be set to fade out at high Z offset values. These values
	// control the distance and intensity of the fade
	public float m_MaxFogDistance = 1.0f;
	public float m_MaxFogAlphaScale = 1.0f;

	// Two way fog applies fogging when the Z value moves down as well as up
	// One way fog only applies fogging when the Z value moves down from the origin
	public bool m_TwoWayFog = true;

	/// Optionally render the grid as a flat plane with a texture applied, instead of as a grid
	public enum RenderMode
	{
		Grid,
		Plane,
		Sphere,
	}

	public RenderMode m_RenderMode = RenderMode.Grid;

	// How heavy the grid points are - will alter the spring behaviour
	public float m_VectorGridPointMass = 1.0f;

	// Velocity damping applied to the point mass movement - used to gradually slow them down
	public float m_VelocityDamping = 0.98f;

	// The grid can be set to spawn alternating thin and thick lines, with different
	// colors and widths. These values control the color, width, and how frequently
	// thick lines appear vs. thin ones
	public Color m_ThickLineSpawnColor = Color.green;
	public Color m_ThinLineSpawnColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
	public float m_ThickLineWidth = 0.1f;
	public float m_ThinLineWidth = 0.05f;
	public int m_ThickLineSpacingX = 6;
	public int m_ThickLineSpacingY = 6;

	// Spring stiffness/damping values for the springs connecting grid points to their neighbors
	public float m_SpringStiffness = 0.28f;
	public float m_SpringDamping = 0.06f;

	// Spring stiffness/damping values for the springs around the edge of the grid
	public float m_EdgeSpringStiffness = 1.0f;
	public float m_EdgeSpringDamping = 0.1f;

	// Use this to clamp the edge nodes so that they physically can't move
	public bool m_ClampEdges = false;

	// Spring stiffness/damping values for the loose springs that gently pull certain grid points 
	// back to their original positions. The spring interval value controls how often we spawn these
	// type of springs - 1 == every column/row, 10 == every 10th column/row.
	public float m_LooseSpringStiffness = 0.002f;
	public float m_LooseSpringDamping = 0.002f;
	public int m_LooseSpringInterval = 3;
	
	// How far apart the resting distance of each spring is. Set to < 1.0 for a taut grid, set to > 1.0
	// for a looser grid
	public float m_RestDistanceScalar = 1.0f;

	// Controls how many subdivisions are generated on the line connecting a grid point and its neighbor.
	// The mesh rendering uses Catmull-Rom interpolation to generate curved lines connecting the points,
	// resulting in smoother deformations at the expense of more vertices/triangles and CPU cost.
	public int m_GridSmoothingFactor = 0;

	// Turns on debug drawing - renders the mesh using basic debug lines
	public bool m_DebugDraw = false;

	// Turn this on if you need normals to be calculated for the mesh
	public bool m_CalculateNormals = false;

	// Assign a camera to this field and the mesh will automatically scroll to match the movement
	// of the camera
	public Camera m_TrackedCamera;

	// When using a tracked camera, this value controls whether it will scroll faster or slower
	// than the camera's movement, providing a parallax effect
	public Vector2 m_ParallaxScroll = Vector2.one;

	// Allows you to disable horizontal or vertical scrolling
	public bool m_AllowHorizontalScroll = true;
	public bool m_AllowVerticalScroll = true;

	// Enable update time diagnostics - just shows some debug info about CPU usage
	public bool m_EnableDiagnostics;

	System.Diagnostics.Stopwatch m_StopWatch = new System.Diagnostics.Stopwatch();
	VectorGridPoint[,] m_VectorGridPoints;
	double[] m_FrameTimes = new double[10];
	Vector3[] m_Vertices;
	Color[] m_VertexColors;
	int[] m_Triangles;
	Vector2[] m_UVs;
	Mesh m_Mesh;
	Vector3 m_PrevCameraPosition;
	Vector3 m_InitialPosition;
	Vector2 m_ScrollAmount = Vector2.zero;
	int m_FrameTimeIndex = 0;
	int m_XRowStart = 0;
	int m_YRowStart = 0;
	bool m_FirstUpdate = true;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{
		m_Mesh = new Mesh();
		m_Mesh.name = this.name + " Mesh";
		m_Mesh.MarkDynamic();
		GetComponent<MeshFilter>().mesh = m_Mesh;
		InitGrid();
	}

	/// <summary>
	/// Inits the grid - creates a grid of points at the appropriate positions
	/// </summary>
	public void InitGrid()
	{
		m_ThickLineSpacingX = Mathf.Max(m_ThickLineSpacingX, 1);
		m_ThickLineSpacingY = Mathf.Max(m_ThickLineSpacingY, 1);

		if(m_RenderMode == RenderMode.Sphere)
		{
			int latitudes = m_GridWidth;
			int longitudes = m_GridHeight;
			float radius = m_GridScale;
			
			float latitudeIncrement = 360.0f / latitudes;
			float longitudeIncrement = 180.0f / longitudes;

			m_VectorGridPoints = new VectorGridPoint[m_GridWidth + 1, m_GridHeight + 1];
			int uIndex = 0;
			int tIndex = 0;
			
			for (float u = 0; u < 360.0f; u += latitudeIncrement) 
			{
				for (float t = 0; t < 180.0f; t += longitudeIncrement) 
				{			
					float rad = radius;		
					Vector3 position;
					Color spawnColor = (uIndex % m_ThickLineSpacingX == 0 || tIndex % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
					position.x = (float) (rad * Mathf.Sin(Mathf.Deg2Rad * t) * Mathf.Sin(Mathf.Deg2Rad * u));
					position.y = (float) (rad * Mathf.Cos(Mathf.Deg2Rad * t));
					position.z = (float) (rad * Mathf.Sin(Mathf.Deg2Rad * t) * Mathf.Cos(Mathf.Deg2Rad * u));
					m_VectorGridPoints[uIndex, tIndex] = new VectorGridPoint(position, m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
					tIndex++;
				}

				tIndex = 0;
				uIndex++;
			}
		}
		else
		{
			m_VectorGridPoints = new VectorGridPoint[m_GridWidth, m_GridHeight];

			for(int x = 0; x < m_GridWidth; x++)
			{
				for(int y = 0; y < m_GridHeight; y++)
				{
					Color spawnColor = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
					Vector3 position = new Vector3((x * m_GridScale) - (m_GridScale * m_GridWidth * 0.5f), (y * m_GridScale) - (m_GridScale * m_GridHeight * 0.5f), 0.0f);
					m_VectorGridPoints[x, y] = new VectorGridPoint(position, m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
				}
			}
		}

		InitSprings();
		BuildMesh();
	}

	/// <summary>
	/// Initialises all the springs
	/// </summary>
	void InitSprings()
	{
		for(int xLoop = m_XRowStart; xLoop < m_XRowStart + m_GridWidth; xLoop++)
		{
			for(int yLoop = m_YRowStart; yLoop < m_YRowStart + m_GridHeight; yLoop++)
			{
				int x = xLoop % m_GridWidth;
				int y = yLoop % m_GridHeight;

				// Reset any springs that we might already have set up
				for(int springLoop = 0; springLoop < (int)VectorGridSpring.GridSpringType.Max; springLoop++)
				{
					m_VectorGridPoints[x, y].m_GridSpring[springLoop].Reset();
					m_VectorGridPoints[x, y].m_Clamped = false;
				}

				// Connect points at the edge of the grid to their original position
				if(xLoop == m_XRowStart || yLoop == m_YRowStart || xLoop == m_XRowStart + m_GridWidth - 1 || yLoop == m_YRowStart + m_GridHeight - 1)
				{
					if(m_ClampEdges)
					{
						m_VectorGridPoints[x, y].m_Clamped = true;
					}
					else
					{
						m_VectorGridPoints[x, y].ConnectTo(m_VectorGridPoints[x, y], VectorGridSpring.GridSpringType.OriginalPosition, m_EdgeSpringStiffness, m_EdgeSpringDamping, m_RestDistanceScalar);
					}
				}
				// Attach loose springs to encourage certain points to move back to their origin
				else if(x % m_LooseSpringInterval == 0 || y % m_LooseSpringInterval == 0)
				{
					m_VectorGridPoints[x, y].ConnectTo(m_VectorGridPoints[x, y], VectorGridSpring.GridSpringType.OriginalPosition, m_LooseSpringStiffness, m_LooseSpringDamping, m_RestDistanceScalar);
				}

				// Connect horizontal springs
				if(xLoop > m_XRowStart)
				{
					int prevX = (xLoop - 1) % m_GridWidth;
					m_VectorGridPoints[prevX, y].ConnectTo(m_VectorGridPoints[x, y], VectorGridSpring.GridSpringType.Right, m_SpringStiffness, m_SpringDamping, m_RestDistanceScalar);

					if(m_RenderMode == RenderMode.Sphere && xLoop == m_XRowStart + m_GridWidth - 1)
					{
						prevX = m_XRowStart;
						m_VectorGridPoints[prevX, y].ConnectTo(m_VectorGridPoints[x, y], VectorGridSpring.GridSpringType.Right, m_SpringStiffness, m_SpringDamping, m_RestDistanceScalar);
					}
				}

				// Connect vertical springs
				if(yLoop > m_YRowStart)
				{
					int prevY = (yLoop - 1) % m_GridHeight;
					m_VectorGridPoints[x, prevY].ConnectTo(m_VectorGridPoints[x, y], VectorGridSpring.GridSpringType.Up, m_SpringStiffness, m_SpringDamping, m_RestDistanceScalar);
				}
			}
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() 
	{	
		if(Application.isPlaying)
		{
			if(m_EnableDiagnostics)
			{
				m_StopWatch.Reset();
				m_StopWatch.Start();
			}

			if(m_FirstUpdate)
			{
				m_InitialPosition = this.transform.localPosition;
			}

			// Reset the grid if the user has changed the dimensions
			if(m_RenderMode != RenderMode.Sphere &&
			   (m_VectorGridPoints.GetLength(0) != m_GridWidth || m_VectorGridPoints.GetLength(1) != m_GridHeight))
			{
				InitGrid();
			}

			UpdateGrid();
			UpdateMesh();

			if(m_DebugDraw)
			{
				DebugDrawGrid();
			}

			if(m_EnableDiagnostics)
			{
				m_StopWatch.Stop();
				m_FrameTimes[m_FrameTimeIndex] = m_StopWatch.Elapsed.TotalMilliseconds;
				m_FrameTimeIndex++;

				if(m_FrameTimeIndex >= m_FrameTimes.Length)
				{
					m_FrameTimeIndex = 0;
				}
			}
		}
	}

#if UNITY_EDITOR
/// <summary>
/// Raises the draw gizmos selected event.
/// </summary>
	void OnDrawGizmosSelected()
	{
		if(m_EnableDiagnostics && Application.isPlaying)
		{
			double totalMilliseconds = 0;
			
			for(int loop = 0; loop < m_FrameTimes.Length; loop++)
			{
				totalMilliseconds += m_FrameTimes[loop];
			}
			
			double averageMilliseconds = totalMilliseconds/(double)m_FrameTimes.Length;
			Handles.Label(this.transform.position, "Ms: " + averageMilliseconds.ToString("0.000"));
		}
	}
#endif 

	/// <summary>
	/// Late update - scroll the grid if there is a tracked camera present
	/// </summary>
	void LateUpdate()
	{ 
		if(m_TrackedCamera)
		{
			if(!m_FirstUpdate)
			{
				Vector3 cameraMovement = m_TrackedCamera.transform.position - m_PrevCameraPosition;

				if(!m_AllowHorizontalScroll)
				{
					cameraMovement.x = 0;
				}
				
				if(!m_AllowVerticalScroll)
				{
					cameraMovement.y = 0;
				}

				this.transform.localPosition += cameraMovement;
				Scroll(new Vector2(-cameraMovement.x * m_ParallaxScroll.x, -cameraMovement.y * m_ParallaxScroll.y));
			}

			m_PrevCameraPosition = m_TrackedCamera.transform.position;
			m_FirstUpdate = false;
		}
	}

	/// <summary>
	/// Reset the grid - undoes any vertex movement and resets the position if following a camera
	/// </summary>
	public void Reset()
	{
		if(m_TrackedCamera)
		{
			this.transform.localPosition = m_InitialPosition;
			m_PrevCameraPosition = Camera.main.transform.position;
		}

		m_XRowStart = 0;
		m_YRowStart = 0;

		if(m_VectorGridPoints != null)
		{
			if(m_RenderMode == RenderMode.Sphere)
			{
				int latitudes = m_GridWidth;
				int longitudes = m_GridHeight;
				float radius = m_GridScale;
				
				float latitudeIncrement = 360.0f / latitudes;
				float longitudeIncrement = 180.0f / longitudes;
				
				int uIndex = 0;
				int tIndex = 0;
				
				for (float u = 0; u < 360.0f; u += latitudeIncrement) 
				{
					for (float t = 0; t < 180.0f; t += longitudeIncrement) 
					{			
						float rad = radius;		
						Vector3 position;
						Color spawnColor = (uIndex % m_ThickLineSpacingX == 0 || tIndex % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
						position.x = (float) (rad * Mathf.Sin(Mathf.Deg2Rad * t) * Mathf.Sin(Mathf.Deg2Rad * u));
						position.y = (float) (rad * Mathf.Cos(Mathf.Deg2Rad * t));
						position.z = (float) (rad * Mathf.Sin(Mathf.Deg2Rad * t) * Mathf.Cos(Mathf.Deg2Rad * u));
						m_VectorGridPoints[uIndex, tIndex].Init(position, m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
						tIndex++;
					}
					
					tIndex = 0;
					uIndex++;
				}
			}
			else
			{
				for(int x = 0; x < m_GridWidth; x++)
				{
					for(int y = 0; y < m_GridHeight; y++)
					{
						Color spawnColor = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
						Vector3 position = new Vector3((x * m_GridScale) - (m_GridScale * m_GridWidth * 0.5f), (y * m_GridScale) - (m_GridScale * m_GridHeight * 0.5f), 0.0f);
						m_VectorGridPoints[x, y].Init(position, m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
					}
				}
			}
		}

		m_FirstUpdate = true;
		m_ScrollAmount = Vector2.zero;
	}

	/// <summary>
	/// Scroll the specified amount.
	/// </summary>
	public void Scroll(Vector2 amount)
	{	
		if(amount != Vector2.zero)
		{
			if(!m_AllowHorizontalScroll)
			{
				amount.x = 0;
			}

			if(!m_AllowVerticalScroll)
			{
				amount.y = 0;
			}

			m_ScrollAmount += amount;

			for (int x = 0; x < m_GridWidth; x++)
			{
				for (int y = 0; y < m_GridHeight; y++)
				{
					m_VectorGridPoints[x,y].AdjustPosition(amount);
				}
			}

			bool updateSprings = false;

			// Scroll right
			while(m_ScrollAmount.x > m_GridScale)
			{
				int x = (m_XRowStart + m_GridWidth - 1) % m_GridWidth;
				int nextRow = (x + 1) % m_GridWidth;
						
				for (int y = 0; y < m_GridHeight; y++)
				{
					Color spawnColor = ((nextRow - 1) % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
					m_VectorGridPoints[x, y].Init(m_VectorGridPoints[nextRow, y].m_OriginalPosition + new Vector3(-m_GridScale, 0, 0), m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
				}
				
				m_XRowStart--;
				
				if(m_XRowStart < 0)
				{
					m_XRowStart = m_GridWidth - 1;
				}

				if(m_RenderMode == RenderMode.Plane)
				{
					int xIndex = m_XRowStart % m_GridWidth;
					
					for(int y = 0; y < m_GridHeight; y++)
					{
						m_UVs[(m_GridHeight * xIndex) + y].x -= (1.0f + (1.0f / (m_GridWidth - 1)));
					}
									
					m_Mesh.uv = m_UVs;
				}

				m_ScrollAmount.x = m_ScrollAmount.x - m_GridScale;
				updateSprings = true;
			}

			// Scroll left
			while(m_ScrollAmount.x < 0)
			{
				int x = m_XRowStart;
				int nextRow = (x - 1) % m_GridWidth;
						
				if(nextRow < 0)
				{
					nextRow = m_GridWidth - 1;
				}
				
				for (int y = 0; y < m_GridHeight; y++)
				{
					Color spawnColor = ((nextRow + 1) % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
					m_VectorGridPoints[x, y].Init(m_VectorGridPoints[nextRow, y].m_OriginalPosition + new Vector3(m_GridScale, 0, 0), m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
				}
				
				m_XRowStart++;
				
				if(m_XRowStart >= m_GridWidth)
				{
					m_XRowStart = 0;
				}

				if(m_RenderMode == RenderMode.Plane)
				{
					int xIndex = (m_XRowStart + m_GridWidth - 1) % m_GridWidth;
					
					for(int y = 0; y < m_GridHeight; y++)
					{
						m_UVs[(m_GridHeight * xIndex) + y].x += (1.0f + (1.0f / (m_GridWidth - 1)));
					}
					
					m_Mesh.uv = m_UVs;
				}

				m_ScrollAmount.x = m_ScrollAmount.x + m_GridScale;
				updateSprings = true;
			}

			// Scroll up
			while(m_ScrollAmount.y > m_GridScale)
			{
				int y = (m_YRowStart + m_GridHeight - 1) % m_GridHeight;
				int nextRow = (y + 1) % m_GridHeight;
						
				for (int x = 0; x < m_GridWidth; x++)
				{
					Color spawnColor = (x % m_ThickLineSpacingX == 0 || (nextRow - 1) % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
					m_VectorGridPoints[x, y].Init(m_VectorGridPoints[x, nextRow].m_OriginalPosition + new Vector3(0, -m_GridScale, 0), m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
				}
				
				m_YRowStart--;
				
				if(m_YRowStart < 0)
				{
					m_YRowStart = m_GridHeight - 1;
				}

				if(m_RenderMode == RenderMode.Plane)
				{
					int yIndex = m_YRowStart % m_GridHeight;
					
					for(int x = 0; x < m_GridWidth; x++)
					{
						m_UVs[(m_GridHeight * x) + yIndex].y -= (1.0f + (1.0f / (m_GridHeight - 1)));
					}
					
					m_Mesh.uv = m_UVs;
				}

				m_ScrollAmount.y = m_ScrollAmount.y - m_GridScale;
				updateSprings = true;
			}

			// Scroll down
			while(m_ScrollAmount.y < 0)
			{
				int y = m_YRowStart;
				int nextRow = y - 1;
				
				if(nextRow < 0)
				{
					nextRow = m_GridHeight - 1;
				}
				
				for (int x = 0; x < m_GridWidth; x++)
				{
					Color spawnColor = (x % m_ThickLineSpacingX == 0 || (nextRow + 1) % m_ThickLineSpacingY == 0) ? m_ThickLineSpawnColor : m_ThinLineSpawnColor;
					m_VectorGridPoints[x, y].Init(m_VectorGridPoints[x, nextRow].m_OriginalPosition + new Vector3(0, m_GridScale, 0), m_VectorGridPointMass, m_VelocityDamping, spawnColor, m_MaxFogDistance, m_MaxFogAlphaScale, m_TwoWayFog);
				}
				
				m_YRowStart++;
				
				if(m_YRowStart >= m_GridHeight)
				{
					m_YRowStart = 0;
				}

				if(m_RenderMode == RenderMode.Plane)
				{
					int yIndex = (m_YRowStart + m_GridHeight - 1) % m_GridHeight;
					
					for(int x = 0; x < m_GridWidth; x++)
					{
						m_UVs[(m_GridHeight * x) + yIndex].y += (1.0f + (1.0f / (m_GridHeight - 1)));
					}
					
					m_Mesh.uv = m_UVs;
				}

				m_ScrollAmount.y = m_ScrollAmount.y + m_GridScale;
				updateSprings = true;
			}

			if(updateSprings)
			{
				InitSprings();
			}
		}
	}
	
	/// <summary>
	/// Updates the grid.
	/// </summary>
	void UpdateGrid()
	{		
		for (int x = 0; x < m_GridWidth; x++)
		{
			for (int y = 0; y < m_GridHeight; y++)
			{
				if(m_VectorGridPoints[x, y] != null)
				{
					m_VectorGridPoints[x, y].UpdateSprings();
				}
			}
		}

		for (int x = 0; x < m_GridWidth; x++)
		{
			for (int y = 0; y < m_GridHeight; y++)
			{
				if(m_VectorGridPoints[x, y] != null)
				{
					m_VectorGridPoints[x, y].UpdatePositionAndColor();
				}
			}
		}
	}
		
	/// <summary>
	/// Debugs draw grid positions using the debug line renderer.
	/// </summary>
	void DebugDrawGrid()
	{
		for(int xLoop = m_XRowStart; xLoop < m_XRowStart + m_GridWidth; xLoop++)
		{
			for(int yLoop = m_YRowStart; yLoop < m_YRowStart + m_GridHeight; yLoop++)
			{
				int x = xLoop % m_GridWidth;
				int y = yLoop % m_GridHeight;

				for(int springLoop = 0; springLoop < m_VectorGridPoints[x, y].m_GridSpring.Length; springLoop++)
				{
					VectorGridSpring spring = m_VectorGridPoints[x, y].m_GridSpring[springLoop];

					if(spring.IsValid())
					{
						if(spring.m_SpringType == VectorGridSpring.GridSpringType.OriginalPosition)
						{
							Debug.DrawLine(this.transform.TransformPoint(spring.m_Point1.m_Position), this.transform.TransformPoint(spring.m_Point2.m_OriginalPosition), Color.red);
						}
						else
						{
							Debug.DrawLine(this.transform.TransformPoint(spring.m_Point1.m_Position), this.transform.TransformPoint(spring.m_Point2.m_Position), Color.white);
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// Add a grid force at the given position, extending in a sphere around the point
	/// </summary>
	public void AddGridForce(Vector3 worldPosition, float force, float radius, Color color, bool hasColor)
	{
		if(m_VectorGridPoints != null)
		{
			Vector3 localPosition = this.transform.InverseTransformPoint(worldPosition);
			
			for(int x = 0; x < m_GridWidth; x++)
			{
				for(int y = 0; y < m_GridHeight; y++)
				{
					float dist2 = Vector3.SqrMagnitude(localPosition - m_VectorGridPoints[x,y].m_Position);
					
					if (dist2 < radius * radius)
					{
						Vector3 forceVector = m_VectorGridPoints[x,y].m_Position - localPosition;
						float distanceFactor = 1.0f - (forceVector.magnitude/radius);
						Vector3 forceVectorNormalized = forceVector.normalized;

						m_VectorGridPoints[x,y].m_Acceleration += (forceVectorNormalized * distanceFactor * force) * m_VectorGridPoints[x,y].m_InverseMass;
						m_VectorGridPoints[x,y].m_Damping *= 0.6f;

						if(hasColor)
						{
							Color newColor = m_VectorGridPoints[x, y].m_TargetColor;
							float alpha = m_VectorGridPoints[x, y].m_TargetColor.a;
							
							// Grid forces should only affect RGB, not alpha channel
							newColor = m_VectorGridPoints[x, y].m_TargetColor + ((color - m_VectorGridPoints[x,y].m_TargetColor) * distanceFactor);
							newColor.a = alpha;
							
							m_VectorGridPoints[x, y].m_TargetColor = newColor;
						}
					}
				}
			}	
		}				
	}

	/// <summary>
	/// Add a grid force at the given position, pushing all grid points in the given radius in the given direction
	/// </summary>
	public void AddGridForce(Vector3 worldPosition, Vector3 force, float radius, Color color, bool hasColor)
	{
		if(m_VectorGridPoints != null)
		{
			Vector3 localPosition = this.transform.InverseTransformPoint(worldPosition);
			
			for(int x = 0; x < m_GridWidth; x++)
			{
				for(int y = 0; y < m_GridHeight; y++)
				{
					float dist2 = Vector3.SqrMagnitude(localPosition - m_VectorGridPoints[x,y].m_Position);
					
					if (dist2 < radius * radius)
					{
						Vector3 offset = m_VectorGridPoints[x,y].m_Position - localPosition;
						float distanceFactor = 1.0f - (offset.magnitude/radius);						
						m_VectorGridPoints[x,y].m_Acceleration += (force * distanceFactor) * m_VectorGridPoints[x,y].m_InverseMass;
						m_VectorGridPoints[x,y].m_Damping *= 0.6f;
						
						if(hasColor)
						{
							Color newColor = m_VectorGridPoints[x, y].m_TargetColor;
							float alpha = m_VectorGridPoints[x, y].m_TargetColor.a;
							
							// Grid forces should only affect RGB, not alpha channel
							newColor = m_VectorGridPoints[x, y].m_TargetColor + ((color - m_VectorGridPoints[x,y].m_TargetColor) * distanceFactor);
							newColor.a = alpha;
							
							m_VectorGridPoints[x, y].m_TargetColor = newColor;
						}
					}
				}
			}	
		}
	}

	/// <summary>
	/// Update the vertex positions/colors of the mesh
	/// </summary>
	void UpdateMesh()
	{
		int vertexIndex = 0;

		if(m_RenderMode == RenderMode.Plane)
		{
			int triangleIndex = 0;
			int xIndex = 0;

			for(int xLoop = m_XRowStart; xLoop < m_XRowStart + m_GridWidth; xLoop++, xIndex++)
			{
				int yIndex = 0;
				int prevX = (xLoop - 1) % m_GridWidth;
				int x = xLoop % m_GridWidth;

				for(int yLoop = m_YRowStart; yLoop < m_YRowStart + m_GridHeight; yLoop++, yIndex++)
			    {
					int prevY = (yLoop - 1) % m_GridHeight;
					int y = yLoop % m_GridHeight;

					m_Vertices[(m_GridHeight * xIndex) + yIndex] = m_VectorGridPoints[xIndex, yIndex].m_Position;
					m_VertexColors[(m_GridHeight * xIndex) + yIndex] = m_VectorGridPoints[xIndex, yIndex].m_Color;

					if(xLoop != m_XRowStart && yLoop != m_YRowStart)
					{
						m_Triangles[triangleIndex++] = (m_GridHeight * prevX) + prevY;
						m_Triangles[triangleIndex++] = (m_GridHeight * prevX) + y;
						m_Triangles[triangleIndex++] = (m_GridHeight * x) + y;

						m_Triangles[triangleIndex++] = (m_GridHeight * x) + y;
						m_Triangles[triangleIndex++] = (m_GridHeight * x) + prevY;
						m_Triangles[triangleIndex++] = (m_GridHeight * prevX) + prevY;
					}
				}
			}

			m_Mesh.triangles = m_Triangles;
		}
		else
		{
			if(m_GridSmoothingFactor > 0)
			{
				for(int xLoop = m_XRowStart; xLoop < m_XRowStart + m_GridWidth; xLoop++)
				{
					int x = xLoop % m_GridWidth;
					float lineWidth = x % m_ThickLineSpacingX == 0? m_ThickLineWidth : m_ThinLineWidth;
					Vector3 lineOffsetLeft = new Vector3(-lineWidth * 0.5f, 0, 0);
					Vector3 lineOffsetRight = new Vector3(lineWidth * 0.5f, 0, 0);
					
					int y, startY, previousY;
					startY = previousY = y = m_YRowStart % m_GridHeight;
					int nextY = (m_YRowStart + 1) % m_GridHeight;
					
					for(int yLoop = m_YRowStart + 1; yLoop < m_YRowStart + m_GridHeight; yLoop++)
					{
						previousY = startY;
						startY = y;
						y = nextY;
						nextY = yLoop < m_YRowStart + m_GridHeight - 1? (yLoop + 1) % m_GridHeight : y;
						
						Vector3 previousPosition = m_VectorGridPoints[x, previousY].m_Position;
						Vector3 startPosition = m_VectorGridPoints[x, startY].m_Position;
						Vector3 endPosition = m_VectorGridPoints[x, y].m_Position;
						Vector3 nextPosition = m_VectorGridPoints[x, nextY].m_Position;
						
						Color prevColor = m_VectorGridPoints[x, startY].m_FoggedColor;
						Color nextColor = m_VectorGridPoints[x, y].m_FoggedColor;
						
						if(yLoop == m_YRowStart + 1)
						{
							m_Vertices[vertexIndex] = startPosition + lineOffsetLeft;
							m_VertexColors[vertexIndex++] = prevColor;
							
							m_Vertices[vertexIndex] = startPosition + lineOffsetRight;
							m_VertexColors[vertexIndex++] = prevColor;
						}
						
						for(int smoothingLoop = 1; smoothingLoop <= m_GridSmoothingFactor + 1; smoothingLoop++)
						{
							float fraction = smoothingLoop/(float)(m_GridSmoothingFactor + 1);
							Vector3 interpolatedPosition = CatmullRom(previousPosition, startPosition, endPosition, nextPosition, fraction);
							Color interpolatedColor = prevColor + ((nextColor - prevColor) * fraction);
							
							m_Vertices[vertexIndex] = interpolatedPosition + lineOffsetLeft;
							m_VertexColors[vertexIndex++] = interpolatedColor;
							
							m_Vertices[vertexIndex] = interpolatedPosition + lineOffsetRight;
							m_VertexColors[vertexIndex++] = interpolatedColor;
						}
					}
				}
				
				for (int yLoop = m_YRowStart; yLoop < m_YRowStart + m_GridHeight; yLoop++)
				{
					int y = yLoop % m_GridHeight;
					float lineWidth = y % m_ThickLineSpacingY == 0? m_ThickLineWidth : m_ThinLineWidth;			
					Vector3 lineOffsetUp = new Vector3(0, lineWidth * 0.5f, 0);
					Vector3 lineOffsetDown = new Vector3(0, -lineWidth * 0.5f, 0);
					
					int x, startX, previousX;
					startX = previousX = x = m_XRowStart % m_GridWidth;
					int nextX = (m_XRowStart + 1) % m_GridWidth;
					int xLoopStart = (m_RenderMode == RenderMode.Sphere)? m_XRowStart : m_XRowStart + 1;

					if(m_RenderMode == RenderMode.Sphere)
					{
						startX = (m_XRowStart - 1 + m_GridWidth) % m_GridWidth;
						previousX = (m_XRowStart - 2 + m_GridWidth) % m_GridWidth;
					}
					
					for (int xLoop = xLoopStart; xLoop < m_XRowStart + m_GridWidth; xLoop++)
					{
						previousX = startX;
						startX = x;
						x = nextX;

						if(m_RenderMode == RenderMode.Sphere)
						{
							nextX = (xLoop + 1) % m_GridWidth;
						}
						else
						{
							nextX = xLoop < m_XRowStart + m_GridWidth - 1? (xLoop + 1) % m_GridWidth : x;
						}
						
						Vector3 previousPosition = m_VectorGridPoints[previousX, y].m_Position;
						Vector3 startPosition = m_VectorGridPoints[startX, y].m_Position;
						Vector3 endPosition = m_VectorGridPoints[x, y].m_Position;
						Vector3 nextPosition = m_VectorGridPoints[nextX, y].m_Position;
						
						Color prevColor = m_VectorGridPoints[startX, y].m_FoggedColor;
						Color nextColor = m_VectorGridPoints[x, y].m_FoggedColor;

						if(m_RenderMode == RenderMode.Sphere)
						{
							if(xLoop == m_XRowStart)
							{
								m_Vertices[vertexIndex] = previousPosition + lineOffsetDown;
								m_VertexColors[vertexIndex++] = prevColor;
								
								m_Vertices[vertexIndex] = startPosition + lineOffsetUp;
								m_VertexColors[vertexIndex++] = prevColor;
							}
						}
						else
						{
							if(xLoop == m_XRowStart + 1)
							{
								m_Vertices[vertexIndex] = startPosition + lineOffsetDown;
								m_VertexColors[vertexIndex++] = prevColor;
								
								m_Vertices[vertexIndex] = startPosition + lineOffsetUp;
								m_VertexColors[vertexIndex++] = prevColor;
							}
						}
						
						for(int smoothingLoop = 1; smoothingLoop <= m_GridSmoothingFactor + 1; smoothingLoop++)
						{
							float fraction = smoothingLoop/(float)(m_GridSmoothingFactor + 1);
							Vector3 interpolatedPosition = CatmullRom(previousPosition, startPosition, endPosition, nextPosition, fraction);
							Color interpolatedColor = prevColor + ((nextColor - prevColor) * fraction);
							
							m_Vertices[vertexIndex] = interpolatedPosition + lineOffsetDown;
							m_VertexColors[vertexIndex++] = interpolatedColor;
							
							m_Vertices[vertexIndex] = interpolatedPosition + lineOffsetUp;
							m_VertexColors[vertexIndex++] = interpolatedColor;
						}
					}
				}
			}
			else
			{
				for(int xLoop = m_XRowStart; xLoop < m_XRowStart + m_GridWidth; xLoop++)
				{
					int x = xLoop % m_GridWidth;
					float lineWidth = x % m_ThickLineSpacingX == 0? m_ThickLineWidth : m_ThinLineWidth;
					Vector3 lineOffsetLeft = new Vector3(-lineWidth * 0.5f, 0, 0);
					Vector3 lineOffsetRight = new Vector3(lineWidth * 0.5f, 0, 0);
					
					for(int yLoop = m_YRowStart; yLoop < m_YRowStart + m_GridHeight; yLoop++)
					{
						int y = yLoop % m_GridHeight;
						
						m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + lineOffsetLeft;
						m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
						
						m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + lineOffsetRight;
						m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					}
				}
				
				for (int yLoop = m_YRowStart; yLoop < m_YRowStart + m_GridHeight; yLoop++)
				{
					int y = yLoop % m_GridHeight;
					float lineWidth = y % m_ThickLineSpacingY == 0? m_ThickLineWidth : m_ThinLineWidth;			
					Vector3 lineOffsetUp = new Vector3(0, lineWidth * 0.5f, 0);
					Vector3 lineOffsetDown = new Vector3(0, -lineWidth * 0.5f, 0);
					int maxXLoop = m_XRowStart + m_GridWidth;

					if(m_RenderMode == RenderMode.Sphere)
					{
						maxXLoop++;
					}

					for (int xLoop = m_XRowStart; xLoop < maxXLoop; xLoop++)
					{
						int x = xLoop % m_GridWidth;
						
						m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + lineOffsetDown;
						m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
						
						m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + lineOffsetUp;
						m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					}
				}
			}

			if(m_RenderMode != RenderMode.Sphere)
			{
				// Bottom Left Corner Join
				{
					int x = (m_XRowStart) % m_GridWidth;
					int y = (m_YRowStart) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(-lineWidth * 0.5f, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, -lineWidth * 0.5f, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
				}
				
				// Bottom Right Corner Join
				{
					int x = (m_XRowStart + m_GridWidth - 1) % m_GridWidth;
					int y = (m_YRowStart) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(lineWidth * 0.5f, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, -lineWidth * 0.5f, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
				}
				
				// Top Left Corner Join
				{
					int x = (m_XRowStart) % m_GridHeight;
					int y = (m_YRowStart + m_GridHeight - 1) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(-lineWidth * 0.5f, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, lineWidth * 0.5f, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
				}
				
				// Top Right Corner Join
				{
					int x = (m_XRowStart + m_GridWidth - 1) % m_GridWidth;
					int y = (m_YRowStart + m_GridHeight - 1) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(lineWidth * 0.5f, 0, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
					
					m_Vertices[vertexIndex] = m_VectorGridPoints[x, y].m_Position + new Vector3(0, lineWidth * 0.5f, 0);
					m_VertexColors[vertexIndex++] = m_VectorGridPoints[x, y].m_FoggedColor;
				}
			}
		}

		m_Mesh.vertices = m_Vertices;
		m_Mesh.colors = m_VertexColors;
		m_Mesh.RecalculateBounds();

		if(m_CalculateNormals)
		{
			m_Mesh.RecalculateNormals();
		}
	}

	/// <summary>
	/// Add a new vertical line vertex with a start and end point
	/// </summary>
	void AddVerticalLine(Vector3 startPos, Vector3 endPos, Color startColor, Color endColor, float lineWidth, ref List<Vector3> vertices, ref List<Color> colors, ref List<Vector2> uvs, ref List<int> triangles)
	{
		vertices.Add(startPos + new Vector3(-lineWidth * 0.5f, 0, 0));
		vertices.Add(startPos + new Vector3(lineWidth * 0.5f, 0, 0));
		vertices.Add(endPos + new Vector3(-lineWidth * 0.5f, 0, 0));
		vertices.Add(endPos + new Vector3(lineWidth * 0.5f, 0, 0));
		
		colors.Add(startColor);
		colors.Add(startColor);
		colors.Add(endColor);
		colors.Add(endColor);
		
		uvs.Add(new Vector2(0.0f, 1.0f));
		uvs.Add(new Vector2(0.0f, 0.0f));
		uvs.Add(new Vector2(1.0f, 1.0f));
		uvs.Add(new Vector2(1.0f, 0.0f));
		
		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 3);
		
		triangles.Add(vertices.Count - 3);
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 1);
	}

	/// <summary>
	/// Add a new vertical line vertex, assuming the previously added vertex was the connector before
	/// </summary>
	void AddVerticalLine(Vector3 endPos, Color endColor, float lineWidth, ref List<Vector3> vertices, ref List<Color> colors, ref List<Vector2> uvs, ref List<int> triangles)
	{
		vertices.Add(endPos + new Vector3(-lineWidth * 0.5f, 0, 0));
		vertices.Add(endPos + new Vector3(lineWidth * 0.5f, 0, 0));
		
		colors.Add(endColor);
		colors.Add(endColor);
		
		uvs.Add(new Vector2(1.0f, 1.0f));
		uvs.Add(new Vector2(1.0f, 0.0f));
		
		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 3);
		
		triangles.Add(vertices.Count - 3);
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 1);
	}

	/// <summary>
	/// Add a new horizontal line vertex with a start and end point
	/// </summary>
	void AddHorizontalLine(Vector3 startPos, Vector3 endPos, Color startColor, Color endColor, float lineWidth, ref List<Vector3> vertices, ref List<Color> colors, ref List<Vector2> uvs, ref List<int> triangles)
	{
		vertices.Add(startPos + new Vector3(0, -lineWidth * 0.5f, 0));
		vertices.Add(startPos + new Vector3(0, lineWidth * 0.5f, 0));
		vertices.Add(endPos + new Vector3(0, -lineWidth * 0.5f, 0));
		vertices.Add(endPos + new Vector3(0, lineWidth * 0.5f, 0));
		
		colors.Add(startColor);
		colors.Add(startColor);
		colors.Add(endColor);
		colors.Add(endColor);
		
		uvs.Add(new Vector2(0.0f, 0.0f));
		uvs.Add(new Vector2(0.0f, 1.0f));
		uvs.Add(new Vector2(1.0f, 0.0f));
		uvs.Add(new Vector2(1.0f, 1.0f));
		
		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 3);
		triangles.Add(vertices.Count - 1);
		
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 1);
	}

	/// <summary>
	/// Add a new horizontal line vertex, assuming the previously added vertex was the connector before
	/// </summary>
	void AddHorizontalLine(Vector3 endPos, Color endColor, float lineWidth, ref List<Vector3> vertices, ref List<Color> colors, ref List<Vector2> uvs, ref List<int> triangles)
	{
		vertices.Add(endPos + new Vector3(0, -lineWidth * 0.5f, 0));
		vertices.Add(endPos + new Vector3(0, lineWidth * 0.5f, 0));

		colors.Add(endColor);
		colors.Add(endColor);

		uvs.Add(new Vector2(1.0f, 0.0f));
		uvs.Add(new Vector2(1.0f, 1.0f));
		
		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 3);
		triangles.Add(vertices.Count - 1);
		
		triangles.Add(vertices.Count - 2);
		triangles.Add(vertices.Count - 4);
		triangles.Add(vertices.Count - 1);
	}

	/// <summary>
	/// Blends a vector smoothly based on the given control points
	/// </summary>
	static Vector3 CatmullRom(Vector3 previous, Vector3 start, Vector3 end, Vector3 next, float fraction) 
	{
		float fractionSquared = fraction * fraction;
		float fractionCubed = fractionSquared * fraction;
		
		return previous * (-0.5f * fractionCubed + fractionSquared - 0.5f * fraction) +
			   start * ( 1.5f * fractionCubed + -2.5f * fractionSquared + 1.0f) +
			   end * (-1.5f * fractionCubed + 2.0f * fractionSquared + 0.5f * fraction) +
			   next * ( 0.5f * fractionCubed - 0.5f * fractionSquared);
	}

	/// <summary>
	/// Build the rendered mesh.
	/// </summary>
	void BuildMesh()
	{
		List<Vector3> vertices = new List<Vector3>();
		List<Color> colors = new List<Color>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> triangles = new List<int>();

		if(m_RenderMode == RenderMode.Plane)
		{
			for(int xLoop = 0; xLoop < m_GridWidth; xLoop++)
			{
				for(int yLoop = 0; yLoop < m_GridHeight; yLoop++)
				{
					vertices.Add(m_VectorGridPoints[xLoop, yLoop].m_Position);
					colors.Add(m_VectorGridPoints[xLoop, yLoop].m_Color);
					uvs.Add(new Vector2(xLoop/((float)m_GridWidth - 1), yLoop/((float)m_GridHeight - 1)));

					if(xLoop > 0 && yLoop > 0)
					{
						triangles.Add((m_GridHeight * (xLoop - 1)) + (yLoop - 1));
						triangles.Add((m_GridHeight * (xLoop - 1)) + (yLoop));
						triangles.Add((m_GridHeight * (xLoop)) + (yLoop));

						triangles.Add((m_GridHeight * (xLoop)) + (yLoop));
						triangles.Add((m_GridHeight * (xLoop)) + (yLoop - 1));
						triangles.Add((m_GridHeight * (xLoop - 1)) + (yLoop - 1));
					}
				}
			}
		}
		else
		{
			// Draw Vertical lines
			for(int xLoop = m_XRowStart; xLoop < m_XRowStart + m_GridWidth; xLoop++)
			{
				int x = xLoop % m_GridWidth;
				float lineWidth = x % m_ThickLineSpacingX == 0? m_ThickLineWidth : m_ThinLineWidth;
				
				for(int yLoop = m_YRowStart + 1; yLoop < m_YRowStart + m_GridHeight; yLoop++)
				{
					int y = yLoop % m_GridHeight;
					
					if(m_GridSmoothingFactor > 0)
					{
						int startY = yLoop > m_YRowStart? yLoop - 1 % m_GridHeight : y;
						int previousY = yLoop > m_YRowStart + 1? yLoop - 2 % m_GridHeight : startY;
						int nextY = yLoop < m_YRowStart + m_GridHeight - 1? yLoop + 1 % m_GridHeight : y;
						
						Vector3 previousPosition = m_VectorGridPoints[x, previousY].m_Position;
						Vector3 startPosition = m_VectorGridPoints[x, startY].m_Position;
						Vector3 endPosition = m_VectorGridPoints[x, y].m_Position;
						Vector3 nextPosition = m_VectorGridPoints[x, nextY].m_Position;
						
						Color prevColor = m_VectorGridPoints[x, startY].m_Color;
						Color nextColor = m_VectorGridPoints[x, y].m_Color;
						
						for(int smoothingLoop = 1; smoothingLoop <= m_GridSmoothingFactor + 1; smoothingLoop++)
						{
							float fraction = smoothingLoop/(float)(m_GridSmoothingFactor + 1);
							Color interpolatedColor = prevColor + ((nextColor - prevColor) * fraction);
							
							if(smoothingLoop == 1 && yLoop == m_YRowStart + 1)
							{
								AddVerticalLine(startPosition,
								                CatmullRom(previousPosition, startPosition, endPosition, nextPosition, fraction),
								                prevColor,
								                interpolatedColor, 
								                lineWidth,
								                ref vertices,
								                ref colors,
								                ref uvs,
								                ref triangles);
							}
							else
							{
								AddVerticalLine(CatmullRom(previousPosition, startPosition, endPosition, nextPosition, fraction),
								                interpolatedColor, 
								                lineWidth,
								                ref vertices,
								                ref colors,
								                ref uvs,
								                ref triangles);
							}
						}
					}
					else
					{
						if(yLoop == m_YRowStart + 1)
						{
							int prevY = (yLoop - 1) % m_GridHeight;
							
							AddVerticalLine(m_VectorGridPoints[x, prevY].m_Position, 
							                m_VectorGridPoints[x, y].m_Position,
							                m_VectorGridPoints[x, prevY].m_Color,
							                m_VectorGridPoints[x, y].m_Color, 
							                lineWidth,
							                ref vertices,
							                ref colors,
							                ref uvs,
							                ref triangles);
						}
						else
						{
							AddVerticalLine(m_VectorGridPoints[x, y].m_Position,
							                m_VectorGridPoints[x, y].m_Color, 
							                lineWidth,
							                ref vertices,
							                ref colors,
							                ref uvs,
							                ref triangles);
						}
					}
				}
			}
			
			// Draw horizontal lines
			for (int yLoop = m_YRowStart; yLoop < m_YRowStart + m_GridHeight; yLoop++)
			{
				int y = yLoop % m_GridHeight;
				float lineWidth = y % m_ThickLineSpacingY == 0? m_ThickLineWidth : m_ThinLineWidth;

				int xStart = m_RenderMode == RenderMode.Sphere? m_XRowStart : m_XRowStart + 1;
				
				for (int xLoop = xStart; xLoop < m_XRowStart + m_GridWidth; xLoop++)
				{
					int x = xLoop % m_GridWidth;
					
					if(m_GridSmoothingFactor > 0)
					{
						int startX = 0;
						int previousX = 0;
						int nextX = 0;

						if(m_RenderMode == RenderMode.Sphere)
						{
							startX = (xLoop - 1 + m_GridWidth) % m_GridWidth;
							previousX = (xLoop - 2 + m_GridWidth) % m_GridWidth;
							nextX = (xLoop + 1 + m_GridWidth) % m_GridWidth;
						}
						else
						{
							startX = xLoop > m_XRowStart? xLoop - 1 % m_GridWidth : x;
							previousX = xLoop > m_XRowStart + 1? xLoop - 2 % m_GridWidth : startX;
							nextX = xLoop < m_XRowStart + m_GridWidth - 1? xLoop + 1 % m_GridWidth : x;
						}

						Vector3 previousPosition = m_VectorGridPoints[previousX, y].m_Position;
						Vector3 startPosition = m_VectorGridPoints[startX, y].m_Position;
						Vector3 endPosition = m_VectorGridPoints[x, y].m_Position;
						Vector3 nextPosition = m_VectorGridPoints[nextX, y].m_Position;
						
						Color prevColor = m_VectorGridPoints[startX, y].m_Color;
						Color nextColor = m_VectorGridPoints[x, y].m_Color;
						
						for(int smoothingLoop = 1; smoothingLoop <= m_GridSmoothingFactor + 1; smoothingLoop++)
						{
							float fraction = smoothingLoop/(float)(m_GridSmoothingFactor + 1);
							Color interpolatedColor = prevColor + ((nextColor - prevColor) * fraction);

							if(smoothingLoop == 1 && m_RenderMode == RenderMode.Sphere && xLoop == m_XRowStart)
							{
								AddHorizontalLine(startPosition,
								                  CatmullRom(previousPosition, startPosition, endPosition, nextPosition, fraction),
								                  prevColor,
								                  interpolatedColor, 
								                  lineWidth,
								                  ref vertices,
								                  ref colors,
								                  ref uvs,
								                  ref triangles);
							}
							else if(smoothingLoop == 1 && xLoop == m_XRowStart + 1 && m_RenderMode != RenderMode.Sphere)
							{
								AddHorizontalLine(startPosition,
								                  CatmullRom(previousPosition, startPosition, endPosition, nextPosition, fraction),
								                  prevColor,
								                  interpolatedColor, 
								                  lineWidth,
								                  ref vertices,
								                  ref colors,
								                  ref uvs,
								                  ref triangles);
							}
							else
							{
								AddHorizontalLine(CatmullRom(previousPosition, startPosition, endPosition, nextPosition, fraction),
								                  interpolatedColor, 
								                  lineWidth,
								                  ref vertices,
								                  ref colors,
								                  ref uvs,
								                  ref triangles);
							}
						}
					}
					else
					{
						if(m_RenderMode == RenderMode.Sphere && xLoop == m_XRowStart)
						{
							int prevX = (xLoop + m_GridWidth - 1) % m_GridWidth;
							
							AddHorizontalLine(m_VectorGridPoints[prevX, y].m_Position, 
							                  m_VectorGridPoints[x, y].m_Position,
							                  m_VectorGridPoints[prevX, y].m_Color,
							                  m_VectorGridPoints[x, y].m_Color, 
							                  lineWidth,
							                  ref vertices,
							                  ref colors,
							                  ref uvs,
							                  ref triangles);
						}
						else if(xLoop == m_XRowStart + 1 && m_RenderMode != RenderMode.Sphere)
						{
							int prevX = (xLoop - 1) % m_GridWidth;
							
							AddHorizontalLine(m_VectorGridPoints[prevX, y].m_Position, 
							                  m_VectorGridPoints[x, y].m_Position,
							                  m_VectorGridPoints[prevX, y].m_Color,
							                  m_VectorGridPoints[x, y].m_Color, 
							                  lineWidth,
							                  ref vertices,
							                  ref colors,
							                  ref uvs,
							                  ref triangles);
						}
						else
						{
							AddHorizontalLine(m_VectorGridPoints[x, y].m_Position,
							                  m_VectorGridPoints[x, y].m_Color, 
							                  lineWidth,
							                  ref vertices,
							                  ref colors,
							                  ref uvs,
							                  ref triangles);
						}
					}
				}
			}

			if(m_RenderMode != RenderMode.Sphere)
			{
				// Bottom Left Corner Join
				{
					int x = (m_XRowStart) % m_GridWidth;
					int y = (m_YRowStart) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(-lineWidth * 0.5f, 0, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, -lineWidth * 0.5f, 0));
					
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					
					uvs.Add(new Vector2(0.5f, 1.0f));
					uvs.Add(new Vector2(0.5f, 0.5f));
					uvs.Add(new Vector2(1.0f, 1.0f));				
					
					triangles.Add(vertices.Count - 3);
					triangles.Add(vertices.Count - 2);
					triangles.Add(vertices.Count - 1);
				}
				
				// Bottom Right Corner Join
				{
					int x = (m_XRowStart + m_GridWidth - 1) % m_GridWidth;
					int y = (m_YRowStart) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(lineWidth * 0.5f, 0, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, -lineWidth * 0.5f, 0));
					
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					
					uvs.Add(new Vector2(0.5f, 0.5f));
					uvs.Add(new Vector2(0.5f, 1.0f));
					uvs.Add(new Vector2(1.0f, 1.0f));				
					
					triangles.Add(vertices.Count - 3);
					triangles.Add(vertices.Count - 2);
					triangles.Add(vertices.Count - 1);
				}
				
				// Top Left Corner Join
				{
					int x = (m_XRowStart) % m_GridHeight;
					int y = (m_YRowStart + m_GridHeight - 1) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(-lineWidth * 0.5f, 0, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, lineWidth * 0.5f, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0));
					
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					
					uvs.Add(new Vector2(0.5f, 1.0f));
					uvs.Add(new Vector2(1.0f, 1.0f));				
					uvs.Add(new Vector2(0.5f, 0.5f));
					
					triangles.Add(vertices.Count - 3);
					triangles.Add(vertices.Count - 2);
					triangles.Add(vertices.Count - 1);
				}
				
				// Top Right Corner Join
				{
					int x = (m_XRowStart + m_GridWidth - 1) % m_GridWidth;
					int y = (m_YRowStart + m_GridHeight - 1) % m_GridHeight;
					float lineWidth = (x % m_ThickLineSpacingX == 0 || y % m_ThickLineSpacingY == 0) ? m_ThickLineWidth : m_ThinLineWidth;
					
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, 0, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(lineWidth * 0.5f, 0, 0));
					vertices.Add(m_VectorGridPoints[x, y].m_Position + new Vector3(0, lineWidth * 0.5f, 0));
					
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					colors.Add(m_VectorGridPoints[x, y].m_FoggedColor);
					
					uvs.Add(new Vector2(0.5f, 0.5f));
					uvs.Add(new Vector2(0.5f, 1.0f));
					uvs.Add(new Vector2(1.0f, 1.0f));				
					
					triangles.Add(vertices.Count - 3);
					triangles.Add(vertices.Count - 2);
					triangles.Add(vertices.Count - 1);
				}
			}
		}

		m_Vertices = vertices.ToArray();
		m_VertexColors = colors.ToArray();
		m_Triangles = triangles.ToArray();
		m_UVs = uvs.ToArray();
				
		m_Mesh.Clear();
		m_Mesh.vertices = m_Vertices;
		m_Mesh.colors = m_VertexColors;
		m_Mesh.uv = m_UVs;
		m_Mesh.triangles = m_Triangles;
		m_Mesh.RecalculateBounds();
		m_Mesh.RecalculateNormals();
	}
	
	#if UNITY_EDITOR
	[MenuItem("GameObject/Create Other/Vector Grid/Vector Grid", false, 12951)]
	static void DoCreateVectorGridObject()
	{
		GameObject gameObject = new GameObject("Vector Grid");
		gameObject.AddComponent<VectorGrid>();
		Selection.activeGameObject = gameObject;
		Undo.RegisterCreatedObjectUndo(gameObject, "Create Vector Grid");
	}
	#endif
}
