using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MoveController : MonoBehaviour
{
    #region Variables
    private LevelController _LevelController;
    public LayerMask[] layerMask; // Через редактор принимает Layer персонажей
    private UnitOptions ComponentUnitOptions; // Принимает компонент для управления параметрами персонажа
    private Attack_System ComponentAttack_System;
    private AnimationController ComponentAnimationController;
    private RaycastHit hit; // Содержит в себе конечную точку для "Move"
    private GameObject Sprite; // Объект хранящий в себе все элементы "модели"
    public bool bMove = false; // Есть ли куда двигаться ?
    public Vector3 point;// Точка места назначения
    public GameObject FocusSound;
    private GameObject line;
    private bool selected = false;
    public bool IgnoreAutoAttack = false;
    public GameObject Hero;
    private GameObject origin;
    private float SleepingChangeRotation = 0f;
    private float originRange = 0;

    private float focusTimer;
    public bool focusActive = false;
    #endregion 

    void Start()
    {
        #region Initialization
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        origin = transform.FindChild("origin").gameObject;
        ComponentUnitOptions = gameObject.GetComponent<UnitOptions>();
        ComponentAttack_System = GetComponent<Attack_System>();
        ComponentAnimationController = GetComponent<AnimationController>();
        if (transform.GetChild(0).gameObject != null) Sprite = transform.GetChild(0).gameObject;
        DrawingSprites();
        Hero = GameObject.Find("Hero");
        focusTimer = UnityEngine.Random.Range(3, 10);
        originRange = Vector3.Distance(transform.position, origin.transform.position);
        #endregion
    }

    void Update()
    {
        #region FUN анимации в режиме ожидания
        if (focusTimer <= 0 && focusActive)
        {
            if (ComponentAnimationController.status == AnimationStatus.stand)
            {
                if (GameController.Sound && FocusSound != null) Destroy(Instantiate(FocusSound), 1);
                ComponentAnimationController.ChangeStatus(AnimationStatus.focus);
                StartCoroutine(ReturnStandStatus());
            }
            focusTimer = UnityEngine.Random.Range(5, 10);
        }
        else if (ComponentAnimationController.status == AnimationStatus.stand) focusTimer -= Time.deltaTime;
        #endregion

        if (ComponentUnitOptions.isDead == false && _LevelController.globalpause == false)
        {
            if (bMove == true) Move();
            DrawingSprites();
        }

        if (SleepingChangeRotation > 0) SleepingChangeRotation -= Time.deltaTime;
    }

    public void Destination(Vector3 point, bool rotation)
    {
        if (Vector3.Distance(transform.position, point) > 0.1f && ComponentAttack_System.bAnimSleep == false && _LevelController.globalpause == false && ComponentUnitOptions.status == unit_status.normal)
        {
            if (ComponentAnimationController != null) ComponentAnimationController.ChangeStatus(AnimationStatus.walk);
            bMove = true;
            this.point = point;
            if (rotation == true && SleepingChangeRotation <= 0 && ComponentUnitOptions.isDead == false)
            {
                SleepingChangeRotation = 0.1f;
                if (point.x > transform.position.x) Sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (point.x < transform.position.x) Sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    public void Stop()
    {
        if (ComponentAnimationController != null)
        {
            if (ComponentAnimationController.status != AnimationStatus.focus && ComponentUnitOptions.status == unit_status.normal)
            {
                if (ComponentAttack_System.active == false && ((gameObject != Hero && Hero.GetComponent<AnimationController>().status != AnimationStatus.walk) || ComponentUnitOptions.IsEnemy || gameObject == Hero)) ComponentAnimationController.ChangeStatus(AnimationStatus.stand);
                else if (ComponentAttack_System.active == true && ComponentAnimationController.group_status != GroupStatus.attack) ComponentAnimationController.ChangeStatus(AnimationStatus.attack_wait);
            }
        }

        IgnoreAutoAttack = false;
        bMove = false;
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, point) > 0.1f)
        {
            if (ComponentAnimationController.status != AnimationStatus.focus && ComponentUnitOptions.status == unit_status.normal)
            {
                float speed = 0;
                if (Vector3.Distance(transform.position, point) > 0.3f && gameObject != Hero)
                {
                    speed = ComponentUnitOptions.movespeed + 1;
                }
                else speed = ComponentUnitOptions.movespeed;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(point.x, point.y, 0), speed * Time.deltaTime);
            }  
        }
        else Stop();
    }

    void DrawingSprites()
    {
        if (Sprite != null)
            for (int i = 0; i < Sprite.transform.childCount; i++)
                DrawindSprited(Sprite.transform.GetChild(i).gameObject, i); // Рекурсия
    }

    void DrawindSprited(GameObject obj, int i)
    {
        if (obj.GetComponent<SpriteRenderer>() != null)
        {
            obj.GetComponent<SpriteRenderer>().sortingOrder = ~(int)(origin.transform.position.y * 100 - (i - 0.9f));
            if (obj.transform.childCount != 0)
                for (int y = 0; y < obj.transform.childCount; y++)
                    DrawindSprited(obj.transform.GetChild(y).gameObject, i); // Рекурсия
        }
    }

    private IEnumerator ReturnStandStatus()
    {
        yield return new WaitForSeconds(1.2f);
        if (ComponentAnimationController.status == AnimationStatus.focus && ComponentUnitOptions.status == unit_status.normal) ComponentAnimationController.ChangeStatus(AnimationStatus.stand);
    }
}
