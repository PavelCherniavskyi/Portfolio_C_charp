using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BirdController : MonoBehaviour {
    private GameObject PointPack;
    public List<Vector3> points;
    public Vector3 desiredPosition;
    public float speed = 0;
    public float maxSpeed = 3;
    public float acceleration = 0.05f;
    public bool active = true;
    public bool stop = false;
    public float sleeping;
    private Animator ComponentAnimator;
    private GameplaySoundHelper _gameplaySoundHelper;

    void Start ()
    {
        _gameplaySoundHelper = GameObject.Find("GameController").GetComponent<GameplaySoundHelper>();
        ComponentAnimator = GetComponent<Animator>();
        points = new List<Vector3>();
        PointPack = GameObject.Find("BirdPointPack");
        for (int i = 0; i < PointPack.transform.childCount; i++)
            points.Add(PointPack.transform.GetChild(i).transform.position);
        NearestPosition();
    }

    void Update () {
        if (!stop)
        {
            if (!active)
                Search();
            else Destination();
        }
    }

    public void Click()
    {
        if (stop == true)
        {
            Search();
        }
    }

    void Search()
    {
        List<Vector3> temp = new List<Vector3>();
        foreach (Vector3 t in points)
            temp.Add(t);
        active = true;
        stop = false;
        temp.Remove(desiredPosition);
        desiredPosition = temp[Random.Range(0, temp.Count)];
        _gameplaySoundHelper.SoundBird(1);
    }

    void Destination()
    {
        ComponentAnimator.Play("fly");
        if (speed < maxSpeed) speed += acceleration;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, speed * Time.deltaTime);
        if (desiredPosition.x > transform.position.x) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (desiredPosition.x < transform.position.x) transform.rotation = Quaternion.Euler(0, 180, 0);
        if (Vector3.Distance(transform.position, desiredPosition) < 0.1f) Stop();
    }

    void Stop()
    {
        _gameplaySoundHelper.SoundBird(2);
        ComponentAnimator.Play("stand");
        speed = 0f;
        stop = true;
        sleeping = Random.Range(8, 16);
        StartCoroutine(Reload());
    }

    Vector3 NearestPosition()
    {
        int index = 0;
        float min = 1000;
        for (int i = 0; i < points.Count; i++)
        {
            if (Vector3.Distance(transform.position, points[i]) < min)
            {
                index = i;
                min = Vector3.Distance(transform.position, points[i]);
            }
        }
        return points[index];
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(sleeping);
        if (stop == true) Search();
    }
}
