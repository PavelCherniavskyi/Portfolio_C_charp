using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour {

    public static GameObject
        ClickSound,
        ResetUpgradeClick,
        UpgradeClick,
        CoinsAdd,
        BuyUnitDone,
        StartGame,
        Arrow1, Arrow2,
        Swordswish1, Swordswish2,
        TipPopUp, TipPopUpClick,
        PauseClick,
        ScratchClick,
        BirdUp, BirdDown,
        LevelWin, LevelLose,
        E1dead1, E1dead2, E1attack, E1aggression, E1awakening, E1hit1, E1hit2,
        E5dead, E5attack1, E5attack2, E5aggression, E5hit1, E5hit2,
        E2dead, E2attack1, E2attack2, E2aggression, E2hit1, E2hit2,
        E3dead, E3attack1, E3attack2, E3aggression, E3hit1, E3hit2,
        E4dead, E4attack1, E4attack2, E4aggression, E4hit1, E4hit2, E4hit3;

    private void Awake()
    {
        ClickSound = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/ClickSound");
        ResetUpgradeClick = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/ResetUpgradeClick");
        UpgradeClick = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/UpgradeClick");
        CoinsAdd = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/CoinsAdd");
        BuyUnitDone = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/BuyUnitDone");
        StartGame = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/StartGame");
        TipPopUp = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/TipPopUp");
        TipPopUpClick = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/TipPopUpClick");
        PauseClick = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/PauseClick");
        ScratchClick = (GameObject)Resources.Load("Prefabs/Interface/SoundsPref/ScratchClick");

        LevelWin = (GameObject)Resources.Load("Sound/Gameplay/System/Prefabs/Sound_Win");
        LevelLose = (GameObject)Resources.Load("Sound/Gameplay/System/Prefabs/Sound_Lose");

        Arrow1 = (GameObject)Resources.Load("Sound/Gameplay/Prefabs/Arrow 4");
        Arrow2 = (GameObject)Resources.Load("Sound/Gameplay/Prefabs/Arrow 2");
        Swordswish1 = (GameObject)Resources.Load("Sound/Gameplay/Prefabs/Sword Swish 1");
        Swordswish2 = (GameObject)Resources.Load("Sound/Gameplay/Prefabs/Sword Swish 2");

        BirdUp = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Bird/Prefabs/BirdUp");
        BirdDown = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Bird/Prefabs/BirdDown");

        #region Enemy
        E1dead1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E1/Prefabs/E1_dead1");
        E1dead2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E1/Prefabs/E1_dead2");
        E1attack = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E1/Prefabs/E1_attack");
        E1aggression = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E1/Prefabs/E1_aggression");
        E1awakening = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E1/Prefabs/E1_awakening");
        E1hit1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E1/Prefabs/E1_hit1");
        E1hit2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E1/Prefabs/E1_hit2");

        E5dead = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E5/Prefabs/E5_dead");
        E5attack1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E5/Prefabs/E5_attack1");
        E5attack2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E5/Prefabs/E5_attack2");
        E5hit1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E5/Prefabs/E5_hit1");
        E5hit2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E5/Prefabs/E5_hit2");
        E5aggression = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E5/Prefabs/E5_aggression");

        E2dead = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E2/Prefabs/E2_dead");
        E2attack1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E2/Prefabs/E2_attack1");
        E2attack2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E2/Prefabs/E2_attack2");
        E2hit1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E2/Prefabs/E2_hit1");
        E2hit2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E2/Prefabs/E2_hit2");
        E2aggression = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E2/Prefabs/E2_aggression");

        E3dead = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E3/Prefabs/E3_dead");
        E3attack1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E3/Prefabs/E3_attack1");
        E3attack2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E3/Prefabs/E3_attack2");
        E3hit1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E3/Prefabs/E3_hit1");
        E3hit2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E3/Prefabs/E3_hit2");
        E3aggression = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E3/Prefabs/E3_aggression");

        E4dead = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E4/Prefabs/E4_dead");
        E4attack1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E4/Prefabs/E4_attack1");
        E4attack2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E4/Prefabs/E4_attack2");
        E4hit1 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E4/Prefabs/E4_hit1");
        E4hit2 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E4/Prefabs/E4_hit2");
        E4hit3 = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E4/Prefabs/E4_hit3");
        E4aggression = (GameObject)Resources.Load("Sound/Gameplay/UnitsSound/Enemy/E4/Prefabs/E4_aggression");
        #endregion
    }

}
