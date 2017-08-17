using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArrowController2 : MonoBehaviour
{
    private GameObject A, B;
    private Vector3 C, D;
    private Animator anim;
    private float dis = 0;
    private float acceleration = 0;
    private float speed;
    public string shelltype;
    private bool stop = false;
    public bool crit = false;
    private bool bEffectController = false;
    private bool Missing = false;
    private float alpha = 255;

    private void Start()
    {
        if (crit)
        {
            transform.FindChild("Sprite/EffectLightning").gameObject.SetActive(true);
            transform.FindChild("Sprite").GetComponent<SpriteRenderer>().sprite = GameObject.Find("GameController").GetComponent<SpritesBank>().AlterArrow1;
        }
        speed = A.GetComponent<UnitOptions>().shellspeed; // * A.GetComponent<UnitOptions>().attack_speed
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (dis > 100 && !crit)
        {
            alpha -= 200 * Time.deltaTime;
            if (alpha < 0)
                alpha = 0;

            for (int i = 0; i < transform.childCount; i++)
                Drawind(transform.GetChild(i).gameObject); // Рекурсия
        }

        if (stop == false)
        {
            if (A != null && !Missing) C = A.transform.position;
            if (B != null && !Missing) D = B.transform.position;
            try
            {
                acceleration += 0.01f;
                transform.position = GlobalFunctions.offset_point(C, D, (Vector3.Distance(C, D) / 100) * (dis += speed * Time.deltaTime + acceleration));
                anim.SetTime((2f / 100 * dis));

                if (dis > 100)
                {
                    stop = true;
                    anim.Stop();
                    if (B != null && A != null && Missing == false)
                    {
                        GameObject.Find("GameController").GetComponent<GameplaySoundHelper>().SoundHit(B);
                        if (crit) Destroy(Instantiate(Resources.Load("Effects/EnergyExplosion") as GameObject, transform.FindChild("Sprite/pit").transform.position, Quaternion.identity), 2f);
                        B.GetComponent<UnitOptions>().AcceptDamage(A, crit);
                    }

                    GameObject.Find("GameController").GetComponent<GameplaySoundHelper>().SoundHit(gameObject, shelltype);

                    if (B == null && Missing) transform.FindChild("Sprite/pit").gameObject.SetActive(true);
                    if (B != null && !Missing) Destroy(gameObject);
                    else StartCoroutine(Stop());
                }
            }
            catch
            {
                Debug.Log("Ошибка стрелы");
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(2f);
        if (crit) Destroy(Instantiate(Resources.Load("Effects/EnergyExplosion") as GameObject, transform.position, Quaternion.identity), 2f);
        Destroy(gameObject);
    }

    public void SetTransforms(GameObject _A, Vector3 _C, GameObject _B, bool _crit)
    {
        try
        {
            if (_A != null && _B != null)
            {
                crit = _crit;
                A = _A;
                B = _B;
                if (A.transform.position.x < B.transform.position.x) transform.rotation = Quaternion.Euler(0, 0, 0);
                else if (A.transform.position.x > B.transform.position.x) transform.rotation = Quaternion.Euler(0, 180, 0);

                GameObject.Find("GameController").GetComponent<GameplaySoundHelper>().SoundStart(gameObject, shelltype);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    public void SetTransforms(GameObject _A, Vector3 _C, Vector3 _D, bool _crit)
    {
        Missing = true;
        crit = _crit;
        A = _A;
        C = _C;
        D = _D;
        if (A.transform.position.x < D.x) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (A.transform.position.x > D.x) transform.rotation = Quaternion.Euler(0, 180, 0);

        GameObject.Find("GameController").GetComponent<GameplaySoundHelper>().SoundStart(gameObject, shelltype);
    }

    private void Drawind(GameObject obj)
    {
        if (obj.GetComponent<SpriteRenderer>() != null)
        {
            obj.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, (byte)alpha);
            if (obj.transform.childCount != 0)
                for (int y = 0; y < obj.transform.childCount; y++)
                    Drawind(obj.transform.GetChild(y).gameObject); // Рекурсия
        }
    }
}
