using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAllPrefabs : MonoBehaviour {

    public GameObject[] objects = new GameObject[20];

	void Start () {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                GameObject temp = Instantiate(objects[i], transform.position, Quaternion.identity);
                temp.SetActive(false);
                Destroy(temp, 0.1f);
            }
        }
	}
}
