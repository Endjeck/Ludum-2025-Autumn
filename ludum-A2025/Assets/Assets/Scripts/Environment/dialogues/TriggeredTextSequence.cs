using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

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

    private ServiceLocator _locator => ServiceLocator.Container;
    private Localizator _localizator;

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
    private void Awake()
    {
        _localizator = _locator.Get<Localizator>();
        _localizator.OnLanguageChange += ChangeText;
        ChangeText();
    }
    private void OnDisable()
    {
        _localizator.OnLanguageChange -= ChangeText;
    }

    private void ChangeText()
    {
        textLines = SplitBySemicolon(_localizator.GetLocalizetedText(TranslationEnum.COLLECTOR));
    }
    public static string[] SplitBySemicolon(string input)
    {
        if (string.IsNullOrEmpty(input))
            return new string[0]; // ������ ������ ���� ������ ������

        return input
            .Split(';')                  // ��������� �� ;
            .Select(s => s.Trim())       // ������� ������ �������
            .Where(s => !string.IsNullOrEmpty(s)) // ������� ������ ��������
            .ToArray();
    }
}