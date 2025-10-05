using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InteractiveObjectHighlighter : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("��� ������������� ��������")]
    public string interactiveTag = "Interactive";

    [Tooltip("���� ��������� ��� ���������")]
    public Color highlightColor = Color.yellow;

    [Tooltip("UI TextMeshProUGUI ��� ����������� ���������� �� �������")]
    public TextMeshProUGUI infoText;

    private Camera mainCamera;
    private GameObject currentObject;            // ������, �� ������� ������ ������� ������
    private Renderer currentRenderer;
    private Color originalColor;
    private bool isPromptVisible = false;

    private void Start()
    {
        mainCamera = Camera.main;
        infoText.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckObjectUnderCursor();

        if (isPromptVisible && Input.GetKeyDown(KeyCode.E))
        {
            ShowInfoText();
        }
    }

    private void CheckObjectUnderCursor()
    {
        // ���� ��������� ��� UI ���������, �� ������������ ������� �����
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            ClearCurrentObject();
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag(interactiveTag))
            {
                if (currentObject != hitObj)
                {
                    ClearCurrentObject();
                    SetCurrentObject(hitObj);
                }
                return;
            }
        }

        ClearCurrentObject();
    }

    private void SetCurrentObject(GameObject obj)
    {
        currentObject = obj;
        currentRenderer = obj.GetComponent<Renderer>();

        if (currentRenderer != null)
        {
            originalColor = currentRenderer.material.color;
            currentRenderer.material.color = highlightColor;
        }
        Debug.Log(infoText);
        infoText.gameObject.SetActive(true);
        infoText.text = "������� �� �";
        isPromptVisible = true;
    }

    private void ClearCurrentObject()
    {
        if (currentObject != null && currentRenderer != null)
        {
            currentRenderer.material.color = originalColor;
        }

        currentObject = null;
        currentRenderer = null;

        infoText.gameObject.SetActive(false);
        isPromptVisible = false;
    }

    private void ShowInfoText()
    {
       
        if (currentObject == null) return;

        // �������� ��������� � ��������� �������, ���� ����
        ObjectDescription description = currentObject.GetComponent<ObjectDescription>();
        if (description != null && !string.IsNullOrEmpty(description.descriptionText))
        {
            infoText.text = description.descriptionText;
        }
        else
        {
            infoText.text = "���������� �� ������� �����������";
        }
        infoText.gameObject.SetActive(true);
    }
}
