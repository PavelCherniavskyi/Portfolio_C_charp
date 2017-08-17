using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleManaController : MonoBehaviour {

    public int AddMana = 70;

    public void Click()
    {
        GetComponent<Animator>().Play("dead");
        Destroy(Instantiate(Resources.Load("Effects/BottleUseBLUE") as GameObject, transform.position, Quaternion.identity), 3f);
        GameObject.Find("LevelControllerPref").GetComponent<LevelController>().ManaAdd(AddMana);
        Destroy(Instantiate(Resources.Load("Sound/Gameplay/Prefabs/BottleSound")), 1.5f);
        Destroy(gameObject);
    }

}
