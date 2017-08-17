using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public float TimePause;
    
    public Button Skip;

    public Animator []Layers;
    private int _currentIndexToPlay = 0;

    void Start () {
        Layers[_currentIndexToPlay].gameObject.SetActive(true);
        InvokeRepeating("ChangeAnimation", TimePause, TimePause);
	}
	
	
	void Update () {
		
	}

    private void ChangeAnimation()
    {
        Layers[_currentIndexToPlay++].gameObject.SetActive(false);
        if (_currentIndexToPlay == Layers.Length)
        {
            SkipClick();
            StopCoroutine("ChangeAnimation");
            return;
        }

        Layers[_currentIndexToPlay].gameObject.SetActive(true);
    }

    public void SkipClick()
    {
        FadeInOut.EndScene("Level1");
    }
}
