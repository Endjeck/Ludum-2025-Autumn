using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageFadeOut : MonoBehaviour
{
    [Tooltip("����������� ��� ��������� ������������")]
    public Image targetImage;

    [Tooltip("����� � �������� ��� ������� ������������")]
    public float fadeDuration = 2f;

    private Coroutine fadeCoroutine;

    private void Reset()
    {
        // ������� ������������� ��������� Image, ���� ������ ����� �� ������� � Image
        if (targetImage == null)
            targetImage = GetComponent<Image>();
    }

    /// <summary>
    /// ��������� ������� �������� ������������ �����������
    /// </summary>
    public void StartFadeOut()
    {
        if (targetImage == null)
        {
            Debug.LogWarning("Target Image �� ���������.");
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

        // �����������, ��� ����� ����� 0 � �����
        color.a = endAlpha;
        targetImage.color = color;

        fadeCoroutine = null;
    }
    private void Start()
    {
        StartFadeOut();
    }
}