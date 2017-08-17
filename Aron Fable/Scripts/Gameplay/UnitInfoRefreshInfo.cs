using UnityEngine;
using UnityEngine.UI;

public class UnitInfoRefreshInfo : MonoBehaviour {
    private LevelController _LevelController;
    public GameObject target;
    private UnitOptions targetUO;
    private int upgradecost = 100;
    public GameObject SelectionEffect;

    void Start()
    {
        _LevelController = GameObject.Find("LevelControllerPref").GetComponent<LevelController>();
    }

    public void SetTarget(GameObject obj)
    {
        if (!obj.GetComponent<UnitOptions>().isDead)
        {
            target = obj;
            targetUO = target.GetComponent<UnitOptions>();
            if (SelectionEffect == null) SelectionEffect = Instantiate(Resources.Load("Effects/UnitSelectionEffect") as GameObject);
            SelectionEffect.transform.SetParent(target.transform.FindChild("origin").transform);
            SelectionEffect.transform.localPosition = new Vector3(0, 0, 0);
        }
        else target = null;
    }

    void Update () {

        if (target != null)
        {
            #region Замена текста на панели информации о юните
            transform.FindChild("HP").GetComponent<Text>().text = targetUO.health + " / " + targetUO.MaxHealth;
            transform.FindChild("Damage").GetComponent<Text>().text = (int)targetUO.damage.x + " - " + (int)targetUO.damage.y;
            transform.FindChild("Protection").GetComponent<Text>().text = targetUO.protection.ToString();
            transform.FindChild("AttackSpeed").GetComponent<Text>().text = targetUO.attack_speed.ToString();
            transform.FindChild("CharFace").GetComponent<Image>().sprite = targetUO.iconImage;
            #endregion

            if ((int)targetUO.rankid > 3 || _LevelController.Exp < _LevelController.MaxExp || targetUO.IsEnemy == true || targetUO.UnitsType == UnitsID.hero)
            {
                transform.FindChild("UpgradeIcon").gameObject.SetActive(false);
            }
            else if ((int)targetUO.rankid < 4 && _LevelController.Exp >= _LevelController.MaxExp) transform.FindChild("UpgradeIcon").gameObject.SetActive(true);

            if (targetUO.UnitsType == UnitsID.hero || targetUO.IsEnemy == true)
            {
                transform.FindChild("SellIcon").gameObject.SetActive(false);
            }

            if (targetUO.isDead)
            {
                target = null;
                targetUO = null;
                Destroy(SelectionEffect);
            }
        }
        else
        {
            gameObject.SetActive(false); // Если таргета нету, отключить панель
        }
    }

    public void UpgradeUnit()
    {
        if ((int)targetUO.rankid < 4 && targetUO.isDead == false)
        {
            targetUO.Upgrade(targetUO.UnitsType, targetUO.rankid + 1);
            _LevelController.Exp -= upgradecost;

            if (_LevelController.Exp < _LevelController.MaxExp)
            {
                HeroInfo Hero = GameObject.Find("Hero").GetComponent<HeroInfo>();
                for (int i = 0; i < Hero.units.Count; i++)
                {
                    Hero.units[i].GetComponent<UnitOptions>().DestroyUpgradeIcon();
                } 
            }

            if (GameController.Sound) Destroy(Instantiate(SoundBank.CoinsAdd), 2f);
            Destroy(Instantiate(Resources.Load("Effects/UpgradeEffect"), target.transform.position, Quaternion.identity), 3f);
            GameObject star = Instantiate(Resources.Load("Prefabs/GamePlay/upgradestar")) as GameObject;
            star.transform.SetParent(target.transform.FindChild("UICanvas/StarWindow/Panel"));
            star.transform.localScale = new Vector3(25, 50, 25);
        }
    }

    public void SellUnit()
    {
        if (target.GetComponent<UnitOptions>().isDead == false)
        {
            target.GetComponent<InstantiateEffectController>().EffectDeadInstantly();
            _LevelController.ManaAdd(CharactersData.characterInfo[target.GetComponent<UnitOptions>().UnitsType].rankData[Rank.star0].Manacost / 2);
            target.GetComponent<UnitOptions>().health = 0;
        }
    }
}
