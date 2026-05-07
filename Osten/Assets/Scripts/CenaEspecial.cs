using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenaEspecial : MonoBehaviour
{

    [Header("Música da Cena Especial")]
    public AudioSource specialMusic;

    [Header("Som de Porta Abrindo (SFX)")]
    public AudioClip doorSound;
    public float doorSoundDelay = 1f;

    [Header("Próxima cena (opcional)")]
    public string nextSceneName = "lvl_1";
    public float delayToNextScene = 2f;

    void Start()
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.StopNormalMusic();

        if (specialMusic != null)
        {
            specialMusic.loop = true;
            specialMusic.Play();
        }
    }

    public void PlayDoorAndEndScene()
    {
        StartCoroutine(PlayDoorAndTransition());
    }

    private System.Collections.IEnumerator PlayDoorAndTransition()
    {
        yield return new WaitForSeconds(doorSoundDelay);

        AudioSource.PlayClipAtPoint(doorSound, Camera.main.transform.position, 1f);

        if (string.IsNullOrEmpty(nextSceneName))
            yield break;

        yield return new WaitForSeconds(delayToNextScene);

        SceneManager.LoadScene(nextSceneName);
    }
}

