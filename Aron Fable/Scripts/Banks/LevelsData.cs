using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelProgress
{
    // удцулдйцлдкжцй
    public int Stars { get; set; }

    /// <summary>
    /// Убийство боса означает, что уровень пройден на самой высокой сложности
    /// </summary>
    public bool IsBossKilled { get; set; }

    public bool IsChallengeDone { get; set; }
}

/// <summary>
/// Задает настройки в LevelPreview при выборе испытания
/// </summary>
[Serializable]
public class ChallengeModeSettings
{
    /// <summary>
    /// Задействовать только первые 4 типа в перечислениии
    /// </summary>
    public UpgradeID NoClass { get; set; }

    public string NoClassText { get; set; }

    public string MaxUpgrade { get; set; }
}

[Serializable]
public class LevelData
{
    public LevelData()
    {
        mode = new Dictionary<Difficulty, bool>();
        isCoinRewardTaken = new Dictionary<LevelSteps, bool>();
        isStarRewardTaken = new Dictionary<LevelSteps, bool>();
        LvlProgress = new LevelProgress();
        ChallengeData = new ChallengeModeSettings();

        for (int i = 0; i < Enum.GetNames(typeof(Difficulty)).Length; i++)
        {
            mode[(Difficulty)i] = false;
        }
        for (int i = 0; i < Enum.GetNames(typeof(LevelSteps)).Length; i++)
        {
            isCoinRewardTaken[(LevelSteps)i] = false;
            isStarRewardTaken[(LevelSteps)i] = false;
        }
    }

    public string SceneName { get; set; }
    public string SceneNameShort { get; set; }
    public string Describtion { get; set; }
    public bool IsVisible { get; set; }
    public int LvlIndex { get; set; }
    public ChallengeModeSettings ChallengeData { get; set; }

    /// <summary>
    /// Структура с достижениями по уровню.
    /// </summary>
    public LevelProgress LvlProgress { set; get; }

    /// <summary>
    /// На какой сложности пройден уровень. Плюшек не дает. Служит для лояльности игры.
    /// </summary>
    public Dictionary<Difficulty, bool> mode;

    /// <summary>
    /// Взята ли награда монетами за прохождение трех звезд, испытания, и убитого боса.
    /// </summary>
    public Dictionary<LevelSteps, bool> isCoinRewardTaken;

    /// <summary>
    /// Взята ли награда звездами за прохождение уровня на 1 2 и 3 звезды.
    /// </summary>
    public Dictionary<LevelSteps, bool> isStarRewardTaken;
}

public class LevelsData : MonoBehaviour
{
    public Dictionary<int, LevelData> LevelData;
    public int TotalLevels { get; set; }

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        TotalLevels = 5; // временный расчет на 5 уровней
        LevelData = new Dictionary<int, LevelData>(TotalLevels);

        LevelData[] lvlData = new LevelData[TotalLevels];
        for (int i = 0; i < lvlData.Length; i++)
        {
            lvlData[i] = new LevelData();
        }

        lvlData[0].SceneName = "Level 1";
        lvlData[0].SceneNameShort = "Lvl 1";
        lvlData[0].Describtion = "Describtion about how good this level is and how much you want it to get. It may be some story";
        lvlData[0].LvlIndex = 1;
        lvlData[0].ChallengeData.MaxUpgrade = "UPGRADES\nMAX LV. 1";
        lvlData[0].ChallengeData.NoClassText = "NO\nWARRIOR";
        lvlData[0].ChallengeData.NoClass = UpgradeID.warrior;

        lvlData[1].SceneName = "Level 2";
        lvlData[1].SceneNameShort = "Lvl 2";
        lvlData[1].Describtion = "Describtion about how good this level is and how much you want it to get. It may be some story";
        lvlData[1].LvlIndex = 2;
        lvlData[1].ChallengeData.MaxUpgrade = "UPGRADES\nMAX LV. 1";
        lvlData[1].ChallengeData.NoClassText = "NO\nWARRIOR";
        lvlData[1].ChallengeData.NoClass = UpgradeID.warrior;

        lvlData[2].SceneName = "Level 3";
        lvlData[2].SceneNameShort = "Lvl 3";
        lvlData[2].Describtion = "Describtion about how good this level is and how much you want it to get. It may be some story";
        lvlData[2].LvlIndex = 3;
        lvlData[2].ChallengeData.MaxUpgrade = "UPGRADES\nMAX LV. 1";
        lvlData[2].ChallengeData.NoClassText = "NO\nWARRIOR";
        lvlData[2].ChallengeData.NoClass = UpgradeID.warrior;

        lvlData[3].SceneName = "Level 4";
        lvlData[3].SceneNameShort = "Lvl 4";
        lvlData[3].Describtion = "Describtion about how good this level is and how much you want it to get. It may be some story";
        lvlData[3].LvlIndex = 4;
        lvlData[3].ChallengeData.MaxUpgrade = "UPGRADES\nMAX LV. 2";
        lvlData[3].ChallengeData.NoClassText = "NO\nWARRIOR";
        lvlData[3].ChallengeData.NoClass = UpgradeID.warrior;

        lvlData[4].SceneName = "Level 5";
        lvlData[4].SceneNameShort = "Lvl 5";
        lvlData[4].Describtion = "Describtion about how good this level is and how much you want it to get. It may be some story";
        lvlData[4].LvlIndex = 5;
        lvlData[4].ChallengeData.MaxUpgrade = "UPGRADES\nMAX LV. 2";
        lvlData[4].ChallengeData.NoClassText = "NO\nRANGE";
        lvlData[4].ChallengeData.NoClass = UpgradeID.range;

        for (int i = 0; i < lvlData.Length; i++)
        {
            LevelData[i] = lvlData[i];
        }
    }
}