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
    public DialogueText displayText;

    [Header("Player Settings")]
    [Tooltip("��� ������� ������")]
    public string playerTag = "Player";

    private bool sequenceRunning = false;

    private ServiceLocator _locator => ServiceLocator.Container;
    private Localizator _localizator;
    [SerializeField] private TranslationEnum _translation;

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
            displayText.Back.SetActive(true);
            displayText.Text.text = textLines[i];
            

            yield return new WaitForSeconds(delays[i]);

            displayText.Back.SetActive(false);
        }

        sequenceRunning = false;
    }
    private void Start()
    {
        _localizator = _locator.Get<Localizator>();
        _localizator.OnLanguageChange += ChangeText;
        ChangeText();
        displayText.Back.SetActive(false);


    }
    private void OnDestroy()
    {
        _localizator.OnLanguageChange -= ChangeText;
    }

    private void ChangeText()
    {
        textLines = SplitBySemicolon(_localizator.GetLocalizetedText(_translation));
    }
    public string[] SplitBySemicolon(string input)
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
