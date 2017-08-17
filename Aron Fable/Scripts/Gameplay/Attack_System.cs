using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Attack_System : MonoBehaviour
{
    private GameObject _GameController;
    private LevelController _LevelController;
    public LayerMask[] layerMask; // Через редактор принимает Layer персонажей
    private UnitOptions ComponentUnitOptions; // Принимает компонент для управления параметрами персонажа
    private AnimationController ComponentAnimationController;
    private MoveController ComponentMoveController;
    private RaycastHit hit;
    private float SleepingChangeRotation = 0f;

    public bool active = false;
    public GameObject target;
    private float pause;

    private bool bCrit = false;
    public bool bAnimSleep = false;

    static GameObject Hero;

    private bool soundAggression = false;

    public float alter_ping = 0.1f;

    private float originRange = 0;

    public bool MultiShot = false;
    public float MultiRange = 3f;

    void Start()
    {
        originRange = Vector3.Distance(transform.position, transform.FindChild("origin").transform.position);
        Hero = GameObject.Find("Hero");
        _GameController = GameObject.Find("GameController");
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        ComponentUnitOptions = gameObject.GetComponent<UnitOptions>();
        ComponentAnimationController = GetComponent<AnimationController>();
        ComponentMoveController = GetComponent<MoveController>();
    }

    void Update()
    {
        if (ComponentUnitOptions.isDead == false && bAnimSleep == false && _LevelController.globalpause == false && ComponentMoveController.IgnoreAutoAttack == false)
        {
            if (ComponentMoveController.IgnoreAutoAttack == false && ComponentAnimationController.status != AnimationStatus.focus && ComponentUnitOptions.status == unit_status.normal)
            {
                Search();
            }

            #region Модификация внешности
            if (ComponentUnitOptions.EnemyType == EnemiesID.potsik && target != null) 
            {
                gameObject.transform.FindChild("Sprite/head/head rage").gameObject.SetActive(true);
            }
            else if (ComponentUnitOptions.EnemyType == EnemiesID.potsik && target == null)
            {
                gameObject.transform.FindChild("Sprite/head/head rage").gameObject.SetActive(false);
            }
            #endregion
        }
        if (SleepingChangeRotation > 0) SleepingChangeRotation -= Time.deltaTime;
    }

    void HeroInviolability()
    {
        #region AI - Если цель герой, а его сумоны еще живы - то смениться таргет на кого-то из них
        if (target == Hero)
        {
            if (Hero.GetComponent<HeroInfo>().units.Count != 0)
            {
                //Debug.Log("Смена цели");
                target = Hero.GetComponent<HeroInfo>().units[Random.Range(0, Hero.GetComponent<HeroInfo>().units.Count)];
            }
        }
        #endregion
    }

    void Search()
    {
        if (active == false) // Если атакующий режим не активен...
        {
            float distance = ComponentUnitOptions.aggression_range;
            Collider[] temp = Physics.OverlapSphere(transform.position, distance, layerMask[0].value);
            if (temp.Length > 0)
            {
                active = true; // Переводим объект в активный режим атаки
                #region Поиск ближайшего врага
                List<float> range = new List<float>();
                float min = 1000;
                int index = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    range.Add(Vector3.Distance(temp[i].transform.position, transform.position));
                }
                for (int i = 0; i < temp.Length; i++)
                {
                    if (range[i] < min)
                    {
                        min = range[i];
                        index = i;
                    }
                }
                target = temp[index].gameObject; // Определили таргета
                if (target.GetComponent<UnitOptions>().isDead == true) return;
                HeroInviolability();
                if (soundAggression == false)
                {
                    soundAggression = true;
                    _GameController.GetComponent<GameplaySoundHelper>().SoundAggression(gameObject);
                }
                #endregion
            }
        }

        if (active == true) // Если объект находится в активном режиме, то вызываем реализацию атаки
            Attack();
    }

    public void SetTarget(GameObject obj)
    {
        target = obj;
        active = true;
    }

    public void DropTarget()
    {
        active = false;
    }

    void Attack()
    {
        try
        {
            if (target != null)
            {
                HeroInviolability(); // Проверка на необходимость смены цели при условии что у Hero еще живы союзники
                UnitOptions TargetUnitOptions = target.GetComponent<UnitOptions>(); // Создаем временную переменную компонента UnitOptions для цели
                if (TargetUnitOptions.isDead == false)
                {
                    #region Реализация атаки

                    float range = 0; // Переменная которая хранит в себе расстояние на которое нужно приблизиться для атаки
                    if (ComponentUnitOptions._attack_type == attack_type.infighting) range = ComponentUnitOptions.range_infighting;
                    else if (ComponentUnitOptions._attack_type == attack_type.distant_battle) range = ComponentUnitOptions.range_distant;

                    // Точка с учетом радиуса
                    Vector3 tempposition = GlobalFunctions.offset_point(target.transform.FindChild("origin").transform.position, transform.FindChild("origin").transform.position, TargetUnitOptions.CapsuleCollider);

                    if (Vector3.Distance(transform.FindChild("origin").transform.position, tempposition) >= range) // Если расстояние больше чем range, то двигаться к объекту
                    {
                        ComponentMoveController.Destination(new Vector3(tempposition.x, tempposition.y + originRange, transform.position.z), true);

                        //if (pause <= 0) // Повторное движение через N времени
                        //{
                        //    //
                        //    pause = 0.5f;
                        //}
                        //else pause -= Time.deltaTime;
                    }
                    else if (Vector3.Distance(transform.FindChild("origin").transform.position, tempposition) < range)
                    {
                        #region Нанесение урона
                        ComponentMoveController.Stop();
                        // Принудительный поворот к таргету
                        GameObject Sprite = transform.GetChild(0).gameObject;
                        if (SleepingChangeRotation <= 0 && ComponentMoveController.IgnoreAutoAttack == false)
                        {
                            SleepingChangeRotation = 0.1f;
                            if (target.transform.position.x > transform.position.x) Sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
                            else if (target.transform.position.x < transform.position.x) Sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
                        }

                        if (ComponentUnitOptions.reload <= 0) // Если перезарядка "готова" - нанести удар
                        {
                            #region Доп. способности

                            #region Knight
                            if (ComponentUnitOptions.UnitsType == UnitsID.knight)
                            {
                                PassiveSpellStun spell = GetComponent<PassiveSpellStun>();
                                if (spell.CheckChance())
                                {
                                    ComponentAnimationController.ChangeStatus(AnimationStatus.alter_attack_1);
                                    StartCoroutine(UseSpell(TargetUnitOptions, spell.ping, spell.time, "stunned", 0, spell.damage));
                                    ComponentUnitOptions.Reload();
                                    bAnimSleep = true;
                                    ComponentMoveController.IgnoreAutoAttack = false;
                                    StartCoroutine(SleepingAnimation());
                                    return;
                                }
                            }
                            #endregion

                            #region BearBoss
                            if (ComponentUnitOptions.EnemyType == EnemiesID.bearBoss)
                            {
                                if (GetComponent<PassiveSpellMassStun>().CheckChance())
                                {
                                    PassiveSpellMassStun spell = GetComponent<PassiveSpellMassStun>();
                                    ComponentAnimationController.ChangeStatus(AnimationStatus.alter_attack_1);
                                    StartCoroutine(UseSpell(TargetUnitOptions, spell.ping, spell.time, "massstunned", spell.range, spell.damage));
                                    ComponentUnitOptions.Reload();
                                    bAnimSleep = true;
                                    ComponentMoveController.IgnoreAutoAttack = false;
                                    StartCoroutine(SleepingAnimation());
                                    return;
                                }
                                else if (GetComponent<PassiveSpellKick>().CheckChance())
                                {
                                    PassiveSpellKick spell = GetComponent<PassiveSpellKick>();
                                    ComponentAnimationController.ChangeStatus(AnimationStatus.alter_attack_2);
                                    StartCoroutine(UseSpell(TargetUnitOptions, spell.ping, spell.time, "kick", 0, spell.damage));
                                    ComponentUnitOptions.Reload();
                                    bAnimSleep = true;
                                    ComponentMoveController.IgnoreAutoAttack = false;
                                    StartCoroutine(SleepingAnimation());
                                    return;
                                }
                            }
                            #endregion

                            #region Archer
                            //if (ComponentUnitOptions.UnitsType == UnitsID.archer)
                            //{
                            //    PassiveSpellStun spell = GetComponent<PassiveSpellStun>();
                            //    if (spell.CheckChance())
                            //    {
                            //        ComponentAnimationController.ChangeStatus(AnimationStatus.alter_attack_1);
                            //        StartCoroutine(UseSpell(TargetUnitOptions, spell.ping, spell.time, "stunned", 0, spell.damage));
                            //        ComponentUnitOptions.Reload();
                            //        bAnimSleep = true;
                            //        ComponentMoveController.IgnoreAutoAttack = false;
                            //        StartCoroutine(SleepingAnimation());
                            //        return;
                            //    }
                            //}
                            #endregion

                            #endregion

                            #region Обычная атака
                            int rand = Random.Range(0, 100);
                            bool alter_check = false;

                            #region Определение анимации
                            if (ComponentAnimationController != null)
                            {
                                if (ComponentUnitOptions._attack_type == attack_type.distant_battle)
                                {
                                    if (Vector3.Distance(transform.position, tempposition) <= ComponentUnitOptions.range_infighting)
                                    {
                                        ComponentAnimationController.ChangeStatus(AnimationStatus.attack_alter);
                                        alter_check = true;
                                    }
                                    else
                                    {
                                        if (rand <= ComponentUnitOptions.Сritical_chance)
                                        {
                                            GetComponent<CritEffect>().Create();
                                            ComponentAnimationController.ChangeStatus(AnimationStatus.critical);
                                            bCrit = true;
                                        }
                                        else
                                        {
                                            ComponentAnimationController.ChangeStatus(AnimationStatus.attack);
                                            bCrit = false;
                                        }
                                    }
                                }
                                else
                                {
                                    if (rand <= ComponentUnitOptions.Сritical_chance)
                                    {
                                        GetComponent<CritEffect>().Create();
                                        ComponentAnimationController.ChangeStatus(AnimationStatus.critical);
                                        bCrit = true;
                                    }
                                    else
                                    {
                                        ComponentAnimationController.ChangeStatus(AnimationStatus.attack);
                                        bCrit = false;
                                    }
                                }
                            }
                            #endregion

                            #endregion

                            ComponentUnitOptions.Reload();
                            bAnimSleep = true;
                            StartCoroutine(SleepingAnimation());

                            _GameController.GetComponent<GameplaySoundHelper>().SoundAttack(gameObject);

                            if (ComponentUnitOptions._attack_type == attack_type.infighting || (ComponentUnitOptions._attack_type == attack_type.distant_battle && alter_check == true))
                            {
                                if (ComponentUnitOptions._attack_type == attack_type.infighting) StartCoroutine(DamageDelay(TargetUnitOptions, ComponentUnitOptions.ping / ComponentUnitOptions.attack_speed));
                                else if (ComponentUnitOptions._attack_type == attack_type.distant_battle) StartCoroutine(DamageDelay(TargetUnitOptions, alter_ping / ComponentUnitOptions.attack_speed, 2)); // Если лучник атакует в ближнем бою то урон / на factor (2)
                            }
                            else if (ComponentUnitOptions._attack_type == attack_type.distant_battle && alter_check == false)
                            {
                                StartCoroutine(DamageDelayRange(TargetUnitOptions, ComponentUnitOptions.ping / ComponentUnitOptions.attack_speed));
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else DropTarget();
            }
            else
            {
                DropTarget();
            }
        }
        catch
        {
            DropTarget();
            Debug.Log("Ошибка Attack_System");
        }
    }

    private IEnumerator UseSpell(UnitOptions TargetUnitOptions, float ping, float time, string spell, float range = 0, float damage = 0)
    {
        yield return new WaitForSeconds(ping);
        if (ComponentAnimationController.status != AnimationStatus.walk && ComponentUnitOptions.status == unit_status.normal)
        {
            if (spell == "stunned")
            {
                _GameController.GetComponent<GameplaySoundHelper>().SoundHit(target);
                if (TargetUnitOptions != null)
                {
                    TargetUnitOptions.SetStunned(time);
                    TargetUnitOptions.AcceptDamage(damage, damage_type.normal);
                }
            }
            else if (spell == "massstunned")
            {
                Destroy(Instantiate(Resources.Load("Effects/EffectBossDust") as GameObject, transform.FindChild("Sprite/point weapon").transform.position, Quaternion.identity), 3f);
                CameraShaker.MakeShake2();

                Collider[] temp = Physics.OverlapSphere(transform.FindChild("Sprite/point weapon").transform.position, range, layerMask[0].value);

                if (temp.Length > 0)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].GetComponent<UnitOptions>() != null)
                        {
                            temp[i].GetComponent<UnitOptions>().SetStunned(time);
                            temp[i].GetComponent<UnitOptions>().AcceptDamage(damage, damage_type.normal);
                        }
                    }
                }
            }
            else if (spell == "kick")
            {

                if (TargetUnitOptions != null)
                {
                    TargetUnitOptions.SetStunned(time);
                    TargetUnitOptions.AcceptDamage(damage, damage_type.normal);
                    GetComponent<PassiveSpellKick>().PlayKick(target);
                    DropTarget();
                }
            }
        }
    }

    private IEnumerator DamageDelay(UnitOptions TargetUnitOptions, float timer, int factor = 1)
    {
        yield return new WaitForSeconds(timer);
        if (ComponentAnimationController.status != AnimationStatus.walk)
        {
            _GameController.GetComponent<GameplaySoundHelper>().SoundHit(target);
            if (TargetUnitOptions != null) TargetUnitOptions.AcceptDamage(gameObject, bCrit);

        }
    }

    private IEnumerator DamageDelayRange(UnitOptions TargetUnitOptions, float timer, int factor = 1)
    {
        yield return new WaitForSeconds(timer);
        if (ComponentAnimationController.status != AnimationStatus.walk)
        {
            Vector3 shellstartposition = transform.position;

            #region Проверки на необходимость создания снаряда в спец. позиции
            if (ComponentUnitOptions.EnemyType == EnemiesID.littleWizard) shellstartposition = transform.FindChild("Sprite/hand right/weapon/shellpoint").gameObject.transform.position;
            else if (ComponentUnitOptions.UnitsType == UnitsID.archer) shellstartposition = transform.FindChild("Sprite/weapon/shellpoint").gameObject.transform.position;
            else if (ComponentUnitOptions.UnitsType == UnitsID.puppeteer) shellstartposition = transform.FindChild("Sprite/doll 2/shellpoint").gameObject.transform.position;
            #endregion

            if (MultiShot) // стрелять по всем
            {
                Collider[] temp = Physics.OverlapSphere(target.transform.position, MultiRange, layerMask[0].value);
                if (temp.Length != 0)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        GameObject shall = Instantiate(Resources.Load(ComponentUnitOptions.shell), shellstartposition, Quaternion.identity) as GameObject;

                        if (shall.GetComponent<ArrowController>() == null)
                        {
                            if (CheckMissing(attack_type.distant_battle)) shall.GetComponent<ArrowController2>().SetTransforms(gameObject, shellstartposition, GlobalFunctions.offset_point(temp[i].gameObject.transform.FindChild("origin").transform.position, Random.Range(0, 360), Random.Range(0.8f, 1.5f)), bCrit); // Стрельба навесом
                            else shall.GetComponent<ArrowController2>().SetTransforms(gameObject, shellstartposition, temp[i].gameObject, bCrit); // Стрельба навесом
                        }
                        else shall.GetComponent<ArrowController>().SetTransforms(gameObject, temp[i].gameObject, bCrit); // Обычная стрельба
                    }
                }
            }
            else
            {
                GameObject shall = Instantiate(Resources.Load(ComponentUnitOptions.shell), shellstartposition, Quaternion.identity) as GameObject;

                if (shall.GetComponent<ArrowController>() == null)
                {
                    if (CheckMissing(attack_type.distant_battle)) shall.GetComponent<ArrowController2>().SetTransforms(gameObject, shellstartposition, GlobalFunctions.offset_point(target.transform.FindChild("origin").transform.position, Random.Range(0, 360), Random.Range(0.8f, 1.5f)), bCrit); // Стрельба навесом
                    else shall.GetComponent<ArrowController2>().SetTransforms(gameObject, shellstartposition, target, bCrit); // Стрельба навесом
                }
                else shall.GetComponent<ArrowController>().SetTransforms(gameObject, target, bCrit); // Обычная стрельба
            } 
        }
    }

    private IEnumerator SleepingAnimation()
    {
        yield return new WaitForSeconds((ComponentUnitOptions.ping / ComponentUnitOptions.attack_speed) * 2);
        bAnimSleep = false;
    }

    private bool CheckMissing(attack_type type)
    {
        int rand = Random.Range(0, 100), range = 0;
        if (type == attack_type.infighting) range = ComponentUnitOptions.Accuracy_infighting;
        else if (type == attack_type.distant_battle) range = ComponentUnitOptions.Accuracy_distant_battle;
        if (rand <= range) return false;
        else return true;
    }
}
