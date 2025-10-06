using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChangerOnTrigger : MonoBehaviour
{
    [Tooltip("����� � �������� �� ����� ����� ����� ����� � �������")]
    public float delay = 15f;

    [Tooltip("����� ����� ��� ��������")]
    public int sceneIndex = 2;

    [Tooltip("����������� ��� ����������")]
    public Image fadeImage;

    [Tooltip("������� ����� ������������ (0-255, 110 �� ���������)")]
    [Range(0, 255)]
    public byte targetAlpha = 250;

    private Coroutine changeSceneCoroutine;

    private void Start()
    {
        if (fadeImage != null)
        {
            // ��������, ��� ����������� ���������� ��������
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

        float fadeDuration = delay; // ����� ��� ������������ ���������� ������������
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

        // �����������, ��� ����� ����� targetAlpha
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