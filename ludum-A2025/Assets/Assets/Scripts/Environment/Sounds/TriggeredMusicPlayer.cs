using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class TriggeredMusicPlayer : MonoBehaviour
{
    [Header("Audio Clips")]
    [Tooltip("Массив аудиоклипов, которые будут проигрываться по порядку")]
    public AudioClip[] audioClips;

    [Header("Player Settings")]
    [Tooltip("Тег объекта игрока")]
    public string playerTag = "Player";

    [Header("Fade Settings")]
    [Tooltip("Время в секундах для плавного увеличения громкости")]
    public float fadeInDuration = 5f;

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool isPlaying = false;
    private Coroutine fadeCoroutine;

    private void Reset()
    {
        // Автоматически включаем Is Trigger для коллайдера
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = 0f; // Начинаем с 0 громкости
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaying)
            return;

        if (other.CompareTag(playerTag))
        {
            if (audioClips == null || audioClips.Length == 0)
            {
                Debug.LogWarning($"[{gameObject.name}] Массив аудиоклипов пуст.");
                return;
            }

            currentClipIndex = 0;
            PlayCurrentClip();
        }
    }

    private void PlayCurrentClip()
    {
        if (currentClipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.volume = 0f;

            audioSource.Play();

            // Запускаем корутину плавного увеличения громкости
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeInVolume(fadeInDuration));

            isPlaying = true;
            Invoke(nameof(OnClipFinished), audioSource.clip.length);
        }
        else
        {
            // Все клипы проиграны
            isPlaying = false;
            audioSource.Stop();
        }
    }

    private IEnumerator FadeInVolume(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        audioSource.volume = 1f; // Максимальная громкость
    }

    private void OnClipFinished()
    {
        currentClipIndex++;
        if (currentClipIndex < audioClips.Length)
        {
            PlayCurrentClip();
        }
        else
        {
            isPlaying = false;
            audioSource.Stop();
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                fadeCoroutine = null;
            }
        }
    }
}