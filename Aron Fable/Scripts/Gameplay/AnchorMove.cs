using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnchorMove : MonoBehaviour {

    private GameObject Hero;
    private GameObject Aura;
    private RaycastHit hit;
    private bool selected = false;
    private float maxRange = 2;
    public LayerMask[] layerMask;

    void Start () {
        Hero = GameObject.Find("Hero");
        Aura = GameObject.Find("Hero/UnitCenter/Aura");
	}
	
	void Update () {
        if (Time.deltaTime == 0)
        {
            if (GetComponent<SpriteRenderer>().enabled == true)
            {
                #region
                if (selected == true && Vector3.Distance(hit.point, Hero.transform.position) > maxRange)
                {
                    transform.position = GlobalFunctions.offset_point(Hero.transform.position, transform.position, maxRange);
                }
                else if (Vector3.Distance(transform.position, Hero.transform.position) < 0.8f)
                {
                    transform.position = GlobalFunctions.offset_point(Hero.transform.position, transform.position, 0.9f);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, layerMask[0].value))
                    {
                        if (hit.transform.gameObject == gameObject)
                        {
                            selected = true;
                            DragCamera.IsOtherObjectDragging = true;
                        }
                    }
                }

                if (selected == true && Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, layerMask[0].value))
                    {
                        if (Vector3.Distance(hit.point, Hero.transform.position) < maxRange)
                        {
                            transform.position = new Vector3(hit.point.x, hit.point.y, 0);
                            Check(gameObject);
                        }
                        else
                        {
                            transform.position = GlobalFunctions.offset_point(Hero.transform.position, new Vector3(hit.point.x, hit.point.y), maxRange);
                            Check(gameObject);
                        }
                    }
                }

                if (Input.GetMouseButtonUp(0) && selected == true)
                {
                    selected = false;
                    DragCamera.IsOtherObjectDragging = false;
                }
                #endregion
            }
        }
    }


    public void Check(GameObject obj)
    {
        for (int i = 0; i < 6; i++)
        {
            if (Aura.transform.GetChild(i) == obj.transform || obj.GetComponent<SpriteRenderer>().enabled == false) continue;

            if (Vector3.Distance(Aura.transform.GetChild(i).transform.position, obj.transform.position) < 0.8f && Aura.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                Aura.transform.GetChild(i).transform.position = GlobalFunctions.offset_point(obj.transform.position, Aura.transform.GetChild(i).transform.position, 0.9f);
                if (Vector3.Distance(Aura.transform.GetChild(i).transform.position, Hero.transform.position) > maxRange)
                {
                    Aura.transform.GetChild(i).transform.position = GlobalFunctions.offset_point(Hero.transform.position, Aura.transform.GetChild(i).transform.position, maxRange);
                }
                Check(Aura.transform.GetChild(i).gameObject);
            }
        }
    }

    public void CheckAlter(GameObject obj)
    {
        for (int i = 0; i < 6; i++)
        {
            if (Aura.transform.GetChild(i) == obj.transform) continue;

            if (Vector3.Distance(Aura.transform.GetChild(i).transform.position, obj.transform.position) < 0.8f)
            {
                Aura.transform.GetChild(i).transform.position = GlobalFunctions.offset_point(obj.transform.position, Aura.transform.GetChild(i).transform.position, 0.9f);
                if (Vector3.Distance(Aura.transform.GetChild(i).transform.position, Hero.transform.position) > maxRange)
                {
                    Aura.transform.GetChild(i).transform.position = GlobalFunctions.offset_point(Hero.transform.position, Aura.transform.GetChild(i).transform.position, maxRange);
                }
                Check(Aura.transform.GetChild(i).gameObject);
            }
        }
    }
}
