using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlideShow : MonoBehaviour
{
    public Image slideImage;
    public Sprite[] slides;
    public float slideDuration = 3f;
    public float fadeDuration = 1f;

    private CanvasGroup canvasGroup;
    private int index = 0;

    void Start()
    {
        canvasGroup = slideImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = slideImage.gameObject.AddComponent<CanvasGroup>();
        }

        StartCoroutine(PlaySlideshow());
    }

    IEnumerator PlaySlideshow()
    {
        while (index < slides.Length)
        {
            slideImage.sprite = slides[index];
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(slideDuration);
            yield return StartCoroutine(FadeOut());
            index++;
        }

        EndSlideShow();
    }

    IEnumerator FadeIn()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float t = fadeDuration;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            canvasGroup.alpha = t / fadeDuration;
            yield return null;
        }
    }

    void EndSlideShow()
    {
        
        gameObject.SetActive(false);
  
        SceneManager.LoadScene("lvl_1");
    }
}
