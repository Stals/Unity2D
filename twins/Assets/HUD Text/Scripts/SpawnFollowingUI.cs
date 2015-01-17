using UnityEngine;
using System.Collections;

public class SpawnFollowingUI : MonoBehaviour {

    Camera uiCamera;

    [SerializeField]
    GameObject guiPrefab;

    [HideInInspector]
    public GameObject guiObject;

	

	// Use this for initialization
	void Start () {

	}

    public void createPrefab()
    {
        uiCamera = UICamera.mainCamera;
        GameObject go = uiCamera.transform.parent.gameObject;

        guiObject = NGUITools.AddChild(uiCamera.transform.parent.gameObject, guiPrefab);
        UIFollowTarget followTargetScript = guiObject.AddComponent<UIFollowTarget>();

        followTargetScript.gameCamera = Camera.main;
        followTargetScript.target = transform;
        followTargetScript.uiCamera = uiCamera;
        followTargetScript.disableIfInvisible = false;
    }

}
