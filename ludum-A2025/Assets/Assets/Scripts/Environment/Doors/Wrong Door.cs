using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class WrongDoor : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("��� ������������� ��������")]
    public string interactiveTag = "Wrong Door";

    [Tooltip("���� ��������� ��� ���������")]
    public Color highlightColor = Color.yellow;

    [Tooltip("UI TextMeshProUGUI ��� ����������� ���������� �� �������")]
    public TextMeshProUGUI infoText;

    [Tooltip("������������ ���������� ��� ��������������")]
    public float maxDistance = 5f;

    private Camera mainCamera;
    private GameObject currentObject;
    private Renderer currentRenderer;
    private Color originalColor;
    private bool isPromptVisible = false;

    private void Start()
    {
        mainCamera = Camera.main;
        if (infoText != null)
            infoText.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckObjectInView();

        if (isPromptVisible && Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            currentObject.SetActive(false);
            ClearCurrentObject();
            SceneManager.LoadScene(0);
        }
    }

    private void CheckObjectInView()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
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

        if (infoText != null)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "������� �� �";
        }
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

        if (infoText != null)
            infoText.gameObject.SetActive(false);

        isPromptVisible = false;
    }
}