using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CardCollectionPref : MonoBehaviour
{
    private GameObject testLevelTool;
    private int currentResolution;
    private Vector2 cellSize;
    GameObject windowBackgound;
    private SpritesBank spriteBank;
    private GameObject ButtonGO;
    public Dictionary<UnitsID, bool> ActivePlayersInLevel;
    private Transform _whereToAttachChars;


    private void Start()
    {
        spriteBank = GameObject.Find("GameController").GetComponent<SpritesBank>();
        _whereToAttachChars = GameObject.Find("CardPanelPref/CardWindow").transform;
        windowBackgound = GameObject.Find("UnitWindow/ViewPort/BackGround");
        ActivePlayersInLevel = new Dictionary<UnitsID, bool>();
        var startCardUnits = GameObject.Find("UnitWindow/ViewPort/BackGround").GetComponentsInChildren<Button>();

        for (int i = 0; i < Enum.GetValues(typeof(UnitsID)).Length; i++) // для инициализации
        {
            ActivePlayersInLevel[(UnitsID)i] = false;
        }

        for (int i = 0; i < Enum.GetValues(typeof(UnitsID)).Length - 2; i++) // -2 потому что последний энум - no, а хер тут не считается тоже
        {

            if (GameController.CurrentPlayerProfile.activeUnits[(UnitsID)i])
            {
                startCardUnits[i].GetComponent<StartCardUnit>().UnitType = (UnitsID)i;
                startCardUnits[i].GetComponent<Image>().sprite = 
                    CharactersData.characterInfo[(UnitsID) i].rankData[Rank.star0].RankCardSprite;
                startCardUnits[i].transform.FindChild("Plashka/Cost").GetComponent<Text>().text =
                    CharactersData.characterInfo[(UnitsID) i].rankData[Rank.star0].Manacost.ToString();
            }
            else
            {
                startCardUnits[i].image.sprite = spriteBank.backCard;
                startCardUnits[i].enabled = false;
                startCardUnits[i].transform.FindChild("Plashka/Cost").gameObject.SetActive(false);
                startCardUnits[i].transform.FindChild("Plashka/Mana").gameObject.SetActive(false);
            }
        }
        ButtonGO = GameObject.Find("ButtonGO");
        ButtonGO.SetActive(false);
    }

    public void ContinueClick()
    {
        GameObject.Find("LevelControllerPref").GetComponent<LevelController>().LoadLevelInfo();
        ButtonGO.SetActive(true);
        Destroy(gameObject);
    }

    public void ResetActivePlayers()
    {

        for (int i = 0; i < Enum.GetValues(typeof(UnitsID)).Length; i++)
        {
            ActivePlayersInLevel[(UnitsID)i] = false;
        }
        GameObject.Find("CardPanelPref").GetComponent<CardPanelPref>().ResetStack();
    }

    public void SelectUnit(int index)
    {
        UnitsID choice = (UnitsID)index;
        ActivePlayersInLevel[choice] = true;
        GameObject.Find("CardPanelPref").GetComponent<CardPanelPref>().CardClick(choice);
    }

    public void DiselectUnit(int index)
    {
        UnitsID choice = (UnitsID)index;
        ActivePlayersInLevel[choice] = false;
        GameObject.Find("UnitWindow/ViewPort/BackGround").transform.GetChild(index).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    public void ResetClick()
    {
        for (int i = 0; i < windowBackgound.transform.childCount; i++)
        {
            if (GameController.CurrentPlayerProfile.activeUnits[(UnitsID)i])
            {
                windowBackgound.transform.GetChild(i).GetComponent<Button>().enabled = true;
                windowBackgound.transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
        ResetActivePlayers();
    }

}