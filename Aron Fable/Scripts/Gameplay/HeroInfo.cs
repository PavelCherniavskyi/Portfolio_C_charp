using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeroInfo : MonoBehaviour {
    private LevelController _LevelController;
    public GameObject Aura;
    private float AuraAlfa = 0;
    public GameObject _GameController;
    public List<GameObject> units = new List<GameObject>();
    public Vector3[] positions = new Vector3[6];
    public bool bStandOnThePosition = false;
    public int EventsActive = 0;

    public List<GameObject> upgradeIcons = new List<GameObject>();

    public bool auracontroll = false;

    private void Start()
    {
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        Aura = GameObject.Find("Hero/UnitCenter/Aura");
        transform.position = GameObject.Find("PointPuck").transform.GetChild(0).transform.position;
        _GameController = GameObject.Find("GameController");
        SetupStartUnitsPositions();
    }

    void Update () {

        if (auracontroll == false)
            AuraActive();
        else AuraDeactivation();

        #region Участвует ли игрок в каких либо событиях ?
        if (EventsActive != 0)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].gameObject != null)
                {
                    units[i].GetComponent<UnitOptions>().AggressionActivate();
                }
            }
        }
        #endregion

        #region Обновление позиций
        try
        {
            for (int i = 0; i < units.Count; i++)
            {
                positions[i] = Aura.transform.GetChild(i).transform.position;
                if (units[i] == null)
                    units.Remove(units[i]);

                if (_LevelController.Exp >= _LevelController.MaxExp)
                {
                    if (units[i] != null) units[i].GetComponent<UnitOptions>().CreateUpdateIcon();
                }
            }
        }
        catch
        {
            Debug.Log("Ошибка индекса");
        }
        
        #endregion

        #region
        if (bStandOnThePosition == true) // Стоят ли юниты на своих позициях
        {
            bool check = true;
            bool FokusWait = false;
            if (units.Count != 0)
            {
                for (int i = 0; i < units.Count; i++)
                {
                    if (units[i].gameObject != null)
                    {
                        if (units[i].GetComponent<AnimationController>().status == AnimationStatus.focus)
                        {
                            FokusWait = true;
                        }

                        if (Vector3.Distance(units[i].transform.position, positions[i]) > 0.1f)
                        {
                            check = false;
                        }
                    }
                }
            }


            if (check == false)
            {
                StandOnThePosition();
            }
            else if (check == true && FokusWait == false)
            {

                bStandOnThePosition = false;
                _LevelController.pause = false;
            }
        }
        #endregion
    }

    public void SetupStartUnitsPositions()
    {
        for (int i = 0; i < 6; i++)
        {
            positions[i].x = transform.position.x + Mathf.Sin((360 * Mathf.Deg2Rad) / 6 * i) * 1.6f;
            positions[i].y = transform.position.y + Mathf.Cos((360 * Mathf.Deg2Rad) / 6 * i) * 1.6f;

            Instantiate((GameObject)Resources.Load("Prefabs/AnchorPosition"), positions[i], Quaternion.identity).transform.SetParent(GameObject.Find("Hero/UnitCenter/Aura").transform);
        }
    }

    public void StandOnThePosition()
    {
        DragCamera.IsOtherObjectDragging = false;
        if (units.Count != 0)
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].gameObject != null && units[i].GetComponent<AnimationController>().status != AnimationStatus.focus)
                {
                    units[i].GetComponent<AIFollow>().StandOnThePosition();
                }
            }
        }
    }

    public void AuraDeactivation()
    {
        if (AuraAlfa < 255)
        {
            AuraAlfa += 500 * Time.deltaTime;
            if (AuraAlfa > 255)
                AuraAlfa = 255;

            Aura.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, (byte)AuraAlfa);
        }
    }

    public void AuraActive()
    {
        if (AuraAlfa > 0)
        {
            AuraAlfa += -(500 * Time.deltaTime);
            if (AuraAlfa < 0)
                AuraAlfa = 0;

            Aura.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, (byte)AuraAlfa);
        }
    }
}
