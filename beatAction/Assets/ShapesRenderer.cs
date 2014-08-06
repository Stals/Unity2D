using UnityEngine;
using System.Collections;

public class ShapesRenderer : MonoBehaviour {

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	private LineRenderer lineRenderer;

	private float radius = 4.0f;
	//private int size;

	private int vertexCount = 60;

	void Start() {

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.SetVertexCount(vertexCount+1);

	}
	void Update() {

		float x, y = 0.0f;

		for(int i = 0; i < vertexCount + 1; ++i){

			float angle = ((i / (float)vertexCount) * (Mathf.PI * 2));

			Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
			lineRenderer.SetPosition(i, pos);

		}
	}
}