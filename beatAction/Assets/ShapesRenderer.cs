using UnityEngine;
using System.Collections;

public class ShapesRenderer : MonoBehaviour {

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	public int lengthOfLineRenderer = 20;
	private LineRenderer lineRenderer;
	private float theta_scale = 0.1f; 

	private int size;

	void Start() {
		/*LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);*/

		            //Set lower to add more points
		size = (int)((2f * Mathf.PI) / theta_scale); //Total number of points in circle.

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.1f, 0.1f);
		lineRenderer.SetVertexCount(size);

	}
	void Update() {
		const float r = 4.0f;


		
		/*LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
		lineRenderer.SetWidth(0.2f, 0.2f);
		lineRenderer.SetVertexCount(size);
*/
		float x, y = 0.0f;

		for(int i = 0; i < size; ++i){

		//for(float theta = 0; theta < (float)(2.f * Mathf.PI); theta += theta_scale) {
			x = r * Mathf.Cos(theta_scale * i);
			y = r * Mathf.Sin(theta_scale * i);
			
			Vector3 pos = new Vector3(x, y, 0);
			lineRenderer.SetPosition(i, pos);

		}
	}
}
