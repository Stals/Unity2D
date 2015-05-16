using UnityEngine;
using System.Collections;

public class BezierHelper  {

	public static GameObject moveTo(GameObject gameObject, GameObject uiTarget, GameObject prefab, float time, float delay){

        Camera gameCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        Camera uiCamera = UICamera.mainCamera;


        GameObject guiObject = NGUITools.AddChild(uiTarget.transform.parent.gameObject, prefab);  

       /* UILabel label = guiObject.GetComponentInChildren<UILabel>();
        label.text = amout.ToString();*/

        /* MOVE TO CORRECT POSITION*/
        // Get screen location of node
        Vector2 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // Move to node
        guiObject.transform.position = uiCamera.ScreenToWorldPoint(screenPos);


        // радиус окружности на которой находятся точки начала и коца
        float r = (Vector3.Distance(guiObject.transform.position, uiTarget.transform.position) / 3);

        Vector3 middlePoint = 0.5f * Vector3.Normalize(uiTarget.transform.position - guiObject.transform.position) + guiObject.transform.position;

        Vector3 bezierPoint = middlePoint;
        bezierPoint.x += Random.Range(-r, r);
        bezierPoint.y += Random.Range(-r, r);

        //Vector3 bezierPoint = //new Vector3( Random.Range(-r, r), Random.Range(-r, r), 0);      

        Vector3[] path = new Vector3[3] {guiObject.transform.position, bezierPoint,  uiTarget.transform.position};

        guiObject.MoveTo(path, time, delay, EaseType.easeInSine);
        guiObject.RotateBy(new Vector3(0, 0, 1f), 0.5f, 0, EaseType.easeInOutSine, LoopType.loop);

   
        return guiObject;
}

}
