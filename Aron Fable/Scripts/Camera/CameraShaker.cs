using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private static Animator _animator;
	void Awake ()
	{
	   _animator =  GetComponent<Animator>();
	}

    public static void MakeShake1()
    {
        _animator.Play("Shake1");
    }

    public static void MakeShake2()
    {
        _animator.Play("Shake2");
    }
}
