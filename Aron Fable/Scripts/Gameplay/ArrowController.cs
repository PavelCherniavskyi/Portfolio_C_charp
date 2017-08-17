using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
    private GameObject A, B;
    private float speed;
    public float acceleration = 0;
    public string destroyeffect;
    public bool crit = false;
    public float destroyping = 0.1f;
    private bool isDead = false;
    public bool AnimationDead = false;
    private void Start()
    {
        speed = A.GetComponent<UnitOptions>().shellspeed;
    }

    private void Update()
    {
        if (A == null || B == null)
        {
            if (isDead == false) StartCoroutine(MyDestroy());
        }

        try
        {
            if (acceleration != 0) speed += acceleration;
            transform.position = Vector3.MoveTowards(transform.position, B.transform.position, speed * Time.deltaTime);
        }
        catch
        {
            Debug.Log("Обект уже уничтожен!");
            Destroy(gameObject);
        }

        if (isDead == false)
        {
            if (Vector3.Distance(transform.position, B.transform.position) <= 0.2f)
            {
                isDead = true;
                if (AnimationDead) GetComponent<Animator>().Play("dead");
                if (destroyeffect != "") Destroy(Instantiate(Resources.Load(destroyeffect), transform.position, Quaternion.identity) as GameObject, 2f);
                B.GetComponent<UnitOptions>().AcceptDamage(A, crit);
                StartCoroutine(MyDestroy());
            }
        }
    }

    public void SetTransforms(GameObject _A, GameObject _B, bool _crit)
    {
        if (_A != null && _B != null)
        {
            crit = _crit;
            A = _A;
            B = _B;
            if (A.transform.position.x < B.transform.position.x) transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (A.transform.position.x > B.transform.position.x) transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else Destroy(gameObject);
    }

    private IEnumerator MyDestroy()
    {
        yield return new WaitForSeconds(destroyping);
        Destroy(gameObject);
    }
}
