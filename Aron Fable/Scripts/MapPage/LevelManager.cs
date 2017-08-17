using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Button[] _levels;
    public GameObject LevelPreviewPref;
    public Sprite[] FiveStars;

    private void Awake()
    {
        _levels = new Button[GameObject.Find("GameController").GetComponent<LevelsData>().TotalLevels];
        //_spritesBank = GameObject.Find("GameController").GetComponent<SpritesBank>();
    }

    private void Start()
    {
        _levels = transform.GetComponentsInChildren<Button>();

        for (int i = 0; i < _levels.Length; i++)
        {
            if (GameController.CurrentPlayerProfile.levelData[i].IsVisible)
            {
                _levels[i].gameObject.SetActive(true);
                var lvlProgress = GameController.CurrentPlayerProfile.levelData[i].LvlProgress;
                var levelSteps = _levels[i].transform.FindChild("LevelProgress").GetComponentsInChildren<Image>();
                
                if (GameController.CurrentPlayerProfile.levelData[i + 1].IsVisible)
                {
                    _levels[i].transform.Find("Particles").GetComponent<ParticleSystem>().Stop();
                    _levels[i].GetComponent<Animator>().Stop();
                }

                if (lvlProgress.Stars != 3)
                {
                    if (lvlProgress.Stars == 2)
                    {
                        LevelProgressHelper(levelSteps, 2);
                    }
                    else if (lvlProgress.Stars == 1)
                    {
                        LevelProgressHelper(levelSteps, 1);
                    }
                    continue;
                }

                if (lvlProgress.Stars == 3 && !lvlProgress.IsBossKilled && !lvlProgress.IsChallengeDone)
                {
                    LevelProgressHelper(levelSteps, 3);
                    continue;
                }

                if (lvlProgress.IsChallengeDone)
                {
                    LevelProgressHelper(levelSteps, 4);
                }

                if (lvlProgress.IsBossKilled)
                {
                    LevelProgressHelper(levelSteps, 5);
                }


            }
            else
                _levels[i].gameObject.SetActive(false);
        }
    }

    private void LevelProgressHelper(Image[] levelSteps, int count)
    {
        for (var i = 0; i < count; i++)
        {
            levelSteps[i].sprite = FiveStars[i];
        }
    }

    public void LevelClick(int lvl)
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameObject instance = Instantiate(LevelPreviewPref);
        LevelProgress lvlData = GameController.CurrentPlayerProfile.levelData[lvl - 1].LvlProgress;
        instance.GetComponent<LevelPreview>().Initialize(lvl, lvlData, LevelSteps.star3, instance.transform);
    }
}