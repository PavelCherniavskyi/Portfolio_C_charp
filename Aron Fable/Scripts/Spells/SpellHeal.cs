using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHeal : MonoBehaviour
{
    public LayerMask[] layerMask;
    public float timer = 5f;

    void Start()
    {
        Destroy(gameObject, timer);
    }

    void Update()
    {
        StartCoroutine(Heal());
    }

    private IEnumerator Heal()
    {
        yield return new WaitForSeconds(0.2f);
        Collider[] temp = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, 1), 2f, layerMask[0].value);
        if (temp.Length != 0)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].GetComponent<UnitOptions>().health < temp[i].GetComponent<UnitOptions>().MaxHealth)
                    temp[i].GetComponent<UnitOptions>().health += 1;
            }
        }
    }
}
