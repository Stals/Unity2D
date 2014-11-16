using UnityEngine;
using System.Collections;

public class ErrorHandler : MonoBehaviour {

	[SerializeField]
	UILabel linesLabel;

	[SerializeField]
	UILabel moneyLabel;

	[SerializeField]
	UILabel warningLabel;

	// Use this for initialization
	void Start () {
		Game.Instance.setErrorHandler(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void notEnoughMoney()
	{
		startTween(moneyLabel);
		showText("Not Enough Money");
	}

	public void noLinesLeft()
	{
		startTween(linesLabel);
		showText("No more Lines. Spin again");
	}

	void showText(string text)
	{
		warningLabel.text = text;
		startTween(warningLabel);
	}

	void startTween(UILabel _go)
	{
		UITweener tween = _go.GetComponent<UITweener>();
		if(tween){
			tween.PlayForward();
		}
	}
}
