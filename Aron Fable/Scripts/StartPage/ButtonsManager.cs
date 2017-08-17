using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{
    public Sprite soundOff;
    public Sprite soundOn;
    public Sprite musicOff;
    public Sprite musicOn;

    private GameObject slotsObject;
    private Transform[] slots = new Transform[3];
    private Animator slotsAnimator;
    private Animator exitSoundMusicAnim;
    private Animator startsCreditAnim;
    private Animator fBTwitterCups;

    private GameObject upgradeNotification;
    private GameObject questNotification;

    private float startCreditsAnimTime;
    private float slotsAnimTime;

    private RuntimeAnimatorController sa; // для получения длины времени анимации
    private RuntimeAnimatorController sc; // для получения длины времени анимации
    private bool pushSlots;
    private bool pushStart;

    private void Start()
    {
        SetUpMusicAndSound();
        if (SceneManager.GetActiveScene().name == "StartPage")
        {
            slotsObject = GameObject.Find("Slots");
            SlotsSetup();
            slotsAnimator = slotsObject.transform.Find("Panel").GetComponent<Animator>();
            exitSoundMusicAnim = GameObject.Find("ExitSoundMusic").GetComponent<Animator>();
            startsCreditAnim = GameObject.Find("StartCredits").GetComponent<Animator>();
            fBTwitterCups = GameObject.Find("FBTwitterCups").GetComponent<Animator>();
            exitSoundMusicAnim.SetBool("isOpen", true);
            startsCreditAnim.SetBool("isOpen", true);
            fBTwitterCups.SetBool("isOpen", true);

            sa = slotsAnimator.runtimeAnimatorController;
            sc = startsCreditAnim.runtimeAnimatorController;
            startCreditsAnimTime = sc.animationClips[0].length;
            slotsAnimTime = sa.animationClips[0].length;
        }

        if (SceneManager.GetActiveScene().name == "MapPage")
        {
            upgradeNotification = GameObject.Find("Upgrades/Notification");
            questNotification = GameObject.Find("Quests/Notification");
            SetUpCoinsAndStars();
            СheckForAvailableUpdates();
            CheckForAvailableNotification();
        }
    }
    private void SlotsSetup()
    {
        slots[0] = slotsObject.transform.FindChild("Panel/Slot1");
        slots[1] = slotsObject.transform.FindChild("Panel/Slot2");
        slots[2] = slotsObject.transform.FindChild("Panel/Slot3");

        for (int i = 0; i < SaveLoad.savedGames.Count; i++)
        {
            if (SaveLoad.savedGames[i] != null)
            {
                string text = (SaveLoad.savedGames[i].CardsOpened) + "/" + GameController.MaxCards.ToString();
                slots[i].transform.FindChild("cardText").GetComponent<Text>().text = text;
                text = SaveLoad.savedGames[i].Stars + "/" + GameController.MaxStars.ToString();
                slots[i].transform.FindChild("starText").GetComponent<Text>().text = text;
                slots[i].transform.FindChild("coinText").GetComponent<Text>().text = SaveLoad.savedGames[i].Coins.ToString();
                slots[i].transform.FindChild("NewGameImage").GetComponent<Image>().gameObject.SetActive(false);
            }
            else
                NewGameSlot(i);
        }

        slotsObject.SetActive(false);
    }

    private void NewGameSlot(int index)
    {
        slots[index].transform.FindChild("cardText").gameObject.SetActive(false);
        slots[index].transform.FindChild("starText").gameObject.SetActive(false);
        slots[index].transform.FindChild("coinText").gameObject.SetActive(false);
        slots[index].transform.FindChild("cardImage").gameObject.SetActive(false);
        slots[index].transform.FindChild("starImage").gameObject.SetActive(false);
        slots[index].transform.FindChild("coinsImage").gameObject.SetActive(false);
        slots[index].transform.FindChild("DeleteSlotButton").gameObject.SetActive(false);
    }

    private int ReturnAvailableUpdates(UpgradeID id)
    {
        int total = 0;
        if (GameController.CurrentPlayerProfile.upgrades[id].Is1Done)
            total += CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[0];
        if (GameController.CurrentPlayerProfile.upgrades[id].Is2Done)
            total += CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[1];
        if (GameController.CurrentPlayerProfile.upgrades[id].Is3Done)
            total += CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[2];
        if (GameController.CurrentPlayerProfile.upgrades[id].Is4Done)
            total += CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[3];
        if (GameController.CurrentPlayerProfile.upgrades[id].Is5Done)
            total += CharactersData.UpgrageCost[UpgradeID.warrior].upgradeRankCost[4];

        return total;
    }
    public void СheckForAvailableUpdates()
    {

        int warCount = ReturnAvailableUpdates(UpgradeID.warrior);
        int rangeCount = ReturnAvailableUpdates(UpgradeID.range);
        int mageCount = ReturnAvailableUpdates(UpgradeID.magic);
        int stealthCount = ReturnAvailableUpdates(UpgradeID.stealth);

        int sum = warCount + rangeCount + mageCount + stealthCount;
        int availableStars = GameController.CurrentPlayerProfile.Stars - sum;
        if (availableStars > 0)
        {
            upgradeNotification.SetActive(true);
            upgradeNotification.transform.Find("Text").GetComponent<Text>().text = availableStars.ToString();
        }
        else
        {
            upgradeNotification.SetActive(false);
        }

    }

    public void CheckForAvailableNotification()
    {
        int count = GameController.CurrentPlayerProfile.levelData.Count;
        for (int i = 0; i < count; i++)
        {
            LevelData currentLvlData = GameController.CurrentPlayerProfile.levelData[i];
            if (!currentLvlData.IsVisible)
                continue;
            bool rewardStar3 = currentLvlData.isCoinRewardTaken[LevelSteps.star3];
            bool rewardChallenge = currentLvlData.isCoinRewardTaken[LevelSteps.challenge];
            bool rewardBoss = currentLvlData.isCoinRewardTaken[LevelSteps.boss];

            if (currentLvlData.LvlProgress.Stars != 3)
            {
                continue;
            }
            if (!rewardStar3)
            {
                questNotification.SetActive(true);
                return;
            }
            if (currentLvlData.LvlProgress.IsChallengeDone && !rewardChallenge)
            {
                questNotification.SetActive(true);
                return;
            }
            if (currentLvlData.LvlProgress.IsBossKilled && !rewardBoss)
            {
                questNotification.SetActive(true);
                return;
            }
        }

        //questNotification.SetActive(false);
    }
    private void SetUpCoinsAndStars()
    {
        string text = GameController.CurrentPlayerProfile.Stars.ToString() + " / " + GameController.MaxStars.ToString();
        Text starText = GameObject.Find("StarInfo/StarText").GetComponent<Text>();
        Text coinsText = GameObject.Find("CoinsInfo/CoinsText").GetComponent<Text>();
        starText.text = text;
        text = GameController.CurrentPlayerProfile.Coins.ToString();
        coinsText.text = text;
    }

    private void SetUpMusicAndSound()
    {
        if (GameController.Sound)
            GameObject.Find("Sound").GetComponent<Image>().sprite = soundOn;
        else
            GameObject.Find("Sound").GetComponent<Image>().sprite = soundOff;

        if (GameController.Music)
        {
            GameObject.Find("Music").GetComponent<Image>().sprite = musicOn;
        }
        else
        {
            GameObject.Find("Music").GetComponent<Image>().sprite = musicOff;
        }
    }

    public void DeleteSlotData(int select)
    {
        if(GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        SaveLoad.savedGames[select] = null;
        NewGameSlot(select);
        slots[select].transform.FindChild("NewGameImage").GetComponent<Image>().gameObject.SetActive(true);
        SaveLoad.Save(null, select);
    }

    public void ExitClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        Application.Quit();
    }

    public void SoundClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameController.Sound = !GameController.Sound;
        SetUpMusicAndSound();
    }

    public void MusicClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameController.Music = !GameController.Music;
        GameObject.Find("GameController/AudioController").GetComponent<SoundController>().MusicOnOff();
        SetUpMusicAndSound();
    }

    public void FasebookClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        Application.OpenURL("https://www.facebook.com/profile.php?id=100001390822365");
    }

    public void TwitterClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        Application.OpenURL("https://vk.com/thedefong");
    }

    public void BackOnMapClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        SaveLoad.Save(GameController.CurrentPlayerProfile, GameController.CurrentSlot);
        FadeInOut.EndScene("StartPage");
    }

    public void StartClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        pushStart = true;
        exitSoundMusicAnim.SetBool("isOpen", false);
        startsCreditAnim.SetBool("isOpen", false);
        fBTwitterCups.SetBool("isOpen", false);
    }

    public void BackToStartPageClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        slotsAnimator.SetBool("isOpen", false);

        pushSlots = true;
    }

    private void Update()
    {
        if (pushSlots)
            PushSlots();
        if (pushStart)
            PushStart();
    }

    private void PushSlots()
    {
        slotsAnimTime -= Time.deltaTime;
        if (slotsAnimTime < 0)
        {
            startsCreditAnim.SetBool("isOpen", true);
            pushSlots = false;
            slotsAnimTime = sa.animationClips[0].length;
            exitSoundMusicAnim.SetBool("isOpen", true);
            fBTwitterCups.SetBool("isOpen", true);
        }
    }

    private void PushStart()
    {
        startCreditsAnimTime -= Time.deltaTime;
        if (startCreditsAnimTime < 0)
        {
            slotsObject.SetActive(true);
            slotsAnimator.SetBool("isOpen", true);

            pushStart = false;
            startCreditsAnimTime = sc.animationClips[0].length;
        }
    }

    public void SlotClick(int select)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameController.CurrentSlot = select;
        PlayerProfile player = null;
        if (SaveLoad.savedGames[select] != null)
        {
            player = SaveLoad.savedGames[select];
            GameController.CurrentPlayerProfile = player;
            FadeInOut.EndScene("MapPage");
        }
        else
        {
            player = new PlayerProfile();
            GameController.CurrentPlayerProfile = player;
            LevelSettings.CurrentLvlData = LevelSteps.star3;
            LevelSettings.CurrentLvlDifficulty = Difficulty.easy;
            FadeInOut.EndScene("Story");
        }
    }
}