using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UpgradeID { warrior, range, magic, stealth, regen, starfall, trap }; // список названий апгрейдов

public enum UnitsID { knight, archer, puppeteer, rouge, berserker, hunter, clerc, assassin, palladin, sentiel, wizard, dancer, hero, no}; // список игровых персонажей

public enum EnemiesID
{ potsik, shpinatic, doggie, littleWizard, venerina, bearBoss, butterfly, banditChief, hornet, stinger, goo, smallGoo, murkee, darkWalker, phantomNear, wildTreant, prongle, cursedBat, clown, loonatic, no }; // список врагов

public enum AchievID
{ shiny, starry, starrific, startacular, steppedUp, distantDreams, lionHeart, treasureHunter, snareCare, revive, cometPlumet, collector }

public enum Rank
{ star0, star1, star2, star3, star4 };

public enum Difficulty
{ easy, normal, hard }

public enum LevelSteps
{ star1, star2, star3, challenge, boss }

public class GameController : MonoBehaviour
{
    #region Variables

    private static GameController _gameControl; // переменная самого класса, нужна для паттерна синглтон
    private static PlayerProfile _currentPlayerProfile = null;
    private static string[] tips = new string[30]; // набор текста из подсказок
    private static bool _sound; // включен ли звук
    private static bool _music; // включена ли музыка

    #endregion Variables

    // все свойства в сеттере записывают переменную сразу в кэш, чтобы сохранялся прогресс. Поэтому важно CurrentLvl, Stars и Coins начислять только в конце миссии, при ее завершении.

    #region Properties

    public static int CurrentSlot { set; get; }
    public static int MaxStars { set; get; }
    public static int MaxCards { set; get; }

    public static PlayerProfile CurrentPlayerProfile
    {
        set { _currentPlayerProfile = value; }
        get { return _currentPlayerProfile; }
    }

    public static bool Sound
    {
        get { return _sound; }
        set
        {
            _sound = value;
            PlayerPrefs.SetInt("Sound", _sound ? 1 : 0);
        }
    }

    public static bool Music
    {
        get { return _music; }
        set
        {
            _music = value;
            PlayerPrefs.SetInt("Music", _music ? 1 : 0);
        }
    }

    #endregion Properties

    #region Methods

    private void Awake() // паттерн синглтон. Проверяет есть ли обьект класса в мире. Если уже есть - уничтожается, если нет создается.
    {
        if (_gameControl == null)
        {
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            _gameControl = this;
            DontDestroyOnLoad(gameObject); // не уничтожается при загрузке другой сцены. Переходит из одной в другую и живет на протяжении всей игры.
            MaxStars = 85;
            MaxCards = Enum.GetValues(typeof(UnitsID)).Length;
            PrepareTips();
            PrepareMusicAndSound();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void PrepareMusicAndSound()
    {
        _sound = !PlayerPrefs.HasKey("Sound") || Convert.ToBoolean(PlayerPrefs.GetInt("Sound"));
        _music = !PlayerPrefs.HasKey("Music") || Convert.ToBoolean(PlayerPrefs.GetInt("Music"));
        GameObject.Find("AudioController").GetComponent<SoundController>().MusicOnOff();
    }

    private static void PrepareTips() // инициализирует список подсказок в игре
    {
        tips[0] = "1 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[1] = "2 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[2] = "3 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[3] = "4 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[4] = "5 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[5] = "6 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[6] = "7 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[7] = "8 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[8] = "9 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[9] = "10 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[10] = "11 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[11] = "12 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[12] = "13 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[13] = "14 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[14] = "15 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[15] = "16 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[16] = "17 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[17] = "18 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[18] = "19 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[19] = "20 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[20] = "21 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[21] = "22 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[22] = "23 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[23] = "24 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[24] = "25 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[25] = "26 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[26] = "27 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[27] = "28 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[28] = "29 tip tip tip itsdflkjdklj dkjasl;dfkj ";
        tips[29] = "30 tip tip tip itsdflkjdklj dkjasl;dfkj ";
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="index"> возвращает подсказку от 0 до 29 </param>
    /// <returns></returns>
    public static string getTip(int index)
    {
        if (index >= 30 || index < 0)
        {
            print("Total tips are 30!");
            return "";
        }
        return tips[index];
    }

    private void OnApplicationQuit()
    {
        if (_currentPlayerProfile != null)
            SaveLoad.Save(_currentPlayerProfile, CurrentSlot);
    }

    #endregion Methods
}