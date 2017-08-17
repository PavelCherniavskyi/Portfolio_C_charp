using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputRayCastController : MonoBehaviour {

    private bool selected = false;
    private RaycastHit hit;
    private GameObject target;
    public LayerMask[] layerMask;
    private GameObject line;
    public GameObject Hero;
    private UnitOptions UO;
    private MoveController MC;

    public float maxDistanceHit = 10;

    void Start () {
        Hero = GameObject.Find("Hero");
    }
	
	void Update () {
        #region MouseInput
        if (Input.GetMouseButtonDown(0) && selected == false && Time.deltaTime != 0)
        {
            RaycastHit[] temphit = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 1000, layerMask[0].value);
            if (temphit.Length > 0)
            {
                int index = 0;
                float min = temphit[0].transform.position.y; // Минимальная позиция == позиция первого элемента в массиве
                for (int i = 1; i < temphit.Length; i++) // Поиск элемента с наименьшим Y
                {
                    if (temphit[i].transform.position.y < min && temphit[i].transform.GetComponent<UnitOptions>() != null)
                    {
                        index = i;
                        min = temphit[i].transform.position.y;
                    }
                }

                target = temphit[index].transform.gameObject;
                UO = target.GetComponent<UnitOptions>();
                MC = target.GetComponent<MoveController>();

                if (target != Hero && UO.isDead == false)
                {
                    target.transform.GetComponent<MoveController>().IgnoreAutoAttack = true;
                    selected = true;
                    DragCamera.LineActive = true;
                } 
            }
        }

        if (line == null && selected == true && Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (Vector3.Distance(hit.point, target.transform.position) >= 1f)
                {
                    line = Instantiate((GameObject)Resources.Load("Prefabs/Line"));
                    line.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
                }
            }
        }

        if (selected == true && line != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.GetComponent<LineRenderer>().SetPosition(0, new Vector3(target.transform.position.x, target.transform.position.y - 0.2f, 1));
            line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, 1));

            DragCamera.LineActive = true;

            if (UO.Autopilot == true)
            {
                DestroyLine();
            }
        }

        if (Input.GetMouseButtonUp(0) && selected == true)
        {
            DestroyLine();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (Vector3.Distance(new Vector3(hit.point.x, hit.point.y + 0.2f), new Vector3(target.transform.position.x, target.transform.position.y + 0.2f)) > 0.7f)
                {
                    if (Vector3.Distance(hit.point, Hero.transform.position) < maxDistanceHit) // Максимальное расстояние призака передвижения 
                    {
                        target.GetComponent<Attack_System>().DropTarget();
                        MC.Destination(new Vector3(hit.point.x, hit.point.y + 0.2f), true);
                    }
                    else
                    {
                        target.GetComponent<Attack_System>().DropTarget();
                        MC.Destination(GlobalFunctions.offset_point(Hero.transform.Find("UnitCenter").transform.position, new Vector3(hit.point.x, hit.point.y + 0.2f), maxDistanceHit), true);
                    }
                }
                else target.transform.GetComponent<MoveController>().IgnoreAutoAttack = false;
            }
            else target.transform.GetComponent<MoveController>().IgnoreAutoAttack = false;
        }
        #endregion
    }


    public void DestroyLine()
    {
        selected = false;
        DragCamera.LineActive = false;
        if (line != null) Destroy(line);
        target.GetComponent<Attack_System>().bAnimSleep = false;
    }
}
