using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradesStats
{
    public bool Is1Done { get; set; }
    public bool Is2Done { get; set; }
    public bool Is3Done { get; set; }
    public bool Is4Done { get; set; }
    public bool Is5Done { get; set; }
}

[Serializable]
public class PlayerProfile
{
    public Dictionary<UpgradeID, UpgradesStats> upgrades = new Dictionary<UpgradeID, UpgradesStats>(); // содержит переменные всех апгрейдов и их значений в паре ключ-значение
    public Dictionary<UnitsID, bool> activeUnits = new Dictionary<UnitsID, bool>(); // набор всех карт, проверяет, сколько открыто на данном этапе. Возвращает true или false по ключу. Открывать карты через функцию public int OpenCard(UnitsID id);
    public Dictionary<EnemiesID, bool> activeEnemies = new Dictionary<EnemiesID, bool>(); // список всех открытых врагов на данный момент. Можно сделать проверку при появлении в миссии. Если этот враг еще не открыт - поставить в true и вызвать сразу tip по нему. Также он автоматом откроется в энциклопедии потом.
    public Dictionary<AchievID, bool> achievements = new Dictionary<AchievID, bool>();

    /// <summary>
    /// Содержит информацию о всех уровнях и их прогрессе
    /// </summary>
    public Dictionary<int, LevelData> levelData;

    private int _cardsOpened;
    private int _achievementsReached;
    private int _stars;
    

    public int Stars
    {
        set
        {
            if ((_stars + value) <= GameController.MaxStars)
                _stars = value;
        }
        get { return _stars; }
    }

    public int Coins { set; get; }
    public int CardsOpened { get { return _cardsOpened; } } // общее количество открытых карт
    public int AchievementsReached { get { return _achievementsReached; } } // не понятно что это, уже забыл
    public int ChallengeLevelsCount { get; set; } // Счетчик пройденных challenge levels (for atchiev window)
    public int CompletedQuests { get; set; } // Сколько квестов пройдено с таверны (for atchiev window)
    public int UnitsKilledWithTrap { set; get; } // (for atchiev window)
    public int UnitsRevivedWithRevive { set; get; } // (for atchiev window)
    public int UnitsKillesWithStarfall { set; get; } // (for atchiev window)

    public PlayerProfile()
    {
        levelData = GameObject.Find("GameController").GetComponent<LevelsData>().LevelData;
        PrepareUpgrades();
        PrepareActiveEnemies();
        PrepareActiveUnits();
        PrepareAchievements();
    }

    public int OpenCard(UnitsID id)
    {
        if (activeUnits[id] == false)
        {
            activeUnits[id] = true;
            _cardsOpened += 1;
        }
        return _cardsOpened;
    }

    public void OpenAchievement(AchievID id)
    {
        if (achievements[id] == false)
        {
            achievements[id] = true;
            _achievementsReached += 1;
            MonoBehaviour.print(id + " reached");
        }
    }

    private void PrepareUpgrades() // Инициализирует весь контейнер
    {
        int length = Enum.GetValues(typeof(UpgradeID)).Length;
        for (int i = 0; i < length; i++)
        {
            upgrades[(UpgradeID) i] = new UpgradesStats
            {
                Is1Done = false,
                Is2Done = false,
                Is3Done = false,
                Is4Done = false,
                Is5Done = false
            };
        }
    }

    private void PrepareActiveEnemies() // Инициализирует весь контейнер
    {
        int length = Enum.GetValues(typeof(EnemiesID)).Length;
        for (int i = 0; i < length; i++)
        {
            activeEnemies[(EnemiesID)i] = false;
        }
    }

    private void PrepareActiveUnits() // Инициализирует весь контейнер
    {
        int length = Enum.GetValues(typeof(UnitsID)).Length;
        for (int i = 0; i < length; i++)
        {
            activeUnits[(UnitsID)i] = false;
        }
        OpenCard(UnitsID.knight);
        OpenCard(UnitsID.archer);
        OpenCard(UnitsID.puppeteer);
    }

    private void PrepareAchievements()
    {
        int length = Enum.GetValues(typeof(AchievID)).Length;
        for (int i = 0; i < length; i++)
        {
            achievements[(AchievID)i] = false;
        }
    }
}