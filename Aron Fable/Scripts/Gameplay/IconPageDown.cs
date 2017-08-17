using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IconPageDown : MonoBehaviour
{

    public GameObject Hero;
    public UnitsID UnitType { set; get; }

    private LevelController _LevelController;
    private RaycastHit hit;
    private bool selected = false;
    private GameObject card;
    private int UnitLimits;
    private int manaCost;
    private bool block = false;
    private Image image;
    private string path;

    private void Start()
    {
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        image = GetComponent<Image>();
        UnitLimits = 6;
        ReInitialize();
    }

    public void ReInitialize()
    {
        manaCost = CharactersData.characterInfo[UnitType].rankData[Rank.star0].Manacost;
        if(image == null)
            image = GetComponent<Image>();
        image.sprite = CharactersData.characterInfo[UnitType].rankData[Rank.star0].RankCardSprite;
        path = CharactersData.characterInfo[UnitType].rankData[Rank.star0].PathToPrefab;
    }

    private void Update()
    {
        #region
        if (block == false)
        {
            if (Input.GetMouseButton(0) && selected == true && card != null)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                    card.transform.position = new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z);
                DragCamera.LineActive = true;
            }

            if (Input.GetMouseButtonUp(0) && selected == true)
            {
                if (Vector3.Distance(hit.point, Hero.transform.position) < 5.2f)
                {
                    Instantiate((GameObject)Resources.Load(path), new Vector3(hit.point.x, hit.point.y + 1f, hit.point.z), Quaternion.identity);
                    if (GameController.Sound) Destroy(Instantiate(Resources.Load("Sound/Gameplay/System/Prefabs/Sound_Spawn")), 1.5f);
                    _LevelController.mana -= manaCost;

                    for (int i = 0; i < 6; i++) GameObject.Find("Hero/UnitCenter/Aura").transform.GetChild(i).GetComponent<AnchorMove>().CheckAlter(GameObject.Find("Hero/UnitCenter/Aura").transform.GetChild(i).gameObject);
                }

                GameObject.Find("Hero").GetComponent<HeroInfo>().auracontroll = false;
                DragCamera.LineActive = false;
                selected = false;
                Destroy(card);
                card = null;
            }
        }
        #endregion Реализация

        #region Проверка на возможность использовать
        if (_LevelController.isLevelStarted)
        {
            if (_LevelController.mana < manaCost || Hero.GetComponent<HeroInfo>().units.Count >= 6)
            {
                block = true;
                image.color = new Color32(255, 225, 225, 150);
            }
            else
            {
                block = false;
                image.color = new Color32(255, 255, 255, 255);
            }
        }
        #endregion
    }

    public void CardDrop()
    {
        if (block == false)
        {
            #region ВРЕМЕННЫЙ КОСТЫЛЬ!!!
            int tempCount = 0;
            for (int i = 0; i < Hero.GetComponent<HeroInfo>().units.Count; i++)
            {
                if (Hero.GetComponent<HeroInfo>().units[i] != null)
                    tempCount++;
            }
            #endregion

            if (tempCount < UnitLimits && _LevelController.isLevelStarted)
            {
                if (card == null)
                {
                    selected = true;
                    GameObject.Find("Hero").GetComponent<HeroInfo>().auracontroll = true;

                    string result = CharactersData.characterInfo[UnitType].rankData[Rank.star0].PathToShadow;
                    card = Instantiate((GameObject)Resources.Load(result));
                }
            }
        }
        if (!_LevelController.isLevelStarted)
        {
            if (GameController.Sound)
                Destroy(Instantiate(SoundBank.ClickSound), 1);
            GameObject.Find("CardCollectionPref").GetComponent<CardCollectionPref>().DiselectUnit((int)UnitType);
            gameObject.SetActive(false);
        }
    }
}