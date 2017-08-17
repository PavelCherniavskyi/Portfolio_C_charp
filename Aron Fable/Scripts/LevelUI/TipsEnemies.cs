using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsEnemies : MonoBehaviour {

    public LayerMask[] layerMask;
	private GameObject enemiesTipsSide;

	private void Start()
	{
		enemiesTipsSide = GameObject.Find("EnemyTipSidePref/Background");
        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        yield return new WaitForSeconds(1f);
        Collider[] temp = Physics.OverlapSphere(transform.position, 8f, layerMask[0].value);
        if (temp.Length != 0)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == null) continue;
                if (GameController.CurrentPlayerProfile.activeEnemies[temp[i].GetComponent<UnitOptions>().EnemyType] == false)
                {
                    GameController.CurrentPlayerProfile.activeEnemies[temp[i].GetComponent<UnitOptions>().EnemyType] = true;
                    GameObject tip = Instantiate(Resources.Load("Prefabs/Interface/EnemyPopUp") as GameObject);
                    tip.GetComponent<EnemyPopUp>().SetType(temp[i].GetComponent<UnitOptions>().EnemyType);
                    tip.transform.SetParent(enemiesTipsSide.transform);

                    if (GameController.Sound)
                        Destroy(Instantiate(SoundBank.TipPopUp), 1);
                }
            }
        }
        StartCoroutine(Check());
    }
}
