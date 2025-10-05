using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InteractiveObjectHighlighter : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Тег интерактивных объектов")]
    public string interactiveTag = "Interactive";

    [Tooltip("Цвет подсветки при наведении")]
    public Color highlightColor = Color.yellow;

    [Tooltip("UI TextMeshProUGUI для отображения информации об объекте")]
    public TextMeshProUGUI infoText;

    private Camera mainCamera;
    private GameObject currentObject;            // Объект, на который сейчас наведен курсор
    private Renderer currentRenderer;
    private Color originalColor;
    private bool isPromptVisible = false;
    private NoteObject _note = null;

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
        if ((Input.GetAxis("Horizontal") !=0 || Input.GetAxis("Vertical") != 0)
            && _note!=null)
            _note.ActivateNote(false);

    }

    private void CheckObjectUnderCursor()
    {
        // Если указатель над UI элементом, не обрабатывать объекты сцены
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
        if(PlayerPrefs.GetInt(TranslationConstants.TranslateParametr) == 0)
            infoText.text = "Нажмите на Е";
        else
            infoText.text = "Press Е";
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

        // Получаем компонент с описанием объекта, если есть
        ObjectDescription description = null;
        currentObject.TryGetComponent(out description);
        currentObject.TryGetComponent(out _note);
        if (description != null && !string.IsNullOrEmpty(description.Description))
        {
            infoText.text = description.Description;
        }
        else if (_note != null) 
        {
            _note.ActivateNote(true);
        }
        else
        {
            if (PlayerPrefs.GetInt(TranslationConstants.TranslateParametr) == 0)
                infoText.text = "Информация об объекте отсутствует";
            else
                infoText.text = "sorry, I forgot to put text here";
        }
        infoText.gameObject.SetActive(true);
    }
}
