using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    private GameObject questWindow;
    private Text coinsCount;
    private MenuBox menuBoxScript;
    private Animator windowAnim;

    public GameObject questItemPref;
    public Sprite Crystal1;
    public Sprite Crystal2;
    public Sprite Crystal3;
    public Sprite Crystal1Light;
    public Sprite Crystal2Light;
    public Sprite Crystal3Light;
    public Sprite EasyStrip;
    public Sprite MiddleStrip;
    public Sprite HardStrip;
    public Sprite EasyMode;
    public Sprite MiddleMode;
    public Sprite HardMode;
    public GameObject levelPreviewPref;


    private void Awake()
    {
        questWindow = GameObject.Find("Table/Quests");
        coinsCount = GameObject.Find("CoinsCountPanel/CoinsCountText").GetComponent<Text>();

        menuBoxScript = GameObject.Find("MenuBox").GetComponent<MenuBox>();

        windowAnim = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        PrepareQuestWindow();
        windowAnim.SetBool("isOpen", true);
        coinsCount.text = GameController.CurrentPlayerProfile.Coins.ToString();
        SetUpSellSize();
    }

    private void SetUpSellSize()
    {
        
    }

    private void OnEnable()
    {
        if (windowAnim != null)
            windowAnim.SetBool("isOpen", true);
    }

    private void PrepareQuestWindow()
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
                GameObject instance = Instantiate(questItemPref, questWindow.transform) as GameObject;
                PrepareQuestHelper(instance, currentLvlData, EasyStrip, EasyMode, 1, false, LevelSteps.star3);
                continue;
            }
            // все, что ниже, будет подразумеваться, что 3 звезды уже взяты.

            if (!rewardStar3)
            {
                GameObject instance = Instantiate(questItemPref, questWindow.transform) as GameObject;
                PrepareQuestHelper(instance, currentLvlData, EasyStrip, EasyMode, 1, true, LevelSteps.star3);
            }

            if (!currentLvlData.LvlProgress.IsChallengeDone)
            {
                GameObject instance = Instantiate(questItemPref, questWindow.transform) as GameObject;
                PrepareQuestHelper(instance, currentLvlData, MiddleStrip, MiddleMode, 2, false, LevelSteps.challenge);
            }
            else if (currentLvlData.LvlProgress.IsChallengeDone && !rewardChallenge)
            {
                GameObject instance = Instantiate(questItemPref, questWindow.transform) as GameObject;
                PrepareQuestHelper(instance, currentLvlData, MiddleStrip, MiddleMode, 2, true, LevelSteps.challenge);
            }

            if (!currentLvlData.LvlProgress.IsBossKilled)
            {
                GameObject instance = Instantiate(questItemPref, questWindow.transform) as GameObject;
                PrepareQuestHelper(instance, currentLvlData, HardStrip, HardMode, 3, false, LevelSteps.boss);
            }
            else if (currentLvlData.LvlProgress.IsBossKilled && !rewardBoss)
            {
                GameObject instance = Instantiate(questItemPref, questWindow.transform) as GameObject;
                PrepareQuestHelper(instance, currentLvlData, HardStrip, HardMode, 3, true, LevelSteps.boss);
            }
        }
    }

    private void PrepareQuestHelper(GameObject instance, LevelData currentLvlData, Sprite strip, Sprite challengeImage, int coins, bool isClaim, LevelSteps questType)
    {
        var rect = instance.transform;
        rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f); //не понятно чего оно тулит в окно элементы дикого размера. Это костыль, так как не разобрался в чем причина.
        rect.localScale = new Vector3(1f, 1f, 1f);

        QuestItem questItem = instance.GetComponent<QuestItem>();
        questItem.QuestValue = coins;
        questItem.RewardType = questType;
        questItem.LvlIndex = currentLvlData.LvlIndex;

        instance.GetComponentInChildren<Text>().text = currentLvlData.SceneNameShort;
        

        instance.transform.Find("LineDifficult").GetComponent<Image>().sprite = strip;
        instance.transform.Find("QuestImage").GetComponent<Image>().sprite = challengeImage;
        Transform reward = instance.transform.Find("Reward");
        if (isClaim)
        {
            instance.GetComponent<Animator>().Play("rewardIsReady");
            instance.GetComponent<QuestItem>().IsRewardReady = true;
        }

        if (coins == 1)
        {
            reward.GetComponent<Image>().sprite = Crystal1;
            reward.FindChild("Light").GetComponent<Image>().sprite = Crystal1Light;
        }
            
        else if (coins == 2)
        {
            reward.GetComponent<Image>().sprite = Crystal2;
            reward.FindChild("Light").GetComponent<Image>().sprite = Crystal2Light;
        }
            
        else if (coins == 3)
        {
            reward.GetComponent<Image>().sprite = Crystal3;
            reward.FindChild("Light").GetComponent<Image>().sprite = Crystal3Light;
        }
            
    }

    public void ClaimReward(GameObject obj)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.CoinsAdd), 2);
        int questValue = obj.GetComponent<QuestItem>().QuestValue;
        int lvlIndex = obj.GetComponent<QuestItem>().LvlIndex - 1;
        LevelSteps rewardType = obj.GetComponent<QuestItem>().RewardType;
        GameController.CurrentPlayerProfile.levelData[lvlIndex].isCoinRewardTaken[rewardType] = true;
        GameController.CurrentPlayerProfile.Coins += questValue;
        GameObject.Find("CoinsInfo/CoinsText").GetComponent<Text>().text = coinsCount.text = GameController.CurrentPlayerProfile.Coins.ToString();
        DestroyObject(obj);
    }

    public void RunLevelPreview(GameObject obj)
    {
        GameObject instance = Instantiate(levelPreviewPref);
        int lvlIndex = obj.GetComponent<QuestItem>().LvlIndex;
        LevelProgress lvlProgress = GameController.CurrentPlayerProfile.levelData[lvlIndex - 1].LvlProgress;
        instance.GetComponent<LevelPreview>().Initialize(lvlIndex, lvlProgress, obj.GetComponent<QuestItem>().RewardType, instance.transform);
        menuBoxScript.CloseClick(gameObject);
    }
}
