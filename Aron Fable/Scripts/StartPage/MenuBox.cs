using UnityEngine;
using UnityEngine.UI;

public class MenuBox : MonoBehaviour
{
    private GameObject upgradeWindow;
    private GameObject collectionWindow;
    private GameObject atchievementWindow;
    private GameObject questWindow;
    private GameObject shopWindow;
    private GameObject background;
    private float animationLength; // длинна всех анимаций (все анимации делал ровно пол секунды (30 фреймов из 60))
    private GameObject currentObjectToClose;
    private Animator backgoundAnim;
    private Animator backGroundButtons;
    private bool isClosing;
    
    private void Start()
    {
        background = transform.FindChild("Background").gameObject;
        upgradeWindow = background.transform.FindChild("UpgradeWindow").gameObject;
        collectionWindow = background.transform.FindChild("CollectionWindow").gameObject;
        atchievementWindow = background.transform.FindChild("AtchievementsWindow").gameObject;
        questWindow = background.transform.FindChild("QuestWindow").gameObject;
        shopWindow = background.transform.FindChild("ShopWindow").gameObject;
        backgoundAnim = background.GetComponent<Animator>();
        backGroundButtons = GameObject.Find("BackGround").GetComponent<Animator>();
        backGroundButtons.SetBool("moveOut", false);
    }

    public void UpgradeClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        background.SetActive(true);
        backgoundAnim.SetBool("isOpen", true);
        upgradeWindow.SetActive(true);
        backGroundButtons.SetBool("moveOut", true);
    }

    public void CollectionClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        background.SetActive(true);
        collectionWindow.SetActive(true);
        backgoundAnim.SetBool("isOpen", true);
        backGroundButtons.SetBool("moveOut", true);
    }

    public void AtchievmentClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        background.SetActive(true);
        backgoundAnim.SetBool("isOpen", true);
        atchievementWindow.SetActive(true);
        backGroundButtons.SetBool("moveOut", true);
    }

    public void QuestsClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        background.SetActive(true);
        backgoundAnim.SetBool("isOpen", true);
        questWindow.SetActive(true);
        backGroundButtons.SetBool("moveOut", true);
    }

    public void ShopClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        background.SetActive(true);
        backgoundAnim.SetBool("isOpen", true);
        shopWindow.SetActive(true);
        backGroundButtons.SetBool("moveOut", true);
    }

    public void CloseClick(GameObject obj)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        currentObjectToClose = obj;
        currentObjectToClose.GetComponent<Animator>().SetBool("isOpen", false);
        backgoundAnim.SetBool("isOpen", true);
        isClosing = true;
        if (currentObjectToClose.name == "CollectionWindow")
            animationLength = 0.417f;
        else
            animationLength = 0.167f;
        if (currentObjectToClose.name == "ShopWindow")
        {
            if(ShopManager.CancelAnimation != null)
                ShopManager.CancelAnimation.Invoke();
        }
    }

    private void CloseWindow()
    {
        isClosing = false;
        string name =  currentObjectToClose.name;
        if (name == "CollectionWindow")
            GameObject.Find("UnitsEnemiesTips").SetActive(false);
        if(name == "UnitsPanel" || name == "EnemiesPanel" || name == "TipsPanel")
            collectionWindow.SetActive(false);
        if (name == "UpgradeWindow")
            GameObject.Find("Buttons").GetComponent<ButtonsManager>().СheckForAvailableUpdates();
        if (name == "QuestWindow")
            GameObject.Find("Buttons").GetComponent<ButtonsManager>().CheckForAvailableNotification();
        background.SetActive(false);
        currentObjectToClose.SetActive(false);
        
        backGroundButtons.SetBool("moveOut", false);
    }

    private void Update()
    {
        if (isClosing)
        {
            animationLength -= Time.deltaTime;
            if (animationLength < 0)
                CloseWindow();
        }
    }
}