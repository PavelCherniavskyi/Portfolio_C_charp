using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritEffect : MonoBehaviour {

    public string EffectAttack = "";
    public string EffectAttackSetParent = "";
    public int EffectAttackTime = 1;
    public float EffectAttackPing = 0.01f;
    private GameObject EffectAttackObject;
    public Vector3 EffectAttackShiftPosition;

    public string EffectHit = "";
    public string EffectHitSetParent = "";
    public int EffectHitTime = 1;
    public float EffectHitPing = 0.01f;
    private GameObject EffectHitObject;
    public Vector3 EffectHitShiftPosition;

    public void Create()
    {
        if (EffectAttack != "") StartCoroutine(InstantiateAttackEffect());
        if (EffectHit != "") StartCoroutine(InstantiateHitEffect());
    }

    private IEnumerator InstantiateAttackEffect()
    {
        yield return new WaitForSeconds(EffectAttackPing / GetComponent<Animator>().speed);
        EffectAttackObject = Instantiate(Resources.Load("Effects/" + EffectAttack) as GameObject);
        if (EffectAttackObject.GetComponent<Animator>() != null) EffectAttackObject.GetComponent<Animator>().speed = GetComponent<Animator>().speed;
        EffectAttackObject.transform.position = transform.position + EffectAttackShiftPosition;
        EffectAttackObject.transform.rotation = transform.FindChild("Sprite").transform.rotation;
        if (EffectAttackSetParent != "")
        {
            EffectAttackObject.transform.SetParent(transform.FindChild(EffectAttackSetParent).transform);
            EffectAttackObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        Destroy(EffectAttackObject, EffectAttackTime);
    }

    private IEnumerator InstantiateHitEffect()
    {
        yield return new WaitForSeconds(EffectHitPing / GetComponent<Animator>().speed);
        try
        {
            GameObject target = GetComponent<Attack_System>().target;
            EffectHitObject = Instantiate(Resources.Load("Effects/" + EffectHit) as GameObject);
            EffectHitObject.transform.position = target.transform.position + EffectHitShiftPosition;
            EffectHitObject.transform.rotation = transform.FindChild("Sprite").transform.rotation;
            if (EffectHitSetParent != "") EffectHitObject.transform.SetParent(transform.FindChild(EffectHitSetParent).transform);
            Destroy(EffectHitObject, EffectHitTime);
        }
        catch
        {
            Debug.Log("Ошибка! InstantiateHitEffect() target == null");
        }
    }
}
