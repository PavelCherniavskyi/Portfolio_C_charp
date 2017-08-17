using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtchievManager : MonoBehaviour
{
    static public Dictionary<AchievID, AchievmentData> achievInfo = new Dictionary<AchievID, AchievmentData>();
    private Image titleImage;
    private Image titleImageOpen;
    private Image titleImageClose;
    private Text header;
    private Text description;
    private Text countWindow;
    private AchievID currentSlotChoise;
    private Animator windowAnim;

    private int currentResolution;
    private Vector2 cellSize;

    private void Start()
    {
        windowAnim = gameObject.GetComponent<Animator>();
        titleImage = GameObject.Find("AtchievementsWindow/TitlePanel/TitleImage").GetComponent<Image>();
        titleImageOpen = GameObject.Find("AtchievementsWindow/TitlePanel/TitleImage/Open").GetComponent<Image>();
        titleImageClose = GameObject.Find("AtchievementsWindow/TitlePanel/TitleImage/Close").GetComponent<Image>();
        header = GameObject.Find("AtchievementsWindow/TitlePanel/TitleTextPanel/Header").GetComponent<Text>();
        description = GameObject.Find("AtchievementsWindow/TitlePanel/TitleTextPanel/Describtion").GetComponent<Text>();
        countWindow = GameObject.Find("AtchievementsWindow/TitlePanel/CountWindow/Text").GetComponent<Text>();
        OpenSlots();
        SlotClick(1);
        windowAnim.SetBool("isOpen", true);
    }

    private void OnEnable()
    {
        if (windowAnim != null)
            windowAnim.SetBool("isOpen", true);
    }

    private void OpenSlots()
    {
        Image[] images = GameObject.Find("AtchievementsWindow/AtchievSlots").GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].name == "BackLight")
                continue;
            if (images[i].name == "shiny")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.shiny], AchievID.shiny);
            }
            else if (images[i].name == "starry")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.starry], AchievID.starry);
            }
            else if (images[i].name == "starrific")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.starrific], AchievID.starrific);
            }
            else if (images[i].name == "startacular")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.startacular], AchievID.startacular);
            }
            else if (images[i].name == "steppedUp")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.steppedUp], AchievID.steppedUp);
            }
            else if (images[i].name == "distantDreams")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.distantDreams], AchievID.distantDreams);
            }
            else if (images[i].name == "lionHeart")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.lionHeart], AchievID.lionHeart);
            }
            else if (images[i].name == "treasureHunter")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.treasureHunter], AchievID.treasureHunter);
            }
            else if (images[i].name == "snareCare")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.snareCare], AchievID.snareCare);
            }
            else if (images[i].name == "revive")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.revive], AchievID.revive);
            }
            else if (images[i].name == "cometPlumet")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.cometPlumet], AchievID.cometPlumet);
            }
            else if (images[i].name == "collector")
            {
                SetUpImage(images[i], GameController.CurrentPlayerProfile.achievements[AchievID.collector], AchievID.collector);
            }
        }
    }

    private void SetUpImage(Image image, bool isActive, AchievID id)
    {
        if (isActive)
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            image.transform.FindChild("Close").gameObject.SetActive(false);
        }
        image.sprite = AchievmentData.achievInfo[id].sprite;
    }

    private void SetUptitleUpgradeImage(Image source)
    {
        titleImage.sprite = source.sprite;
        titleImage.color = source.color;
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

    private void TurnOnBackLight(AchievID id)
    {
        Component[] objects = null;
        switch (id)
        {
            case AchievID.shiny:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/shiny").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.starry:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/starry").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.starrific:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/starrific").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.startacular:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/startacular").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.steppedUp:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/steppedUp").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.distantDreams:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/distantDreams").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.lionHeart:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/lionHeart").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.treasureHunter:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/treasureHunter").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.snareCare:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/snareCare").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.revive:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/revive").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.cometPlumet:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/cometPlumet").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case AchievID.collector:
                objects = GameObject.Find("AtchievementsWindow/AtchievSlots/collector").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            default:
                break;
        }
        GetInactiveObject(objects, "BackLight").SetActive(true);
    }

    private void TurnOffBackLight(AchievID id)
    {
        GameObject obj = null;
        switch (id)
        {
            case AchievID.shiny:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/shiny/BackLight");
                break;

            case AchievID.starry:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/starry/BackLight");
                break;

            case AchievID.starrific:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/starrific/BackLight");
                break;

            case AchievID.startacular:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/startacular/BackLight");
                break;

            case AchievID.steppedUp:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/steppedUp/BackLight");
                break;

            case AchievID.distantDreams:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/distantDreams/BackLight");
                break;

            case AchievID.lionHeart:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/lionHeart/BackLight");
                break;

            case AchievID.treasureHunter:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/treasureHunter/BackLight");
                break;

            case AchievID.snareCare:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/snareCare/BackLight");
                break;

            case AchievID.revive:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/revive/BackLight");
                break;

            case AchievID.cometPlumet:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/cometPlumet/BackLight");
                break;

            case AchievID.collector:
                obj = GameObject.Find("AtchievementsWindow/AtchievSlots/collector/BackLight");
                break;

            default:
                break;
        }
        if (obj != null)
            obj.SetActive(false);
    }

    public void SlotClick(int button)
    {
        TurnOffBackLight(currentSlotChoise);
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        switch (button)
        {
            case 1:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.shiny])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.Stars.ToString() + " / " + "8";
                }

                SlotClickHelper(AchievID.shiny);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/shiny").GetComponent<Image>());
                break;

            case 2:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.starry])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.Stars.ToString() + " / " + "15";
                }
                SlotClickHelper(AchievID.starry);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/starry").GetComponent<Image>());
                break;

            case 3:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.starrific])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.Stars.ToString() + " / " + "35";
                }
                SlotClickHelper(AchievID.starrific);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/starrific").GetComponent<Image>());
                break;

            case 4:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.startacular])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.Stars.ToString() + " / " + "56";
                }
                SlotClickHelper(AchievID.startacular);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/startacular").GetComponent<Image>());
                break;

            case 5:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.steppedUp])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.ChallengeLevelsCount.ToString() + " / " + "3";
                }
                SlotClickHelper(AchievID.steppedUp);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/steppedUp").GetComponent<Image>());
                break;

            case 6:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.distantDreams])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.ChallengeLevelsCount.ToString() + " / " + "7";
                }
                SlotClickHelper(AchievID.distantDreams);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/distantDreams").GetComponent<Image>());
                break;

            case 7:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.lionHeart])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.ChallengeLevelsCount.ToString() + " / " + "14";
                }
                SlotClickHelper(AchievID.lionHeart);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/lionHeart").GetComponent<Image>());
                break;

            case 8:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.treasureHunter])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.CompletedQuests + " / " + "25";
                }
                SlotClickHelper(AchievID.treasureHunter);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/treasureHunter").GetComponent<Image>());
                break;

            case 9:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.snareCare])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.UnitsKilledWithTrap.ToString() + " / " + "30";
                }
                SlotClickHelper(AchievID.snareCare);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/snareCare").GetComponent<Image>());
                break;

            case 10:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.revive])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.UnitsRevivedWithRevive.ToString() + " / " + "100";
                }
                SlotClickHelper(AchievID.revive);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/revive").GetComponent<Image>());
                break;

            case 11:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.cometPlumet])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.UnitsKillesWithStarfall.ToString() + " / " + "1000";
                }
                SlotClickHelper(AchievID.cometPlumet);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/cometPlumet").GetComponent<Image>());
                break;

            case 12:
                if (GameController.CurrentPlayerProfile.achievements[AchievID.collector])
                    TitleSetup(true);
                else
                {
                    TitleSetup(false);
                    countWindow.text = GameController.CurrentPlayerProfile.CardsOpened.ToString() + " / " + "12";
                }
                SlotClickHelper(AchievID.collector);
                SetUptitleUpgradeImage(GameObject.Find("AtchievementsWindow/AtchievSlots/collector").GetComponent<Image>());
                break;

            default:
                break;
        }
        TurnOnBackLight(currentSlotChoise);
    }

    private void SlotClickHelper(AchievID id)
    {
        currentSlotChoise = id;
        header.text = AchievmentData.achievInfo[id].header;
        description.text = AchievmentData.achievInfo[id].description;
    }

    private void TitleSetup(bool isOpen)
    {
        if (isOpen)
        {
            titleImageClose.gameObject.SetActive(false);
            titleImageOpen.gameObject.SetActive(true);
            countWindow.gameObject.SetActive(false);
        }
        else
        {
            titleImageClose.gameObject.SetActive(true);
            titleImageOpen.gameObject.SetActive(false);
            countWindow.gameObject.SetActive(true);
        }
    }
}