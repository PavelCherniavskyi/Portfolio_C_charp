using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanelPref : MonoBehaviour {

    public Animator CardPanelAnim;
    private IconPageDown[] _cards;

    void Awake ()
    {
        _cards = transform.FindChild("CardWindow").GetComponentsInChildren<IconPageDown>();
        ResetStack();
    }

    public void ResetStack()
    {
        foreach (IconPageDown t in _cards)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void CardPanelClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        if (CardPanelAnim.GetBool("isClosed"))
        {
            CardPanelAnim.SetBool("isClosed", false);
            return;
        }

        CardPanelAnim.SetBool("isClosed", true);
    }

    public void CardClick(UnitsID id)
    {
        for (int i = _cards.Length - 1; i >= 0 ; i--)
        {
            if (!_cards[i].gameObject.activeSelf)
            {
                _cards[i].gameObject.SetActive(true);
                _cards[i].UnitType = id;
                _cards[i].ReInitialize();
                break;
            }
        }
    }


}
