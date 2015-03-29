using UnityEngine;
using System.Collections;

public class FinalPanelController : MonoBehaviour
{

    [SerializeField]
    TweenAlpha tween;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void close()
    {
        tween.PlayReverse();
        Destroy(gameObject, tween.duration);
    }
}
