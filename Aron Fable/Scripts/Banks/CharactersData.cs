using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitsIDData
{
    public Dictionary<Rank, UnitPerformance> rankData = new Dictionary<Rank, UnitPerformance>();
}


public class UnitPerformance
{
    public string titleText;
    public string describingText;
    public Sprite RankCardSprite;

    public float MaxHealth;
    public float MaxMana;
    public string ProtectionStr;
    public float ProtectionNumber;
    public float MagicResistance;
    public Vector2 Damage;
    public string AttackSpeedStr;
    public float AttackSpeedNumber;
    public string PathToPrefab;
    public string PathToShadow;
    public int Manacost;
}

public static class VectorExtention
{
    public static string My_ToString(this Vector2 vector)
    {
        return ((int)vector.x).ToString() + " - " + ((int)vector.y).ToString();
    }
}

public class UpgrageCostHelper
{
    public Dictionary<int, int> upgradeRankCost = new Dictionary<int, int>();
}

public class CharactersData : MonoBehaviour
{
    public static Dictionary<UnitsID, UnitsIDData> characterInfo = new Dictionary<UnitsID, UnitsIDData>(Enum.GetNames(typeof(UnitsID)).Length);
    private SpritesBank _spritesBankScript;
    public static Dictionary<UpgradeID, UpgrageCostHelper> UpgrageCost = new Dictionary<UpgradeID, UpgrageCostHelper>();

    private void Awake()
    {
        _spritesBankScript = GetComponent<SpritesBank>();
        InitializeCharacterData();
    }

    private void InitializeCharacterData()
    {
        UnitsIDData[] unitsIDData = new UnitsIDData[Enum.GetNames(typeof(UnitsID)).Length];
        UnitPerformance[] archerData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];
        

        for (int i = 0; i < Enum.GetNames(typeof(UnitsID)).Length; i++)
            unitsIDData[i] = new UnitsIDData();
        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            archerData[i] = new UnitPerformance();

        UpgrageCostHelper [] upgrageCostHelper = new UpgrageCostHelper[4];
        for (int i = 0; i < upgrageCostHelper.Length; i++)
        {
            upgrageCostHelper[i] = new UpgrageCostHelper();
        }

        

        UpgrageCost[UpgradeID.range] = upgrageCostHelper[0];
        UpgrageCost[UpgradeID.warrior] = upgrageCostHelper[1];
        UpgrageCost[UpgradeID.magic] = upgrageCostHelper[2];
        UpgrageCost[UpgradeID.stealth] = upgrageCostHelper[3];

        UpgrageCost[UpgradeID.warrior].upgradeRankCost[0] = 1;
        UpgrageCost[UpgradeID.warrior].upgradeRankCost[1] = 2;
        UpgrageCost[UpgradeID.warrior].upgradeRankCost[2] = 3;
        UpgrageCost[UpgradeID.warrior].upgradeRankCost[3] = 3;
        UpgrageCost[UpgradeID.warrior].upgradeRankCost[4] = 3;

        UpgrageCost[UpgradeID.range].upgradeRankCost[0] = 1;
        UpgrageCost[UpgradeID.range].upgradeRankCost[1] = 2;
        UpgrageCost[UpgradeID.range].upgradeRankCost[2] = 3;
        UpgrageCost[UpgradeID.range].upgradeRankCost[3] = 3;
        UpgrageCost[UpgradeID.range].upgradeRankCost[4] = 3;

        UpgrageCost[UpgradeID.magic].upgradeRankCost[0] = 1;
        UpgrageCost[UpgradeID.magic].upgradeRankCost[1] = 2;
        UpgrageCost[UpgradeID.magic].upgradeRankCost[2] = 3;
        UpgrageCost[UpgradeID.magic].upgradeRankCost[3] = 3;
        UpgrageCost[UpgradeID.magic].upgradeRankCost[4] = 3;

        UpgrageCost[UpgradeID.stealth].upgradeRankCost[0] = 1;
        UpgrageCost[UpgradeID.stealth].upgradeRankCost[1] = 2;
        UpgrageCost[UpgradeID.stealth].upgradeRankCost[2] = 3;
        UpgrageCost[UpgradeID.stealth].upgradeRankCost[3] = 3;
        UpgrageCost[UpgradeID.stealth].upgradeRankCost[4] = 3;




        archerData[(int)Rank.star0].ProtectionStr = "None";
        archerData[(int)Rank.star0].titleText = "Archer";
        archerData[(int)Rank.star0].describingText = "She's got\ndummy ears ant tail.\nbut tsss...\nAct like you don't know it.";
        archerData[(int)Rank.star0].RankCardSprite = _spritesBankScript.CARD_Ranger;
        archerData[(int)Rank.star0].MaxHealth = 120;
        archerData[(int)Rank.star0].MaxMana = 0;
        archerData[(int)Rank.star0].AttackSpeedNumber = 1f;
        archerData[(int)Rank.star0].ProtectionNumber = 0;
        archerData[(int)Rank.star0].Damage = new Vector2(16f, 28f);
        archerData[(int)Rank.star0].MagicResistance = 0;
        archerData[(int)Rank.star0].AttackSpeedStr = "Normal";
        archerData[(int)Rank.star0].PathToPrefab = "Prefabs/Units/Ranger";
        archerData[(int)Rank.star0].PathToShadow = "Prefabs/Units/UnitsShadow/Ranger_Shadow";

        archerData[(int)Rank.star1].ProtectionStr = "None";
        archerData[(int)Rank.star1].titleText = "Burst Shot I";
        archerData[(int)Rank.star1].describingText = "Increase her own attack speed for a short duration";
        archerData[(int)Rank.star1].RankCardSprite = _spritesBankScript.archerStar1;
        archerData[(int)Rank.star1].MaxHealth = 150;
        archerData[(int)Rank.star1].MaxMana = 0;
        archerData[(int)Rank.star1].AttackSpeedNumber = 1.1f;
        archerData[(int)Rank.star1].MagicResistance = 0;
        archerData[(int)Rank.star1].ProtectionNumber = 1;
        archerData[(int)Rank.star1].Damage = new Vector2(22f, 34f);
        archerData[(int)Rank.star1].AttackSpeedStr = "Normal";

        archerData[(int)Rank.star2].ProtectionStr = "None";
        archerData[(int)Rank.star2].titleText = "Burst Shot II";
        archerData[(int)Rank.star2].describingText = "Duration is increased";
        archerData[(int)Rank.star2].RankCardSprite = _spritesBankScript.archerStar2;
        archerData[(int)Rank.star2].MaxHealth = 180;
        archerData[(int)Rank.star2].MaxMana = 0;
        archerData[(int)Rank.star2].AttackSpeedNumber = 1.2f;
        archerData[(int)Rank.star2].MagicResistance = 1;
        archerData[(int)Rank.star2].ProtectionNumber = 2;
        archerData[(int)Rank.star2].Damage = new Vector2(30f, 50f);
        archerData[(int)Rank.star2].AttackSpeedStr = "Normal";

        archerData[(int)Rank.star3].ProtectionStr = "None";
        archerData[(int)Rank.star3].titleText = "Burst Shot III";
        archerData[(int)Rank.star3].describingText = "Grants a chance to shoot two targets when skill is active";
        archerData[(int)Rank.star3].RankCardSprite = _spritesBankScript.archerStar3;
        archerData[(int)Rank.star3].MaxHealth = 220;
        archerData[(int)Rank.star3].MaxMana = 0;
        archerData[(int)Rank.star3].AttackSpeedNumber = 1.3f;
        archerData[(int)Rank.star3].MagicResistance = 2;
        archerData[(int)Rank.star3].ProtectionNumber = 2;
        archerData[(int)Rank.star3].Damage = new Vector2(38f, 54f);
        archerData[(int)Rank.star3].AttackSpeedStr = "Normal";

        archerData[(int)Rank.star4].ProtectionStr = "Low";
        archerData[(int)Rank.star4].titleText = "Burst Shot IV";
        archerData[(int)Rank.star4].describingText = "Increases chance of shooting two targets";
        archerData[(int)Rank.star4].RankCardSprite = _spritesBankScript.archerStar4;
        archerData[(int)Rank.star4].MaxHealth = 275;
        archerData[(int)Rank.star4].MaxMana = 0;
        archerData[(int)Rank.star4].AttackSpeedNumber = 1.4f;
        archerData[(int)Rank.star4].MagicResistance = 3;
        archerData[(int)Rank.star4].ProtectionNumber = 3;
        archerData[(int)Rank.star4].Damage = new Vector2(55f, 75f);
        archerData[(int)Rank.star4].AttackSpeedStr = "Normal";

        unitsIDData[0].rankData[Rank.star0] = archerData[0];
        unitsIDData[0].rankData[Rank.star1] = archerData[1];
        unitsIDData[0].rankData[Rank.star2] = archerData[2];
        unitsIDData[0].rankData[Rank.star3] = archerData[3];
        unitsIDData[0].rankData[Rank.star4] = archerData[4];

        UnitPerformance[] knightData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            knightData[i] = new UnitPerformance();

       

        knightData[(int)Rank.star0].titleText = "Knight";
        knightData[(int)Rank.star0].describingText = "It's an ordinary and\nusual night.\nHe is so usual that it\ndisheartenes even him.";
        knightData[(int)Rank.star0].MaxHealth = 300;
        knightData[(int)Rank.star0].MaxMana = 0;
        knightData[(int)Rank.star0].AttackSpeedNumber = 1f;
        knightData[(int)Rank.star0].MagicResistance = 1;
        knightData[(int)Rank.star0].ProtectionNumber = 3;
        knightData[(int)Rank.star0].Damage = new Vector2(12f, 22f);
        knightData[(int)Rank.star0].ProtectionStr = "High";
        knightData[(int)Rank.star0].AttackSpeedStr = "Normal";
        knightData[(int)Rank.star0].RankCardSprite = _spritesBankScript.CARD_Knight;
        knightData[(int)Rank.star0].PathToPrefab = "Prefabs/Units/Warrior";
        knightData[(int)Rank.star0].PathToShadow = "Prefabs/Units/UnitsShadow/Warrior_Shadow";

        knightData[(int)Rank.star1].titleText = "Honed Strike I";
        knightData[(int)Rank.star1].describingText = "Stuns and damages a target";
        knightData[(int)Rank.star1].MaxHealth = 350;
        knightData[(int)Rank.star1].MaxMana = 0;
        knightData[(int)Rank.star1].AttackSpeedNumber = 1.1f;
        knightData[(int)Rank.star1].MagicResistance = 1;
        knightData[(int)Rank.star1].ProtectionNumber = 4;
        knightData[(int)Rank.star1].Damage = new Vector2(20f, 30f);
        knightData[(int)Rank.star1].ProtectionStr = "Medium";
        knightData[(int)Rank.star1].AttackSpeedStr = "Normal";
        knightData[(int)Rank.star1].RankCardSprite = _spritesBankScript.knightStar1;

        knightData[(int)Rank.star2].titleText = "Honed Strike II";
        knightData[(int)Rank.star2].describingText = "Longer stun duration and higher damage";
        knightData[(int)Rank.star2].MaxHealth = 400;
        knightData[(int)Rank.star2].MaxMana = 0;
        knightData[(int)Rank.star2].AttackSpeedNumber = 1.2f;
        knightData[(int)Rank.star2].MagicResistance = 2;
        knightData[(int)Rank.star2].ProtectionNumber = 5;
        knightData[(int)Rank.star2].Damage = new Vector2(28f, 38f);
        knightData[(int)Rank.star2].ProtectionStr = "Medium";
        knightData[(int)Rank.star2].AttackSpeedStr = "Normal";
        knightData[(int)Rank.star2].RankCardSprite = _spritesBankScript.knightStar2;

        knightData[(int)Rank.star3].titleText = "Honed Strike III";
        knightData[(int)Rank.star3].describingText = "Also deals damage to those around the target";
        knightData[(int)Rank.star3].MaxHealth = 450;
        knightData[(int)Rank.star3].MaxMana = 0;
        knightData[(int)Rank.star3].AttackSpeedNumber = 1.3f;
        knightData[(int)Rank.star3].MagicResistance = 3;
        knightData[(int)Rank.star3].ProtectionNumber = 5;
        knightData[(int)Rank.star3].Damage = new Vector2(30f, 45f);
        knightData[(int)Rank.star3].ProtectionStr = "High";
        knightData[(int)Rank.star3].AttackSpeedStr = "Normal";
        knightData[(int)Rank.star3].RankCardSprite = _spritesBankScript.knightStar3;

        knightData[(int)Rank.star4].titleText = "Honed Strike IV";
        knightData[(int)Rank.star4].describingText = "Honer strike deals higher damage";
        knightData[(int)Rank.star4].MaxHealth = 500;
        knightData[(int)Rank.star4].MaxMana = 0;
        knightData[(int)Rank.star4].AttackSpeedNumber = 1.4f;
        knightData[(int)Rank.star4].MagicResistance = 3;
        knightData[(int)Rank.star4].ProtectionNumber = 6;
        knightData[(int)Rank.star4].Damage = new Vector2(40f, 60f);
        knightData[(int)Rank.star4].ProtectionStr = "High";
        knightData[(int)Rank.star4].AttackSpeedStr = "Normal";
        knightData[(int)Rank.star4].RankCardSprite = _spritesBankScript.knightStar4;

        unitsIDData[1].rankData[Rank.star0] = knightData[0];
        unitsIDData[1].rankData[Rank.star1] = knightData[1];
        unitsIDData[1].rankData[Rank.star2] = knightData[2];
        unitsIDData[1].rankData[Rank.star3] = knightData[3];
        unitsIDData[1].rankData[Rank.star4] = knightData[4];

        UnitPerformance[] puppeteerData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            puppeteerData[i] = new UnitPerformance();

        puppeteerData[(int)Rank.star0].titleText = "Puppeteer";
        puppeteerData[(int)Rank.star0].describingText = "That's what games\nwith dolls leads to if you\ndon't stop at the right time";
        puppeteerData[(int)Rank.star0].MaxHealth = 100;
        puppeteerData[(int)Rank.star0].MaxMana = 200;
        puppeteerData[(int)Rank.star0].AttackSpeedNumber = 1;
        puppeteerData[(int)Rank.star0].MagicResistance = 1;
        puppeteerData[(int)Rank.star0].ProtectionNumber = 0;
        puppeteerData[(int)Rank.star0].Damage = new Vector2(4f, 10f);
        puppeteerData[(int)Rank.star0].ProtectionStr = "Low";
        puppeteerData[(int)Rank.star0].AttackSpeedStr = "Normal";
        puppeteerData[(int)Rank.star0].RankCardSprite = _spritesBankScript.CARD_Summoner;
        puppeteerData[(int)Rank.star0].PathToPrefab = "Prefabs/Units/Summoner";
        puppeteerData[(int)Rank.star0].PathToShadow = "Prefabs/Units/UnitsShadow/Summoner_Shadow";

        puppeteerData[(int)Rank.star1].titleText = "Frost Blast I";
        puppeteerData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        puppeteerData[(int)Rank.star1].MaxHealth = 150;
        puppeteerData[(int)Rank.star1].MaxMana = 250;
        puppeteerData[(int)Rank.star1].AttackSpeedNumber = 1.1f;
        puppeteerData[(int)Rank.star1].MagicResistance = 2;
        puppeteerData[(int)Rank.star1].ProtectionNumber = 1;
        puppeteerData[(int)Rank.star1].Damage = new Vector2(10f, 18f);
        puppeteerData[(int)Rank.star1].ProtectionStr = "Low";
        puppeteerData[(int)Rank.star1].AttackSpeedStr = "Normal";
        puppeteerData[(int)Rank.star1].RankCardSprite = _spritesBankScript.puppeteerStar1;

        puppeteerData[(int)Rank.star2].titleText = "Frost Blast II";
        puppeteerData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        puppeteerData[(int)Rank.star2].MaxHealth = 200;
        puppeteerData[(int)Rank.star2].MaxMana = 300;
        puppeteerData[(int)Rank.star2].AttackSpeedNumber = 1.2f;
        puppeteerData[(int)Rank.star2].MagicResistance = 3;
        puppeteerData[(int)Rank.star2].ProtectionNumber = 2;
        puppeteerData[(int)Rank.star2].Damage = new Vector2(18f, 24f);
        puppeteerData[(int)Rank.star2].ProtectionStr = "Medium";
        puppeteerData[(int)Rank.star2].AttackSpeedStr = "Normal";
        puppeteerData[(int)Rank.star2].RankCardSprite = _spritesBankScript.puppeteerStar2;

        puppeteerData[(int)Rank.star3].titleText = "Frost Blast III";
        puppeteerData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        puppeteerData[(int)Rank.star3].MaxHealth = 250;
        puppeteerData[(int)Rank.star3].MaxMana = 400;
        puppeteerData[(int)Rank.star3].AttackSpeedNumber = 1.3f;
        puppeteerData[(int)Rank.star3].MagicResistance = 4;
        puppeteerData[(int)Rank.star3].ProtectionNumber = 2;
        puppeteerData[(int)Rank.star3].Damage = new Vector2(24f, 32f);
        puppeteerData[(int)Rank.star3].ProtectionStr = "Medium";
        puppeteerData[(int)Rank.star3].AttackSpeedStr = "Normal";
        puppeteerData[(int)Rank.star3].RankCardSprite = _spritesBankScript.puppeteerStar3;

        puppeteerData[(int)Rank.star4].titleText = "Frost Blast IV";
        puppeteerData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        puppeteerData[(int)Rank.star4].MaxHealth = 300;
        puppeteerData[(int)Rank.star4].MaxMana = 500;
        puppeteerData[(int)Rank.star4].AttackSpeedNumber = 1.4f;
        puppeteerData[(int)Rank.star4].MagicResistance = 5;
        puppeteerData[(int)Rank.star4].ProtectionNumber = 3;
        puppeteerData[(int)Rank.star4].Damage = new Vector2(32f, 46f);
        puppeteerData[(int)Rank.star4].ProtectionStr = "Medium";
        puppeteerData[(int)Rank.star4].AttackSpeedStr = "Normal";
        puppeteerData[(int)Rank.star4].RankCardSprite = _spritesBankScript.puppeteerStar4;

        unitsIDData[2].rankData[Rank.star0] = puppeteerData[0];
        unitsIDData[2].rankData[Rank.star1] = puppeteerData[1];
        unitsIDData[2].rankData[Rank.star2] = puppeteerData[2];
        unitsIDData[2].rankData[Rank.star3] = puppeteerData[3];
        unitsIDData[2].rankData[Rank.star4] = puppeteerData[4];

        UnitPerformance[] rougeData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            rougeData[i] = new UnitPerformance();

        rougeData[(int)Rank.star0].titleText = "Rouge";
        rougeData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        rougeData[(int)Rank.star0].MaxHealth = 100;
        rougeData[(int)Rank.star0].MaxMana = 100;
        rougeData[(int)Rank.star0].AttackSpeedNumber = 1;
        rougeData[(int)Rank.star0].MagicResistance = 1;
        rougeData[(int)Rank.star0].ProtectionNumber = 1;
        rougeData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        rougeData[(int)Rank.star0].ProtectionStr = "Low";
        rougeData[(int)Rank.star0].AttackSpeedStr = "Normal";
        rougeData[(int)Rank.star0].RankCardSprite = _spritesBankScript.rougeStar0;

        rougeData[(int)Rank.star1].titleText = "Smokescreen I";
        rougeData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        rougeData[(int)Rank.star1].MaxHealth = 200;
        rougeData[(int)Rank.star1].MaxMana = 200;
        rougeData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        rougeData[(int)Rank.star1].MagicResistance = 2;
        rougeData[(int)Rank.star1].ProtectionNumber = 2;
        rougeData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        rougeData[(int)Rank.star1].ProtectionStr = "Low";
        rougeData[(int)Rank.star1].AttackSpeedStr = "Normal";
        rougeData[(int)Rank.star1].RankCardSprite = _spritesBankScript.rougeStar1;

        rougeData[(int)Rank.star2].titleText = "Smokescreen II";
        rougeData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        rougeData[(int)Rank.star2].MaxHealth = 300;
        rougeData[(int)Rank.star2].MaxMana = 300;
        rougeData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        rougeData[(int)Rank.star2].MagicResistance = 3;
        rougeData[(int)Rank.star2].ProtectionNumber = 3;
        rougeData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        rougeData[(int)Rank.star2].ProtectionStr = "Medium";
        rougeData[(int)Rank.star2].AttackSpeedStr = "Normal";
        rougeData[(int)Rank.star2].RankCardSprite = _spritesBankScript.rougeStar2;

        rougeData[(int)Rank.star3].titleText = "Smokescreen III";
        rougeData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        rougeData[(int)Rank.star3].MaxHealth = 400;
        rougeData[(int)Rank.star3].MaxMana = 400;
        rougeData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        rougeData[(int)Rank.star3].MagicResistance = 4;
        rougeData[(int)Rank.star3].ProtectionNumber = 4;
        rougeData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        rougeData[(int)Rank.star3].ProtectionStr = "Medium";
        rougeData[(int)Rank.star3].AttackSpeedStr = "Normal";
        rougeData[(int)Rank.star3].RankCardSprite = _spritesBankScript.rougeStar3;

        rougeData[(int)Rank.star4].titleText = "Smokescreen IV";
        rougeData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        rougeData[(int)Rank.star4].MaxHealth = 500;
        rougeData[(int)Rank.star4].MaxMana = 500;
        rougeData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        rougeData[(int)Rank.star4].MagicResistance = 5;
        rougeData[(int)Rank.star4].ProtectionNumber = 5;
        rougeData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        rougeData[(int)Rank.star4].ProtectionStr = "Medium";
        rougeData[(int)Rank.star4].AttackSpeedStr = "Normal";
        rougeData[(int)Rank.star4].RankCardSprite = _spritesBankScript.rougeStar4;

        unitsIDData[3].rankData[Rank.star0] = rougeData[0];
        unitsIDData[3].rankData[Rank.star1] = rougeData[1];
        unitsIDData[3].rankData[Rank.star2] = rougeData[2];
        unitsIDData[3].rankData[Rank.star3] = rougeData[3];
        unitsIDData[3].rankData[Rank.star4] = rougeData[4];

        UnitPerformance[] berserkerData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            berserkerData[i] = new UnitPerformance();

        berserkerData[(int)Rank.star0].titleText = "Berserker";
        berserkerData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        berserkerData[(int)Rank.star0].MaxHealth = 100;
        berserkerData[(int)Rank.star0].MaxMana = 100;
        berserkerData[(int)Rank.star0].AttackSpeedNumber = 1;
        berserkerData[(int)Rank.star0].MagicResistance = 1;
        berserkerData[(int)Rank.star0].ProtectionNumber = 1;
        berserkerData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        berserkerData[(int)Rank.star0].ProtectionStr = "Low";
        berserkerData[(int)Rank.star0].AttackSpeedStr = "Normal";
        berserkerData[(int)Rank.star0].RankCardSprite = _spritesBankScript.berserkerStar0;

        berserkerData[(int)Rank.star1].titleText = "Smokescreen I";
        berserkerData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        berserkerData[(int)Rank.star1].MaxHealth = 200;
        berserkerData[(int)Rank.star1].MaxMana = 200;
        berserkerData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        berserkerData[(int)Rank.star1].MagicResistance = 2;
        berserkerData[(int)Rank.star1].ProtectionNumber = 2;
        berserkerData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        berserkerData[(int)Rank.star1].ProtectionStr = "Low";
        berserkerData[(int)Rank.star1].AttackSpeedStr = "Normal";
        berserkerData[(int)Rank.star1].RankCardSprite = _spritesBankScript.berserkerStar1;

        berserkerData[(int)Rank.star2].titleText = "Smokescreen II";
        berserkerData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        berserkerData[(int)Rank.star2].MaxHealth = 300;
        berserkerData[(int)Rank.star2].MaxMana = 300;
        berserkerData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        berserkerData[(int)Rank.star2].MagicResistance = 3;
        berserkerData[(int)Rank.star2].ProtectionNumber = 3;
        berserkerData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        berserkerData[(int)Rank.star2].ProtectionStr = "Medium";
        berserkerData[(int)Rank.star2].AttackSpeedStr = "Normal";
        berserkerData[(int)Rank.star2].RankCardSprite = _spritesBankScript.berserkerStar2;

        berserkerData[(int)Rank.star3].titleText = "Smokescreen III";
        berserkerData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        berserkerData[(int)Rank.star3].MaxHealth = 400;
        berserkerData[(int)Rank.star3].MaxMana = 400;
        berserkerData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        berserkerData[(int)Rank.star3].MagicResistance = 4;
        berserkerData[(int)Rank.star3].ProtectionNumber = 4;
        berserkerData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        berserkerData[(int)Rank.star3].ProtectionStr = "Medium";
        berserkerData[(int)Rank.star3].AttackSpeedStr = "Normal";
        berserkerData[(int)Rank.star3].RankCardSprite = _spritesBankScript.berserkerStar3;

        berserkerData[(int)Rank.star4].titleText = "Smokescreen IV";
        berserkerData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        berserkerData[(int)Rank.star4].MaxHealth = 500;
        berserkerData[(int)Rank.star4].MaxMana = 500;
        berserkerData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        berserkerData[(int)Rank.star4].MagicResistance = 5;
        berserkerData[(int)Rank.star4].ProtectionNumber = 5;
        berserkerData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        berserkerData[(int)Rank.star4].ProtectionStr = "Medium";
        berserkerData[(int)Rank.star4].AttackSpeedStr = "Normal";
        berserkerData[(int)Rank.star4].RankCardSprite = _spritesBankScript.berserkerStar4;

        unitsIDData[4].rankData[Rank.star0] = berserkerData[0];
        unitsIDData[4].rankData[Rank.star1] = berserkerData[1];
        unitsIDData[4].rankData[Rank.star2] = berserkerData[2];
        unitsIDData[4].rankData[Rank.star3] = berserkerData[3];
        unitsIDData[4].rankData[Rank.star4] = berserkerData[4];

        UnitPerformance[] hunterData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            hunterData[i] = new UnitPerformance();

        hunterData[(int)Rank.star0].titleText = "Hunter";
        hunterData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        hunterData[(int)Rank.star0].MaxHealth = 100;
        hunterData[(int)Rank.star0].MaxMana = 100;
        hunterData[(int)Rank.star0].AttackSpeedNumber = 1;
        hunterData[(int)Rank.star0].MagicResistance = 1;
        hunterData[(int)Rank.star0].ProtectionNumber = 1;
        hunterData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        hunterData[(int)Rank.star0].ProtectionStr = "Low";
        hunterData[(int)Rank.star0].AttackSpeedStr = "Normal";
        hunterData[(int)Rank.star0].RankCardSprite = _spritesBankScript.hunterStar0;

        hunterData[(int)Rank.star1].titleText = "Smokescreen I";
        hunterData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        hunterData[(int)Rank.star1].MaxHealth = 200;
        hunterData[(int)Rank.star1].MaxMana = 200;
        hunterData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        hunterData[(int)Rank.star1].MagicResistance = 2;
        hunterData[(int)Rank.star1].ProtectionNumber = 2;
        hunterData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        hunterData[(int)Rank.star1].ProtectionStr = "Low";
        hunterData[(int)Rank.star1].AttackSpeedStr = "Normal";
        hunterData[(int)Rank.star1].RankCardSprite = _spritesBankScript.hunterStar1;

        hunterData[(int)Rank.star2].titleText = "Smokescreen II";
        hunterData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        hunterData[(int)Rank.star2].MaxHealth = 300;
        hunterData[(int)Rank.star2].MaxMana = 300;
        hunterData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        hunterData[(int)Rank.star2].MagicResistance = 3;
        hunterData[(int)Rank.star2].ProtectionNumber = 3;
        hunterData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        hunterData[(int)Rank.star2].ProtectionStr = "Medium";
        hunterData[(int)Rank.star2].AttackSpeedStr = "Normal";
        hunterData[(int)Rank.star2].RankCardSprite = _spritesBankScript.hunterStar2;

        hunterData[(int)Rank.star3].titleText = "Smokescreen III";
        hunterData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        hunterData[(int)Rank.star3].MaxHealth = 400;
        hunterData[(int)Rank.star3].MaxMana = 400;
        hunterData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        hunterData[(int)Rank.star3].MagicResistance = 4;
        hunterData[(int)Rank.star3].ProtectionNumber = 4;
        hunterData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        hunterData[(int)Rank.star3].ProtectionStr = "Medium";
        hunterData[(int)Rank.star3].AttackSpeedStr = "Normal";
        hunterData[(int)Rank.star3].RankCardSprite = _spritesBankScript.hunterStar3;

        hunterData[(int)Rank.star4].titleText = "Smokescreen IV";
        hunterData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        hunterData[(int)Rank.star4].MaxHealth = 500;
        hunterData[(int)Rank.star4].MaxMana = 500;
        hunterData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        hunterData[(int)Rank.star4].MagicResistance = 5;
        hunterData[(int)Rank.star4].ProtectionNumber = 5;
        hunterData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        hunterData[(int)Rank.star4].ProtectionStr = "Medium";
        hunterData[(int)Rank.star4].AttackSpeedStr = "Normal";
        hunterData[(int)Rank.star4].RankCardSprite = _spritesBankScript.hunterStar4;

        unitsIDData[5].rankData[Rank.star0] = hunterData[0];
        unitsIDData[5].rankData[Rank.star1] = hunterData[1];
        unitsIDData[5].rankData[Rank.star2] = hunterData[2];
        unitsIDData[5].rankData[Rank.star3] = hunterData[3];
        unitsIDData[5].rankData[Rank.star4] = hunterData[4];

        UnitPerformance[] clercData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            clercData[i] = new UnitPerformance();

        clercData[(int)Rank.star0].titleText = "Clerc";
        clercData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        clercData[(int)Rank.star0].MaxHealth = 100;
        clercData[(int)Rank.star0].MaxMana = 100;
        clercData[(int)Rank.star0].AttackSpeedNumber = 1;
        clercData[(int)Rank.star0].MagicResistance = 1;
        clercData[(int)Rank.star0].ProtectionNumber = 1;
        clercData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        clercData[(int)Rank.star0].ProtectionStr = "Low";
        clercData[(int)Rank.star0].AttackSpeedStr = "Normal";
        clercData[(int)Rank.star0].RankCardSprite = _spritesBankScript.clericStar0;

        clercData[(int)Rank.star1].titleText = "Smokescreen I";
        clercData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        clercData[(int)Rank.star1].MaxHealth = 200;
        clercData[(int)Rank.star1].MaxMana = 200;
        clercData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        clercData[(int)Rank.star1].MagicResistance = 2;
        clercData[(int)Rank.star1].ProtectionNumber = 2;
        clercData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        clercData[(int)Rank.star1].ProtectionStr = "Low";
        clercData[(int)Rank.star1].AttackSpeedStr = "Normal";
        clercData[(int)Rank.star1].RankCardSprite = _spritesBankScript.clericStar1;

        clercData[(int)Rank.star2].titleText = "Smokescreen II";
        clercData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        clercData[(int)Rank.star2].MaxHealth = 300;
        clercData[(int)Rank.star2].MaxMana = 300;
        clercData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        clercData[(int)Rank.star2].MagicResistance = 3;
        clercData[(int)Rank.star2].ProtectionNumber = 3;
        clercData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        clercData[(int)Rank.star2].ProtectionStr = "Medium";
        clercData[(int)Rank.star2].AttackSpeedStr = "Normal";
        clercData[(int)Rank.star2].RankCardSprite = _spritesBankScript.clericStar2;

        clercData[(int)Rank.star3].titleText = "Smokescreen III";
        clercData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        clercData[(int)Rank.star3].MaxHealth = 400;
        clercData[(int)Rank.star3].MaxMana = 400;
        clercData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        clercData[(int)Rank.star3].MagicResistance = 4;
        clercData[(int)Rank.star3].ProtectionNumber = 4;
        clercData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        clercData[(int)Rank.star3].ProtectionStr = "Medium";
        clercData[(int)Rank.star3].AttackSpeedStr = "Normal";
        clercData[(int)Rank.star3].RankCardSprite = _spritesBankScript.clericStar3;

        clercData[(int)Rank.star4].titleText = "Smokescreen IV";
        clercData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        clercData[(int)Rank.star4].MaxHealth = 500;
        clercData[(int)Rank.star4].MaxMana = 500;
        clercData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        clercData[(int)Rank.star4].MagicResistance = 5;
        clercData[(int)Rank.star4].ProtectionNumber = 5;
        clercData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        clercData[(int)Rank.star4].ProtectionStr = "Medium";
        clercData[(int)Rank.star4].AttackSpeedStr = "Normal";
        clercData[(int)Rank.star4].RankCardSprite = _spritesBankScript.clericStar4;

        unitsIDData[6].rankData[Rank.star0] = clercData[0];
        unitsIDData[6].rankData[Rank.star1] = clercData[1];
        unitsIDData[6].rankData[Rank.star2] = clercData[2];
        unitsIDData[6].rankData[Rank.star3] = clercData[3];
        unitsIDData[6].rankData[Rank.star4] = clercData[4];

        UnitPerformance[] assassinData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            assassinData[i] = new UnitPerformance();

        assassinData[(int)Rank.star0].titleText = "Assassin";
        assassinData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        assassinData[(int)Rank.star0].MaxHealth = 100;
        assassinData[(int)Rank.star0].MaxMana = 100;
        assassinData[(int)Rank.star0].AttackSpeedNumber = 1;
        assassinData[(int)Rank.star0].MagicResistance = 1;
        assassinData[(int)Rank.star0].ProtectionNumber = 1;
        assassinData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        assassinData[(int)Rank.star0].ProtectionStr = "Low";
        assassinData[(int)Rank.star0].AttackSpeedStr = "Normal";
        assassinData[(int)Rank.star0].RankCardSprite = _spritesBankScript.assassinStar0;

        assassinData[(int)Rank.star1].titleText = "Smokescreen I";
        assassinData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        assassinData[(int)Rank.star1].MaxHealth = 200;
        assassinData[(int)Rank.star1].MaxMana = 200;
        assassinData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        assassinData[(int)Rank.star1].MagicResistance = 2;
        assassinData[(int)Rank.star1].ProtectionNumber = 2;
        assassinData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        assassinData[(int)Rank.star1].ProtectionStr = "Low";
        assassinData[(int)Rank.star1].AttackSpeedStr = "Normal";
        assassinData[(int)Rank.star1].RankCardSprite = _spritesBankScript.assassinStar1;

        assassinData[(int)Rank.star2].titleText = "Smokescreen II";
        assassinData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        assassinData[(int)Rank.star2].MaxHealth = 300;
        assassinData[(int)Rank.star2].MaxMana = 300;
        assassinData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        assassinData[(int)Rank.star2].MagicResistance = 3;
        assassinData[(int)Rank.star2].ProtectionNumber = 3;
        assassinData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        assassinData[(int)Rank.star2].ProtectionStr = "Medium";
        assassinData[(int)Rank.star2].AttackSpeedStr = "Normal";
        assassinData[(int)Rank.star2].RankCardSprite = _spritesBankScript.assassinStar2;

        assassinData[(int)Rank.star3].titleText = "Smokescreen III";
        assassinData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        assassinData[(int)Rank.star3].MaxHealth = 400;
        assassinData[(int)Rank.star3].MaxMana = 400;
        assassinData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        assassinData[(int)Rank.star3].MagicResistance = 4;
        assassinData[(int)Rank.star3].ProtectionNumber = 4;
        assassinData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        assassinData[(int)Rank.star3].ProtectionStr = "Medium";
        assassinData[(int)Rank.star3].AttackSpeedStr = "Normal";
        assassinData[(int)Rank.star3].RankCardSprite = _spritesBankScript.assassinStar3;

        assassinData[(int)Rank.star4].titleText = "Smokescreen IV";
        assassinData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        assassinData[(int)Rank.star4].MaxHealth = 500;
        assassinData[(int)Rank.star4].MaxMana = 500;
        assassinData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        assassinData[(int)Rank.star4].MagicResistance = 5;
        assassinData[(int)Rank.star4].ProtectionNumber = 5;
        assassinData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        assassinData[(int)Rank.star4].ProtectionStr = "Medium";
        assassinData[(int)Rank.star4].AttackSpeedStr = "Normal";
        assassinData[(int)Rank.star4].RankCardSprite = _spritesBankScript.assassinStar4;

        unitsIDData[7].rankData[Rank.star0] = assassinData[0];
        unitsIDData[7].rankData[Rank.star1] = assassinData[1];
        unitsIDData[7].rankData[Rank.star2] = assassinData[2];
        unitsIDData[7].rankData[Rank.star3] = assassinData[3];
        unitsIDData[7].rankData[Rank.star4] = assassinData[4];

        UnitPerformance[] paladinData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            paladinData[i] = new UnitPerformance();

        paladinData[(int)Rank.star0].titleText = "Palladin";
        paladinData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        paladinData[(int)Rank.star0].MaxHealth = 100;
        paladinData[(int)Rank.star0].MaxMana = 100;
        paladinData[(int)Rank.star0].AttackSpeedNumber = 1;
        paladinData[(int)Rank.star0].MagicResistance = 1;
        paladinData[(int)Rank.star0].ProtectionNumber = 1;
        paladinData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        paladinData[(int)Rank.star0].ProtectionStr = "Low";
        paladinData[(int)Rank.star0].AttackSpeedStr = "Normal";
        paladinData[(int)Rank.star0].RankCardSprite = _spritesBankScript.paladinStar0;

        paladinData[(int)Rank.star1].titleText = "Smokescreen I";
        paladinData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        paladinData[(int)Rank.star1].MaxHealth = 200;
        paladinData[(int)Rank.star1].MaxMana = 200;
        paladinData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        paladinData[(int)Rank.star1].MagicResistance = 2;
        paladinData[(int)Rank.star1].ProtectionNumber = 2;
        paladinData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        paladinData[(int)Rank.star1].ProtectionStr = "Low";
        paladinData[(int)Rank.star1].AttackSpeedStr = "Normal";
        paladinData[(int)Rank.star1].RankCardSprite = _spritesBankScript.paladinStar1;

        paladinData[(int)Rank.star2].titleText = "Smokescreen II";
        paladinData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        paladinData[(int)Rank.star2].MaxHealth = 300;
        paladinData[(int)Rank.star2].MaxMana = 300;
        paladinData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        paladinData[(int)Rank.star2].MagicResistance = 3;
        paladinData[(int)Rank.star2].ProtectionNumber = 3;
        paladinData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        paladinData[(int)Rank.star2].ProtectionStr = "Medium";
        paladinData[(int)Rank.star2].AttackSpeedStr = "Normal";
        paladinData[(int)Rank.star2].RankCardSprite = _spritesBankScript.paladinStar2;

        paladinData[(int)Rank.star3].titleText = "Smokescreen III";
        paladinData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        paladinData[(int)Rank.star3].MaxHealth = 400;
        paladinData[(int)Rank.star3].MaxMana = 400;
        paladinData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        paladinData[(int)Rank.star3].MagicResistance = 4;
        paladinData[(int)Rank.star3].ProtectionNumber = 4;
        paladinData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        paladinData[(int)Rank.star3].ProtectionStr = "Medium";
        paladinData[(int)Rank.star3].AttackSpeedStr = "Normal";
        paladinData[(int)Rank.star3].RankCardSprite = _spritesBankScript.paladinStar3;

        paladinData[(int)Rank.star4].titleText = "Smokescreen IV";
        paladinData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        paladinData[(int)Rank.star4].MaxHealth = 500;
        paladinData[(int)Rank.star4].MaxMana = 500;
        paladinData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        paladinData[(int)Rank.star4].MagicResistance = 5;
        paladinData[(int)Rank.star4].ProtectionNumber = 5;
        paladinData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        paladinData[(int)Rank.star4].ProtectionStr = "Medium";
        paladinData[(int)Rank.star4].AttackSpeedStr = "Normal";
        paladinData[(int)Rank.star4].RankCardSprite = _spritesBankScript.paladinStar4;

        unitsIDData[8].rankData[Rank.star0] = paladinData[0];
        unitsIDData[8].rankData[Rank.star1] = paladinData[1];
        unitsIDData[8].rankData[Rank.star2] = paladinData[2];
        unitsIDData[8].rankData[Rank.star3] = paladinData[3];
        unitsIDData[8].rankData[Rank.star4] = paladinData[4];

        UnitPerformance[] sentielData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            sentielData[i] = new UnitPerformance();

        sentielData[(int)Rank.star0].titleText = "Sentiel";
        sentielData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        sentielData[(int)Rank.star0].MaxHealth = 100;
        sentielData[(int)Rank.star0].MaxMana = 100;
        sentielData[(int)Rank.star0].AttackSpeedNumber = 1;
        sentielData[(int)Rank.star0].MagicResistance = 1;
        sentielData[(int)Rank.star0].ProtectionNumber = 1;
        sentielData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        sentielData[(int)Rank.star0].ProtectionStr = "Low";
        sentielData[(int)Rank.star0].AttackSpeedStr = "Normal";
        sentielData[(int)Rank.star0].RankCardSprite = _spritesBankScript.sentielStar0;

        sentielData[(int)Rank.star1].titleText = "Smokescreen I";
        sentielData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        sentielData[(int)Rank.star1].MaxHealth = 200;
        sentielData[(int)Rank.star1].MaxMana = 200;
        sentielData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        sentielData[(int)Rank.star1].MagicResistance = 2;
        sentielData[(int)Rank.star1].ProtectionNumber = 2;
        sentielData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        sentielData[(int)Rank.star1].ProtectionStr = "Low";
        sentielData[(int)Rank.star1].AttackSpeedStr = "Normal";
        sentielData[(int)Rank.star1].RankCardSprite = _spritesBankScript.sentielStar1;

        sentielData[(int)Rank.star2].titleText = "Smokescreen II";
        sentielData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        sentielData[(int)Rank.star2].MaxHealth = 300;
        sentielData[(int)Rank.star2].MaxMana = 300;
        sentielData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        sentielData[(int)Rank.star2].MagicResistance = 3;
        sentielData[(int)Rank.star2].ProtectionNumber = 3;
        sentielData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        sentielData[(int)Rank.star2].ProtectionStr = "Medium";
        sentielData[(int)Rank.star2].AttackSpeedStr = "Normal";
        sentielData[(int)Rank.star2].RankCardSprite = _spritesBankScript.sentielStar2;

        sentielData[(int)Rank.star3].titleText = "Smokescreen III";
        sentielData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        sentielData[(int)Rank.star3].MaxHealth = 400;
        sentielData[(int)Rank.star3].MaxMana = 400;
        sentielData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        sentielData[(int)Rank.star3].MagicResistance = 4;
        sentielData[(int)Rank.star3].ProtectionNumber = 4;
        sentielData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        sentielData[(int)Rank.star3].ProtectionStr = "Medium";
        sentielData[(int)Rank.star3].AttackSpeedStr = "Normal";
        sentielData[(int)Rank.star3].RankCardSprite = _spritesBankScript.sentielStar3;

        sentielData[(int)Rank.star4].titleText = "Smokescreen IV";
        sentielData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        sentielData[(int)Rank.star4].MaxHealth = 500;
        sentielData[(int)Rank.star4].MaxMana = 500;
        sentielData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        sentielData[(int)Rank.star4].MagicResistance = 5;
        sentielData[(int)Rank.star4].ProtectionNumber = 5;
        sentielData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        sentielData[(int)Rank.star4].ProtectionStr = "Medium";
        sentielData[(int)Rank.star4].AttackSpeedStr = "Normal";
        sentielData[(int)Rank.star4].RankCardSprite = _spritesBankScript.sentielStar4;

        unitsIDData[9].rankData[Rank.star0] = sentielData[0];
        unitsIDData[9].rankData[Rank.star1] = sentielData[1];
        unitsIDData[9].rankData[Rank.star2] = sentielData[2];
        unitsIDData[9].rankData[Rank.star3] = sentielData[3];
        unitsIDData[9].rankData[Rank.star4] = sentielData[4];

        UnitPerformance[] wizardData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            wizardData[i] = new UnitPerformance();

        wizardData[(int)Rank.star0].titleText = "Wizard";
        wizardData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        wizardData[(int)Rank.star0].MaxHealth = 100;
        wizardData[(int)Rank.star0].MaxMana = 100;
        wizardData[(int)Rank.star0].AttackSpeedNumber = 1;
        wizardData[(int)Rank.star0].MagicResistance = 1;
        wizardData[(int)Rank.star0].ProtectionNumber = 1;
        wizardData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        wizardData[(int)Rank.star0].ProtectionStr = "Low";
        wizardData[(int)Rank.star0].AttackSpeedStr = "Normal";
        wizardData[(int)Rank.star0].RankCardSprite = _spritesBankScript.wizardStar0;

        wizardData[(int)Rank.star1].titleText = "Smokescreen I";
        wizardData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        wizardData[(int)Rank.star1].MaxHealth = 200;
        wizardData[(int)Rank.star1].MaxMana = 200;
        wizardData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        wizardData[(int)Rank.star1].MagicResistance = 2;
        wizardData[(int)Rank.star1].ProtectionNumber = 2;
        wizardData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        wizardData[(int)Rank.star1].ProtectionStr = "Low";
        wizardData[(int)Rank.star1].AttackSpeedStr = "Normal";
        wizardData[(int)Rank.star1].RankCardSprite = _spritesBankScript.wizardStar1;

        wizardData[(int)Rank.star2].titleText = "Smokescreen II";
        wizardData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        wizardData[(int)Rank.star2].MaxHealth = 300;
        wizardData[(int)Rank.star2].MaxMana = 300;
        wizardData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        wizardData[(int)Rank.star2].MagicResistance = 3;
        wizardData[(int)Rank.star2].ProtectionNumber = 3;
        wizardData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        wizardData[(int)Rank.star2].ProtectionStr = "Medium";
        wizardData[(int)Rank.star2].AttackSpeedStr = "Normal";
        wizardData[(int)Rank.star2].RankCardSprite = _spritesBankScript.wizardStar2;

        wizardData[(int)Rank.star3].titleText = "Smokescreen III";
        wizardData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        wizardData[(int)Rank.star3].MaxHealth = 400;
        wizardData[(int)Rank.star3].MaxMana = 400;
        wizardData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        wizardData[(int)Rank.star3].MagicResistance = 4;
        wizardData[(int)Rank.star3].ProtectionNumber = 4;
        wizardData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        wizardData[(int)Rank.star3].ProtectionStr = "Medium";
        wizardData[(int)Rank.star3].AttackSpeedStr = "Normal";
        wizardData[(int)Rank.star3].RankCardSprite = _spritesBankScript.wizardStar3;

        wizardData[(int)Rank.star4].titleText = "Smokescreen IV";
        wizardData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        wizardData[(int)Rank.star4].MaxHealth = 500;
        wizardData[(int)Rank.star4].MaxMana = 500;
        wizardData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        wizardData[(int)Rank.star4].MagicResistance = 5;
        wizardData[(int)Rank.star4].ProtectionNumber = 5;
        wizardData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        wizardData[(int)Rank.star4].ProtectionStr = "Medium";
        wizardData[(int)Rank.star4].AttackSpeedStr = "Normal";
        wizardData[(int)Rank.star4].RankCardSprite = _spritesBankScript.wizardStar4;

        unitsIDData[10].rankData[Rank.star0] = wizardData[0];
        unitsIDData[10].rankData[Rank.star1] = wizardData[1];
        unitsIDData[10].rankData[Rank.star2] = wizardData[2];
        unitsIDData[10].rankData[Rank.star3] = wizardData[3];
        unitsIDData[10].rankData[Rank.star4] = wizardData[4];

        UnitPerformance[] dancerData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            dancerData[i] = new UnitPerformance();

        dancerData[(int)Rank.star0].titleText = "Dancer";
        dancerData[(int)Rank.star0].describingText = "Rouge are all-rounders the have decent melee and ranged attacks along with usefull trinkets";
        dancerData[(int)Rank.star0].MaxHealth = 100;
        dancerData[(int)Rank.star0].MaxMana = 100;
        dancerData[(int)Rank.star0].AttackSpeedNumber = 1;
        dancerData[(int)Rank.star0].MagicResistance = 1;
        dancerData[(int)Rank.star0].ProtectionNumber = 1;
        dancerData[(int)Rank.star0].Damage = new Vector2(7f, 8f);
        dancerData[(int)Rank.star0].ProtectionStr = "Low";
        dancerData[(int)Rank.star0].AttackSpeedStr = "Normal";
        dancerData[(int)Rank.star0].RankCardSprite = _spritesBankScript.dancerStar0;

        dancerData[(int)Rank.star1].titleText = "Smokescreen I";
        dancerData[(int)Rank.star1].describingText = "Slows and damages enemies in an area";
        dancerData[(int)Rank.star1].MaxHealth = 200;
        dancerData[(int)Rank.star1].MaxMana = 200;
        dancerData[(int)Rank.star1].AttackSpeedNumber = 0.8f;
        dancerData[(int)Rank.star1].MagicResistance = 2;
        dancerData[(int)Rank.star1].ProtectionNumber = 2;
        dancerData[(int)Rank.star1].Damage = new Vector2(9f, 12f);
        dancerData[(int)Rank.star1].ProtectionStr = "Low";
        dancerData[(int)Rank.star1].AttackSpeedStr = "Normal";
        dancerData[(int)Rank.star1].RankCardSprite = _spritesBankScript.dancerStar1;

        dancerData[(int)Rank.star2].titleText = "Smokescreen II";
        dancerData[(int)Rank.star2].describingText = "Also has a chance to freeze enemies";
        dancerData[(int)Rank.star2].MaxHealth = 300;
        dancerData[(int)Rank.star2].MaxMana = 300;
        dancerData[(int)Rank.star2].AttackSpeedNumber = 0.6f;
        dancerData[(int)Rank.star2].MagicResistance = 3;
        dancerData[(int)Rank.star2].ProtectionNumber = 3;
        dancerData[(int)Rank.star2].Damage = new Vector2(14f, 19f);
        dancerData[(int)Rank.star2].ProtectionStr = "Medium";
        dancerData[(int)Rank.star2].AttackSpeedStr = "Normal";
        dancerData[(int)Rank.star2].RankCardSprite = _spritesBankScript.dancerStar2;

        dancerData[(int)Rank.star3].titleText = "Smokescreen III";
        dancerData[(int)Rank.star3].describingText = "Has a higher chance to freeze enemies";
        dancerData[(int)Rank.star3].MaxHealth = 400;
        dancerData[(int)Rank.star3].MaxMana = 400;
        dancerData[(int)Rank.star3].AttackSpeedNumber = 0.4f;
        dancerData[(int)Rank.star3].MagicResistance = 4;
        dancerData[(int)Rank.star3].ProtectionNumber = 4;
        dancerData[(int)Rank.star3].Damage = new Vector2(18f, 24f);
        dancerData[(int)Rank.star3].ProtectionStr = "Medium";
        dancerData[(int)Rank.star3].AttackSpeedStr = "Normal";
        dancerData[(int)Rank.star3].RankCardSprite = _spritesBankScript.dancerStar3;

        dancerData[(int)Rank.star4].titleText = "Smokescreen IV";
        dancerData[(int)Rank.star4].describingText = "Area size is doubles and higher damage is dealt";
        dancerData[(int)Rank.star4].MaxHealth = 500;
        dancerData[(int)Rank.star4].MaxMana = 500;
        dancerData[(int)Rank.star4].AttackSpeedNumber = 0.2f;
        dancerData[(int)Rank.star4].MagicResistance = 5;
        dancerData[(int)Rank.star4].ProtectionNumber = 5;
        dancerData[(int)Rank.star4].Damage = new Vector2(24f, 31f);
        dancerData[(int)Rank.star4].ProtectionStr = "Medium";
        dancerData[(int)Rank.star4].AttackSpeedStr = "Normal";
        dancerData[(int)Rank.star4].RankCardSprite = _spritesBankScript.dancerStar4;

        unitsIDData[11].rankData[Rank.star0] = dancerData[0];
        unitsIDData[11].rankData[Rank.star1] = dancerData[1];
        unitsIDData[11].rankData[Rank.star2] = dancerData[2];
        unitsIDData[11].rankData[Rank.star3] = dancerData[3];
        unitsIDData[11].rankData[Rank.star4] = dancerData[4];



        UnitPerformance[] heroData = new UnitPerformance[Enum.GetNames(typeof(Rank)).Length];

        for (int i = 0; i < Enum.GetNames(typeof(Rank)).Length; i++)
            heroData[i] = new UnitPerformance();

        heroData[(int)Rank.star0].MaxHealth = 250;
        heroData[(int)Rank.star0].MaxMana = 250;
        heroData[(int)Rank.star0].AttackSpeedNumber = 1f;
        heroData[(int)Rank.star0].MagicResistance = 0;
        heroData[(int)Rank.star0].ProtectionNumber = 0;
        heroData[(int)Rank.star0].ProtectionStr = "None";
        heroData[(int)Rank.star0].titleText = "Archer";
        heroData[(int)Rank.star0].describingText = "Archers swiftly deal with enemies from a distance";
        heroData[(int)Rank.star0].RankCardSprite = _spritesBankScript.archerStar0;
        heroData[(int)Rank.star0].Damage = new Vector2(15f, 25f);
        heroData[(int)Rank.star0].AttackSpeedStr = "Normal";

        unitsIDData[12].rankData[Rank.star0] = heroData[0];
        unitsIDData[12].rankData[Rank.star1] = heroData[1];
        unitsIDData[12].rankData[Rank.star2] = heroData[2];
        unitsIDData[12].rankData[Rank.star3] = heroData[3];
        unitsIDData[12].rankData[Rank.star4] = heroData[4];

        characterInfo[UnitsID.archer] = unitsIDData[0];
        characterInfo[UnitsID.knight] = unitsIDData[1];
        characterInfo[UnitsID.puppeteer] = unitsIDData[2];
        characterInfo[UnitsID.rouge] = unitsIDData[3];
        characterInfo[UnitsID.berserker] = unitsIDData[4];
        characterInfo[UnitsID.hunter] = unitsIDData[5];
        characterInfo[UnitsID.clerc] = unitsIDData[6];
        characterInfo[UnitsID.assassin] = unitsIDData[7];
        characterInfo[UnitsID.palladin] = unitsIDData[8];
        characterInfo[UnitsID.sentiel] = unitsIDData[9];
        characterInfo[UnitsID.wizard] = unitsIDData[10];
        characterInfo[UnitsID.dancer] = unitsIDData[11];
        characterInfo[UnitsID.hero] = unitsIDData[12];

        #region MANACOST
        puppeteerData[(int)Rank.star0].Manacost = 85;
        archerData[(int)Rank.star0].Manacost = 70;
        knightData[(int)Rank.star0].Manacost = 50;
        #endregion
    }
}