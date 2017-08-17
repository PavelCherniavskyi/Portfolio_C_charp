using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TrainingController : MonoBehaviour {

    public List<GameObject> collection;
    public int index = 0;
    private GameObject ImagePack;
    private bool end = false;
    private static bool _isFirstTime = true;

	void Start () {
        ImagePack = GameObject.Find("UI/InfoPanel/Background/ImagesPack");
        collection.Add(GameObject.Find("UI/SpellsPanelPref"));
        collection.Add(GameObject.Find("UI/CameraMoveToHero"));
        collection.Add(GameObject.Find("UI/hero icon"));
        collection.Add(GameObject.Find("UI/Interface"));
        collection.Add(GameObject.Find("UI/LevelProgress"));
        collection.Add(GameObject.Find("UI/CardPanel"));
        collection.Add(GameObject.Find("UI/CardCollectionPref"));

	    if (_isFirstTime)
	    {
            OffAll();
            StartCoroutine(StartTraining(1f));
	        _isFirstTime = false;
	    }
            
    }

    private IEnumerator StartTraining(float time)
    {
        yield return new WaitForSeconds(time);
        Next();
    }

    public void Next()
    {
        if (end == true) return;

        if (index == ImagePack.transform.childCount)
        {
            GameObject.Find("UI/InfoPanel").GetComponent<Animator>().Play("close");
            GameObject.Find("UI/CardCollectionPref").GetComponent<Animator>().Play("open");
            Destroy(gameObject, 1.5f);
            end = true;
        }
        else
        {
            for (int i = 0; i < ImagePack.transform.childCount; i++)
            {
                ImagePack.transform.GetChild(i).gameObject.SetActive(false);
            }

            ImagePack.transform.GetChild(index).gameObject.SetActive(true);

            if (index == 0)
            {
                GameObject.Find("UI/Interface").GetComponent<Animator>().Play("open");
                GameObject.Find("UI/Interface/UnitInfo").SetActive(true);
                GameObject.Find("UI/Interface/UnitInfo").GetComponent<UnitInfoRefreshInfo>().SetTarget(GameObject.Find("Hero"));

                GameObject.Find("UI/InfoPanel/Background/TextHeader").GetComponent<Text>().text = "Mana";
                GameObject.Find("UI/InfoPanel/Background/TextContent").GetComponent<Text>().text = "It is used to summon creatures. \nPeriodically accumulates, replenishes by killing enemies or by mana potion.";
            }
            else if (index == 1)
            {
                GameObject.Find("UI/CardPanel").GetComponent<Animator>().Play("open");
                GameObject.Find("UI/InfoPanel/Background/TextHeader").GetComponent<Text>().text = "The Summoner's book";
                GameObject.Find("UI/InfoPanel/Background/TextContent").GetComponent<Text>().text = "A book containing cards of creatures that can be summoned for a certain amount of mana.";
            }
            else if (index == 2)
            {
                GameObject.Find("UI/hero icon").GetComponent<Animator>().Play("open");
                GameObject.Find("UI/InfoPanel/Background/TextHeader").GetComponent<Text>().text = "Strategic pause";
                GameObject.Find("UI/InfoPanel/Background/TextContent").GetComponent<Text>().text = "When you press, it stops the game and allows you to redistribute the original positions of the summoned creatures.";
            }
            else if (index == 3)
            {
                GameObject.Find("UI/SpellsPanelPref").GetComponent<Animator>().Play("open");
                GameObject.Find("UI/InfoPanel/Background/TextHeader").GetComponent<Text>().text = "Magical abilities";
                GameObject.Find("UI/InfoPanel/Background/TextContent").GetComponent<Text>().text = "Much easier life, but have a limited supply of use! Replenished with a magic potion";
            }
            else if (index == 4)
            {
                GameObject.Find("UI/LevelProgress").GetComponent<Animator>().Play("open");
                GameObject.Find("UI/InfoPanel/Background/TextHeader").GetComponent<Text>().text = "Game progress";
                GameObject.Find("UI/InfoPanel/Background/TextContent").GetComponent<Text>().text = "A scale showing the completeness of a level, as well as the number of completed tasks";
            }
            else if (index == 5)
            {
                GameObject.Find("UI/CameraMoveToHero").GetComponent<Animator>().Play("open");
                GameObject.Find("UI/InfoPanel/Background/TextHeader").GetComponent<Text>().text = "Gaming camera mode";
                GameObject.Find("UI/InfoPanel/Background/TextContent").GetComponent<Text>().text = "You can switch between the state of the free camera and the automatic following of the character";
            }

            index++;
            GameObject.Find("UI/InfoPanel").GetComponent<Animator>().Play("open");
        }
    }

    private void CloseAll()
    {
        for (int i = 0; i < collection.Count; i++)
            collection[i].GetComponent<Animator>().Play("close");
    }

    private void OpenAll()
    {
        for (int i = 0; i < collection.Count; i++)
            collection[i].GetComponent<Animator>().Play("open");
    }

    private void OffAll()
    {
        for (int i = 0; i < collection.Count; i++)
            collection[i].GetComponent<Animator>().Play("off");
    }
}
