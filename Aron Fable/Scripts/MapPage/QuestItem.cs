using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    private QuestManager questManager;
    public LevelSteps RewardType { get; set; }
    public int QuestValue { get; set; }
    public bool IsRewardReady { get; set; }

    public int LvlIndex { get; set; }

    private void Awake()
    {
        questManager = GameObject.Find("QuestWindow").GetComponent<QuestManager>();
    }

    public void Click()
    {
        if (IsRewardReady)
            questManager.ClaimReward(gameObject);
        else
            questManager.RunLevelPreview(gameObject);
    }
}