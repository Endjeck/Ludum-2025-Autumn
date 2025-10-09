using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFadeOut : MonoBehaviour
{
    [Tooltip("Изображение для изменения прозрачности")]
    public Image targetImage;

    [Tooltip("Время в секундах для полного исчезновения")]
    public float fadeDuration = 2f;

    private Coroutine fadeCoroutine;

    private void Reset()
    {
        // Попытка автоматически назначить Image, если скрипт висит на объекте с Image
        if (targetImage == null)
            targetImage = GetComponent<Image>();
    }

    /// <summary>
    /// Запускает процесс плавного исчезновения изображения
    /// </summary>
    public void StartFadeOut()
    {
        if (targetImage == null)
        {
            Debug.LogWarning("Target Image не назначено.");
            return;
        }

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        Color color = targetImage.color;
        float elapsed = 0f;
        float startAlpha = 255f / 255f; // 1f
        float endAlpha = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            color.a = alpha;
            targetImage.color = color;
            yield return null;
        }

        // Гарантируем, что альфа равна 0 в конце
        color.a = endAlpha;
        targetImage.color = color;

        fadeCoroutine = null;
    }
    private void Start()
    {
        StartFadeOut();
    }
}