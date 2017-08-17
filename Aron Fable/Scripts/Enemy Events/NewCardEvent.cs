using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardEvent : MonoBehaviour {

    public UnitsID UnitType; // на старте инициализировать извне
    public Rank UnitRank;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    GameObject.Find("GameController").GetComponent<LevelSettings>().CardOpen(UnitType);
                    GameObject card = Instantiate(Resources.Load("Prefabs/GamePlay/NewCardTaking") as GameObject);
                    card.GetComponent<NewCardTaking>().UnitType = UnitType;
                    card.GetComponent<NewCardTaking>().UnitRank = UnitRank;
                    Destroy(gameObject);
                }
            }
        }
    }
}
