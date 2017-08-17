using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;


public enum CharacterClass { warrior, archer, mage, rogue, other };

public enum damage_type { normal, penetrating, magical, clean };

public enum protection_type { undefended, light, medium, heavy, magical, hero };

public enum attack_type { unarmed, infighting, distant_battle };

public enum unit_status { normal, stunned}

public class UnitOptions : MonoBehaviour
{
    public string name = "none";
    public bool IsEnemy = false;
    public UnitsID UnitsType; 
    public EnemiesID EnemyType = EnemiesID.no; 
    public Rank rankid = Rank.star0;
    public string describingText; /// инициализировать.
    public string shell = "Prefabs/Units/arrow";
    public float shellspeed = 2f; 
    public float ping = 0f;
    public CharacterClass _CharacterClass = CharacterClass.warrior;
    public int MaxHealth = 600;
    public int health;
    public int MaxMana = 0;
    public int mana;
    public float movespeed = 1;
    public bool Attacking = true;
    public float attack_speed = 0.1f;
    public float reload_time = 1.5f;
    public int Accuracy_infighting = 100;
    public int Accuracy_distant_battle = 50;
    public float Сritical_chance = 25;
    public float Сritical_factor = 3;
    public float reload = 0;
    public damage_type _damage_type = damage_type.normal;
    public Vector2 damage;
    public int protection = 0;
    public protection_type _protection_type = protection_type.undefended;
    public attack_type _attack_type = attack_type.infighting;
    public float range_infighting = 0.8f;
    public float range_distant = 5f;
    public int magic_resistance = 0;
    public bool ImmunityToMagic = false;
    public float aggression_range = 0f; 
    public float aggression_damage = 1f;
    public int bonusEXP = 0; 
    public int bonusMP = 0;
    public bool Autopilot = true;
    public bool isSleeping = false;
    public bool isDead = false;

    public unit_status status = unit_status.normal;

    public List<GameObject> EnemyPack1 = new List<GameObject>();
    public List<float> EnemyPack2 = new List<float>();

    private Slider HPBar;

    public bool AutoLoadOptions = true;

    private GameplaySoundHelper _GameplaySoundHelper;

    private GameObject upgradeIcon;

    private LevelController _LevelController;
    public Sprite iconImage;

    public float CapsuleCollider = 0.3f;

    private GameObject StunEffect;

    public float HitEffectSleeping = 2f;

    private void Start()
    {
        if (AutoLoadOptions == true && IsEnemy == false)
            Upgrade(UnitsType, 0);

        health = MaxHealth;
        mana = MaxMana;
        HPBar = transform.Find("UICanvas/HPBar").GetComponent<Slider>();
        _GameplaySoundHelper = GameObject.Find("GameController").GetComponent<GameplaySoundHelper>();
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();

        if (isSleeping)
        {
            GetComponent<AnimationController>().ChangeStatus(AnimationStatus.sleep);
        }

        CapsuleCollider = GetComponent<CapsuleCollider>().radius;
    }

    public void Upgrade(UnitsID id, Rank rank)
    {
        rankid = rank;
        name = CharactersData.characterInfo[id].rankData[Rank.star0].titleText;
        MaxHealth = (int)CharactersData.characterInfo[id].rankData[rank].MaxHealth;
        MaxMana = (int)CharactersData.characterInfo[id].rankData[rank].MaxMana;
        attack_speed = CharactersData.characterInfo[id].rankData[rank].AttackSpeedNumber;
        damage = CharactersData.characterInfo[id].rankData[rank].Damage;
        protection = (int)CharactersData.characterInfo[id].rankData[rank].ProtectionNumber;
        magic_resistance = (int)CharactersData.characterInfo[id].rankData[rank].MagicResistance;
        health = MaxHealth;
        mana = MaxMana;
    }

    private void Update()
    {
        if (isDead == false)
        {
            if (health <= 0)
            {
                GetComponent<CapsuleCollider>().enabled = false;
                isDead = true;
                if (GetComponent<AnimationController>() != null) GetComponent<AnimationController>().ChangeStatus(AnimationStatus.dead);
                _GameplaySoundHelper.SoundDead(gameObject);
                if (GetComponent<InstantiateEffectController>() != null && IsEnemy) GetComponent<InstantiateEffectController>().EffectDead();
                gameObject.layer = 0;
                gameObject.transform.SetParent(GameObject.Find("DeadPack").transform);
                StartCoroutine(Dead(3f));
                if (StunEffect != null) Destroy(StunEffect);
            }
            if (reload > 0)
            {
                reload -= Time.deltaTime;
                if (reload < 0)
                    reload = 0;
            }
            HPBar.value = health * 100 / MaxHealth;
            if (HitEffectSleeping > 0) HitEffectSleeping -= Time.deltaTime;
        }
    }

    private IEnumerator Dead(float time)
    {
        yield return new WaitForSeconds(time);
        if (gameObject.name == "Hero")
        {
            GameObject.Find("LevelControllerPref").GetComponent<LevelController>().Gameover();
        }
        else
        {
            if (IsEnemy == true)
            {
                _LevelController.Exp += bonusEXP;
                _LevelController.ManaAdd(bonusMP);
            }
            Destroy(gameObject);
        }
    }

    public void Reload()
    {
        reload = reload_time / attack_speed;
    }

    public void AcceptAggression(GameObject attacking, float _aggression)
    {
        #region Добавление в список агрессии
        if (transform.tag == "Enemy")
        {
            if (EnemyPack1.Contains(attacking))
            {
                EnemyPack2[EnemyPack1.IndexOf(attacking)] += _aggression;
            }
            else
            {
                EnemyPack1.Add(attacking);
                EnemyPack2.Add(0);
            }
            ControlAggression(); // Вызываем проверку для смены таргета по уровню агрессии
        }
        #endregion
    }

    public void AcceptDamage(float damage, damage_type type)
    {
        HitEffect();

        int result = 0;

        if (type == damage_type.normal) // обычный урон
        {
            result = (int)damage - (protection * 10 / 100); // 1 защиты == блокировка 10% урона
        }
        else if (type == damage_type.magical) // магический
        {
            result = (int)damage - (magic_resistance * 10 / 100); // 1 магической защиты == блокировка 10% урона
        }
        else if (type == damage_type.penetrating) // проникающий
        {
            result = (int)damage - (((protection * 10) / 100) / 2); // принцип такой же как и с обычной защитов, только на 50% сильнее
        }
        else if (type == damage_type.clean) // чистый
        {
            result = (int)damage; // игнорирует защиту
        }

        if (result > 0)
        {
            health -= result;
            transform.Find("UICanvas/HPBar").GetComponent<Slider>().value = health * 100 / MaxHealth;
        }
    }

    public void AcceptDamage(GameObject attacking, bool crit, int factor = 1)
    {
        HitEffect();
        UnitOptions AUO = attacking.GetComponent<UnitOptions>();

        #region Добавление в список агрессии
        if (transform.tag == "Enemy")
        {
            if (EnemyPack1.Contains(attacking))
            {
                EnemyPack2[EnemyPack1.IndexOf(attacking)] += AUO.aggression_damage;
            }
            else
            {
                EnemyPack1.Add(attacking);
                EnemyPack2.Add(0);
            }
            ControlAggression(); // Вызываем проверку для смены таргета по уровню агрессии
        }
        #endregion

        #region Расчет урона
        if (health > 0)
        {
            int result = 0;
            int currentdamage = 0;
            if (crit == true) currentdamage = (int)UnityEngine.Random.Range(AUO.damage.x * AUO.Сritical_factor, AUO.damage.y * AUO.Сritical_factor);
            else currentdamage = (int)UnityEngine.Random.Range(AUO.damage.x, AUO.damage.y);

            if (AUO._damage_type == damage_type.normal) // обычный урон
            {
                result = currentdamage - (currentdamage * (protection * 10) / 100); // 1 защиты == блокировка 10% урона
            }
            else if (AUO._damage_type == damage_type.magical) // магический
            {
                result = currentdamage - (currentdamage * (magic_resistance * 10) / 100); // 1 магической защиты == блокировка 10% урона
            }
            else if (AUO._damage_type == damage_type.penetrating) // проникающий
            {
                result = currentdamage - ((currentdamage * (protection * 10) / 100) / 2); // принцип такой же как и с обычной защитов, только на 50% сильнее
            }
            else if (AUO._damage_type == damage_type.clean) // чистый
            {
                result = currentdamage; // игнорирует защиту
            }


            if (result > 0)
            {
                result = result / factor;
                health -= result;
                transform.Find("UICanvas/HPBar").GetComponent<Slider>().value = health * 100 / MaxHealth;
            }
        }
        #endregion
    }

    private void HitEffect()
    {
        Destroy((GameObject)Instantiate(Resources.Load("Effects/BloodEffects/Hit" + UnityEngine.Random.Range(1, 8).ToString()), transform.position + new Vector3(UnityEngine.Random.Range(-0.25f, 0.25f), UnityEngine.Random.Range(-0.25f, 0.25f), 0), Quaternion.identity), 1f);
    }

    public void ControlAggression()
    {
        int index = 0;
        float max = 0;

        for (int i = 0; i < EnemyPack1.Count; i++)
        {
            if (EnemyPack1[i] == null)
            {
                EnemyPack1.RemoveAt(i);
                EnemyPack2.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < EnemyPack1.Count; i++)
        {
            if (EnemyPack2[i] > max)
            {
                index = i;
                max = EnemyPack2[i];
            }
        }
        GetComponent<Attack_System>().target = EnemyPack1[index];
    }

    public void AggressionActivate()
    {
        aggression_range = 7;
    }
    public void AggressionStop()
    {
        aggression_range = 0;
    }

    public void CreateUpdateIcon()
    {
        if (upgradeIcon == null)
        {
            if ((int)rankid < 4)
            {
                upgradeIcon = Instantiate((GameObject)Resources.Load("Prefabs/Interface/UpgradeIcon 1"));
                upgradeIcon.transform.SetParent(gameObject.transform);
                upgradeIcon.transform.position = new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z);
            }
            else DestroyUpgradeIcon();
        }
        else if ((int)rankid > 3) DestroyUpgradeIcon();
    }

    public void DestroyUpgradeIcon()
    {
        if (upgradeIcon != null)
            Destroy(upgradeIcon);
    }

    public void SetStunned(float time)
    {
        if (status != unit_status.stunned && !isDead && !ImmunityToMagic)
        {
            status = unit_status.stunned;

            StartCoroutine(StatusController(time));
            GetComponent<AnimationController>().ChangeStatus(AnimationStatus.stunned);
            if (StunEffect != null) Destroy(StunEffect);
            StunEffect = Instantiate(Resources.Load("Effects/EffectStunned") as GameObject);
            StunEffect.transform.SetParent(transform.FindChild("Sprite/head/point overhead").transform);
            StunEffect.transform.localPosition = new Vector3(0, 0, 0);
            Destroy(StunEffect, time);
        }
    }

    private IEnumerator StatusController(float time)
    {
        yield return new WaitForSeconds(time);
        reload = 1;
        status = unit_status.normal;
        if (!isDead)
        {
            GetComponent<AnimationController>().ChangeStatus(AnimationStatus.stand);
        }
    }
}