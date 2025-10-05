using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("UI Components")]
    public Text DialogueText; // Ссылка на UI Text

    [Header("Dialogue Settings")]
    public string[] dialogues; // Массив реплик персонажа
    public float delayBetweenDialogues = 2f; // Задержка перед появлением следующей реплики
    public float dialogueDisplayTime = 3f; // Время отображения реплики

    private void Start()
    {
        if (DialogueText == null)
        {
            Debug.LogError("DialogueText UI component is not assigned.");
            return;
        }

        DialogueText.text = "";
        StartCoroutine(ShowDialogues());
    }

    private IEnumerator ShowDialogues()
    {
        foreach (string dialogue in dialogues)
        {
            // Показать реплику
            DialogueText.text = dialogue;
            DialogueText.enabled = true;

            // Ждать время отображения
            yield return new WaitForSeconds(dialogueDisplayTime);

            // Скрыть реплику
            DialogueText.enabled = false;

            // Ждать задержку перед следующей репликой
            yield return new WaitForSeconds(delayBetweenDialogues);
        }

        // Очистить текст после показа всех реплик
        DialogueText.text = "";
    }
}