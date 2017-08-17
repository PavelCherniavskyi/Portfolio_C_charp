using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySoundHelper : MonoBehaviour
{

    public void SoundAttack(GameObject obj, int AttackIndex = 0)
    {
        UnitOptions UO = obj.GetComponent<UnitOptions>();
        if (UO.IsEnemy)
        {
            if (UO.EnemyType == EnemiesID.shpinatic || UO.EnemyType == EnemiesID.potsik)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E1attack), 2f);
            }
            else if (UO.EnemyType == EnemiesID.bearBoss)
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        StartCoroutine(Play(SoundBank.E5attack1, 0.3f, 2f));
                        break;
                    case 2:
                        StartCoroutine(Play(SoundBank.E5attack2, 0.3f, 2f));
                        break;
                }
            }
            else if (UO.EnemyType == EnemiesID.doggie)
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        StartCoroutine(Play(SoundBank.E2attack1, 0.1f, 2f));
                        break;
                    case 2:
                        StartCoroutine(Play(SoundBank.E2attack2, 0.1f, 2f));
                        break;
                }
            }
            else if (UO.EnemyType == EnemiesID.littleWizard)
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        StartCoroutine(Play(SoundBank.E4attack1, 0.1f, 2f));
                        break;
                    case 2:
                        StartCoroutine(Play(SoundBank.E4attack2, 0.1f, 2f));
                        break;
                }
            }
            else if (UO.EnemyType == EnemiesID.venerina)
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        StartCoroutine(Play(SoundBank.E3attack1, 0.1f, 2f));
                        break;
                    case 2:
                        StartCoroutine(Play(SoundBank.E3attack2, 0.1f, 2f));
                        break;
                }
            }
        }
        else
        {
            if (UO.UnitsType == UnitsID.knight)
            {
                if (AttackIndex == 0)
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            StartCoroutine(Play(SoundBank.Swordswish1, 0.3f, 1f));
                            break;
                        case 2:
                            StartCoroutine(Play(SoundBank.Swordswish2, 0.3f, 1f));
                            break;
                    }
                }
                else
                {
                    Debug.Log("Звук удара щитом");
                }
                
            }
        }
    }

    public void SoundAwakening(GameObject obj)
    {
        UnitOptions UO = obj.GetComponent<UnitOptions>();
        if (UO.EnemyType == EnemiesID.shpinatic)
        {
            if (GameController.Sound) Destroy(Instantiate(SoundBank.E1awakening), 3f);
        }
    }

    public void SoundDead(GameObject obj)
    {
        UnitOptions UO = obj.GetComponent<UnitOptions>();
        if (UO.IsEnemy)
        {
            if (UO.EnemyType == EnemiesID.shpinatic || UO.EnemyType == EnemiesID.potsik)
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        StartCoroutine(Play(SoundBank.E1dead1, 0.3f, 3f));
                        break;
                    case 2:
                        StartCoroutine(Play(SoundBank.E1dead2, 0.3f, 3f));
                        break;
                }
            }
            else if (UO.EnemyType == EnemiesID.bearBoss)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E5dead), 3f);
            }
            else if (UO.EnemyType == EnemiesID.doggie)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E2dead), 3f);
            }
            else if (UO.EnemyType == EnemiesID.littleWizard)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E4dead), 3f);
            }
            else if (UO.EnemyType == EnemiesID.venerina)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E3dead), 3f);
            }
        }

    }

    public void SoundAggression(GameObject obj)
    {
        UnitOptions UO = obj.GetComponent<UnitOptions>();
        if (UO.IsEnemy)
        {
            if (UO.EnemyType == EnemiesID.shpinatic || UO.EnemyType == EnemiesID.potsik)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E1aggression), 2f);
            }
            else if (UO.EnemyType == EnemiesID.bearBoss)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E5aggression), 2f);
            }
            else if (UO.EnemyType == EnemiesID.doggie)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E2aggression), 2f);
            }
            else if (UO.EnemyType == EnemiesID.littleWizard)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E4aggression), 2f);
            }
            else if (UO.EnemyType == EnemiesID.venerina)
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.E3aggression), 2f);
            }
        }
        else
        {

        }
    }

    public void SoundStart(GameObject obj, string identifier = "")
    {
        if (identifier == "arrow")
        {
            if (GameController.Sound) Destroy(Instantiate(SoundBank.Arrow1), 1f);
        }
    }

    public void SoundHit(GameObject obj, string identifier = "")
    {
        try
        {
            if (obj.GetComponent<UnitOptions>().HitEffectSleeping > 0 || obj == null) return;
            obj.GetComponent<UnitOptions>().HitEffectSleeping = 2f;
            if (identifier == "arrow")
            {
                if (GameController.Sound) Destroy(Instantiate(SoundBank.Arrow2), 1f);
            }
            else if (obj.GetComponent<UnitOptions>().isDead == false && obj != null)
            {
                #region
                UnitOptions UO = obj.GetComponent<UnitOptions>();
                if (UO.EnemyType == EnemiesID.bearBoss)
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            StartCoroutine(Play(SoundBank.E5hit1, 0.3f, 2f));
                            break;
                        case 2:
                            StartCoroutine(Play(SoundBank.E5hit2, 0.3f, 2f));
                            break;
                    }
                }
                else if (UO.EnemyType == EnemiesID.doggie)
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            StartCoroutine(Play(SoundBank.E2hit1, 0.3f, 2f));
                            break;
                        case 2:
                            StartCoroutine(Play(SoundBank.E2hit2, 0.3f, 2f));
                            break;
                    }
                }
                else if (UO.EnemyType == EnemiesID.littleWizard)
                {
                    switch (Random.Range(1, 4))
                    {
                        case 1:
                            StartCoroutine(Play(SoundBank.E4hit1, 0.3f, 2f));
                            break;
                        case 2:
                            StartCoroutine(Play(SoundBank.E4hit2, 0.3f, 2f));
                            break;
                        case 3:
                            StartCoroutine(Play(SoundBank.E4hit3, 0.3f, 2f));
                            break;
                    }
                }
                else if (UO.EnemyType == EnemiesID.venerina)
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            StartCoroutine(Play(SoundBank.E3hit1, 0.3f, 2f));
                            break;
                        case 2:
                            StartCoroutine(Play(SoundBank.E3hit2, 0.3f, 2f));
                            break;
                    }
                }
                else if (UO.EnemyType == EnemiesID.shpinatic || UO.EnemyType == EnemiesID.potsik)
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                            StartCoroutine(Play(SoundBank.E1hit1, 0.3f, 2f));
                            break;
                        case 2:
                            StartCoroutine(Play(SoundBank.E1hit2, 0.3f, 2f));
                            break;
                    }
                }
                #endregion
            }
        }
        catch
        {
            Debug.Log("Объект уже удалён");
        }

    }

    private IEnumerator Play(GameObject sound, float ping, float time)
    {
        yield return new WaitForSeconds(ping);
        if (GameController.Sound) Destroy(Instantiate(sound), time);
    }

    public void SoundBird(int index)
    {
        if (index == 1)
        {
            if (GameController.Sound)
                Destroy(Instantiate(SoundBank.BirdUp), 2f);
        }
        else if (index == 2)
        {
            if (GameController.Sound)
                Destroy(Instantiate(SoundBank.BirdDown), 2f);
        }
    }
}
