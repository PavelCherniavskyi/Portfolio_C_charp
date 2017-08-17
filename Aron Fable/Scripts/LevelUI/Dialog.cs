using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    public Text Text { get; set; }
    public Animator animator;

    private Animator _interfaceAnim;

    private void Awake () //При Instantiate этого обьекта проинциализировать текст.
	{
	    Text = transform.FindChild("Window/Text").gameObject.GetComponent<Text>();
	    _interfaceAnim = GameObject.Find("UI").GetComponent<Animator>();
	}

    private void Start()
    {
        _interfaceAnim.SetBool("isClosed", true);
        GameObject.Find("LevelControllerPref").GetComponent<LevelController>().globalpause = true;
    }

    public void CloseClick()
    {
        animator.SetBool("isClose", true);
        _interfaceAnim.SetBool("isClosed", false);
        GameObject.Find("LevelControllerPref").GetComponent<LevelController>().globalpause = false;
        Destroy(gameObject, 1);
    }

}
