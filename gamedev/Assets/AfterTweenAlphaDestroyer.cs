using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TweenAlpha))]
public class AfterTweenAlphaDestroyer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var tweenAlpha = GetComponent<TweenAlpha>();

        Destroy(gameObject, tweenAlpha.duration + tweenAlpha.delay);
    }
}