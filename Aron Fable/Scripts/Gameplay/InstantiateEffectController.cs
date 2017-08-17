using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InstantiateEffectController : MonoBehaviour {

    public string effectCreate = "";
    public string effectDead = "";
    public Vector3 DeadPosition;
    public float effectDeadPing = 0.1f;
    public float time = 3f;
    public float effectCount = 1;

    void Start () {
        EffectCreate();
    }

    public void EffectCreate()
    {
        try
        {
            if (effectCreate != "")
            Destroy(Instantiate((GameObject)Resources.Load(effectCreate), transform.position, Quaternion.identity), time);
        }
        catch
        {
            Debug.Log("Error!!! The Object you want to instantiate is null.");
        }
    }

    public void EffectDead()
    {
        try
        {
            if (effectDead != "") StartCoroutine(Dead());
        }
        catch
        {
            Debug.Log("Error!!! The Object you want to instantiate is null.");
        }
    }

    public void EffectDeadInstantly()
    {
        try
        {
            if (effectDead != "")
            {
                for (int i = 0; i < effectCount; i++)
                {
                    Destroy(Instantiate((GameObject)Resources.Load(effectDead), transform.position + DeadPosition, Quaternion.identity), time);
                }
            }
        }
        catch
        {
            Debug.Log("Error!!! The Object you want to instantiate is null.");
        }
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(effectDeadPing);
        for (int i = 0; i < effectCount; i++)
        {
            Destroy(Instantiate((GameObject)Resources.Load(effectDead), transform.position + DeadPosition, Quaternion.identity), time);
        }
    }
}
