using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpellKick : MonoBehaviour {

    public int chance = 10; // %
    public float time = 2f; // sec
    public float damage = 0;
    public float ping = 0.1f;
    public float cooldown = 3f;
    private float cd = 0;
    public string animation = "alter_attack_2";

    public float dis = 6;
    public float speed = 40;
    public float deceleration = 2f;
    private float _speed;

    private bool play = false;
    private GameObject target;
    private Vector3 StartPosition, EndPosition;

    void Update()
    {
        if (cd > 0) cd -= Time.deltaTime;

        if (play)
        {
            if (Vector3.Distance(target.transform.position, EndPosition) >= 0.1f)
            {
                _speed -= deceleration;
                if (_speed <= 0)
                {
                    play = false;
                    return;
                }
                target.transform.position = Vector3.MoveTowards(target.transform.position, EndPosition, _speed * Time.deltaTime);
            }
            else
            {
                target = null;
                play = false;
            }
        }
    }

    private IEnumerator Effect()
    {
        yield return new WaitForSeconds(0.03f);
        if (play)
        {
            Destroy(Instantiate(Resources.Load("Effects/EffectBossDustMini") as GameObject, target.transform.FindChild("origin").transform.position, Quaternion.identity), 2f);
            StartCoroutine(Effect());
        }
    }

    public bool CheckChance()
    {
        int rand = Random.Range(0, 100);
        if (rand > chance || cd > 0) return false;
        else
        {
            cd = cooldown;
            return true;
        }
    }

    public void PlayKick(GameObject obj)
    {
        _speed = speed;
        play = true;
        target = obj;
        StartPosition = obj.transform.FindChild("origin").transform.position;
        EndPosition = GlobalFunctions.offset_point(transform.transform.FindChild("origin").transform.position, StartPosition, dis);
        StartCoroutine(Effect());
    }
}
