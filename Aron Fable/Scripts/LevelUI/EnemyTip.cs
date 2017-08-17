using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyTip : MonoBehaviour {

	[HideInInspector]

	private string titleText;
	private string describingText;
	private Sprite CardSprite;
	private Sprite HPSprite;
	private Sprite AttackSprite;
	private Sprite SpeedAttackSprite;
	private Sprite ArmorSprite;
	private string hp;
	private string armor;
	private Vector2 attackPower;
	private string speed;

	private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", true);
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.TipPopUpClick), 1);
    }
	
	public void CloseClick()
	{
		animator.SetBool ("isOpen", false);
        GameObject.Find("Settings").GetComponent<SettingsMenu>().Pause(false);
        if (GameController.Sound)
            Destroy(Instantiate(SoundBank.ClickSound), 1);
        Destroy(gameObject, 1);
    }

    public void UpgradeStatus(EnemiesID enemyType)
    {
        GameObject.Find("Window/EnemyImage").GetComponent<Image>().sprite = EnemiesData.enemiesInfo[enemyType].cardSprite;
        GameObject.Find("Window/TitleTextImage").GetComponent<Image>().sprite = EnemiesData.enemiesInfo[enemyType].titleTextImage;
        GameObject.Find("InfoWindow/DescribingText").GetComponent<Text>().text = EnemiesData.enemiesInfo[enemyType].describingText;
        GameObject.Find("HP/HPImage").GetComponent<Image>().sprite = EnemiesData.enemiesInfo[enemyType].hPSprite;
        GameObject.Find("HP/HPText").GetComponent<Text>().text = EnemiesData.enemiesInfo[enemyType].hp;
        GameObject.Find("Armor/ArmorImage").GetComponent<Image>().sprite = EnemiesData.enemiesInfo[enemyType].armorSprite;
        GameObject.Find("Armor/ArmorText").GetComponent<Text>().text = EnemiesData.enemiesInfo[enemyType].armor;
        GameObject.Find("Attack/AttackImage").GetComponent<Image>().sprite = EnemiesData.enemiesInfo[enemyType].attackSprite;
        GameObject.Find("Attack/AttackText").GetComponent<Text>().text = EnemiesData.enemiesInfo[enemyType].attackPower.ToString();
        GameObject.Find("Speed/SpeedImage").GetComponent<Image>().sprite = EnemiesData.enemiesInfo[enemyType].speedAttackSprite;
        GameObject.Find("Speed/SpeedText").GetComponent<Text>().text = EnemiesData.enemiesInfo[enemyType].speed;
    }
}
