using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    public Text text;
    public float alpha;

    void Update () {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1, 0), 0.5f * Time.deltaTime);
        alpha -= 0.01f;
        Color colorT = text.color;
        colorT.a = alpha;
        text.color = colorT;
        if (alpha <= 0)
            Destroy(gameObject);
    }
}
