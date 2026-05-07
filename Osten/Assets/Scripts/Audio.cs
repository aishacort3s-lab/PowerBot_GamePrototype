using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private static Audio instance;

    void Awake()
    {
        var audios = FindObjectsOfType<Audio>();
        if (audios.Length > 1)
        {
            for (int i = 1; i < audios.Length; i++)
            {
                Destroy(audios[i].gameObject);
            }
        }

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
