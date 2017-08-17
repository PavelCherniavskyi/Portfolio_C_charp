using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    private float startTime;
    private float endTime;

    public float duration = 3;

    public static bool isLoadByIndex;
    public static string sceneToLoad;
    public static int indexToLoad;

    private void Awake()
    {

        startTime = Time.time;
        endTime = Time.time + duration;
    }

    private void Update()
    {
        if (startTime < endTime)
        {
            startTime += Time.deltaTime;
        }
        else
        {
            LoadPrefabs();
            FadeInOut.EndScene("");
            endTime = float.MaxValue;
        }
    }

    private void LoadPrefabs()
    {
        string[] path =
        {
            "Prefabs/Units/Warrior",
            "Prefabs/Units/Ranger",
            "Prefabs/Units/Summoner",
            "Prefabs/Units/Rogue",
            "Prefabs/Units/Berserker",
            "Prefabs/Units/Hunter",
            "Prefabs/Units/Cleric",
            "Prefabs/Units/Assassin",
            "Prefabs/Units/Paladin",
            "Prefabs/Units/Dancer",
            "Prefabs/GamePlay/WinCanvas",
            "Prefabs/Interface/Defeat"
        };

        foreach (var ar in path)
        {
            Destroy(Instantiate(Resources.Load(ar) as GameObject));
        }

    }

    public static void LoadNextScene()
    {
        if (isLoadByIndex)
            SceneManager.LoadScene(indexToLoad);
        else
            SceneManager.LoadScene(sceneToLoad);
    }

}