using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour {

    private static AudioSource _soundTrack;
    private static SoundController _soundController;
    private const float MuteLength = 1;
    private static float _muteLenghtHelper;
    private static bool _isPlaying;

    public AudioClip Story;
    public AudioClip MapPage;
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;
    public AudioClip Level4;
    public AudioClip Level5;


    private void Awake ()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_soundController == null)
        {
            _soundController = this;
            _muteLenghtHelper = MuteLength;
            _isPlaying = GameController.Music;
            SceneManager.activeSceneChanged += ChangeSceneEvent;
            _soundTrack = GetComponent<AudioSource>();
            _soundTrack.Play();
        }
            
        
        
    }
	
	public void MusicOnOff()
	{
	    if(_soundTrack == null)
            Initialize();
	    _soundTrack.volume = GameController.Music ? 1f : 0f;
        _isPlaying = GameController.Music;
    }

    public void StopCurrentSoundTrack()
    {
        _isPlaying = false;
    }

    private void Update()
    {
        if (!GameController.Music)
            return;
        if (_isPlaying)
        {
            _muteLenghtHelper += Time.deltaTime;
            if (_muteLenghtHelper <= MuteLength)
            {
                
                _soundTrack.volume = _muteLenghtHelper;
            }
            else
            {
                _muteLenghtHelper = 1;
            }
        }
        else
        {
            _muteLenghtHelper -= Time.deltaTime;
            if (_muteLenghtHelper >= 0)
            {
                _soundTrack.volume = _muteLenghtHelper;
            }
            else
            {
                _muteLenghtHelper = 0;
            }
                
        }
        
    }

    void ChangeSceneEvent(Scene previousScene, Scene newScene)
    {
        if (newScene.name == "Transition")
            _isPlaying = false;
        else
        {
            switch (newScene.name)
            {
                case "MapPage":
                    _soundTrack.clip = MapPage;
                    break;
                case "Level1":
                    _soundTrack.clip = Level1;
                    break;
                case "Story":
                    _soundTrack.clip = Story;
                    break;
            }
            _isPlaying = true;
            _soundTrack.loop = true;
            _soundTrack.Play();
        }
    }
}
