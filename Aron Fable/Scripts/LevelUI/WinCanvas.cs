using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class WinCanvas : MonoBehaviour {

    private Vector2 resolution;
    private GameObject firstStar;
    private GameObject secondStar;
    private GameObject thirdStar;
    private Slider slider;
    private Animator shaker;

    private float startDelay = 1;
    private float speedHelper = 0.01f;
    private float step = 0.4f;
    private float speed;

    private bool firstStarShowed;
    private bool secondStarShowed;
    private bool thirdStarShowed;

    private GameObject _ui;

    public AudioSource ImpactSound;

    void Start () {
        GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        firstStar = GameObject.Find("1StarSpot").transform.GetChild(0).gameObject;
        secondStar = GameObject.Find("2StarSpot").transform.GetChild(0).gameObject;
        thirdStar = GameObject.Find("3StarSpot").transform.GetChild(0).gameObject;
        slider = GameObject.Find("Window/Slider").GetComponent<Slider>();
        shaker = transform.Find("Background/Window").GetComponent<Animator>();
        GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        speed = startDelay;
        shaker.Play("WinCanvasMainWindow");

        _ui = GameObject.Find("UI");
        _ui.SetActive(false);
    }
	
	void Update () {
        speed -= Time.deltaTime;
        if (speed < 0 && slider.value < 100)
        {
            slider.value += step;
            speed = speedHelper;
            StarLaunch();

        }
    }

    void StarLaunch()
    {
        if (slider.value >= 30 && !firstStarShowed)
        {
            StartCoroutine("MakeShake");
            firstStar.SetActive(true);
            firstStarShowed = true;
        }
        if (slider.value >= 60 && !secondStarShowed)
        {
            StartCoroutine("MakeShake");
            secondStar.SetActive(true);
            secondStarShowed = true;
        }
        if (slider.value >= 100 && !thirdStarShowed)
        {
            StartCoroutine("MakeShake");
            thirdStarShowed = true;
            thirdStar.SetActive(true);
        }
            

    }

    private IEnumerator MakeShake()
    {
        yield return new WaitForSeconds(1);
        shaker.Play("WinCanvasStarHit");
        if (GameController.Sound)
            ImpactSound.Play();
    } 

    public void NextClick()
    {
        int starsEarned;
        if (firstStarShowed && secondStarShowed && thirdStarShowed)
            starsEarned = 3;
        else if (firstStarShowed && secondStarShowed)
            starsEarned = 2;
        else if (firstStarShowed)
            starsEarned = 1;
        else
            starsEarned = 0;
        LevelSettings.CorrectLevelFinish(starsEarned - 1);
    }
}
