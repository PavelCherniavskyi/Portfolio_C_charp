using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCastController : MonoBehaviour
{
    private LevelController _LevelController;
    public LayerMask[] layerMask;
    private bool selected = false;
    private GameObject line;
    private bool active = false;
    private int spellIndex = 0;

    void Start()
    {
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
    }

    void Update()
    {
        #region Отнимаем ману

        if (_LevelController.spellPoint <= 0)
        {
            active = false;
            GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f, 1f);
        }
        else if (_LevelController.spellPoint > 0)
        {
            active = true;
            GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

        #endregion

        if (line != null)
        {
            DragCamera.LineActive = true;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.GetComponent<LineRenderer>().SetPosition(0, Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, 1)));
            line.GetComponent<LineRenderer>().SetPosition(1, new Vector3(mousePosition.x, mousePosition.y, 1));
        }
        
        if (selected == true)
            if (Input.GetMouseButtonUp(0))
            {
                selected = false;
                Destroy(line);
                RaycastHit hit;
                DragCamera.LineActive = false;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    _LevelController.spellPoint--;
                    GameObject.Find("UI/hero icon/spellpoint/spellpointtext").GetComponent<Text>().text = _LevelController.spellPoint.ToString();

                    if (spellIndex == 0)
                    {
                        GameObject heal = Instantiate((GameObject)Resources.Load("Prefabs/System/AuraHeal")); heal.transform.position = new Vector3(hit.point.x, hit.point.y, 5);
                        Destroy(Instantiate((GameObject)Resources.Load("Effects/EffectAuraHeal1"), new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity), 5f);
                        Destroy(Instantiate((GameObject)Resources.Load("Effects/EffectAuraHeal2"), new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity), 1f);
                        Destroy(Instantiate((GameObject)Resources.Load("Effects/MagicEffectsPack01/prefab/energyBlast"), new Vector3(hit.point.x, hit.point.y, 0), Quaternion.identity), 3f);
                        if (GameController.Sound) Destroy(Instantiate(Resources.Load("Sound/Gameplay/Spells/Prefabs/HealSound")), 7f);
                    }
                    else if (spellIndex == 1)
                    {
                        GameObject FireBall = Instantiate((GameObject)Resources.Load("Effects/2D_FireBall"));
                        FireBall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, 1));
                        FireBall.GetComponent<SpellFireBall>().target = new Vector3(hit.point.x, hit.point.y, 1);
                    }
                    else if (spellIndex == 2)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            GameObject Lightning1 = Instantiate(Resources.Load("Effects/CloudBlack") as GameObject);
                            Lightning1.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
                            Destroy(Lightning1, 15f);
                            Lightning1 = Instantiate(Resources.Load("Effects/CloudWhite") as GameObject);
                            Lightning1.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
                        }
                        if (GameController.Sound) Destroy(Instantiate(Resources.Load("Sound/Gameplay/Spells/Prefabs/LightningSpell3")), 11f);
                        StartCoroutine(Step1(new Vector3(hit.point.x, hit.point.y, 0), 0, 1.5f));
                        StartCoroutine(Step2(new Vector3(hit.point.x, hit.point.y, 0), 0, 2f));
                    }
                }
            }
    }

    public void Click(int index)
    {
        if (_LevelController.spellPoint == 0) return;
        spellIndex = index;
        selected = true;
        if (index == 0)
        {
            line = Instantiate((GameObject)Resources.Load("Prefabs/LinePurple"));
            line.GetComponent<LineRenderer>().sortingLayerName = "UI";
        }
        else if (index == 1)
        {
            line = Instantiate((GameObject)Resources.Load("Prefabs/LineOrange"));
            line.GetComponent<LineRenderer>().sortingLayerName = "UI";
        }
        else if (index == 2)
        {
            line = Instantiate((GameObject)Resources.Load("Prefabs/LineBlue"));
            line.GetComponent<LineRenderer>().sortingLayerName = "UI";
        }


        line.GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
        line.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, 1));
    }

    private IEnumerator Step1(Vector3 pos, int count, float sleeping)
    {
        yield return new WaitForSeconds(1 + sleeping);
        GameObject Lightning3 = Instantiate(Resources.Load("Effects/CloudLightning4") as GameObject);
        Lightning3.transform.position = new Vector3(pos.x + Random.Range(-5, 5), pos.y + Random.Range(-5, 5), pos.z);
        Destroy(Lightning3, 3f);
        count++;
        if (count < 5) StartCoroutine(Step1(pos, count, 0));
    }

    private IEnumerator Step2(Vector3 pos, int count, float sleeping)
    {
        yield return new WaitForSeconds(0.3f + sleeping);

       
        if (GameController.Sound)
        {
            if (count == 0) Destroy(Instantiate(Resources.Load("Sound/Gameplay/Spells/Prefabs/LightningSpell1")), 10f);
            Destroy(Instantiate(Resources.Load("Sound/Gameplay/Spells/Prefabs/LightningSpell2")), 1.3f);
        }
           

        CameraShaker.MakeShake2();
        GameObject Lightning3 = Instantiate(Resources.Load("Effects/CloudLightning2") as GameObject);
        GameObject Lightning4 = Instantiate(Resources.Load("Effects/CloudLightning3") as GameObject);
        GameObject Lightning5 = Instantiate(Resources.Load("Effects/CloudLightning5") as GameObject);
        Vector3 point = GlobalFunctions.offset_point(pos, Random.Range(0, 360), Random.Range(1.5f, 7f));
        Lightning3.transform.position = point;
        Lightning4.transform.position = point;
        Lightning5.transform.position = point;
        Destroy(Lightning3, 3f);
        Destroy(Lightning4, 3f);
        Destroy(Lightning5, 3f);
        count++;
        Collider[] temp = Physics.OverlapSphere(pos, 3.5f, layerMask[0].value);
        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].GetComponent<UnitOptions>() != null)
                {
                    temp[i].GetComponent<UnitOptions>().AcceptDamage(10, damage_type.magical);
                    temp[i].GetComponent<UnitOptions>().SetStunned(3f);
                }
            }
        }
        if (count < 14) StartCoroutine(Step2(pos, count, 0));
    }
}
