using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

    [SerializeField]
    UILabel titleLabel;

	// Use this for initialization
	void Start () {
        titleLabel.gameObject.RotateBy(new Vector3(0, 0, -0.02f), 0.5f, 0, EaseType.easeInOutSine, LoopType.pingPong);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startNewGame()
    {
        Application.LoadLevel("GameSceneManager");
    }
}
