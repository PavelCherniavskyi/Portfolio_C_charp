using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;

public class StartCardUnit : MonoBehaviour {

    [HideInInspector]
    public UnitsID UnitType;

    private Image avatar;
    private Text describtion;
    private Dictionary<UnitsID, bool> _activePlayersInLevel;
    private Image _selection;
    private int availableUnits = 5;

	void Start ()
	{
        _selection = GetComponent<Image>();
       
    }

    public void ClickEvent()
    {
        if (CanExecute())
        {
            if (GameController.Sound)
                Destroy(Instantiate(SoundBank.ClickSound), 1);
            _selection.color = new Color(1f, 1f, 1f, 0.5f);
            GameObject.Find("CardCollectionPref").GetComponent<CardCollectionPref>().SelectUnit((int)UnitType);
        }
    }

    private bool CanExecute()
    {
        int count = 0;
        _activePlayersInLevel = GameObject.Find("CardCollectionPref").GetComponent<CardCollectionPref>().ActivePlayersInLevel;
        if (_activePlayersInLevel[UnitType])
            return false;
        for (int i = 0; i < _activePlayersInLevel.Count; i++)
        {
            if (_activePlayersInLevel[(UnitsID)i])
                count++;
            if (count >= availableUnits)
                return false;

        }
        return true;
    }

}
