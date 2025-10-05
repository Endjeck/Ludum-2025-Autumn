using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [Header("UI Components")]
    public Text DialogueText; // ������ �� UI Text

    [Header("Dialogue Settings")]
    public string[] dialogues; // ������ ������ ���������
    public float delayBetweenDialogues = 2f; // �������� ����� ���������� ��������� �������
    public float dialogueDisplayTime = 3f; // ����� ����������� �������

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
            // �������� �������
            DialogueText.text = dialogue;
            DialogueText.enabled = true;

            // ����� ����� �����������
            yield return new WaitForSeconds(dialogueDisplayTime);

            // ������ �������
            DialogueText.enabled = false;

            // ����� �������� ����� ��������� ��������
            yield return new WaitForSeconds(delayBetweenDialogues);
        }

        // �������� ����� ����� ������ ���� ������
        DialogueText.text = "";
    }
}