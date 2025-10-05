using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characteranswers : MonoBehaviour
{
    [Header("UI Components")]
    public Text CharacterAnswers; // ������ �� UI Text

    [Header("Dialogue Settings")]
    public string[] answers; // ������ ������ ���������
    public float delayBetweenAnswers = 2f; // �������� ����� ���������� ��������� �������
    public float AnswersDisplayTime = 3f; // ����� ����������� �������

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
            // �������� �������
            CharacterAnswers.text = answer;
            CharacterAnswers.enabled = true;

            // ����� ����� �����������
            yield return new WaitForSeconds(AnswersDisplayTime);

            // ������ �������
            CharacterAnswers.enabled = false;

            // ����� �������� ����� ��������� ��������
            yield return new WaitForSeconds(delayBetweenAnswers);
        }

        // �������� ����� ����� ������ ���� ������
        CharacterAnswers.text = "";
    }
}
