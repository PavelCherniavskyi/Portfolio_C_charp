using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveToHero : MonoBehaviour {

    private GameObject Hero;
    private GameObject Camera;
    private GameObject MainCamera;
    static public bool mode = true;

    public GameObject EventPosition;
    public bool EventActive = false;

    private Camera2DFollowTDS CameraSpeed;

    public void SetEventActive(GameObject pos)
    {
        CameraSpeed.smooth = 1;
        EventPosition = pos;
        EventActive = true;
    }

    public void DropEvent()
    {
        EventActive = false;
        StartCoroutine(ReturnCameraSpeed());
    }

    private IEnumerator ReturnCameraSpeed()
    {
        yield return new WaitForSeconds(1.5f);
        if (!mode && EventActive == false) CameraSpeed.smooth = 3;
    }

    void Start () {
        Hero = GameObject.Find("Hero");
        Camera = GameObject.Find("Camera");
        MainCamera = GameObject.Find("Main Camera");
        GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        CameraSpeed = MainCamera.GetComponent<Camera2DFollowTDS>();
        Click();
    }

    void Update()
    {
        if (!mode)
        {
            if (EventActive == false)
            {
                Camera.transform.position = Hero.transform.position;
            }
            else
            {
                Camera.transform.position = GlobalFunctions.offset_point(Hero.transform.position, EventPosition.transform.position, Vector3.Distance(Hero.transform.position, EventPosition.transform.position) / 2);
            }
        }
    }

    public void Click()
    {
        mode = !mode;

        if (!mode)
        {
            CameraSpeed.smooth = 3;
            GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
        }
        else
        {
            CameraSpeed.smooth = 1;
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
