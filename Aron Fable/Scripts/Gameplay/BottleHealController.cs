using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHealController : MonoBehaviour {
    private GameObject Hero;
    private int healstep = 5;
    private int steps = 50;
    private bool active = false;

    void Start () {
        Hero = GameObject.Find("Hero");
    }
	
    public void Click()
    {
        if (!active)
        {
            active = true;
            GetComponent<Animator>().Play("dead");
            Destroy(Instantiate(Resources.Load("Effects/BottleUseRED") as GameObject, transform.position, Quaternion.identity), 3f);
            StartCoroutine(Heal(0));
            Destroy(Instantiate(Resources.Load("Sound/Gameplay/Prefabs/BottleSound")), 1.5f);
            transform.FindChild("Effect").gameObject.SetActive(false);
        }
    }

    private IEnumerator Heal(int count)
    {
        yield return new WaitForSeconds(0.03f);
        count++;

        if (Hero.GetComponent<UnitOptions>().health + healstep < Hero.GetComponent<UnitOptions>().MaxHealth)
        {
            Hero.GetComponent<UnitOptions>().health += healstep;
        }

        for (int i = 0; i < Hero.GetComponent<HeroInfo>().units.Count; i++)
        {
            if (Hero.GetComponent<HeroInfo>().units[i].GetComponent<UnitOptions>().health + healstep < Hero.GetComponent<HeroInfo>().units[i].GetComponent<UnitOptions>().MaxHealth)
            {
                Hero.GetComponent<HeroInfo>().units[i].GetComponent<UnitOptions>().health += healstep;
            }
        }
        if (count < steps)
            StartCoroutine(Heal(count));
        else Destroy(gameObject);
    }
}
