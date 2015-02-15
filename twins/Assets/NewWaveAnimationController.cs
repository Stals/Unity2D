using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewWaveAnimationController : MonoBehaviour {

    [SerializeField]
    UILabel waveNumber;

    [SerializeField]
    UILabel weaponName;

    [SerializeField]
    UILabel weaponDesr;

    [SerializeField]
    List<UITweener> tweens;

    void Awake()
    {
        GetComponent<TweenAlpha>().value = 0;
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showNewWave(int wave)
    {
        waveNumber.text = "WAVE " + wave.ToString();
        weaponName.text = "WEAPON: " + Game.Instance.getManager().playerController.weaponSlot.currentWeapon.name;
        weaponDesr.text = Game.Instance.getManager().playerController.weaponSlot.currentWeapon.shortDescription;

        foreach(UITweener tween in tweens){
            tween.Play();        
        }
    }
}
