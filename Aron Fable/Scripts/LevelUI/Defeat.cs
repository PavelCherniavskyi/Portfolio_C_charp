using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour {

    private LevelController _lvlControllerScript;
    private GameObject _ui;

    void Start () {
        _lvlControllerScript = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        _ui = GameObject.Find("UI");
        _ui.SetActive(false);
    }

    public void RestartLevelClick()
    {
        _lvlControllerScript.RestartLevelClick();
    }

    public void QuitLevelClick()
    {
        _lvlControllerScript.QuitLevelClick();
    }
}
