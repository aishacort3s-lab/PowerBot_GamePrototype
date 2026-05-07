using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.PlayBossMusic();
                Debug.Log("[BossRoom] Tocando música do boss");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (MusicManager.Instance != null)
            {
                MusicManager.Instance.PlayNormalMusic();
                Debug.Log("[BossRoom] Voltando pra música normal");
            }
        }
    }
}
