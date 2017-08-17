using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [HideInInspector]
    public bool isLevelStarted;
    public bool isLevelCompleted;


    public GameObject Hero;
    public GameObject waypoint;
    public int point = 0;
    public int lastpoint;
    public Vector3 rotationToPoint;
    public bool pause = true;

    public bool globalpause = false;

    public int spellPoint = 0;

    public int mana = 0;
    public int MaxExp;
    public int _exp;
    public int Exp
    {
        get { return _exp; }
        set {
            _exp = value;
            GameObject.Find("UI/hero icon/exp/exptext").GetComponent<Text>().text = _exp.ToString();
        }
    }
    public int starpoint { get; set; }

    private Slider SliderProgress;
    private Text ManaText;
    private GameObject UnitInfo;
    private Slider XPHeroSlider;

    public bool bButtonGoPress = false;

    private void Start()
    {
        UnitInfo = GameObject.Find("UI/Interface/UnitInfo");
        if (UnitInfo != null)
            UnitInfo.SetActive(false);
        waypoint = GameObject.Find("PointPuck");
        Hero = GameObject.Find("Hero");
        SliderProgress = GameObject.Find("UI/LevelProgress").GetComponent<Slider>();
        ManaText = GameObject.Find("UI/Interface/ManaIcon/Text").GetComponent<Text>();
        GameObject.Find("UI/hero icon/spellpoint/spellpointtext").GetComponent<Text>().text = spellPoint.ToString();
    }

    public void StartManaPeriodic()
    {
        StartCoroutine(Mana_Periodic());
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MapPage" || SceneManager.GetActiveScene().name == "StartPage" || SceneManager.GetActiveScene().name == "Transition")
            return;

        if (isLevelStarted == true && isLevelCompleted == false)
        {
            ManaText.text = mana.ToString();

            if (isLevelStarted == true && pause == false) // Передвижение по точкам
            {
                rotationToPoint = waypoint.transform.GetChild(point).transform.position;
                Hero.GetComponent<MoveController>().Destination(rotationToPoint, true);
                if (Vector3.Distance(Hero.transform.position, rotationToPoint) <= 0.2f)
                {
                    point++;

                    if (point == lastpoint)
                    {
                        Hero.GetComponent<MoveController>().Stop();
                        isLevelCompleted = true;
                        int count = 0;
                        if (starpoint >= 30) count++; // 1 Звезда
                        if (starpoint >= 60) count++; // 2 Звезды
                        if (starpoint >= 100) count++; // 3 Звезды

                        if (GameController.Sound)
                            Destroy(Instantiate(SoundBank.LevelWin), 10);

                        Instantiate(Resources.Load("Prefabs/GamePlay/WinCanvas") as GameObject);
                    }
                }
            }

            #region Информация о юните
            if (Input.GetMouseButtonUp(0) && globalpause == false)
            {
                RaycastHit hit;
                // Debug.Log(hit.transform.name);

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.GetComponent<BirdController>() != null) // Птица FUN функция
                    {
                        hit.transform.GetComponent<BirdController>().Click();
                    }
                    else if (hit.transform.GetComponent<BottleHealController>() != null) // Heal Bottle
                    {
                        hit.transform.GetComponent<BottleHealController>().Click();
                    }
                    else if (hit.transform.GetComponent<BottleManaController>() != null) // Mana Bottle
                    {
                        hit.transform.GetComponent<BottleManaController>().Click();
                    }
                    else if (hit.transform.GetComponent<BottleSpellController>() != null) // Spell Bottle
                    {
                        hit.transform.GetComponent<BottleSpellController>().Click();
                    }

                    if (hit.transform.gameObject.GetComponent<UnitOptions>() != null)
                    {
                        UnitInfo.SetActive(true);
                        UnitInfo.GetComponent<UnitInfoRefreshInfo>().SetTarget(hit.transform.gameObject);
                        // Не герой, опыта больше N, не враг, не выше конечного ранга
                        if (hit.transform.gameObject == Hero || Exp < MaxExp || hit.transform.gameObject.GetComponent<UnitOptions>().IsEnemy == true || (int)hit.transform.gameObject.GetComponent<UnitOptions>().rankid > 3)
                        {
                            UnitInfo.transform.FindChild("UpgradeIcon").gameObject.SetActive(false);
                        }
                        else
                        {
                            UnitInfo.transform.FindChild("UpgradeIcon").gameObject.SetActive(true);
                        }

                        if (hit.transform.gameObject == Hero || hit.transform.gameObject.GetComponent<UnitOptions>().IsEnemy == true)
                        {
                            UnitInfo.transform.FindChild("SellIcon").gameObject.SetActive(false);
                        }
                        else
                        {
                            UnitInfo.transform.FindChild("SellIcon").gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        //GameObject t = Instantiate(Resources.Load("Effects/Spikes/SpikesEffect") as GameObject);
                        //t.transform.position = new Vector3(hit.point.x, hit.point.y, -2);
                        //Destroy(t, 3f);
                        //UnitInfo.SetActive(false);
                    }
                }
            }
            #endregion
        }
    }

    private IEnumerator LevelProgress()
    {
        yield return new WaitForSeconds(0.03f);
        if (isLevelCompleted == false)
        {
            if (point != 0)
            {
                if (SliderProgress.value < point * 1000 / (lastpoint - 1)) SliderProgress.value += 1;
            }
            if (bButtonGoPress == true && isLevelStarted == true) StartCoroutine(LevelProgress());
        }
    }

    public void LoadLevelInfo()
    {
        isLevelStarted = true;
        lastpoint = waypoint.transform.childCount;
        rotationToPoint = waypoint.transform.GetChild(1).transform.position;
        StartCoroutine(LevelProgress());
    }

    public void ManaAdd(int quantity)
    {
        mana += quantity;
        if (mana < 0) mana = 0;
    }

    private IEnumerator Mana_Periodic()
    {
        yield return new WaitForSeconds(1f);
        ManaAdd(1);
        if (bButtonGoPress == true && isLevelStarted == true && isLevelCompleted == false) StartCoroutine(Mana_Periodic());
    }

    public void Gameover()
    {
        StopCoroutine(LevelProgress());
        Instantiate(Resources.Load("Prefabs/Interface/Defeat") as GameObject, transform.position, Quaternion.identity);
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.LevelLose), 10);
    }

    public void RestartLevelClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        FadeInOut.EndScene(SceneManager.GetActiveScene().name);
    }

    public void QuitLevelClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        FadeInOut.EndScene("MapPage");
    }


}