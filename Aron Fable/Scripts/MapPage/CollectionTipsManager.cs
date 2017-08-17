using UnityEngine;
using UnityEngine.UI;

public class CollectionTipsManager : MonoBehaviour
{
    private Text tipText;
    private int currentTip = 0;

    private void Start()
    {
        tipText = GameObject.Find("ScrollImage/Tip").GetComponent<Text>();
        tipText.text = GameController.getTip(currentTip).ToString();
    }

    public void NextClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        currentTip++;
        if (currentTip > 29)
            currentTip = 0;
        tipText.text = GameController.getTip(currentTip).ToString();
    }

    public void BackClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        currentTip--;
        if (currentTip < 0)
            currentTip = 29;
        tipText.text = GameController.getTip(currentTip).ToString();
    }

    public void BackMenuClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        GameObject.Find("CollectionWindow").GetComponent<CollectionWindManager>().unitsEnemiesTipsWindow.SetActive(true);
        GameObject.Find("CollectionWindow").GetComponent<CollectionWindManager>().collectionAnim.SetBool("isOpen", true);
        gameObject.SetActive(false);
    }

    public void CloseClick()
    {
        gameObject.SetActive(false);
    }
}