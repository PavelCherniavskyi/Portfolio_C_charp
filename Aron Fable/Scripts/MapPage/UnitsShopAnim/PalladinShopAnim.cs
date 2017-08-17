using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PalladinShopAnim : MonoBehaviour, IMakeInactiveUnit
{

    public Animator Animator;
    public Vector2 Interval;

    private static float _intervalHelper;
    private readonly string[] _animations = {"hi", "replaceForward"};
    private int _indexToPlay;

	void Start ()
	{
        ShopManager.CancelAnimation += StopAnimation;
        _intervalHelper = Random.Range(Interval.x, Interval.y);
	    _indexToPlay = 0;
	}
	
	
	void Update ()
	{
	    _intervalHelper -= Time.deltaTime;
	    if (_intervalHelper < 0)
	    {
            Animator.Play(_animations[_indexToPlay]);
	        _intervalHelper = Random.Range(Interval.x, Interval.y);
            _indexToPlay = _indexToPlay == 1 ? 0 : 1;
	    }
            
	}

    private void StopAnimation()
    {
        Animator.Play("Idle");
    }

    public void MakeInactive()
    {
        Animator.speed = 0;
        _intervalHelper = float.MaxValue;
    }
}
