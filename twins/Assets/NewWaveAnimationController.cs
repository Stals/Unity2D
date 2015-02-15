using UnityEngine;
using System.Collections;

public class NewWaveAnimationController : MonoBehaviour {

    [SerializeField]
    UILabel waveNumber;

    [SerializeField]
    UILabel weaponName;

    [SerializeField]
    UILabel weaponDesr;

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
    }
}
