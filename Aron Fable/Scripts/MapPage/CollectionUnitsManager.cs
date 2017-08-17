using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUnitsManager : MonoBehaviour
{
    private UnitsID currentSlotChoise;
    private Text describingText;
    private Image titleRankImage;
    private Text hp;
    private Text armor;
    private Text attackPower;
    private Text speed;
    private Toggle firstToggle;
    private Image leftPanelImage;

    public Sprite knightSprite;
    public Sprite archerSprite;
    public Sprite pupetterSprite;
    public Sprite rougeSprite;
    public Sprite berserkerSprite;
    public Sprite hunterSprite;
    public Sprite clercSprite;
    public Sprite assassinSprite;
    public Sprite palladinSprite;
    public Sprite sentielSprite;
    public Sprite wizardSprite;
    public Sprite dancerSprite;
    public Sprite closedSlotSprite;

    public Sprite KnightEllipse;
    public Sprite ArcherEllipse;
    public Sprite PuppeteerEllipse;
    public Sprite RougeEllipse;


    private void Start()
    {
        leftPanelImage = GameObject.Find("UnitsPanel/LeftPanel").GetComponent<Image>();
        firstToggle = GameObject.Find("TitlePanel/UpgradesToggles").GetComponentInChildren<Toggle>();
        describingText = GameObject.Find("LeftPanel/Describtion").GetComponent<Text>();
        titleRankImage = GameObject.Find("TitlePanel/TitleImage").GetComponent<Image>();
        hp = GameObject.Find("SkillsPanel/HealthText").GetComponent<Text>();
        armor = GameObject.Find("SkillsPanel/ArmorText").GetComponent<Text>();
        attackPower = GameObject.Find("SkillsPanel/AttackText").GetComponent<Text>();
        speed = GameObject.Find("SkillsPanel/SpeedText").GetComponent<Text>();

        OpenSlots();
        currentSlotChoise = UnitsID.knight;
        UnitPanelClick(0);
        UpgradeButtonClick(0);
    }

    private void OpenSlots()
    {
        Image[] images = GameObject.Find("UnitsPanel/UnitsSlot").GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].name == "BackLight")
                continue;
            if (images[i].name == "KnightSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.knight])
            {
                images[i].sprite = knightSprite;
            }
            else if (images[i].name == "ArcherSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.archer])
            {
                images[i].sprite = archerSprite;
            }
            else if (images[i].name == "PupetterSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.puppeteer])
            {
                images[i].sprite = pupetterSprite;
            }
            //else if (images[i].name == "RougeSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.rouge])
            //{
            //    images[i].sprite = rougeSprite;
            //}
            //else if (images[i].name == "BerserkerSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.berserker])
            //{
            //    images[i].sprite = berserkerSprite;
            //}
            //else if (images[i].name == "HunterSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.hunter])
            //{
            //    images[i].sprite = hunterSprite;
            //}
            //else if (images[i].name == "ClercSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.clerc])
            //{
            //    images[i].sprite = clercSprite;
            //}
            //else if (images[i].name == "AssassinSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.assassin])
            //{
            //    images[i].sprite = assassinSprite;
            //}
            //else if (images[i].name == "PalladinSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.palladin])
            //{
            //    images[i].sprite = palladinSprite;
            //}
            //else if (images[i].name == "SentielSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.sentiel])
            //{
            //    images[i].sprite = sentielSprite;
            //}
            //else if (images[i].name == "WizardSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.wizard])
            //{
            //    images[i].sprite = wizardSprite;
            //}
            //else if (images[i].name == "DancerSlot" && GameController.CurrentPlayerProfile.activeUnits[UnitsID.dancer])
            //{
            //    images[i].sprite = dancerSprite;
            //}
        }
    }

    public void BackClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameObject.Find("CollectionWindow").GetComponent<CollectionWindManager>().unitsEnemiesTipsWindow.SetActive(true);
        GameObject.Find("CollectionWindow").GetComponent<CollectionWindManager>().collectionAnim.SetBool("isOpen", true);
        gameObject.SetActive(false);
    }

    public void CloseClick()
    {
        gameObject.SetActive(false);
    }

    private GameObject GetInactiveObject(Component[] transform, string nameToFind)
    {
        foreach (var a in transform)
        {
            if (a.name == nameToFind)
                return a.gameObject;
        }

        return null;
    }

    private void TurnOnBackLight(UnitsID id)
    {
        Component[] objects = null;
        switch (id)
        {
            case UnitsID.knight:
                objects = GameObject.Find("UnitsSlot/KnightSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.archer:
                objects = GameObject.Find("UnitsSlot/ArcherSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.puppeteer:
                objects = GameObject.Find("UnitsSlot/PupetterSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.rouge:
                objects = GameObject.Find("UnitsSlot/RougeSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.berserker:
                objects = GameObject.Find("UnitsSlot/BerserkerSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.hunter:
                objects = GameObject.Find("UnitsSlot/HunterSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.clerc:
                objects = GameObject.Find("UnitsSlot/ClercSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.assassin:
                objects = GameObject.Find("UnitsSlot/AssassinSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.palladin:
                objects = GameObject.Find("UnitsSlot/PalladinSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.sentiel:
                objects = GameObject.Find("UnitsSlot/SentielSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.wizard:
                objects = GameObject.Find("UnitsSlot/WizardSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case UnitsID.dancer:
                objects = GameObject.Find("UnitsSlot/DancerSlot").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            default:
                break;
        }
    }

    private void TurnOffBackLight(UnitsID id)
    {
        GameObject obj = null;
        switch (id)
        {
            case UnitsID.knight:
                obj = GameObject.Find("UnitsSlot/KnightSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.archer:
                obj = GameObject.Find("UnitsSlot/ArcherSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.puppeteer:
                obj = GameObject.Find("UnitsSlot/PupetterSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.rouge:
                obj = GameObject.Find("UnitsSlot/RougeSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.berserker:
                obj = GameObject.Find("UnitsSlot/BerserkerSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.hunter:
                obj = GameObject.Find("UnitsSlot/HunterSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.clerc:
                obj = GameObject.Find("UnitsSlot/ClercSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.assassin:
                obj = GameObject.Find("UnitsSlot/AssassinSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.palladin:
                obj = GameObject.Find("UnitsSlot/PalladinSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.sentiel:
                obj = GameObject.Find("UnitsSlot/SentielSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.wizard:
                obj = GameObject.Find("UnitsSlot/WizardSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case UnitsID.dancer:
                obj = GameObject.Find("UnitsSlot/DancerSlot/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void UnitPanelClick(int button)
    {
        
        firstToggle.isOn = true;
        bool isSlotOpen = false;
        switch (button)
        {
            case 1:
                if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.knight])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = UnitsID.knight;
                leftPanelImage.sprite = KnightEllipse;
                describingText.text = CharactersData.characterInfo[UnitsID.knight].rankData[Rank.star0].describingText;
                hp.text = CharactersData.characterInfo[UnitsID.knight].rankData[Rank.star0].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[UnitsID.knight].rankData[Rank.star0].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[UnitsID.knight].rankData[Rank.star0].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[UnitsID.knight].rankData[Rank.star0].AttackSpeedStr;
                isSlotOpen = true;
                
                break;

            case 2:
                if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.archer])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = UnitsID.archer;
                leftPanelImage.sprite = ArcherEllipse;
                describingText.text = CharactersData.characterInfo[UnitsID.archer].rankData[Rank.star0].describingText;
                hp.text = CharactersData.characterInfo[UnitsID.archer].rankData[Rank.star0].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[UnitsID.archer].rankData[Rank.star0].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[UnitsID.archer].rankData[Rank.star0].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[UnitsID.archer].rankData[Rank.star0].AttackSpeedStr;
                isSlotOpen = true;
                break;

            case 3:
                if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.puppeteer])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = UnitsID.puppeteer;
                leftPanelImage.sprite = PuppeteerEllipse;
                describingText.text = CharactersData.characterInfo[UnitsID.puppeteer].rankData[Rank.star0].describingText;
                hp.text = CharactersData.characterInfo[UnitsID.puppeteer].rankData[Rank.star0].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[UnitsID.puppeteer].rankData[Rank.star0].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[UnitsID.puppeteer].rankData[Rank.star0].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[UnitsID.puppeteer].rankData[Rank.star0].AttackSpeedStr;
                isSlotOpen = true;
                break;

            //case 4:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.rouge])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.rouge;
            //    describingText.text = CharactersData.characterInfo[UnitsID.rouge].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.rouge].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.rouge].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.rouge].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.rouge].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 5:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.berserker])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.berserker;
            //    describingText.text = CharactersData.characterInfo[UnitsID.berserker].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.berserker].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.berserker].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.berserker].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.berserker].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 6:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.hunter])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.hunter;
            //    describingText.text = CharactersData.characterInfo[UnitsID.hunter].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.hunter].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.hunter].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.hunter].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.hunter].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 7:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.clerc])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.clerc;
            //    describingText.text = CharactersData.characterInfo[UnitsID.clerc].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.clerc].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.clerc].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.clerc].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.clerc].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 8:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.assassin])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.assassin;
            //    describingText.text = CharactersData.characterInfo[UnitsID.assassin].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.assassin].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.assassin].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.assassin].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.assassin].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 9:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.palladin])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.palladin;
            //    describingText.text = CharactersData.characterInfo[UnitsID.palladin].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.palladin].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.palladin].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.palladin].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.palladin].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 10:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.sentiel])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.sentiel;
            //    describingText.text = CharactersData.characterInfo[UnitsID.sentiel].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.sentiel].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.sentiel].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.sentiel].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.sentiel].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 11:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.wizard])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.wizard;
            //    describingText.text = CharactersData.characterInfo[UnitsID.wizard].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.wizard].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.wizard].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.wizard].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.wizard].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            //case 12:
            //    if (!GameController.CurrentPlayerProfile.activeUnits[UnitsID.dancer])
            //        return;
            //    TurnOffBackLight(currentSlotChoise);
            //    currentSlotChoise = UnitsID.dancer;
            //    describingText.text = CharactersData.characterInfo[UnitsID.dancer].rankData[Rank.star0].describingText;
            //    hp.text = CharactersData.characterInfo[UnitsID.dancer].rankData[Rank.star0].MaxHealth.ToString();
            //    armor.text = CharactersData.characterInfo[UnitsID.dancer].rankData[Rank.star0].ProtectionStr;
            //    attackPower.text = CharactersData.characterInfo[UnitsID.dancer].rankData[Rank.star0].Damage.My_ToString();
            //    speed.text = CharactersData.characterInfo[UnitsID.dancer].rankData[Rank.star0].AttackSpeedStr;
            //    isSlotOpen = true;
            //    break;

            default:
                break;
        }
        if(isSlotOpen)
            UpgradeButtonClick(0);
        TurnOnBackLight(currentSlotChoise);
    }

    public void UpgradeButtonClick(int button)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        switch (button)
        {
            case 0:
                describingText.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star0].describingText;
                titleRankImage.sprite = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star0].RankCardSprite;
                hp.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star0].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star0].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star0].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star0].AttackSpeedStr;
                break;

            case 1:
                describingText.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star1].describingText;
                titleRankImage.sprite = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star1].RankCardSprite;
                hp.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star1].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star1].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star1].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star1].AttackSpeedStr;
                break;

            case 2:
                describingText.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star2].describingText;
                titleRankImage.sprite = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star2].RankCardSprite;
                hp.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star2].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star2].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star2].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star2].AttackSpeedStr;
                break;

            case 3:
                describingText.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star3].describingText;
                titleRankImage.sprite = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star3].RankCardSprite;
                hp.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star3].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star3].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star3].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star3].AttackSpeedStr;
                break;

            case 4:
                describingText.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star4].describingText;
                titleRankImage.sprite = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star4].RankCardSprite;
                hp.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star4].MaxHealth.ToString();
                armor.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star4].ProtectionStr;
                attackPower.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star4].Damage.My_ToString();
                speed.text = CharactersData.characterInfo[currentSlotChoise].rankData[Rank.star4].AttackSpeedStr;
               
                break;

            default:
                break;
        }
    }
}