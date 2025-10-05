using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

[RequireComponent(typeof(Collider))]
public class TriggeredTextSequence : MonoBehaviour
{
    [Header("Text Sequence Settings")]
    [Tooltip("Массив строк, которые будут показываться по очереди")]
    public string[] textLines;

    [Tooltip("Задержка (в секундах) для каждой строки. Длина массива должна совпадать с textLines")]
    public float[] delays;

    [Header("UI Settings")]
    [Tooltip("Ссылка на TextMeshProUGUI для отображения текста")]
    public TextMeshProUGUI displayText;

    [Header("Player Settings")]
    [Tooltip("Тег объекта игрока")]
    public string playerTag = "Player";

    private bool sequenceRunning = false;

    private ServiceLocator _locator => ServiceLocator.Container;
    private Localizator _localizator;

    private void Reset()
    {
        // Настроим коллайдер как триггер автоматически, если его нет
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
                Debug.LogWarning($"[{gameObject.name}] Неправильная настройка массива textLines или delays, либо не назначен displayText.");
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
            return new string[0]; // пустой массив если строка пустая

        return input
            .Split(';')                  // разбиваем по ;
            .Select(s => s.Trim())       // убираем лишние пробелы
            .Where(s => !string.IsNullOrEmpty(s)) // убираем пустые элементы
            .ToArray();
    }
}