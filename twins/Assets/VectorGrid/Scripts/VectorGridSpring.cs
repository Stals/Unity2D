using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

/// <summary>
/// Vector Grid spring class - represents a spring connecting two grid points. Applies forces to prevent them from
/// moving too far away from one another. Grid points can also be connected to their initial positions, meaning that 
/// they'll try to move back to their origin point
/// </summary>
public class VectorGridSpring
{
	public enum GridSpringType
	{
		Right,
		Up,
		OriginalPosition,
		Max,
	}
	
	public VectorGridPoint m_Point1;
	public VectorGridPoint m_Point2;
	public GridSpringType m_SpringType;
	
	float m_Stiffness;
	float m_Damping;
	float m_RestDistance;

	/// <summary>
	/// Check if the spring is valid
	/// </summary>
	public bool IsValid()
	{
		return m_Point1 != null;
	}

	/// <summary>
	/// Resets the bodies, thereby disconnecting the spring
	/// </summary>
	public void Reset()
	{
		m_Point1 = null;
		m_Point2 = null;
	}

	/// <summary>
	/// Initialise the spring
	/// </summary>
	public void Init(VectorGridPoint point1, VectorGridPoint point2, GridSpringType springType, float stiffness, float damping, float restDistanceScalar)
	{
		m_Point1 = point1;
		m_Point2 = point2;
		m_SpringType = springType;
		
		m_Stiffness = stiffness;
		m_Damping = damping;
		m_RestDistance = Vector3.Distance(m_Point1.m_OriginalPosition, m_Point2.m_OriginalPosition) * restDistanceScalar;
	}

	/// <summary>
	/// Update the spring
	/// </summary>
	public void Update()
	{
		// The spring connects a grid point to its own origin point
		if(m_SpringType == GridSpringType.OriginalPosition)
		{
			Vector3 offset = m_Point1.m_OriginalPosition - m_Point2.m_Position;
			
			if (offset.sqrMagnitude > m_RestDistance * m_RestDistance)
			{
				float distance = SqrtFast((offset.x * offset.x) + (offset.y * offset.y) + (offset.z * offset.z));
				offset = ((distance > 0)? (offset/distance) : Vector3.zero) * (distance - m_RestDistance);
				Vector3 velocityDiff = m_Point2.m_Velocity;
				Vector3 force = m_Stiffness * offset - velocityDiff * m_Damping;			
				m_Point2.m_Acceleration += force * m_Point2.m_InverseMass;
			}
		}
		// The spring connects a grid point to another grid point
		else
		{
			Vector3 offset = m_Point1.m_Position - m_Point2.m_Position;
			
			if (offset.sqrMagnitude > m_RestDistance * m_RestDistance)
			{
				float distance = SqrtFast((offset.x * offset.x) + (offset.y * offset.y) + (offset.z * offset.z));
				offset = ((distance > 0)? (offset/distance) : Vector3.zero) * (distance - m_RestDistance);
				Vector3 velocityDiff = m_Point2.m_Velocity - m_Point1.m_Velocity;
				Vector3 force = m_Stiffness * offset - velocityDiff * m_Damping;			
				m_Point1.m_Acceleration -= force * m_Point1.m_InverseMass;
				m_Point2.m_Acceleration += force * m_Point2.m_InverseMass;
			}
		}
	}
	
	/// <summary>
	/// Helper function to provide a faster, but less accurate square root
	/// </summary>
	public static float SqrtFast(float z)
	{
		if (z == 0) return 0;
		FloatIntUnion u;
		u.tmp = 0;
		float xhalf = 0.5f * z;
		u.f = z;
		u.tmp = 0x5f375a86 - (u.tmp >> 1);
		u.f = u.f * (1.5f - xhalf * u.f * u.f);
		return u.f * z;
	}

	[StructLayout(LayoutKind.Explicit)]
	private struct FloatIntUnion
	{
		[FieldOffset(0)]
		public float f;
		
		[FieldOffset(0)]
		public int tmp;
	}
}