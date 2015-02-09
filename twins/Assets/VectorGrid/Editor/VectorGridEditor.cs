using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VectorGrid))]
public class VectorGridEditor : Editor 
{	
	int m_InitialGridWidth = 0;
	int m_InitialGridHeight = 0;
	int m_InitialLineSpacingX = 0;
	int m_InitialLineSpacingY = 0;
	float m_InitialGridScale = 0.0f;
	Color m_InitialThickColor = Color.white;
	Color m_InitialThinColor = Color.white;
	float m_InitialThickWidth = 0.0f;
	float m_InitialThinWidth = 0.0f;
	int m_InitialGridSmoothing = 0;
	int m_InitialRenderMode = 0;

	protected virtual void StoreInitialValues()
	{
		VectorGrid targetGrid = target as VectorGrid;
		m_InitialGridWidth = targetGrid.m_GridWidth;
		m_InitialGridHeight = targetGrid.m_GridHeight;
		m_InitialLineSpacingX = targetGrid.m_ThickLineSpacingX;
		m_InitialLineSpacingY = targetGrid.m_ThickLineSpacingY;
		m_InitialGridScale = targetGrid.m_GridScale;
		m_InitialThickColor = targetGrid.m_ThickLineSpawnColor;
		m_InitialThinColor = targetGrid.m_ThinLineSpawnColor;
		m_InitialThickWidth = targetGrid.m_ThickLineWidth;
		m_InitialThinWidth = targetGrid.m_ThinLineWidth;
		m_InitialGridSmoothing = targetGrid.m_GridSmoothingFactor;
		m_InitialRenderMode = (int)targetGrid.m_RenderMode;
	}
	
	protected virtual void CheckForObjectChanges()
	{
		VectorGrid targetGrid = target as VectorGrid;

		targetGrid.m_GridWidth = Mathf.Max(1, targetGrid.m_GridWidth);
		targetGrid.m_GridHeight = Mathf.Max(1, targetGrid.m_GridHeight);
		targetGrid.m_LooseSpringInterval = Mathf.Max(1, targetGrid.m_LooseSpringInterval);
		targetGrid.m_GridSmoothingFactor = Mathf.Max(0, targetGrid.m_GridSmoothingFactor);

		if(m_InitialGridWidth != targetGrid.m_GridWidth ||
		   m_InitialGridHeight != targetGrid.m_GridHeight)
		{
			Undo.RecordObject(targetGrid, "Adjusted Vector Grid Dimensions");
			targetGrid.InitGrid();
		}
		   
		if(m_InitialGridScale != targetGrid.m_GridScale)
		{
			Undo.RecordObject(targetGrid, "Adjusted Vector Grid Scale");
			targetGrid.InitGrid();
		}

		if(m_InitialLineSpacingX != targetGrid.m_ThickLineSpacingX ||
		   m_InitialLineSpacingY != targetGrid.m_ThickLineSpacingY)
		{
			Undo.RecordObject(targetGrid, "Adjusted Vector Grid Line Spacing");
			targetGrid.InitGrid();
		}
		   
		if(m_InitialThickColor != targetGrid.m_ThickLineSpawnColor ||
		   m_InitialThinColor != targetGrid.m_ThinLineSpawnColor)
		{
			Undo.RecordObject(targetGrid, "Adjusted Vector Grid Line Color");
			targetGrid.InitGrid();
		}
		   
		if(m_InitialThickWidth != targetGrid.m_ThickLineWidth ||
		   m_InitialThinWidth != targetGrid.m_ThinLineWidth)
		{
			Undo.RecordObject(targetGrid, "Adjusted Vector Grid Line Width");
			targetGrid.InitGrid();
		}

		if(m_InitialGridSmoothing != targetGrid.m_GridSmoothingFactor)
		{
			Undo.RecordObject(targetGrid, "Adjusted Vector Grid Smoothing");
			targetGrid.InitGrid();
		}

		if(m_InitialRenderMode != (int)targetGrid.m_RenderMode)
		{
			Undo.RecordObject(targetGrid, "Changed Vector Grid Render Mode");
			targetGrid.InitGrid();
		}
	}

	public override void OnInspectorGUI() 
	{
		StoreInitialValues();
		DrawDefaultInspector();
		CheckForObjectChanges();
	}	
}
