using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraAggression : MonoBehaviour {

    public LayerMask[] layerMask;

    private void Update()
    {
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        yield return new WaitForSeconds(1f);
        Collider[] temp = Physics.OverlapSphere(transform.position, 2f, layerMask[0].value);

        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].GetComponent<UnitOptions>().IsEnemy == true)
                {
                    temp[i].GetComponent<UnitOptions>().AcceptAggression(gameObject, 1f);
                }
            }
        }
    }
}
