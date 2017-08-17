using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelSettings : MonoBehaviour {

    public static LevelSteps CurrentLvlData { get; set; } // используются только три последние позиции. Инфо на каком моде текущий уровень.
    public static Difficulty CurrentLvlDifficulty { get; set; } /// инфо на какой сложности проходится уровень.

    private void Start()
    {
        //TEMP
        CurrentLvlData = LevelSteps.star3;
        //---------------------
    }

    private static void BossKilled()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex - 1;
        if (GameController.CurrentPlayerProfile.levelData[currentScene].LvlProgress.Stars != 3)
        {
            print("You can't kill boss before 3 stars is earned. Current stars on this level is = " + GameController.CurrentPlayerProfile.levelData[currentScene].LvlProgress.Stars);
            return;
        }

        GameController.CurrentPlayerProfile.levelData[currentScene].LvlProgress.IsBossKilled = true;
        OpenNextScene();
        FadeInOut.EndScene("MapPage");
    }

    private static void ChallengeDone()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex - 1;
        if (GameController.CurrentPlayerProfile.levelData[currentScene].LvlProgress.Stars != 3)
        {
            print("You can't pass challenge before 3 stars is earned. Current stars on this level is = " + GameController.CurrentPlayerProfile.levelData[currentScene].LvlProgress.Stars);
            return;
        }
        GameController.CurrentPlayerProfile.levelData[currentScene].LvlProgress.IsChallengeDone = true;
        OpenNextScene();
        FadeInOut.EndScene("MapPage");
    }

    public void AddStar(int howMuch)
    {
        GameController.CurrentPlayerProfile.Stars += howMuch;
    }

    public void CardOpen(UnitsID id)
    {
        GameController.CurrentPlayerProfile.OpenCard(id);
    }

    public void AtchieveReach(AchievID id)
    {
        GameController.CurrentPlayerProfile.OpenAchievement(id);
    }

    /// <summary>
    /// Вызывать по завершению уровня. Временно содержит 1 параметр (из-за использования кнопки). Буду добавлено еще 2: убит ли босс, пройдено ли испытание.
    /// </summary>
    /// <param name="stars">Количество заработанных звезд, передается как индекс (0 == 1)</param>
    private static void LevelCompleted(int stars)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex - 1;
        LevelData lvlData = GameController.CurrentPlayerProfile.levelData[currentScene];

        int starsEarned = stars + 1; // stars передается как индекс, а счетчик - число, поэтому + 1.
        if (stars == 0)// если пройден уровень на 1 звезду
        {
            if (lvlData.isStarRewardTaken[LevelSteps.star1]) // если этот уровень уже пройден на 1 звезду
                starsEarned -= 1;
            else
            {
                lvlData.LvlProgress.Stars = stars + 1;
                lvlData.isStarRewardTaken[LevelSteps.star1] = true;
            }
        }
        else if (stars == 1) // если пройден уровень на 2 звезды
        {
            if (lvlData.isStarRewardTaken[LevelSteps.star1]) // если этот уровень уже пройден на 1 звезду
                starsEarned -= 1;
            else
                lvlData.isStarRewardTaken[LevelSteps.star1] = true;

            if (lvlData.isStarRewardTaken[LevelSteps.star2]) // если этот уровень уже пройден на 2 звезды
                starsEarned -= 1;
            else
            {
                lvlData.LvlProgress.Stars = stars + 1;
                lvlData.isStarRewardTaken[LevelSteps.star2] = true;
            }
        }
        else if (stars == 2) // если пройден уровень на 3 звезды
        {
            if (lvlData.isStarRewardTaken[LevelSteps.star1]) // если этот уровень уже пройден на 1 звезду
                starsEarned -= 1;
            else
                lvlData.isStarRewardTaken[LevelSteps.star1] = true;

            if (lvlData.isStarRewardTaken[LevelSteps.star2]) // если этот уровень уже пройден на 2 звезды
                starsEarned -= 1;
            else
                lvlData.isStarRewardTaken[LevelSteps.star2] = true;

            if (lvlData.isStarRewardTaken[LevelSteps.star3]) // если этот уровень уже пройден на 3 звезды
                starsEarned -= 1;
            else
            {
                lvlData.LvlProgress.Stars = stars + 1;
                lvlData.isStarRewardTaken[LevelSteps.star3] = true;
            }
        }

        GameController.CurrentPlayerProfile.Stars += starsEarned;

        OpenNextScene();
        FadeInOut.EndScene("MapPage");
    }

    private static void OpenNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        GameController.CurrentPlayerProfile.levelData[currentScene - 1].IsVisible = true;
        var totalLevels = GameObject.Find("GameController").GetComponent<LevelsData>().TotalLevels;
        if (currentScene < totalLevels)
            GameController.CurrentPlayerProfile.levelData[currentScene].IsVisible = true;
    }

    /// <summary>
    /// Логика функции правильного окончания миссии (без принудительных читов)
    /// </summary>
    public static void CorrectLevelFinish(int star)
    {
        if (CurrentLvlData == LevelSteps.star3)
        {
            LevelCompleted(star);
        }
        else if (CurrentLvlData == LevelSteps.challenge)
        {
            ChallengeDone();
        }
        else if (CurrentLvlData == LevelSteps.boss)
        {
            BossKilled();
        }
    }
}
