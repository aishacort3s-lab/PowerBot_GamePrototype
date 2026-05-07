using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance;

    [Header("Áudios")]
    public AudioSource backgroundMusic;
    public AudioSource bossMusic;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "lvl_boss")
        {
            PlayBossMusic();
        }
        else
        {
            
            PlayNormalMusic();
        }
    }

    public void PlayBossMusic()
    {
        if (bossMusic != null && !bossMusic.isPlaying)
        {
            if (backgroundMusic != null && backgroundMusic.isPlaying)
                backgroundMusic.Stop();

            bossMusic.loop = true;
            bossMusic.Play();
        }
    }

    public void PlayNormalMusic()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            if (bossMusic != null && bossMusic.isPlaying)
                bossMusic.Stop();

            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    public void StopNormalMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
            backgroundMusic.Stop();
    }
    
}
