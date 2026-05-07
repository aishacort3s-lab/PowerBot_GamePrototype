using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }

    [Header("Contador de mortes")]
    private int deathCount = 0;

   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            deathCount = PlayerPrefs.GetInt("DeathCount", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Morte e reinício
    public void RestartRoom()
    {
        AddDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartRoom(float delay)
    {
        StartCoroutine(RestartAfterDelay(delay));
    }

    private IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        AddDeath();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartRoomFast()
    {
        StartCoroutine(RestartFastCoroutine());
    }

    private IEnumerator RestartFastCoroutine()
    {
        AddDeath();
        var op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        op.allowSceneActivation = false;
        while (op.progress < 0.2f)
            yield return null;
        op.allowSceneActivation = true;
    }

    private void AddDeath()
    {
        deathCount++;
        PlayerPrefs.SetInt("DeathCount", deathCount);
        PlayerPrefs.Save();
        Debug.Log("Mortes: " + deathCount);

        var ui = FindObjectOfType<DeathCounterUI>();
        if (ui != null) ui.UpdateDeathCount();
    }

    public int GetDeathCount()
    {
        return deathCount;
    }

    public void NewGame()
    {
        deathCount = 0;
        PlayerPrefs.DeleteKey("DeathCount");

        var ui = FindObjectOfType<DeathCounterUI>();
        if (ui != null) ui.UpdateDeathCount();
    }
    #endregion
}



