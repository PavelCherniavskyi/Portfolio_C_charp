using UnityEngine;
using System.Collections;

public class AIFollow : MonoBehaviour {
    private LevelController _LevelController;
    public GameObject _GameController;
    public GameObject Hero;
    private UnitOptions ComponentUnitOptions;
    private MoveController ComponentMoveController;
    private Attack_System ComponentAttack_System;

    void Start () {
        Hero = GameObject.Find("Hero");
        Hero.GetComponent<HeroInfo>().units.Add(gameObject);
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        _GameController = GameObject.Find("GameController");
        ComponentUnitOptions = gameObject.GetComponent<UnitOptions>();
        ComponentMoveController = GetComponent<MoveController>();
        ComponentAttack_System = GetComponent<Attack_System>();
    }

	void Update () {
        if (ComponentAttack_System.active == false && ComponentUnitOptions.Autopilot == true)
            StandOnThePosition();

        // Включение и отключение управления призваных персонажей
        if (_LevelController.pause == true && ComponentUnitOptions.Autopilot == true)
        {
            ComponentUnitOptions.Autopilot = false;
        }
        else if (_LevelController.pause == false && ComponentUnitOptions.Autopilot == false)
        {
            ComponentUnitOptions.Autopilot = true;
        }

        if (_LevelController.pause == true)
        {
            if (transform.GetComponent<Attack_System>().active == false) LookTarget();
        }
    }

    public void StandOnThePosition()
    {
        try
        {
            if (gameObject)
            {
                int index = Hero.GetComponent<HeroInfo>().units.IndexOf(gameObject);
                ComponentMoveController.Destination(Hero.GetComponent<HeroInfo>().positions[index], true);
            }
        }
        catch
        {
            Debug.Log("Ошибка StandOnThePosition - Выход за приделы массива");
        }
        
    }

    public void LookTarget()
    {

        for (int i = 0; i < Hero.GetComponent<HeroInfo>().units.Count; i++)
        {
            if (Hero.GetComponent<HeroInfo>().units[i] != gameObject && Hero.GetComponent<HeroInfo>().units[i] != null && Hero.GetComponent<HeroInfo>().units[i].GetComponent<Attack_System>().target != null && Hero.GetComponent<HeroInfo>().units[i].GetComponent<Attack_System>().target.GetComponent<UnitOptions>().isDead == false)
            {
                transform.GetComponent<Attack_System>().SetTarget(Hero.GetComponent<HeroInfo>().units[i].GetComponent<Attack_System>().target);
            }
            else continue;
        }

      
    }
}
