using System;
using UnityEngine.UI;
using UnityEngine;

public class BuyingUnitsDialog : MonoBehaviour
{
    private int _cost;
    private string _name;
    public Animator Animator;
    private UnitsID _unitType;
    private Text _textToChange;
    private Action _reloadUnits;
    private IMakeInactiveUnit _makeInactive;

    void Start ()
    {
        transform.FindChild("Window/For").GetComponent<Text>().text = "for " + _cost.ToString();
        transform.FindChild("Window/Name").GetComponent<Text>().text = _name;
        Animator.SetBool("isOpen", true);
    }

    public void Initialize(int cost, string name, UnitsID unit, Text textToChange, Action prepareUnits, IMakeInactiveUnit makeInactive)
    {
        _cost = cost;
        _name = name;
        _unitType = unit;
        _textToChange = textToChange;
        _reloadUnits = prepareUnits;
        _makeInactive = makeInactive;
    }

    public void ExitClick()
    {
        Animator.SetBool("isOpen", false);
        Destroy(gameObject, 2);
        if(GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
    }

    public void BuyClick()
    {
        if (GameController.Sound)
        {
            Destroy(Instantiate(SoundBank.BuyUnitDone), 2);
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        }

        _makeInactive.MakeInactive();
        GameController.CurrentPlayerProfile.Coins -= _cost;
        GameController.CurrentPlayerProfile.activeUnits[_unitType] = true;
        _textToChange.text = GameController.CurrentPlayerProfile.Coins.ToString();
        _reloadUnits.Invoke();

        Animator.SetBool("isOpen", false);
        Destroy(gameObject, 2);
    }
}
