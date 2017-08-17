using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyPopUp : MonoBehaviour {


	public EnemiesID EnemyType;

    public void SetType(EnemiesID type)
    {
        EnemyType = type;
    }

	void Start () {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.TipPopUp), 1);
    }

	public void ClickEvent()
	{
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameObject window = Instantiate (Resources.Load ("Prefabs/Interface/EnemyTipPref") as GameObject);
		window.GetComponent<EnemyTip>().UpgradeStatus(EnemyType);
        GameObject.Find("Settings").GetComponent<SettingsMenu>().Pause(true);
        Destroy (gameObject);
        
    }

}
