using UnityEngine;
using UnityEngine.UI;

public class CollectionWindManager : MonoBehaviour
{
    private GameObject unitsWindow;
    private GameObject enemiesWindow;
    private GameObject tipsWindow;

    [HideInInspector]
    public GameObject unitsEnemiesTipsWindow;

    [HideInInspector]
    public Animator collectionAnim;

    private Animator unitsPanelAnim;
    private Animator enemiesPanelAnim;

    private void Start()
    {
        FindWindows();
        unitsEnemiesTipsWindow.SetActive(true);
        collectionAnim = gameObject.GetComponent<Animator>();
        collectionAnim.SetBool("isOpen", true);
    }

    private void OnEnable()
    {
        if(unitsEnemiesTipsWindow != null)
            unitsEnemiesTipsWindow.SetActive(true);
        if (collectionAnim != null)
            collectionAnim.SetBool("isOpen", true);
        
    }

    private void FindWindows()
    {
        Component[] wind = transform.GetComponentsInChildren(typeof(Transform), true);
        foreach (var a in wind)
        {
            if (a.name == "UnitsPanel")
                unitsWindow = a.gameObject;
            else if (a.name == "EnemiesPanel")
                enemiesWindow = a.gameObject;
            else if (a.name == "UnitsEnemiesTips")
                unitsEnemiesTipsWindow = a.gameObject;
        }
        unitsPanelAnim = unitsWindow.GetComponent<Animator>();
        enemiesPanelAnim = enemiesWindow.GetComponent<Animator>();
    }

    public void UnitsClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        collectionAnim.SetBool("isOpen", false);
        unitsEnemiesTipsWindow.SetActive(false);
        unitsWindow.SetActive(true);
        unitsPanelAnim.SetBool("isOpen", true);
    }

    public void EnemiesClick()
    {
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        collectionAnim.SetBool("isOpen", false);
        unitsEnemiesTipsWindow.SetActive(false);
        enemiesWindow.SetActive(true);
        enemiesPanelAnim.SetBool("isOpen", true);
    }

}