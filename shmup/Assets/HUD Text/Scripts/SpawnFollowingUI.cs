using UnityEngine;
using System.Collections;

public class SpawnFollowingUI : MonoBehaviour {

    [SerializeField]
    Camera uiCamera;

    [SerializeField]
    GameObject guiPrefab;

    [HideInInspector]
    public GameObject guiObject;

	// Use this for initialization
	void Start () {
        createPrefab();
	}

    void createPrefab()
    {
        guiObject = NGUITools.AddChild(uiCamera.transform.parent.gameObject, guiPrefab);
        UIFollowTarget followTargetScript = guiObject.AddComponent<UIFollowTarget>();

        followTargetScript.gameCamera = Camera.main;
        followTargetScript.target = transform;
        followTargetScript.uiCamera = uiCamera;
        followTargetScript.disableIfInvisible = false;
    }

}
