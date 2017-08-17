using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private Sprite soundOff;
    private Sprite soundOn;
    private Sprite musicOff;
    private Sprite musicOn;

    private LevelController lvlControllerScript;

    void Start()
    {
        
        soundOff = GameObject.Find("GameController").GetComponent<SpritesBank>().soundOff;
        soundOn = GameObject.Find("GameController").GetComponent<SpritesBank>().soundOn;
        musicOff = GameObject.Find("GameController").GetComponent<SpritesBank>().musicOff;
        musicOn = GameObject.Find("GameController").GetComponent<SpritesBank>().musicOn;
        lvlControllerScript = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
    }

    public void SettingsClick()
    {
        if (GameObject.Find("UI/hero icon").GetComponent<BlackPauseController>().bBlackPause) return;
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.PauseClick), 1);
        Pause(true);

        GameObject temp = Instantiate(Resources.Load("Prefabs/Interface/SettingsPref") as GameObject);
        temp.GetComponent<Animator>().SetBool("isOpen", true);
        temp.transform.Find("Window/Sound").GetComponent<Image>().sprite = GameController.Sound ? soundOn : soundOff;
        temp.transform.Find("Window/Music").GetComponent<Image>().sprite = GameController.Music ? musicOn : musicOff;
    }

    public void Pause(bool mode)
    {
        StartCoroutine(TimePause(mode));
    }

    public void SoundClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameController.Sound = !GameController.Sound;

        transform.Find("Window/Sound").GetComponent<Image>().sprite = GameController.Sound ? soundOn : soundOff;
    }

    public void MusicClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        
        GameController.Music = !GameController.Music;
        GameObject.Find("AudioController").GetComponent<SoundController>().MusicOnOff();

        gameObject.transform.Find("Window/Music").GetComponent<Image>().sprite = GameController.Music ? musicOn : musicOff;
    }

    public void CloseClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.PauseClick), 1);
        Pause(false);
        GetComponent<Animator>().SetBool("isOpen", false);
        Destroy(gameObject, 1);
    }

    public void RestartLevelClick()
    {
        Pause(false);
        lvlControllerScript.RestartLevelClick();
    }

    public void QuitLevelClick()
    {
        Pause(false);
        lvlControllerScript.QuitLevelClick();
    }

    private IEnumerator TimePause(bool mode)
    {
        if (mode == false)
            Time.timeScale = 1;
        yield return new WaitForSeconds(0.5f);
        if (mode == true)
            Time.timeScale = 0;
    }

}