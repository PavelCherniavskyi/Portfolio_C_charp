using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSettings : MonoBehaviour {

	void Start () {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject temp = transform.GetChild(0).gameObject.transform.GetChild(i).gameObject;
            temp.GetComponent<SpriteRenderer>().sortingOrder = ~(int)(temp.transform.GetChild(0).transform.position.y * 100);
        }
	}
}
