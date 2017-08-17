using System;
using UnityEngine;
using UnityEngine.UI;

public class CollectionEnemiesManager : MonoBehaviour
{
    private EnemiesID currentSlotChoise;
    private Text titleText;
    private Text describingText;
    private Image titleImage;
    private Image leftElipse;
    private Text hp;
    private Text armor;
    private Text attackPower;
    private Text speed;

    public Sprite closedSprite;
    public Sprite potsikElipseSprite;
    public Sprite shpinaticElipseSprite;
    public Sprite doggieElipseSprite;
    public Sprite littleWizardElipseSprite;
    public Sprite venerinaElipseSprite;
    public Sprite bearBossElipseSprite;

    private int currentResolution;
    private int cellSize;

    private void Start()
    {
        leftElipse = GameObject.Find("CollectionWindow/EnemiesPanel/LeftPanel").GetComponent<Image>();
        titleText = GameObject.Find("LeftPanel/TitleText").GetComponent<Text>();
        describingText = GameObject.Find("LeftPanel/Describtion").GetComponent<Text>();
        titleImage = GameObject.Find("TitlePanel").GetComponent<Image>();
        hp = GameObject.Find("SkillsPanel/HealthText").GetComponent<Text>();
        armor = GameObject.Find("SkillsPanel/ArmorText").GetComponent<Text>();
        attackPower = GameObject.Find("SkillsPanel/AttackText").GetComponent<Text>();
        speed = GameObject.Find("SkillsPanel/SpeedText").GetComponent<Text>();
        if (describingText == null || hp == null || armor == null || attackPower == null || speed == null)
            print("Can't find objects!!!");
        OpenSlots();
        currentSlotChoise = EnemiesID.potsik;
        UnitPanelClick(1);
    }

    private void OpenSlots()
    {
        Image[] images = GameObject.Find("EnemiesPanel/EnemiesSlot/BrickWall").GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].name == "BackLight")
                continue;
            if (images[i].name == "potsik" && !GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.potsik])
            {
                images[i].sprite = closedSprite;
            }
            else if (images[i].name == "shpinatic" && !GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.shpinatic])
            {
                images[i].sprite = closedSprite;
            }
            else if (images[i].name == "doggie" && !GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.doggie])
            {
                images[i].sprite = closedSprite;
            }
            else if (images[i].name == "littleWizard" && !GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.littleWizard])
            {
                images[i].sprite = closedSprite;
            }
            else if (images[i].name == "venerina" && !GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.venerina])
            {
                images[i].sprite = closedSprite;
            }
            else if (images[i].name == "bearBoss" && !GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.bearBoss])
            {
                images[i].sprite = closedSprite;
            }
            
        }
    }

    public void CloseClick()
    {
        gameObject.SetActive(false);
    }

    public void BackClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameObject.Find("CollectionWindow").GetComponent<CollectionWindManager>().unitsEnemiesTipsWindow.SetActive(true);
        GameObject.Find("CollectionWindow").GetComponent<CollectionWindManager>().collectionAnim.SetBool("isOpen", true);
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

    private void TurnOnBackLight(EnemiesID id)
    {
        Component[] objects = null;
        switch (id)
        {
            case EnemiesID.potsik:
                objects = GameObject.Find("EnemiesSlot/BrickWall/potsik").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.shpinatic:
                objects = GameObject.Find("EnemiesSlot/BrickWall/shpinatic").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.doggie:
                objects = GameObject.Find("EnemiesSlot/BrickWall/doggie").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.littleWizard:
                objects = GameObject.Find("EnemiesSlot/BrickWall/littleWizard").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.venerina:
                objects = GameObject.Find("EnemiesSlot/BrickWall/venerina").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.bearBoss:
                objects = GameObject.Find("EnemiesSlot/BrickWall/bearBoss").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.butterfly:
                objects = GameObject.Find("EnemiesSlot/BrickWall/butterfly").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.banditChief:
                objects = GameObject.Find("EnemiesSlot/BrickWall/banditChief").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.hornet:
                objects = GameObject.Find("EnemiesSlot/BrickWall/hornet").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.stinger:
                objects = GameObject.Find("EnemiesSlot/BrickWall/stinger").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.goo:
                objects = GameObject.Find("EnemiesSlot/BrickWall/goo").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.smallGoo:
                objects = GameObject.Find("EnemiesSlot/BrickWall/smallGoo").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.murkee:
                objects = GameObject.Find("EnemiesSlot/BrickWall/murkee").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.darkWalker:
                objects = GameObject.Find("EnemiesSlot/BrickWall/darkWalker").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.phantomNear:
                objects = GameObject.Find("EnemiesSlot/BrickWall/phantomNear").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.wildTreant:
                objects = GameObject.Find("EnemiesSlot/BrickWall/wildTreant").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.prongle:
                objects = GameObject.Find("EnemiesSlot/BrickWall/prongle").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.cursedBat:
                objects = GameObject.Find("EnemiesSlot/BrickWall/cursedBat").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.clown:
                objects = GameObject.Find("EnemiesSlot/BrickWall/clown").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            case EnemiesID.loonatic:
                objects = GameObject.Find("EnemiesSlot/BrickWall/loonatic").transform.GetComponentsInChildren(typeof(Transform), true);
                break;

            default:
                break;
        }
        GetInactiveObject(objects, "BackLight").SetActive(true);
    }

    private void TurnOffBackLight(EnemiesID id)
    {
        GameObject obj = null;
        switch (id)
        {
            case EnemiesID.potsik:
                obj = GameObject.Find("EnemiesSlot/BrickWall/potsik/BackLight");

                break;

            case EnemiesID.shpinatic:
                obj = GameObject.Find("EnemiesSlot/BrickWall/shpinatic/BackLight");
                break;

            case EnemiesID.doggie:
                obj = GameObject.Find("EnemiesSlot/BrickWall/doggie/BackLight");
                break;

            case EnemiesID.littleWizard:
                obj = GameObject.Find("EnemiesSlot/BrickWall/littleWizard/BackLight");
                break;

            case EnemiesID.venerina:
                obj = GameObject.Find("EnemiesSlot/BrickWall/venerina/BackLight");
                break;

            case EnemiesID.bearBoss:
                obj = GameObject.Find("EnemiesSlot/BrickWall/bearBoss/BackLight");
                break;

            case EnemiesID.butterfly:
                obj = GameObject.Find("EnemiesSlot/BrickWall/butterfly/BackLight");
                break;

            case EnemiesID.banditChief:
                obj = GameObject.Find("EnemiesSlot/BrickWall/banditChief/BackLight");
                break;

            case EnemiesID.hornet:
                obj = GameObject.Find("EnemiesSlot/BrickWall/hornet/BackLight");
                break;

            case EnemiesID.stinger:
                obj = GameObject.Find("EnemiesSlot/BrickWall/stinger/BackLight");
                break;

            case EnemiesID.goo:
                obj = GameObject.Find("EnemiesSlot/BrickWall/goo/BackLight");
                break;

            case EnemiesID.smallGoo:
                obj = GameObject.Find("EnemiesSlot/BrickWall/smallGoo/BackLight");
                break;

            case EnemiesID.murkee:
                obj = GameObject.Find("EnemiesSlot/BrickWall/murkee/BackLight");
                break;

            case EnemiesID.darkWalker:
                obj = GameObject.Find("EnemiesSlot/BrickWall/darkWalker/BackLight");
                break;

            case EnemiesID.phantomNear:
                obj = GameObject.Find("EnemiesSlot/BrickWall/phantomNear/BackLight");
                break;

            case EnemiesID.wildTreant:
                obj = GameObject.Find("EnemiesSlot/BrickWall/wildTreant/BackLight");
                break;

            case EnemiesID.prongle:
                obj = GameObject.Find("EnemiesSlot/BrickWall/prongle/BackLight");
                break;

            case EnemiesID.cursedBat:
                obj = GameObject.Find("EnemiesSlot/BrickWall/cursedBat/BackLight");
                break;

            case EnemiesID.clown:
                obj = GameObject.Find("EnemiesSlot/BrickWall/clown/BackLight");
                break;

            case EnemiesID.loonatic:
                obj = GameObject.Find("EnemiesSlot/BrickWall/loonatic/BackLight");
                break;

            default:
                break;
        }
        if (obj != null)
            obj.SetActive(false);
    }

    public void UnitPanelClick(int button)
    {
        switch (button)
        {
            case 1:
                if (!GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.potsik])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = EnemiesID.potsik;
                describingText.text = EnemiesData.enemiesInfo[EnemiesID.potsik].describingText;
                titleText.text = EnemiesData.enemiesInfo[EnemiesID.potsik].titleText;
                titleImage.sprite = EnemiesData.enemiesInfo[EnemiesID.potsik].cardSprite;
                hp.text = EnemiesData.enemiesInfo[EnemiesID.potsik].hp;
                armor.text = EnemiesData.enemiesInfo[EnemiesID.potsik].armor;
                attackPower.text = EnemiesData.enemiesInfo[EnemiesID.potsik].attackPower.My_ToString();
                speed.text = EnemiesData.enemiesInfo[EnemiesID.potsik].speed;
                leftElipse.sprite = potsikElipseSprite;
                if (GameController.Sound)
                    Destroy(Instantiate(SoundBank.ClickSound), 1);
                break;

            case 2:
                if (!GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.shpinatic])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = EnemiesID.shpinatic;
                describingText.text = EnemiesData.enemiesInfo[EnemiesID.shpinatic].describingText;
                titleText.text = EnemiesData.enemiesInfo[EnemiesID.shpinatic].titleText;
                titleImage.sprite = EnemiesData.enemiesInfo[EnemiesID.shpinatic].cardSprite;
                hp.text = EnemiesData.enemiesInfo[EnemiesID.shpinatic].hp;
                armor.text = EnemiesData.enemiesInfo[EnemiesID.shpinatic].armor;
                attackPower.text = EnemiesData.enemiesInfo[EnemiesID.shpinatic].attackPower.My_ToString();
                speed.text = EnemiesData.enemiesInfo[EnemiesID.shpinatic].speed;
                leftElipse.sprite = shpinaticElipseSprite;
                if (GameController.Sound)
                    Destroy(Instantiate(SoundBank.ClickSound), 1);
                break;

            case 3:
                if (!GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.doggie])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = EnemiesID.doggie;
                describingText.text = EnemiesData.enemiesInfo[EnemiesID.doggie].describingText;
                titleText.text = EnemiesData.enemiesInfo[EnemiesID.doggie].titleText;
                titleImage.sprite = EnemiesData.enemiesInfo[EnemiesID.doggie].cardSprite;
                hp.text = EnemiesData.enemiesInfo[EnemiesID.doggie].hp;
                armor.text = EnemiesData.enemiesInfo[EnemiesID.doggie].armor;
                attackPower.text = EnemiesData.enemiesInfo[EnemiesID.doggie].attackPower.My_ToString();
                speed.text = EnemiesData.enemiesInfo[EnemiesID.doggie].speed;
                leftElipse.sprite = doggieElipseSprite;
                if (GameController.Sound)
                    Destroy(Instantiate(SoundBank.ClickSound), 1);
                break;

            case 4:
                if (!GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.littleWizard])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = EnemiesID.littleWizard;
                describingText.text = EnemiesData.enemiesInfo[EnemiesID.littleWizard].describingText;
                titleText.text = EnemiesData.enemiesInfo[EnemiesID.littleWizard].titleText;
                titleImage.sprite = EnemiesData.enemiesInfo[EnemiesID.littleWizard].cardSprite;
                hp.text = EnemiesData.enemiesInfo[EnemiesID.littleWizard].hp;
                armor.text = EnemiesData.enemiesInfo[EnemiesID.littleWizard].armor;
                attackPower.text = EnemiesData.enemiesInfo[EnemiesID.littleWizard].attackPower.My_ToString();
                speed.text = EnemiesData.enemiesInfo[EnemiesID.littleWizard].speed;
                leftElipse.sprite = littleWizardElipseSprite;
                if (GameController.Sound)
                    Destroy(Instantiate(SoundBank.ClickSound), 1);
                break;

            case 5:
                if (!GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.venerina])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = EnemiesID.venerina;
                describingText.text = EnemiesData.enemiesInfo[EnemiesID.venerina].describingText;
                titleText.text = EnemiesData.enemiesInfo[EnemiesID.venerina].titleText;
                titleImage.sprite = EnemiesData.enemiesInfo[EnemiesID.venerina].cardSprite;
                hp.text = EnemiesData.enemiesInfo[EnemiesID.venerina].hp;
                armor.text = EnemiesData.enemiesInfo[EnemiesID.venerina].armor;
                attackPower.text = EnemiesData.enemiesInfo[EnemiesID.venerina].attackPower.My_ToString();
                speed.text = EnemiesData.enemiesInfo[EnemiesID.venerina].speed;
                leftElipse.sprite = venerinaElipseSprite;
                if (GameController.Sound)
                    Destroy(Instantiate(SoundBank.ClickSound), 1);
                break;

            case 6:
                if (!GameController.CurrentPlayerProfile.activeEnemies[EnemiesID.bearBoss])
                    return;
                TurnOffBackLight(currentSlotChoise);
                currentSlotChoise = EnemiesID.bearBoss;
                describingText.text = EnemiesData.enemiesInfo[EnemiesID.bearBoss].describingText;
                titleText.text = EnemiesData.enemiesInfo[EnemiesID.bearBoss].titleText;
                titleImage.sprite = EnemiesData.enemiesInfo[EnemiesID.bearBoss].cardSprite;
                hp.text = EnemiesData.enemiesInfo[EnemiesID.bearBoss].hp;
                armor.text = EnemiesData.enemiesInfo[EnemiesID.bearBoss].armor;
                attackPower.text = EnemiesData.enemiesInfo[EnemiesID.bearBoss].attackPower.My_ToString();
                speed.text = EnemiesData.enemiesInfo[EnemiesID.bearBoss].speed;
                leftElipse.sprite = bearBossElipseSprite;
                if (GameController.Sound)
                    Destroy(Instantiate(SoundBank.ClickSound), 1);
                break;

            

            default:
                break;
        }
        TurnOnBackLight(currentSlotChoise);
    }
}