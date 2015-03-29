using UnityEngine;
using System.Collections;

public class ProgressController : MonoBehaviour {

    [SerializeField]
    int currentValue = 0;

    [SerializeField]
    int maxValue = 100;

    UISlider progressBar;

    [SerializeField]
    UILabel progressLabel;

	// Use this for initialization
	void Start () {
        progressBar = GetComponent<UISlider>();
        progressBar.value = ((float)currentValue) / maxValue;
	}
	
	// Update is called once per frame
	void Update () {
        progressBar.value = Mathf.Lerp(progressBar.value, ((float)currentValue) / maxValue, Time.deltaTime * 5f);


        progressLabel.text = string.Format("{0} / {1}", Mathf.Round(progressBar.value * maxValue), maxValue);
         
	}

    public void addBlock()
    {
        currentValue += 1;
    }
}
