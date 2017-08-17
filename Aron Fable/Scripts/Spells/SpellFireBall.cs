using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFireBall : MonoBehaviour {

    public Vector3 target;
    private float speed = 0.1f;
    private float acceleration = 0.6f;
    private bool isDead = false;
    public LayerMask[] layerMask;

    void Start()
    {
        if (GameController.Sound) Destroy(Instantiate(Resources.Load("Sound/Gameplay/Spells/Prefabs/FireBallSound2")), 3f);
    }

    void Update () {
		if (target != null && isDead == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, (speed += acceleration) * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) <= 0.1f)
            {
                Collider[] temp = Physics.OverlapSphere(transform.position, 2f, layerMask[0].value);
                if (temp.Length > 0)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].GetComponent<UnitOptions>() != null)
                            temp[i].GetComponent<UnitOptions>().AcceptDamage(250, damage_type.magical);
                    }
                }

                if (GameController.Sound) Destroy(Instantiate(Resources.Load("Sound/Gameplay/Spells/Prefabs/FireBallSound1")), 3f);

                isDead = true;
                CameraShaker.MakeShake2();
                GetComponent<Animator>().Play("dead");
                Destroy(Instantiate((GameObject)Resources.Load("Effects/2D_FireBallExplosion"), transform.position, Quaternion.identity), 2f);
                Destroy(gameObject, 3f);
            }
        }
	}
}
