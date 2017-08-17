using UnityEngine.UI;
using UnityEngine;
using System;

public interface IMakeInactiveUnit
{
    void MakeInactive();
}

public class ShopManager : MonoBehaviour
{
    public static Action CancelAnimation;

    public int PaladinCost;
    public int MeorenaCost;
    public int MagicanCost;
    public int DancerCost;
    public int BerserkCost;
    public int NinjaCost;
    public int ShamanCost;
    public int CrossbowCost;
    public int RougeCost;

    public Text CoinsCount { set; get; }
    
    public Button PalladinBut;
    public Button MeorenaBut;
    public Button MagicanBut;
    public Button BladeDancerBut;
    public Button BerserkBut;
    public Button NinjaBut;
    public Button ShamanBut;
    public Button CrossbowBut;
    public Button RougeBut;

    public Animator windowAnim;
    public GameObject BuyingUnitsDialog;
    public GameObject KittyDialog;

    private static bool _isFirstTimeShown = true;


    void Start () {
        CoinsCount = GameObject.Find("CoinsCountImage/CoinsCountText").GetComponent<Text>();
        CoinsCount.text = GameController.CurrentPlayerProfile.Coins.ToString();
        windowAnim = gameObject.GetComponent<Animator>();
        windowAnim.SetBool("isOpen", true);
        PrepareUnits();
        
        if (_isFirstTimeShown)
            Invoke("ShowKitty", 2);
    }

    private void OnEnable()
    {
        if (windowAnim != null)
            windowAnim.SetBool("isOpen", true);
    }

    private void ShowKitty()
    {
        Instantiate(KittyDialog);
        _isFirstTimeShown = false;
    }

    public void PrepareUnits()
    {
        PrepareUnitsHelper(PalladinBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.palladin]);
        PrepareUnitsHelper(MeorenaBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.sentiel]);
        PrepareUnitsHelper(MagicanBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.wizard]);
        PrepareUnitsHelper(BladeDancerBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.dancer]);
        PrepareUnitsHelper(BerserkBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.berserker]);
        PrepareUnitsHelper(NinjaBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.assassin]);
        PrepareUnitsHelper(ShamanBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.clerc]);
        PrepareUnitsHelper(CrossbowBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.hunter]);
        PrepareUnitsHelper(RougeBut, GameController.CurrentPlayerProfile.activeUnits[UnitsID.rouge]);

        PalladinBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = PaladinCost.ToString();
        MeorenaBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = MeorenaCost.ToString();
        MagicanBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = MagicanCost.ToString();
        BladeDancerBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = DancerCost.ToString();
        BerserkBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = BerserkCost.ToString();
        NinjaBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = NinjaCost.ToString();
        ShamanBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = ShamanCost.ToString();
        CrossbowBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = CrossbowCost.ToString();
        RougeBut.transform.FindChild("Info/CostPlate/Cost").gameObject.GetComponent<Text>().text = RougeCost.ToString();
    }

    private void PrepareUnitsHelper(Button button, bool isActive)
    {
        if (isActive)
        {
            var arr = button.transform.FindChild("Sprites").GetComponentsInChildren<Image>();
            foreach (var image in arr)
            {
                image.color = new Color32(0, 0, 0, 70);
            }
            button.interactable = false;
            button.transform.FindChild("Info/CostPlate/Cost").gameObject.SetActive(false);
            button.transform.FindChild("Info/CostPlate/Crystal").gameObject.SetActive(false);
            button.transform.FindChild("Info/CostPlate/Sold").gameObject.SetActive(true);
        }
        else
        {
            button.transform.FindChild("Info/CostPlate/Sold").gameObject.SetActive(false);
        }
    }

    public void UnitClick(int button)
    {
        BuyingUnitsDialog instance = null;
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        switch (button)
        {
            case 1:
                if (GameController.CurrentPlayerProfile.Coins < PaladinCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = PalladinBut.GetComponent<PalladinShopAnim>();
                    instance.Initialize(PaladinCost, PalladinBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.palladin, CoinsCount, PrepareUnits, script);
                }
                break;

            case 2:
                if (GameController.CurrentPlayerProfile.Coins < MeorenaCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = MeorenaBut.GetComponent<MeorenaShopAnim>();
                    instance.Initialize(MeorenaCost, MeorenaBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.sentiel, CoinsCount, PrepareUnits, script);
                }
                break;

            case 3:
                if (GameController.CurrentPlayerProfile.Coins < MagicanCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = MagicanBut.GetComponent<MagicanShopAnim>();
                    instance.Initialize(MagicanCost, MagicanBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.wizard, CoinsCount, PrepareUnits, script);
                }
                break;

            case 4:
                if (GameController.CurrentPlayerProfile.Coins < DancerCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = BladeDancerBut.GetComponent<BladeDancerShopAnim>();
                    instance.Initialize(DancerCost, BladeDancerBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.dancer, CoinsCount, PrepareUnits, script);
                }
                break;
            case 5:
                if (GameController.CurrentPlayerProfile.Coins < BerserkCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = BerserkBut.GetComponent<BerserkerShopAnim>();
                    instance.Initialize(BerserkCost, BerserkBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.berserker, CoinsCount, PrepareUnits, script);
                }
                break;
            case 6:
                if (GameController.CurrentPlayerProfile.Coins < NinjaCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = NinjaBut.GetComponent<NinjaShopAnim>();
                    instance.Initialize(NinjaCost, NinjaBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.assassin, CoinsCount, PrepareUnits, script);
                }
                break;
            case 7:
                if (GameController.CurrentPlayerProfile.Coins < ShamanCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = ShamanBut.GetComponent<ShamanShopAnim>();
                    instance.Initialize(ShamanCost, ShamanBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.clerc, CoinsCount, PrepareUnits, script);
                }
                break;
            case 8:
                if (GameController.CurrentPlayerProfile.Coins < CrossbowCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = CrossbowBut.GetComponent<CrossbowMan>();
                    instance.Initialize(CrossbowCost, CrossbowBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.hunter, CoinsCount, PrepareUnits, script);
                }
                break;
            case 9:
                if (GameController.CurrentPlayerProfile.Coins < RougeCost)
                    DenySound();
                else
                {
                    instance = Instantiate(BuyingUnitsDialog).GetComponent<BuyingUnitsDialog>();
                    var script = RougeBut.GetComponent<RougeShopAnim>();
                    instance.Initialize(RougeCost, RougeBut.transform.FindChild("Info/Mirror/Title").GetComponent<Text>().text, UnitsID.rouge, CoinsCount, PrepareUnits, script);
                }
                break;

            default:
                break;
        }

    }

    private void DenySound()
    {
        windowAnim.Play("ShopWindowCoin", 1);
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ResetUpgradeClick), 1);
    }





}
