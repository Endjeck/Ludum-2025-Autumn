using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChangerOnTrigger : MonoBehaviour
{
    [Tooltip("Время в секундах до смены сцены после входа в триггер")]
    public float delay = 15f;

    [Tooltip("Номер сцены для загрузки")]
    public int sceneIndex = 2;

    [Tooltip("Изображение для затемнения")]
    public Image fadeImage;

    [Tooltip("Целевая альфа прозрачность (0-255, 110 по умолчанию)")]
    [Range(0, 255)]
    public byte targetAlpha = 250;

    private Coroutine changeSceneCoroutine;

    private void Start()
    {
        if (fadeImage != null)
        {
            // Убедимся, что изображение изначально невидимо
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
            fadeImage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (changeSceneCoroutine == null)
            {
                changeSceneCoroutine = StartCoroutine(ChangeSceneWithFade());
            }
        }
    }

    private IEnumerator ChangeSceneWithFade()
    {
        if (fadeImage != null)
            fadeImage.gameObject.SetActive(true);

        float fadeDuration = delay; // Время для постепенного увеличения прозрачности
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alphaNormalized = Mathf.Clamp01(elapsed / fadeDuration);
            float alphaByte = Mathf.Lerp(0, targetAlpha, alphaNormalized);
            Color c = fadeImage.color;
            c.a = alphaByte / 255f;
            fadeImage.color = c;
            yield return null;
        }

        // Гарантируем, что альфа равна targetAlpha
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = targetAlpha / 255f;
            fadeImage.color = c;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    private void ResetFade()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
            fadeImage.gameObject.SetActive(false);
        }
    }
}