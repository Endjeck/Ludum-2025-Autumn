using UnityEngine;
using TMPro;

public class Rightdoor : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Тег интерактивных объектов")]
    public string interactiveTag = "Right Door";

    [Tooltip("Цвет подсветки при наведении")]
    public Color highlightColor = Color.yellow;

    [Tooltip("UI TextMeshProUGUI для отображения информации об объекте")]
    public TextMeshProUGUI infoText;

    [Tooltip("Максимальное расстояние для взаимодействия")]
    public float maxDistance = 5f;

    private Camera mainCamera;
    private GameObject currentObject;
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
        CheckObjectInView();

        if (isPromptVisible && Input.GetKeyDown(KeyCode.E) && currentObject != null)
        {
            currentObject.SetActive(false);
            ClearCurrentObject();
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

        infoText.gameObject.SetActive(true);
        infoText.text = "Нажмите на Е";
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
}

