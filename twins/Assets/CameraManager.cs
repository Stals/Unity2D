﻿using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    [SerializeField]
    GameObject trackedGameObejct;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // TODO interpolate?
        transform.position = new Vector3(trackedGameObejct.transform.position.x / 4, trackedGameObejct.transform.position.y / 5, -10);
	}
}