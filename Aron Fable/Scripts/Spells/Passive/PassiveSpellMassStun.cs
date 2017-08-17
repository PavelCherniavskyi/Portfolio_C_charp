using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpellMassStun : MonoBehaviour {

    public int chance = 10; // %
    public float range = 2f;
    public float time = 2f; // sec
    public float damage = 50;
    public float ping = 0.1f;
    public float cooldown = 3f;
    private float cd = 0;
    public string animation = "alter_attack_1";

    void Update()
    {
        if (cd > 0) cd -= Time.deltaTime;
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
}
