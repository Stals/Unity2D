using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif 

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// Vector Grid point class - represents a single point in the renderend mesh, along with position,  
/// color, and velocity information
/// </summary>
public class VectorGridPoint
{
	public VectorGridSpring[] m_GridSpring = new VectorGridSpring[(int)VectorGridSpring.GridSpringType.Max];
	public Vector3 m_Position;
	public Vector3 m_OriginalPosition;
	public Vector3 m_Velocity;
	public Vector3 m_Acceleration;
	public Color m_Color;	
	public Color m_FoggedColor;
	public Color m_TargetColor;
	public float m_InverseMass;
	public float m_Damping;
	public bool m_Clamped;
	
	float m_DefaultDamping;
	float m_MaxFogAlphaScale;
	float m_InvMaxFogDistance;
	bool m_TwoWayFog;	
	static float s_MinVelocity = 0.001f * 0.001f;

	/// <summary>
	/// Vector Grid Point Constructor
	/// </summary>
	public VectorGridPoint(Vector3 position, float mass, float velocityDamping, Color color, float maxFogDistance, float maxFogAlphaScale, bool twoWayFog)
	{
		for(int loop = 0; loop < (int)VectorGridSpring.GridSpringType.Max; loop++)
		{
			m_GridSpring[loop] = new VectorGridSpring();
		}
		
		Init(position, mass, velocityDamping, color, maxFogDistance, maxFogAlphaScale, twoWayFog);
	}

	/// <summary>
	/// Connect this grid point to another with a spring
	/// </summary>
	public void ConnectTo(VectorGridPoint other, VectorGridSpring.GridSpringType springType, float stiffness, float damping, float restDistanceScalar)
	{
		m_GridSpring[(int)springType].Init(this, other, springType, stiffness, damping, restDistanceScalar);
	}

	/// <summary>
	/// Initialise the grid point
	/// </summary>
	public void Init(Vector3 position, float mass, float velocityDamping, Color color, float maxFogDistance, float maxFogAlphaScale, bool twoWayFog)
	{
		m_Color = m_FoggedColor = m_TargetColor = color;
		m_TwoWayFog = twoWayFog;
		m_Position = m_OriginalPosition = position;
		
		if(mass > 0.0f)
		{
			m_InverseMass = 1.0f/mass;
		}
		
		m_Damping = m_DefaultDamping = velocityDamping;
		m_Velocity = Vector3.zero;
		m_Acceleration = Vector3.zero;
		m_MaxFogAlphaScale = maxFogAlphaScale;
		m_InvMaxFogDistance = maxFogDistance > 0.0f? 1.0f/maxFogDistance : 0.0f;
	}

	/// <summary>
	/// Apply a force to grid point
	/// </summary>
	/// <param name="force">Force.</param>
	public void ApplyForce(Vector3 force)
	{
		m_Acceleration += force * m_InverseMass;
	}

	/// <summary>
	/// Update all the springs attached to this grid point
	/// </summary>
	public void UpdateSprings()
	{
		for(int loop = 0; loop < (int)VectorGridSpring.GridSpringType.Max; loop++)
		{
			if(m_GridSpring[loop].IsValid())
			{
				m_GridSpring[loop].Update();
			}
		}
	}

	/// <summary>
	/// Update the position and color of the grid point
	/// </summary>
	public void UpdatePositionAndColor()
	{
		if(!m_Clamped)
		{
			m_Velocity += m_Acceleration;
			m_Position += m_Velocity;
			m_Acceleration = Vector3.zero;
			
			if (m_Velocity.sqrMagnitude < s_MinVelocity)
			{
				m_Velocity = Vector3.zero;
			}
			
			m_Velocity *= m_Damping;
			m_Damping = m_DefaultDamping;
		}
		
		m_Color += (m_TargetColor - m_Color) * 0.1f;
		m_FoggedColor = m_Color;

		if(m_Position.z != 0)
		{
			float zPosition = m_TwoWayFog? Mathf.Abs(m_Position.z) : -m_Position.z;
			float distanceFactor = Mathf.Clamp01(zPosition * m_InvMaxFogDistance);
			m_FoggedColor.a = m_FoggedColor.a * Mathf.Lerp(1.0f, m_MaxFogAlphaScale, distanceFactor);
		}
	}

	/// <summary>
	/// Moves the grid point without using any forces - simply translates the position and origin
	/// </summary>
	public void AdjustPosition(Vector3 amount)
	{
		m_Position += amount; 
		m_OriginalPosition += amount;
	}
}
