using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private static float fadeSpeed = 10f;
    private static float fadeSpeedCorrection;
    private Image _fadeInOutImage;
    private static bool _start;
    private static bool _end;
    private static string nextSceneToLoad;
    private static int _index;
    private static bool _isLoadByIndex;

    private void Awake()
    {
        _fadeInOutImage = transform.GetComponentInChildren<Image>();
        StartScene();
    }

    private void Update()
    {
        if (_start)
            FadeIn();
        if (_end)
            FadeOut();
    }

    private void FadeIn()
    {
        if (_fadeInOutImage.color.a > 0.01f)
        {
            _fadeInOutImage.color = Color.Lerp(_fadeInOutImage.color, Color.clear, (fadeSpeed - fadeSpeedCorrection) * Time.deltaTime);
        }
        else
        {
            _start = false;
            _fadeInOutImage.color = Color.clear;
            Cursor.visible = true;
        }
    }

    private void FadeOut()
    {
        if (_fadeInOutImage.color.a < 0.99f)
            _fadeInOutImage.color = Color.Lerp(_fadeInOutImage.color, Color.black, fadeSpeed * Time.deltaTime);
        else
        {
            _end = false;
            _fadeInOutImage.color = Color.black;
            if (_isLoadByIndex)
                GoToTransition(_index, "", true);
            else
                GoToTransition(0, nextSceneToLoad, false);
        }
    }

    public static void StartScene()
    {
        _start = true;
        Cursor.visible = false;

        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "Level1" || activeScene == "Level2" || activeScene == "Level3" || activeScene == "Level4" || activeScene == "Level5")
            fadeSpeedCorrection = 8f;
        else
            fadeSpeedCorrection = 0f;
    }

    public static void EndScene(string NextSceneToLoad)
    {
        _isLoadByIndex = false;
        nextSceneToLoad = NextSceneToLoad;
        _end = true;
        Cursor.visible = false;
        
    }

    public static void EndScene(int Index)
    {
        _isLoadByIndex = true;
        _index = Index;
        _end = true;
        Cursor.visible = false;
        
    }

    private void GoToTransition(int Index, string SceneToLoad, bool IsLoadByIndex)
    {
        if (SceneManager.GetActiveScene().name == "Transition")
        {
            Transition.LoadNextScene();
        }
            
        else
        {
            Transition.isLoadByIndex = IsLoadByIndex;
            Transition.indexToLoad = Index;
            Transition.sceneToLoad = SceneToLoad;
            SceneManager.LoadScene("Transition");
        }
    }
}