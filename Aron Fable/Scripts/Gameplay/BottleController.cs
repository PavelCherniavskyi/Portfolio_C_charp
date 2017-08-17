using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour {
    private GameObject Hero;
    public string Bottle = "HealBottle";
    public string Effect = "RED";

    void Start ()
    {
        Hero = GameObject.Find("Hero");
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 0);
    }

	void Update () {
		if (Vector3.Distance(Hero.transform.position, transform.position) < 5f)
        {
            CreatheHealBottle();
        }
	}

    public void CreatheHealBottle()
    {
        Destroy(Instantiate(Resources.Load("Effects/BottleInst" + Effect) as GameObject, transform.FindChild("point").transform.position, Quaternion.identity), 3f);
        Instantiate(Resources.Load("Effects/" + Bottle) as GameObject, transform.FindChild("point").transform.position, Quaternion.identity);
        if (GameController.Sound) Destroy(Instantiate(Resources.Load("Sound/Gameplay/Prefabs/BottleCreate")), 3f);
        Destroy(gameObject);
    }
}
