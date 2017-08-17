using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentInfo
{
    public Sprite sprite;
    public string header;
    public string description;
}

public class AchievmentData : MonoBehaviour
{
    static public Dictionary<AchievID, AchievmentInfo> achievInfo = new Dictionary<AchievID, AchievmentInfo>(Enum.GetNames(typeof(AchievID)).Length);
    private SpritesBank spritesBankScript;

    private void Start()
    {
        spritesBankScript = GetComponent<SpritesBank>();
        InitializeData();
    }

    private void InitializeData()
    {
        AchievmentInfo shiny = new AchievmentInfo();
        AchievmentInfo starry = new AchievmentInfo();
        AchievmentInfo starrific = new AchievmentInfo();
        AchievmentInfo startacular = new AchievmentInfo();
        AchievmentInfo steppedUp = new AchievmentInfo();
        AchievmentInfo distantDreams = new AchievmentInfo();
        AchievmentInfo lionHeart = new AchievmentInfo();
        AchievmentInfo treasureHunter = new AchievmentInfo();
        AchievmentInfo snareCare = new AchievmentInfo();
        AchievmentInfo revive = new AchievmentInfo();
        AchievmentInfo cometPlumet = new AchievmentInfo();
        AchievmentInfo collector = new AchievmentInfo();

        shiny.sprite = spritesBankScript.shiny;
        shiny.header = "Shiny";
        shiny.description = "Earn a total of 8 stars.";

        starry.sprite = spritesBankScript.starry;
        starry.header = "Starry";
        starry.description = "Earn a total of 15 stars.";

        starrific.sprite = spritesBankScript.starrific;
        starrific.header = "Starrific";
        starrific.description = "Earn a total of 35 stars.";

        startacular.sprite = spritesBankScript.startacular;
        startacular.header = "Startacular";
        startacular.description = "Earn a total of 56 stars.";

        steppedUp.sprite = spritesBankScript.steppedUp;
        steppedUp.header = "SteppedUp";
        steppedUp.description = "Complete 3 challenge levels.";

        distantDreams.sprite = spritesBankScript.distantDreams;
        distantDreams.header = "Distant Dreams";
        distantDreams.description = "Complete 7 challenge levels.";

        lionHeart.sprite = spritesBankScript.lionHeart;
        lionHeart.header = "LionHeart";
        lionHeart.description = "Complete 14 challenge levels.";

        treasureHunter.sprite = spritesBankScript.treasureHunter;
        treasureHunter.header = "Treasure Hunter";
        treasureHunter.description = "Complete 25 quests.";

        snareCare.sprite = spritesBankScript.snareCare;
        snareCare.header = "Snare Care";
        snareCare.description = "Kill 30 enemies in a trap.";

        revive.sprite = spritesBankScript.revive;
        revive.header = "Revive";
        revive.description = "Revive 100 units with regen.";

        cometPlumet.sprite = spritesBankScript.cometPlumet;
        cometPlumet.header = "Comet Plumet";
        cometPlumet.description = "Kiss 1000 units with starfall.";

        collector.sprite = spritesBankScript.collector;
        collector.header = "Collector";
        collector.description = "Gather 15 units in your collection.";

        achievInfo[AchievID.shiny] = shiny;
        achievInfo[AchievID.starry] = starry;
        achievInfo[AchievID.starrific] = starrific;
        achievInfo[AchievID.startacular] = startacular;
        achievInfo[AchievID.steppedUp] = steppedUp;
        achievInfo[AchievID.distantDreams] = distantDreams;
        achievInfo[AchievID.lionHeart] = lionHeart;
        achievInfo[AchievID.treasureHunter] = treasureHunter;
        achievInfo[AchievID.snareCare] = snareCare;
        achievInfo[AchievID.revive] = revive;
        achievInfo[AchievID.cometPlumet] = cometPlumet;
        achievInfo[AchievID.collector] = collector;
    }
}