using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BottleSpellController : MonoBehaviour {

    public void Click()
    {
        GetComponent<Animator>().Play("dead");
        Destroy(Instantiate(Resources.Load("Effects/BottleUseYELLOW") as GameObject, transform.position, Quaternion.identity), 3f);
        GameObject.Find("LevelControllerPref").GetComponent<LevelController>().spellPoint++;
        GameObject.Find("UI/hero icon/spellpoint/spellpointtext").GetComponent<Text>().text = GameObject.Find("LevelControllerPref").GetComponent<LevelController>().spellPoint.ToString();
        Destroy(Instantiate(Resources.Load("Sound/Gameplay/Prefabs/BottleSound")), 1.5f);
        Destroy(gameObject);
    }
}
