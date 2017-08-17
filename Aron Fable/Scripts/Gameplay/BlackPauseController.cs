using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BlackPauseController : MonoBehaviour {
    private LevelController _LevelController;
    private GameObject Hero;
    private GameObject Background;
    private GameObject Aura;
    public bool bBlackPause = false;
    private HeroInfo HI;
    private GameObject[] line = new GameObject[6];
 

    void Start () {
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
        Hero = GameObject.Find("Hero");
        HI = GameObject.Find("Hero").GetComponent<HeroInfo>();
        Aura = GameObject.Find("Hero/UnitCenter/Aura");
        Background = GameObject.Find("UI/Interface/BlackPause");
        Background.SetActive(false);
    }
	

	void Update () {
        if (_LevelController.isLevelStarted)
        {
            Vector3 dir = _LevelController.rotationToPoint - Aura.transform.position;
            Aura.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, Aura.transform.forward);
        }

        if (bBlackPause == true)
        {
            for (int i = 0; i < HI.units.Count; i++)
            {
                try
                {
                    Vector3 tempPosition1 = HI.units[i].transform.position;
                    Vector3 tempPosition2 = Aura.transform.GetChild(i).transform.position;
                    Vector3 tempPosition3 = GlobalFunctions.offset_point(tempPosition1, tempPosition2, Vector3.Distance(tempPosition1, tempPosition2) - 0.35f);

                    line[i].GetComponent<LineRenderer>().SetPosition(0, new Vector3(tempPosition1.x, tempPosition1.y, 0));
                    line[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(tempPosition3.x, tempPosition3.y, 0));
                }
                catch
                {
                    Aura.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                    Aura.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
                    line[i] = Instantiate((GameObject)Resources.Load("Prefabs/LineBlackPause"));
                    line[i].GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
                    break;
                }
                
            }
        }
	}

    public void Click()
    {
        if (bBlackPause == true)
        {
            // Выключить
            Time.timeScale = 1;

            bBlackPause = false;
            Background.SetActive(false);

            if (Hero.GetComponent<HeroInfo>().EventsActive == 0 && _LevelController.bButtonGoPress == true)
            {
                Hero.GetComponent<MoveController>().Stop();
                Hero.GetComponent<HeroInfo>().bStandOnThePosition = true;
            }

            for (int i = 0; i < HI.units.Count; i++)
            {
                Aura.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
                Aura.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
                if (line[i] != null) Destroy(line[i]);
            }
        }
        else if(bBlackPause == false)
        {
            // Включить
            bBlackPause = true;
            Background.SetActive(true);

            if (_LevelController.bButtonGoPress)
            {
                _LevelController.pause = true;
                Hero.GetComponent<MoveController>().Stop();
            }
            
            for (int i = 0; i < HI.units.Count; i++)
            {
                if (HI.units[i] != null)
                {
                    Aura.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                    Aura.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
                    line[i] = Instantiate((GameObject)Resources.Load("Prefabs/LineBlackPause"));
                    line[i].GetComponent<LineRenderer>().material = new Material(Shader.Find("Particles/Additive"));
                    line[i].GetComponent<LineRenderer>().sortingLayerName = "UI";
                }
            }

            Time.timeScale = 0;
        }
    }
}
