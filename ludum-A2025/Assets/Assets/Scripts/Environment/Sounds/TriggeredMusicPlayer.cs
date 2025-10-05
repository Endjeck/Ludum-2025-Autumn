using UnityEngine;

[RequireComponent(typeof(Collider), typeof(AudioSource))]
public class TriggeredMusicPlayer : MonoBehaviour
{
    [Header("Audio Clips")]
    [Tooltip("������ �����������, ������� ����� ������������� �� �������")]
    public AudioClip[] audioClips;

    [Header("Player Settings")]
    [Tooltip("��� ������� ������")]
    public string playerTag = "Player";

    private AudioSource audioSource;
    private int currentClipIndex = 0;
    private bool isPlaying = false;

    private void Reset()
    {
        // ������������� �������� Is Trigger ��� ����������
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaying)
            return;

        if (other.CompareTag(playerTag))
        {
            if (audioClips == null || audioClips.Length == 0)
            {
                Debug.LogWarning($"[{gameObject.name}] ������ ����������� ����.");
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
            audioSource.Play();
            isPlaying = true;
            Invoke(nameof(OnClipFinished), audioSource.clip.length);
        }
        else
        {
            // ��� ����� ���������
            isPlaying = false;
            audioSource.Stop();
        }
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
        }
    }

    

    private void StopMusic()
    {
        isPlaying = false;
        audioSource.Stop();
        CancelInvoke(nameof(OnClipFinished));
    }
}