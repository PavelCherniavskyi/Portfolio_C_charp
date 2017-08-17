using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvents01 : MonoBehaviour {

    public GameObject Hero;
    public GameObject _GameController;
    public bool active = false;
    GameObject Enemyes;
    public bool sleepingParty = false;
    private LevelController _LevelController;

    void Start () {
        Hero = GameObject.Find("Hero");
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        _GameController = GameObject.Find("GameController");
        Enemyes = transform.GetChild(0).gameObject;
        transform.FindChild("AOE").gameObject.SetActive(false);
    }

    void Activate(GameObject target)
    {
        if (Hero.GetComponent<Attack_System>().active == false) Hero.GetComponent<MoveController>().Stop();
        //Hero.GetComponent<Attack_System>().DropTarget();
        Hero.GetComponent<UnitOptions>().AggressionActivate();
        for (int i = 0; i < Hero.GetComponent<HeroInfo>().units.Count; i++)
            if (Hero.GetComponent<HeroInfo>().units[i] != null) Hero.GetComponent<HeroInfo>().units[i].GetComponent<UnitOptions>().AggressionActivate();

        Hero.GetComponent<HeroInfo>().EventsActive++;
        _LevelController.GetComponent<LevelController>().pause = true;
        active = true;

        GameObject.Find("UI/CameraMoveToHero").GetComponent<CameraMoveToHero>().SetEventActive(gameObject);

        #region Реализация нападения на Hero
        for (int i = 0; i < Enemyes.transform.childCount; i++)
        {
            Enemyes.transform.GetChild(i).gameObject.layer = 9;
            Enemyes.transform.GetChild(i).GetComponent<UnitOptions>().AggressionActivate();
            Enemyes.transform.GetChild(i).GetComponent<Attack_System>().SetTarget(Hero); // Атаковать Hero
        }
        #endregion
    }

    void Update()
    {
        if (active == false)
        {
            // Провекра на участие в бою хотя бы одного члена группы
            if (sleepingParty == false)
            {
                for (int i = 0; i < Enemyes.transform.childCount; i++)
                {
                    if (Enemyes.transform.GetChild(i).GetComponent<Attack_System>().target != null)
                    {
                        Activate(Enemyes.transform.GetChild(i).GetComponent<Attack_System>().target);
                        break;
                    }
                }
            }

            // Hero подошел на 5 метров
            if (Vector3.Distance(Hero.transform.position, transform.position) < 5f && active == false)
            {
                if (sleepingParty == false)
                {
                    Activate(Hero);
                }
                else if (sleepingParty == true)
                {
                    for (int i = 0; i < Enemyes.transform.childCount; i++)
                    {
                        Enemyes.transform.GetChild(i).GetComponent<AnimationController>().ChangeStatus(AnimationStatus.awakening);
                        _GameController.GetComponent<GameplaySoundHelper>().SoundAwakening(Enemyes.transform.GetChild(i).gameObject);
                    }
                    active = true;
                    if (Hero.GetComponent<Attack_System>().active == false) Hero.GetComponent<MoveController>().Stop();
                    StartCoroutine(Awakening(1f));
                }
            }
        }
        else if (active == true) // Условия прохождения Event
        {
            if (transform.GetChild(0).transform.childCount == 0)
            {
                Hero.GetComponent<HeroInfo>().EventsActive--;
                if (Hero.GetComponent<HeroInfo>().EventsActive == 0) // Если Hero не участвует в других эвентах то...
                {
                    Hero.GetComponent<HeroInfo>().bStandOnThePosition = true;
                    Hero.GetComponent<UnitOptions>().AggressionStop();
                    for (int i = 0; i < Hero.GetComponent<HeroInfo>().units.Count; i++)
                    {
                        if (Hero.GetComponent<HeroInfo>().units[i] != null) Hero.GetComponent<HeroInfo>().units[i].GetComponent<UnitOptions>().AggressionStop();
                    }
                    GameObject.Find("UI/CameraMoveToHero").GetComponent<CameraMoveToHero>().DropEvent();
                }
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Awakening(float time)
    {
        yield return new WaitForSeconds(time);
        Activate(Hero);
    }
}
