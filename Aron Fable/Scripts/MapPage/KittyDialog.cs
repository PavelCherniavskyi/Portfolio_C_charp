using System;
using UnityEngine.UI;
using UnityEngine;

public class KittyDialog : MonoBehaviour
{

    public Animator Animator;
    public Text Text;
    public Button Next;

    private readonly String[] _text =
    {
        "Now we have some crystals to buy a new companion for our team.",
        "Let's buy someone right now",
        "If you don't have enough crystals you can allways buy it here",
        "Good luck!"
           
    };

    private int _index = 0;

    private void Awake()
    {
        Animator.SetBool("isOpen", true);

    }

    public void ExitClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        Animator.SetBool("isOpen", false);
        Destroy(gameObject, 2);
    }

    public void NextClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        Text.text = _text[_index++];
        if(_index == 2)
            Animator.Play("KittyDialogBuy");
        if(_index == 3)
            Animator.Play("KittyDialogArrowMove");
        if (_index == 4)
            Next.gameObject.SetActive(false);
    }
}
