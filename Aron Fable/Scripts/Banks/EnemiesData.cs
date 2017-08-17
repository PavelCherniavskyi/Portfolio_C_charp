using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesInfo
{
    public string titleText;
    public Sprite titleTextImage;
    public string describingText;
    public Sprite cardSprite;
	// Заполнить во всех тварях, когда будет понятно кто есть кто и у кого какие особенности. (Подгрузить из SpritesBank)
	public Sprite hPSprite;
	public Sprite attackSprite;
	public Sprite speedAttackSprite;
	public Sprite armorSprite;
	public Sprite smallIcon;
	//------------------------------------
    public string hp;
    public string armor;
    public Vector2 attackPower;
    public string speed;
}

public class EnemiesData : MonoBehaviour
{
    public static Dictionary<EnemiesID, EnemiesInfo> enemiesInfo = new Dictionary<EnemiesID, EnemiesInfo>(Enum.GetNames(typeof(EnemiesID)).Length);
    private SpritesBank spritesBankScript;

    private void Start()
    {
        spritesBankScript = GetComponent<SpritesBank>();
        InitializeEnemiesData();
    }

    private void InitializeEnemiesData()
    {
        EnemiesInfo dardi = new EnemiesInfo();
        EnemiesInfo shpinatic = new EnemiesInfo();
        EnemiesInfo bullet = new EnemiesInfo();
        EnemiesInfo izard = new EnemiesInfo();
        EnemiesInfo lantir = new EnemiesInfo();
        EnemiesInfo ursus = new EnemiesInfo();
        EnemiesInfo butterfly = new EnemiesInfo();
        EnemiesInfo banditChief = new EnemiesInfo();
        EnemiesInfo hornet = new EnemiesInfo();
        EnemiesInfo stinger = new EnemiesInfo();
        EnemiesInfo goo = new EnemiesInfo();
        EnemiesInfo smallGoo = new EnemiesInfo();
        EnemiesInfo murkee = new EnemiesInfo();
        EnemiesInfo darkWalker = new EnemiesInfo();
        EnemiesInfo phantomNear = new EnemiesInfo();
        EnemiesInfo wildTreant = new EnemiesInfo();
        EnemiesInfo prongle = new EnemiesInfo();
        EnemiesInfo cursedBat = new EnemiesInfo();
        EnemiesInfo clown = new EnemiesInfo();
        EnemiesInfo loonatic = new EnemiesInfo();

		dardi.hPSprite = spritesBankScript.Mini_heart;
		dardi.attackSprite = spritesBankScript.Mini_sword1;
		dardi.speedAttackSprite = spritesBankScript.Mini_sword2;
		dardi.armorSprite = spritesBankScript.Mini_shield2;
		dardi.cardSprite = spritesBankScript.potsik;
        dardi.titleTextImage = spritesBankScript.DardiName;
        dardi.titleText = "Dardi";
        dardi.describingText = "";
        dardi.armor = "None";
        dardi.hp = "120";
        dardi.attackPower = new Vector2(6f, 8f);
        dardi.speed = "Normal";


        shpinatic.hPSprite = spritesBankScript.Mini_heart;
        shpinatic.attackSprite = spritesBankScript.Mini_sword1;
        shpinatic.speedAttackSprite = spritesBankScript.Mini_sword2;
        shpinatic.armorSprite = spritesBankScript.Mini_shield2;
        shpinatic.titleText = "Shpinatick";
        shpinatic.describingText = "";
        shpinatic.cardSprite = spritesBankScript.shpinatic;
        shpinatic.titleTextImage = spritesBankScript.ShpinatickName;
        shpinatic.armor = "None";
        shpinatic.hp = "140";
        shpinatic.attackPower = new Vector2(8f, 10f);
        shpinatic.speed = "Normal";

        bullet.hPSprite = spritesBankScript.Mini_heart;
        bullet.attackSprite = spritesBankScript.Mini_sword1;
        bullet.speedAttackSprite = spritesBankScript.Mini_sword2;
        bullet.armorSprite = spritesBankScript.Mini_shield2;
        bullet.titleText = "Bullet";
        bullet.describingText = "";
        bullet.cardSprite = spritesBankScript.doggie;
        bullet.titleTextImage = spritesBankScript.BulletName;
        bullet.armor = "Medium";
        bullet.hp = "180";
        bullet.attackPower = new Vector2(12f, 18f);
        bullet.speed = "Fast";

        izard.hPSprite = spritesBankScript.Mini_heart;
        izard.attackSprite = spritesBankScript.Mini_staff1;
        izard.speedAttackSprite = spritesBankScript.Mini_staff2;
        izard.armorSprite = spritesBankScript.Mini_shield2;
        izard.titleText = "Izard";
        izard.describingText = "";
        izard.cardSprite = spritesBankScript.littleWizard;
        izard.titleTextImage = spritesBankScript.IzardName;
        izard.armor = "None";
        izard.hp = "200";
        izard.attackPower = new Vector2(16f, 20f);
        izard.speed = "Fast";

        lantir.hPSprite = spritesBankScript.Mini_heart;
        lantir.attackSprite = spritesBankScript.Mini_sword1;
        lantir.speedAttackSprite = spritesBankScript.Mini_sword2;
        lantir.armorSprite = spritesBankScript.Mini_shield2;
        lantir.titleText = "Lantir";
        lantir.describingText = "";
        lantir.cardSprite = spritesBankScript.venerina;
        lantir.titleTextImage = spritesBankScript.LantirName;
        lantir.armor = "None";
        lantir.hp = "150";
        lantir.attackPower = new Vector2(20f, 25f);
        lantir.speed = "Normal";

        ursus.hPSprite = spritesBankScript.Mini_heart;
        ursus.attackSprite = spritesBankScript.Mini_sword1;
        ursus.speedAttackSprite = spritesBankScript.Mini_sword2;
        ursus.armorSprite = spritesBankScript.Mini_shield2;
        ursus.titleText = "Ursus";
        ursus.describingText = "";
        ursus.cardSprite = spritesBankScript.bearBoss;
        ursus.titleTextImage = spritesBankScript.UrsusName;
        ursus.armor = "Medium";
        ursus.hp = "500";
        ursus.attackPower = new Vector2(30f, 40f);
        ursus.speed = "Normal";

        butterfly.titleText = "Butterfly";
        butterfly.describingText = "Text for example text for example and some text here";
        butterfly.cardSprite = spritesBankScript.butterfly;
        butterfly.armor = "None";
        butterfly.hp = "120";
        butterfly.attackPower = new Vector2(6f, 8f);
        butterfly.speed = "Normal";

        banditChief.titleText = "BanditChief";
        banditChief.describingText = "Text for example text for example and some text here";
        banditChief.cardSprite = spritesBankScript.banditChief;
        banditChief.armor = "None";
        banditChief.hp = "120";
        banditChief.attackPower = new Vector2(6f, 8f);
        banditChief.speed = "Normal";

        hornet.titleText = "Hornet";
        hornet.describingText = "Text for example text for example and some text here";
        hornet.cardSprite = spritesBankScript.hornet;
        hornet.armor = "None";
        hornet.hp = "120";
        hornet.attackPower = new Vector2(6f, 8f);
        hornet.speed = "Normal";

        stinger.titleText = "Stinger";
        stinger.describingText = "Text for example text for example and some text here";
        stinger.cardSprite = spritesBankScript.stinger;
        stinger.armor = "None";
        stinger.hp = "120";
        stinger.attackPower = new Vector2(6f, 8f);
        stinger.speed = "Normal";

        goo.titleText = "Goo";
        goo.describingText = "Text for example text for example and some text here";
        goo.cardSprite = spritesBankScript.goo;
        goo.armor = "None";
        goo.hp = "120";
        goo.attackPower = new Vector2(6f, 8f);
        goo.speed = "Normal";

        smallGoo.titleText = "Small Goo";
        smallGoo.describingText = "Text for example text for example and some text here";
        smallGoo.cardSprite = spritesBankScript.smallGoo;
        smallGoo.armor = "None";
        smallGoo.hp = "120";
        smallGoo.attackPower = new Vector2(6f, 8f);
        smallGoo.speed = "Normal";

        murkee.titleText = "Murkee";
        murkee.describingText = "Text for example text for example and some text here";
        murkee.cardSprite = spritesBankScript.murkee;
        murkee.armor = "None";
        murkee.hp = "120";
        murkee.attackPower = new Vector2(6f, 8f);
        murkee.speed = "Normal";

        darkWalker.titleText = "DarkWalker";
        darkWalker.describingText = "Text for example text for example and some text here";
        darkWalker.cardSprite = spritesBankScript.darkWalker;
        darkWalker.armor = "None";
        darkWalker.hp = "120";
        darkWalker.attackPower = new Vector2(6f, 8f);
        darkWalker.speed = "Normal";

        phantomNear.titleText = "PhantomNear";
        phantomNear.describingText = "Text for example text for example and some text here";
        phantomNear.cardSprite = spritesBankScript.phantomNear;
        phantomNear.armor = "None";
        phantomNear.hp = "120";
        phantomNear.attackPower = new Vector2(6f, 8f);
        phantomNear.speed = "Normal";

        wildTreant.titleText = "WildTreant";
        wildTreant.describingText = "Text for example text for example and some text here";
        wildTreant.cardSprite = spritesBankScript.wildTreant;
        wildTreant.armor = "None";
        wildTreant.hp = "120";
        wildTreant.attackPower = new Vector2(6f, 8f);
        wildTreant.speed = "Normal";

        prongle.titleText = "Prongle";
        prongle.describingText = "Text for example text for example and some text here";
        prongle.cardSprite = spritesBankScript.prongle;
        prongle.armor = "None";
        prongle.hp = "120";
        prongle.attackPower = new Vector2(6f, 8f);
        prongle.speed = "Normal";

        cursedBat.titleText = "CursedBat";
        cursedBat.describingText = "Text for example text for example and some text here";
        cursedBat.cardSprite = spritesBankScript.cursedBat;
        cursedBat.armor = "None";
        cursedBat.hp = "120";
        cursedBat.attackPower = new Vector2(6f, 8f);
        cursedBat.speed = "Normal";

        clown.titleText = "Clown";
        clown.describingText = "Text for example text for example and some text here";
        clown.cardSprite = spritesBankScript.clown;
        clown.armor = "None";
        clown.hp = "120";
        clown.attackPower = new Vector2(6f, 8f);
        clown.speed = "Normal";

        loonatic.titleText = "Loonatic";
        loonatic.describingText = "Text for example text for example and some text here";
        loonatic.cardSprite = spritesBankScript.loonatic;
        loonatic.armor = "None";
        loonatic.hp = "120";
        loonatic.attackPower = new Vector2(6f, 8f);
        loonatic.speed = "Normal";

        enemiesInfo[EnemiesID.potsik] = dardi;
        enemiesInfo[EnemiesID.shpinatic] = shpinatic;
        enemiesInfo[EnemiesID.doggie] = bullet;
        enemiesInfo[EnemiesID.littleWizard] = izard;
        enemiesInfo[EnemiesID.venerina] = lantir;
        enemiesInfo[EnemiesID.bearBoss] = ursus;
        enemiesInfo[EnemiesID.butterfly] = butterfly;
        enemiesInfo[EnemiesID.banditChief] = banditChief;
        enemiesInfo[EnemiesID.hornet] = hornet;
        enemiesInfo[EnemiesID.stinger] = stinger;
        enemiesInfo[EnemiesID.goo] = goo;
        enemiesInfo[EnemiesID.smallGoo] = smallGoo;
        enemiesInfo[EnemiesID.murkee] = murkee;
        enemiesInfo[EnemiesID.darkWalker] = darkWalker;
        enemiesInfo[EnemiesID.phantomNear] = phantomNear;
        enemiesInfo[EnemiesID.wildTreant] = wildTreant;
        enemiesInfo[EnemiesID.prongle] = prongle;
        enemiesInfo[EnemiesID.cursedBat] = cursedBat;
        enemiesInfo[EnemiesID.clown] = clown;
        enemiesInfo[EnemiesID.loonatic] = loonatic;
    }
}