using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWindManager : MonoBehaviour
{
    private enum Choise
    { w1, w2, w3, w4, w5, m1, m2, m3, m4, m5, s1, s2, s3, s4, s5, r1, r2, r3, r4, r5, regen, star, trap };

   

    private Choise currentChoise;
    //private Image[] regenStars;
    //private Image[] starfallsStars;
    //private Image[] trapStars;
    private int availableStars;
    private Image titleGeneralImageLeft;
    private Image titleGeneralImageRight;
    private Text titleText;
    private Text describingText;
    private Animator windowAnim;
    private GameObject[] _allBackLights;
    

    public Sprite warriorSpriteLeft;
    public Sprite rangeSpriteLeft;
    public Sprite magicSpriteLeft;
    public Sprite stealthSpriteLeft;
    public Sprite warriorSpriteRight;
    public Sprite rangeSpriteRight;
    public Sprite magicSpriteRight;
    public Sprite stealthSpriteRight;
    public Sprite spellsSprite;

    public ParticleSystem blow;
    public GameObject _upgradeButton;
    

    private void Start()
    {
        availableStars = GameController.CurrentPlayerProfile.Stars;
        titleText = GameObject.Find("TitleTextPanel/TitleText").GetComponent<Text>();
        describingText = GameObject.Find("TitleTextPanel/DescribingText").GetComponent<Text>();
        titleGeneralImageLeft = GameObject.Find("LeftPanel/Title").GetComponent<Image>();
        titleGeneralImageRight = GameObject.Find("RightPanel").GetComponent<Image>();
        //regenStars = GameObject.Find("RegenCard/StarPanel").GetComponentsInChildren<Image>();
        //starfallsStars = GameObject.Find("StarCard/StarPanel").GetComponentsInChildren<Image>();
        //trapStars = GameObject.Find("TrapCard/StarPanel").GetComponentsInChildren<Image>();
        windowAnim = gameObject.GetComponent<Animator>();
        ResetUpgradesRowPanels(GameObject.Find("UpgradeButtons").transform.GetComponentsInChildren(typeof(Transform), true));
        LoadPreviousUpgrades();
        SetStarsCount(availableStars);
        windowAnim.SetBool("isOpen", true);
        windowAnim.Play("StealthOpen", 1);
        windowAnim.Play("WarriorOpen", 2);
        windowAnim.Play("MageOpen", 3);
        windowAnim.Play("RangeOpen", 4);

        FindAllBacklights();
        ClearChoice();
    }

    private void OnEnable()
    {
        if (windowAnim != null)
        {
            windowAnim.SetBool("isOpen", true);
            windowAnim.Play("StealthOpen", 1);
            windowAnim.Play("WarriorOpen", 2);
            windowAnim.Play("MageOpen", 3);
            windowAnim.Play("RangeOpen", 4);
        }
    }

    public void StealthClick()
    {
        windowAnim.SetBool("StealthOpen", !windowAnim.GetBool("StealthOpen"));
        ClearChoice();
    }

    public void WarriorClick()
    {
        windowAnim.SetBool("WarriorOpen", !windowAnim.GetBool("WarriorOpen"));
        ClearChoice();
    }

    public void MageClick()
    {
        windowAnim.SetBool("MageOpen", !windowAnim.GetBool("MageOpen"));
        ClearChoice();
    }

    public void RangeClick()
    {
        windowAnim.SetBool("RangeOpen", !windowAnim.GetBool("RangeOpen"));
        ClearChoice();
    }

    private void ResetUpgradesRowPanels(Component[] panel)
    {
        foreach (var a in panel)
        {
            if (a.name != "BackLight" && a.name != "UpgradeButtons")
            {
                a.gameObject.SetActive(true);
                a.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
            }
        }
    }

    public void ResetUpdatesClick(bool whoIsCalling)
    {
        if (GameController.Sound && whoIsCalling)
            Destroy(Instantiate(SoundBank.ResetUpgradeClick), 1);
        availableStars = GameController.CurrentPlayerProfile.Stars;
        Component[] upgradeButtons = GameObject.Find("UpgradeButtons").transform.GetComponentsInChildren(typeof(Transform), true);
        //Component[] RegenPanel = GameObject.Find("RegenCard/StarPanel").transform.GetComponentsInChildren(typeof(Transform), true);
        //Component[] StarfallPanel = GameObject.Find("StarCard/StarPanel").transform.GetComponentsInChildren(typeof(Transform), true);
        //Component[] TrapPanel = GameObject.Find("TrapCard/StarPanel").transform.GetComponentsInChildren(typeof(Transform), true);
        ResetUpgradesRowPanels(upgradeButtons);
        //ResetUpgradeCardPanel(RegenPanel);
        //ResetUpgradeCardPanel(StarfallPanel);
        //ResetUpgradeCardPanel(TrapPanel);

        for (int i = 0; i < GameController.CurrentPlayerProfile.upgrades.Count; i++)
        {
            GameController.CurrentPlayerProfile.upgrades[(UpgradeID)i].Is1Done = false;
            GameController.CurrentPlayerProfile.upgrades[(UpgradeID)i].Is2Done = false;
            GameController.CurrentPlayerProfile.upgrades[(UpgradeID)i].Is3Done = false;
            GameController.CurrentPlayerProfile.upgrades[(UpgradeID)i].Is4Done = false;
            GameController.CurrentPlayerProfile.upgrades[(UpgradeID)i].Is5Done = false;
        }
        SetStarsCount(availableStars);

    }

    private void LoadPreviousUpgrades()
    {
        var warUpgrades = GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior];
        LoadPreviousUpgradesHelper(warUpgrades);

        var magUpgrades = GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic];
        LoadPreviousUpgradesHelper(magUpgrades);

        var ranUpgrades = GameController.CurrentPlayerProfile.upgrades[UpgradeID.range];
        LoadPreviousUpgradesHelper(ranUpgrades);

        var stelUpgrades = GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth];
        LoadPreviousUpgradesHelper(stelUpgrades);
    }

    private void LoadPreviousUpgradesHelper(UpgradesStats upgrades)
    {
        if (upgrades.Is1Done)
        {
            WarriorClick(1);
            UpgradeClick(false);
        }
        if (upgrades.Is2Done)
        {
            WarriorClick(2);
            UpgradeClick(false);
        }
        if (upgrades.Is3Done)
        {
            WarriorClick(3);
            UpgradeClick(false);
        }
        if (upgrades.Is4Done)
        {
            WarriorClick(4);
            UpgradeClick(false);
        }
        if (upgrades.Is5Done)
        {
            WarriorClick(5);
            UpgradeClick(false);
        }
    }

    private void SetStarsCount(int count)
    {
        Text starText = GameObject.Find("StarsCountPanel/StarsCountText").GetComponent<Text>();
        starText.text = count.ToString();
    }

    private void FindAllBacklights()
    {
        var buttons = GameObject.Find("UpgradeWindow/UpgradeButtons").GetComponentsInChildren<Button>();
        _allBackLights = new GameObject[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            _allBackLights[i] = buttons[i].transform.FindChild("BackLight").gameObject;
        }
    }

    private void ClearChoice()
    {
        _upgradeButton.transform.FindChild("Text").GetComponent<Text>().text = string.Empty;
        _upgradeButton.GetComponent<Button>().gameObject.SetActive(false);
        GameObject.Find("LeftPanel/TitleTextPanel/TitleText").GetComponent<Text>().text = string.Empty;
        GameObject.Find("LeftPanel/TitleTextPanel/DescribingText").GetComponent<Text>().text = string.Empty;

        foreach (var a in _allBackLights)
        {
            if(a.activeSelf)
                a.SetActive(false);
        }
    }

    private void SetUpgradeCostAndButton(int count, bool clear)
    {
        _upgradeButton.transform.FindChild("Text").GetComponent<Text>().text = count.ToString();
        _upgradeButton.GetComponent<Button>().gameObject.SetActive(!clear);
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

    private void TurnOnBackLight(Choise id)
    {
        Component[] objects = null;
        switch (id)
        {
            case Choise.w1:
                objects = GameObject.Find("UpgradeWar1").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.w2:
                objects = GameObject.Find("UpgradeWar2").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.w3:
                objects = GameObject.Find("UpgradeWar3").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.w4:
                objects = GameObject.Find("UpgradeWar4").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.w5:
                objects = GameObject.Find("UpgradeWar5").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.m1:
                objects = GameObject.Find("UpgradeMag1").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.m2:
                objects = GameObject.Find("UpgradeMag2").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.m3:
                objects = GameObject.Find("UpgradeMag3").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.m4:
                objects = GameObject.Find("UpgradeMag4").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.m5:
                objects = GameObject.Find("UpgradeMag5").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.s1:
                objects = GameObject.Find("UpgradeStealth1").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.s2:
                objects = GameObject.Find("UpgradeStealth2").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.s3:
                objects = GameObject.Find("UpgradeStealth3").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.s4:
                objects = GameObject.Find("UpgradeStealth4").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.s5:
                objects = GameObject.Find("UpgradeStealth5").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.r1:
                objects = GameObject.Find("UpgradeRange1").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.r2:
                objects = GameObject.Find("UpgradeRange2").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.r3:
                objects = GameObject.Find("UpgradeRange3").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.r4:
                objects = GameObject.Find("UpgradeRange4").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.r5:
                objects = GameObject.Find("UpgradeRange5").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.regen:
                objects = GameObject.Find("RegenCard").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.star:
                objects = GameObject.Find("StarCard").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            case Choise.trap:
                objects = GameObject.Find("TrapCard").transform.GetComponentsInChildren(typeof(Transform), true);
                GetInactiveObject(objects, "BackLight").SetActive(true);
                break;

            default:
                break;
        }
    }

    private void TurnOffBackLight(Choise id)
    {
        GameObject obj = null;
        switch (id)
        {
            case Choise.w1:
                obj = GameObject.Find("UpgradeWar1/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.w2:
                obj = GameObject.Find("UpgradeWar2/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.w3:
                obj = GameObject.Find("UpgradeWar3/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.w4:
                obj = GameObject.Find("UpgradeWar4/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.w5:
                obj = GameObject.Find("UpgradeWar5/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.m1:
                obj = GameObject.Find("UpgradeMag1/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.m2:
                obj = GameObject.Find("UpgradeMag2/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.m3:
                obj = GameObject.Find("UpgradeMag3/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.m4:
                obj = GameObject.Find("UpgradeMag4/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.m5:
                obj = GameObject.Find("UpgradeMag5/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.s1:
                obj = GameObject.Find("UpgradeStealth1/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.s2:
                obj = GameObject.Find("UpgradeStealth2/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.s3:
                obj = GameObject.Find("UpgradeStealth3/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.s4:
                obj = GameObject.Find("UpgradeStealth4/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.s5:
                obj = GameObject.Find("UpgradeStealth5/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.r1:
                obj = GameObject.Find("UpgradeRange1/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.r2:
                obj = GameObject.Find("UpgradeRange2/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.r3:
                obj = GameObject.Find("UpgradeRange3/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.r4:
                obj = GameObject.Find("UpgradeRange4/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.r5:
                obj = GameObject.Find("UpgradeRange5/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.regen:
                obj = GameObject.Find("RegenCard/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.star:
                obj = GameObject.Find("StarCard/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            case Choise.trap:
                obj = GameObject.Find("TrapCard/BackLight");
                if (obj != null)
                    obj.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void RegenClick()
    {
        //if (GameController.Sound)
        //    Destroy(Instantiate(SoundBank.ClickSound), 1);
        //TurnOffBackLight(currentChoise);
        //currentChoise = Choise.regen;
        //TurnOnBackLight(currentChoise);
        //titleGeneralImageLeft.sprite = spellsSprite;
        //titleText.text = "Regen";
        //int currentUpgrade = GameController.CurrentPlayerProfile.upgrades[UpgradeID.regen];
        //switch (currentUpgrade)
        //{
        //    case 0:
        //        describingText.text = "Increases Regens healing power from 20 to 24 per second";
        //        break;

        //    case 1:
        //        describingText.text = "Increases Regens healing power from 24 to 28 per second";
        //        break;

        //    case 2:
        //        describingText.text = "Increases Regens healing power from 28 to 32 per second";
        //        break;

        //    case 3:
        //        describingText.text = "Increases Regens healing power from 32 to 36 per second";
        //        break;

        //    case 4:
        //        describingText.text = "Increases Regens healing power from 36 to 40 per second";
        //        break;

        //    case 5:
        //        describingText.text = "You have max Regen level";
        //        break;

        //    default:
        //        describingText.text = "Default choice";
        //        break;
        //}
    }

    public void StarfallClick()
    {
        //if (GameController.Sound)
        //    Destroy(Instantiate(SoundBank.ClickSound), 1);
        //TurnOffBackLight(currentChoise);
        //currentChoise = Choise.star;
        //TurnOnBackLight(currentChoise);
        //titleGeneralImageLeft.sprite = spellsSprite;
        //titleText.text = "Starfalls";
        //int currentUpgrade = GameController.CurrentPlayerProfile.upgrades[UpgradeID.starfall];
        //switch (currentUpgrade)
        //{
        //    case 0:
        //        describingText.text = "Increases Starfalls damage from 25 to 30";
        //        break;

        //    case 1:
        //        describingText.text = "Increases Starfalls damage from 30 to 35";
        //        break;

        //    case 2:
        //        describingText.text = "Increases Starfalls damage from 35 to 40";
        //        break;

        //    case 3:
        //        describingText.text = "Increases Starfalls damage from 40 to 45";
        //        break;

        //    case 4:
        //        describingText.text = "Increases Starfalls damage from 45 to 50";
        //        break;

        //    case 5:
        //        describingText.text = "You have max Starfalls level";
        //        break;

        //    default:
        //        describingText.text = "Default choice";
        //        break;
        //}
    }

    public void TrapClick()
    {
        //if (GameController.Sound)
        //    Destroy(Instantiate(SoundBank.ClickSound), 1);
        //TurnOffBackLight(currentChoise);
        //currentChoise = Choise.trap;
        //TurnOnBackLight(currentChoise);
        //titleGeneralImageLeft.sprite = spellsSprite;
        //titleText.text = "Trap";
        //int currentUpgrade = GameController.CurrentPlayerProfile.upgrades[UpgradeID.trap];
        //switch (currentUpgrade)
        //{
        //    case 0:
        //        describingText.text = "Reduces Traps cooldown time from 80 to 77 second";
        //        break;

        //    case 1:
        //        describingText.text = "Reduces Traps cooldown time from 77 to 74 second";
        //        break;

        //    case 2:
        //        describingText.text = "Reduces Traps cooldown time from 74 to 71 second";
        //        break;

        //    case 3:
        //        describingText.text = "Reduces Traps cooldown time from 71 to 68 second";
        //        break;

        //    case 4:
        //        describingText.text = "Reduces Traps cooldown time from 68 to 65 second";
        //        break;

        //    case 5:
        //        describingText.text = "You have max Traps level";
        //        break;

        //    default:
        //        describingText.text = "Default choice";
        //        break;
        //}
    }

    public void WarriorClick(int button)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        TurnOffBackLight(currentChoise);
        switch (button)
        {
            case 1:
                currentChoise = Choise.w1;
                titleText.text = "Hearty Health";
                describingText.text = "Increases the maximum health of Warrior units.";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[0], GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is1Done);
                break;

            case 2:
                currentChoise = Choise.w2;
                titleText.text = "Solid Shield";
                describingText.text = "Increases the defence of the Warrior units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[1], GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is2Done);
                break;

            case 3:
                currentChoise = Choise.w3;
                titleText.text = "Rapid Regen";
                describingText.text = "Warrior units recover more health";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[2], GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is3Done);
                break;

            case 4:
                currentChoise = Choise.w4;
                titleText.text = "Resistant";
                describingText.text = "Negative effects lasts shorter on warriors units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[3], GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is4Done);
                break;

            case 5:
                currentChoise = Choise.w5;
                titleText.text = "Quick Revive";
                describingText.text = "Warrior units revive soon than other unit types";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[4], GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is5Done);
                break;

            default:
                break;
        }
        TurnOnBackLight(currentChoise);
        titleGeneralImageLeft.sprite = warriorSpriteLeft;
        titleGeneralImageRight.sprite = warriorSpriteRight;
        
    }

    public void MagicClick(int button)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        TurnOffBackLight(currentChoise);
        switch (button)
        {
            case 1:
                currentChoise = Choise.m1;
                titleText.text = "Mighty Magic";
                describingText.text = "Increases the damage of Magic units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[0], GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is1Done);
                break;

            case 2:
                currentChoise = Choise.m2;
                titleText.text = "Far cast";
                describingText.text = "Increases the casting range of Magic units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[1], GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is2Done);
                break;

            case 3:
                currentChoise = Choise.m3;
                titleText.text = "Enchantment";
                describingText.text = "Increases the splash damage of Magic units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[2], GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is3Done);
                break;

            case 4:
                currentChoise = Choise.m4;
                titleText.text = "Swift Spells";
                describingText.text = "Skill cooldowns are reduced";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[3], GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is4Done);
                break;

            case 5:
                currentChoise = Choise.m5;
                titleText.text = "Sorcery sale";
                describingText.text = "Magic units cost less to level up";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[4], GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is5Done);
                break;

            default:
                break;
        }

        TurnOnBackLight(currentChoise);
        titleGeneralImageLeft.sprite = magicSpriteLeft;
        titleGeneralImageRight.sprite = magicSpriteRight;
    }

    public void RangeClick(int button)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        TurnOffBackLight(currentChoise);
        switch (button)
        {
            case 1:
                currentChoise = Choise.r1;
                titleText.text = "Rapid Fire";
                describingText.text = "Increases the attack speed of Ranged units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[0], GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is1Done);
                break;

            case 2:
                currentChoise = Choise.r2;
                titleText.text = "Light Footed";
                describingText.text = "Increases the movement speed of Ranged units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[1], GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is2Done);
                break;

            case 3:
                currentChoise = Choise.r3;
                titleText.text = "Distant shots";
                describingText.text = "Increases the attack range of Ranged units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[2], GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is3Done);
                break;

            case 4:
                currentChoise = Choise.r4;
                titleText.text = "Hawk eye";
                describingText.text = "Increases the accuracy of Ranged units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[3], GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is4Done);
                break;

            case 5:
                currentChoise = Choise.r5;
                titleText.text = "Twin trigger";
                describingText.text = "Ranged units have a chance to shoot twice per attack";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[4], GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is5Done);
                break;

            default:
                break;
        }

        TurnOnBackLight(currentChoise);
        titleGeneralImageLeft.sprite = rangeSpriteLeft;
        titleGeneralImageRight.sprite = rangeSpriteRight;
    }

    public void StealthClick(int button)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        TurnOffBackLight(currentChoise);
        switch (button)
        {
            case 1:
                currentChoise = Choise.s1;
                titleText.text = "Dodge Up";
                describingText.text = "Increases the dodge rate of Stealth units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[0], GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is1Done);
                break;

            case 2:
                currentChoise = Choise.s2;
                titleText.text = "Critical Charge";
                describingText.text = "Increases the chances of dealing the critical attacks";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[1], GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is2Done);
                break;

            case 3:
                currentChoise = Choise.s3;
                titleText.text = "Agile Assault";
                describingText.text = "Increases the attack speed of Ranged units";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[2], GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is3Done);
                break;

            case 4:
                currentChoise = Choise.s4;
                titleText.text = "Wreck 'n Tear";
                describingText.text = "Increases the critical damage";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[3], GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is4Done);
                break;

            case 5:
                currentChoise = Choise.s5;
                titleText.text = "Piersing Blades";
                describingText.text = "Critical attack by Stealth units ignore defence";
                SetUpgradeCostAndButton(CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[4], GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is5Done);
                break;

            default:
                break;
        }

        TurnOnBackLight(currentChoise);
        titleGeneralImageLeft.sprite = stealthSpriteLeft;
        titleGeneralImageRight.sprite = stealthSpriteRight;
    }

    public void UpgradeClick(bool whoIsCalling)
    {
        
        if (availableStars == 0)
            return;
        bool isUpgradeDone = false;
        switch (currentChoise)
        {
            case Choise.w1:
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is1Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is1Done = true;
                GameObject.Find("UpgradeWar1").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[0]);
                isUpgradeDone = true;
                break;

            case Choise.w2:
                if (availableStars < 2)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is2Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is2Done = true;
                GameObject.Find("UpgradeWar2").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[1]);
                isUpgradeDone = true;
                break;

            case Choise.w3:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is3Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is3Done = true;
                GameObject.Find("UpgradeWar3").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[2]);
                isUpgradeDone = true;
                break;

            case Choise.w4:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is4Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is4Done = true;
                GameObject.Find("UpgradeWar4").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[3]);
                isUpgradeDone = true;
                break;

            case Choise.w5:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is5Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.warrior].Is5Done = true;
                GameObject.Find("UpgradeWar5").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[4]);
                isUpgradeDone = true;
                break;

            case Choise.m1:
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is1Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is1Done = true;
                GameObject.Find("UpgradeMag1").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[0]);
                isUpgradeDone = true;
                break;

            case Choise.m2:
                if (availableStars < 2)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is2Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is2Done = true;
                GameObject.Find("UpgradeMag2").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[1]);
                isUpgradeDone = true;
                break;

            case Choise.m3:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is3Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is3Done = true;
                GameObject.Find("UpgradeMag3").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[2]);
                isUpgradeDone = true;
                break;

            case Choise.m4:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is4Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is4Done = true;
                GameObject.Find("UpgradeMag4").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[3]);
                isUpgradeDone = true;
                break;

            case Choise.m5:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is5Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.magic].Is5Done = true;
                GameObject.Find("UpgradeMag5").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.magic].upgradeRankCost[4]);
                isUpgradeDone = true;
                break;

            case Choise.s1:
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is1Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is1Done = true;
                GameObject.Find("UpgradeStealth1").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[0]);
                isUpgradeDone = true;
                break;

            case Choise.s2:
                if (availableStars < 2)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is2Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is2Done = true;
                GameObject.Find("UpgradeStealth2").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[1]);
                isUpgradeDone = true;
                break;

            case Choise.s3:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is3Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is3Done = true;
                GameObject.Find("UpgradeStealth3").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[2]);
                isUpgradeDone = true;
                break;

            case Choise.s4:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is4Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is4Done = true;
                GameObject.Find("UpgradeStealth4").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[3]);
                isUpgradeDone = true;
                break;

            case Choise.s5:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is5Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.stealth].Is5Done = true;
                GameObject.Find("UpgradeStealth5").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.stealth].upgradeRankCost[4]);
                isUpgradeDone = true;
                break;

            case Choise.r1:
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is1Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is1Done = true;
                GameObject.Find("UpgradeRange1").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[0]);
                isUpgradeDone = true;
                break;

            case Choise.r2:
                if (availableStars < 2)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is2Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is2Done = true;
                GameObject.Find("UpgradeRange2").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[1]);
                isUpgradeDone = true;
                break;

            case Choise.r3:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is3Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is3Done = true;
                GameObject.Find("UpgradeRange3").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[2]);
                isUpgradeDone = true;
                break;

            case Choise.r4:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is4Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is4Done = true;
                GameObject.Find("UpgradeRange4").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[3]);
                isUpgradeDone = true;
                break;

            case Choise.r5:
                if (availableStars < 3)
                    return;
                if (GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is5Done && whoIsCalling)
                    return;
                GameController.CurrentPlayerProfile.upgrades[UpgradeID.range].Is5Done = true;
                GameObject.Find("UpgradeRange5").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                SetStarsCount(availableStars -= CharactersData.UpgrageCost[UpgradeID.range].upgradeRankCost[4]);
                isUpgradeDone = true;
                break;

            //case Choise.regen:
            //    index = GameController.CurrentPlayerProfile.upgrades[UpgradeID.regen] + 1;
            //    if (index > 5)
            //        return;
            //    //regenStars[index].color = new Color(1f, 1f, 1f, 1f);
            //    GameController.CurrentPlayerProfile.upgrades[UpgradeID.regen] += 1;
            //    SetStarsCount(--availableStars);
            //    RegenClick();
            //    isUpgradeDone = true;
            //    break;

            //case Choise.star:
            //    index = GameController.CurrentPlayerProfile.upgrades[UpgradeID.starfall] + 1;
            //    if (index > 5)
            //        return;
            //    //starfallsStars[index].color = new Color(1f, 1f, 1f, 1f);
            //    GameController.CurrentPlayerProfile.upgrades[UpgradeID.starfall] += 1;
            //    SetStarsCount(--availableStars);
            //    StarfallClick();
            //    isUpgradeDone = true;
            //    break;

            //case Choise.trap:
            //    index = GameController.CurrentPlayerProfile.upgrades[UpgradeID.trap] + 1;
            //    if (index > 5)
            //        return;
            //    //trapStars[index].color = new Color(1f, 1f, 1f, 1f);
            //    GameController.CurrentPlayerProfile.upgrades[UpgradeID.trap] += 1;
            //    SetStarsCount(--availableStars);
            //    isUpgradeDone = true;
            //    break;

            default:
                break;
        }

        if (GameController.Sound && isUpgradeDone && whoIsCalling)
            Destroy(Instantiate(SoundBank.UpgradeClick), 2);
        if (isUpgradeDone && whoIsCalling)
        {
            blow.Play();
        }
            
    }
}