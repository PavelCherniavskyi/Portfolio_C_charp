using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Обязательно после создания запустить функцию Initialize!
/// </summary>
public class LevelPreview : MonoBehaviour
{
    private Image _levelPreview;
    public Button StartButton;
    private Text _title;
    private GameObject _levelSteps;
    private Button _challengeModeButton;
    private Button _playModeButton;
    public Sprite[] fiveStars;
    private Animator backGroundButtons;
    private SpritesBank _spriteBank;

    private GameObject _backLightChallenge;

    private Animator _windowAnim;

    private Difficulty _currentPlayMode;
    private int _levelIndex;
    private bool _isChallengeModePressed;
    private LevelSteps _currentlvlMode;

    private void Activate(Transform instance)
    {
        _levelPreview = instance.Find("BackGround/Window").GetComponent<Image>();
        
        _title = instance.Find("BackGround/Window/Title").GetComponent<Text>();
        _challengeModeButton = instance.Find("BackGround/Window/ChallengeModeBut").GetComponent<Button>();
        _playModeButton = instance.Find("BackGround/Window/PlayMode").GetComponent<Button>();
        _levelSteps = instance.Find("BackGround/Window/LevelProgress").gameObject;
        _windowAnim = GetComponent<Animator>();
        _windowAnim.SetBool("isOpen", true);

        _spriteBank = GameObject.Find("GameController").GetComponent<SpritesBank>();

        _backLightChallenge = instance.Find("BackGround/Window/ChallengeModeBut/BackLight").gameObject;

        _isChallengeModePressed = false;
        backGroundButtons = GameObject.Find("BackGround").GetComponent<Animator>();
        backGroundButtons.SetBool("moveOut", true);

        _backLightChallenge.SetActive(false);
        _currentPlayMode = Difficulty.easy;
        _playModeButton.GetComponent<Image>().sprite = _spriteBank.easyMode;

    }

    public void Initialize(int lvlIndex, LevelProgress lvlProgress, LevelSteps lvlMode, Transform instance)
    {
        Activate(instance);
        _currentlvlMode = lvlMode;
        _levelIndex = lvlIndex - 1;
        _title.text = GameController.CurrentPlayerProfile.levelData[_levelIndex].SceneName;
        switch (lvlIndex)
        {
            case 1:
                _levelPreview.sprite = _spriteBank.level1Preview;
                break;

            case 2:
                _levelPreview.sprite = _spriteBank.level2Preview;
                StartButton.enabled = false;
                break;

            case 3:
                _levelPreview.sprite = _spriteBank.level3Preview;
                StartButton.enabled = false;
                break;

            case 4:
                _levelPreview.sprite = _spriteBank.level4Preview;
                StartButton.enabled = false;
                break;

            case 5:
                _levelPreview.sprite = _spriteBank.level5Preview;
                StartButton.enabled = false;
                break;

            default:
                break;
        }

        RectTransform[] stars = _levelSteps.GetComponentsInChildren<RectTransform>();
        if (lvlProgress.Stars == 3)
        {
            stars[1].gameObject.GetComponent<Image>().sprite = fiveStars[0];
            stars[2].gameObject.GetComponent<Image>().sprite = fiveStars[1];
            stars[3].gameObject.GetComponent<Image>().sprite = fiveStars[2];
            if (lvlProgress.IsChallengeDone)
            {
                stars[4].gameObject.GetComponent<Image>().sprite = fiveStars[3];
            }
            if (lvlProgress.IsBossKilled)
            {
                stars[5].gameObject.GetComponent<Image>().sprite = fiveStars[4];
            }
        }
        else
        {
            _challengeModeButton.GetComponent<Button>().interactable = false;
            if (lvlProgress.Stars == 1)
            {
                stars[1].gameObject.GetComponent<Image>().sprite = fiveStars[0];
            }
            else if (lvlProgress.Stars == 2)
            {
                stars[1].gameObject.GetComponent<Image>().sprite = fiveStars[0];
                stars[2].gameObject.GetComponent<Image>().sprite = fiveStars[1];
            }
        }

        if (_currentlvlMode == LevelSteps.challenge)
        {
            _backLightChallenge.gameObject.SetActive(true);
            _playModeButton.GetComponent<Image>().sprite = _spriteBank.normalMode;
            _playModeButton.GetComponent<Button>().enabled = false;
            _playModeButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            _challengeModeButton.GetComponent<Button>().interactable = false;
            _currentPlayMode = Difficulty.normal;
            _currentlvlMode = LevelSteps.challenge;
        }
        else if (_currentlvlMode == LevelSteps.boss)
        {
            _challengeModeButton.GetComponent<Button>().interactable = false;
            while (_currentPlayMode != Difficulty.hard)
                PlayModeClicked();
            _playModeButton.GetComponent<Button>().interactable = false;
            _playModeButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    /// <summary>
    /// Обязательно инициализировать поля CurrentLvlStep и CurrentLvlDifficulty в LevelsData, чтобы понимать в каком режиме проходится игра
    /// </summary>
    public void PlayClick()
    {
        if (GameController.Sound)
        {
            GameObject obj = Instantiate(SoundBank.StartGame);
            obj.transform.parent = GameObject.Find("GameController").transform;
            Destroy(obj, 3);
        }
            
        LevelSettings.CurrentLvlData = _currentlvlMode;
        LevelSettings.CurrentLvlDifficulty = _currentPlayMode;

        FadeInOut.EndScene(_levelIndex + 1);
    }

    public void CloseClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        _windowAnim.SetBool("isOpen", false);
        backGroundButtons.SetBool("moveOut", false);
        Destroy(gameObject, 1f);
    }

   public void ChallengeModeClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        _isChallengeModePressed = !_isChallengeModePressed;
        if (_isChallengeModePressed)
        {
            _backLightChallenge.gameObject.SetActive(true);
            _playModeButton.GetComponent<Image>().sprite = _spriteBank.normalMode;
            _playModeButton.GetComponent<Button>().enabled = false;
            _playModeButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
            _currentPlayMode = Difficulty.normal;
            _currentlvlMode = LevelSteps.challenge;
        }
        else
        {
            _backLightChallenge.gameObject.SetActive(false);
            _playModeButton.GetComponent<Button>().enabled = true;
            _currentlvlMode = LevelSteps.star3;
            _playModeButton.GetComponent<Button>().enabled = true;
            _playModeButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }

    public void PlayModeClicked()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        switch (_currentPlayMode)
        {
            case Difficulty.easy:
                _playModeButton.GetComponent<Image>().sprite = _spriteBank.normalMode;
                _currentPlayMode = Difficulty.normal;
                break;

            case Difficulty.normal:
                _playModeButton.GetComponent<Image>().sprite = _spriteBank.hardMode;
                _currentPlayMode = Difficulty.hard;
                break;

            case Difficulty.hard:
                _playModeButton.GetComponent<Image>().sprite = _spriteBank.easyMode;
                _currentPlayMode = Difficulty.easy;
                break;

            default:
                break;
        }
    }
}