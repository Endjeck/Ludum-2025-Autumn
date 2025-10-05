using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characteranswers : MonoBehaviour
{
    [Header("UI Components")]
    public Text CharacterAnswers; // Ссылка на UI Text

    [Header("Dialogue Settings")]
    public string[] answers; // Массив реплик персонажа
    public float delayBetweenAnswers = 2f; // Задержка перед появлением следующей реплики
    public float AnswersDisplayTime = 3f; // Время отображения реплики

    private void Start()
    {
        if (CharacterAnswers == null)
        {
            Debug.LogError("CharacterAnswers UI component is not assigned.");
            return;
        }

        CharacterAnswers.text = "";
        StartCoroutine(ShowDialogues());
    }

    private IEnumerator ShowDialogues()
    {
        foreach (string answer in answers)
        {
            // Показать реплику
            CharacterAnswers.text = answer;
            CharacterAnswers.enabled = true;

            // Ждать время отображения
            yield return new WaitForSeconds(AnswersDisplayTime);

            // Скрыть реплику
            CharacterAnswers.enabled = false;

            // Ждать задержку перед следующей репликой
            yield return new WaitForSeconds(delayBetweenAnswers);
        }

        // Очистить текст после показа всех реплик
        CharacterAnswers.text = "";
    }
}
