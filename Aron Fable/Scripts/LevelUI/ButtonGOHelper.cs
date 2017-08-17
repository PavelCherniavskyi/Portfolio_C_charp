using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGOHelper : MonoBehaviour
{
    private LevelController _LevelController;
    private SpritesBank spriteBank;
    private void Start()
    {
        spriteBank = GameObject.Find("GameController").GetComponent<SpritesBank>();
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        _LevelController.isLevelStarted = true;

    }
    public void GoClick()
    {
        _LevelController.bButtonGoPress = true;
        _LevelController.StartManaPeriodic();
        _LevelController.LoadLevelInfo();
        GameObject.Find("Hero").GetComponent<HeroInfo>().bStandOnThePosition = true;
        Destroy(gameObject);
    }
}
