using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class TriggeredTextSequence : MonoBehaviour
{
    [Header("Text Sequence Settings")]
    [Tooltip("������ �����, ������� ����� ������������ �� �������")]
    public string[] textLines;

    [Tooltip("�������� (� ��������) ��� ������ ������. ����� ������� ������ ��������� � textLines")]
    public float[] delays;

    [Header("UI Settings")]
    [Tooltip("������ �� TextMeshProUGUI ��� ����������� ������")]
    public TextMeshProUGUI displayText;

    [Header("Player Settings")]
    [Tooltip("��� ������� ������")]
    public string playerTag = "Player";

    private bool sequenceRunning = false;

    private void Reset()
    {
        // �������� ��������� ��� ������� �������������, ���� ��� ���
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (sequenceRunning)
            return;

        if (other.CompareTag(playerTag))
        {
            if (textLines.Length == 0 || delays.Length != textLines.Length || displayText == null)
            {
                Debug.LogWarning($"[{gameObject.name}] ������������ ��������� ������� textLines ��� delays, ���� �� �������� displayText.");
                return;
            }

            StartCoroutine(ShowTextSequence());
        }
    }

    private IEnumerator ShowTextSequence()
    {
        sequenceRunning = true;

        for (int i = 0; i < textLines.Length; i++)
        {
            displayText.text = textLines[i];
            displayText.gameObject.SetActive(true);

            yield return new WaitForSeconds(delays[i]);

            displayText.gameObject.SetActive(false);
        }

        sequenceRunning = false;
    }
}