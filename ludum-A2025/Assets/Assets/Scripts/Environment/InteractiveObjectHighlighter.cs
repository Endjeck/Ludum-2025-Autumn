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
    public DialogueText infoText;

    private Camera mainCamera;
    private GameObject currentObject;            // Объект, на который сейчас наведен курсор
    private Renderer currentRenderer;
    private Color originalColor;
    private bool isPromptVisible = false;
    private NoteObject _note = null;

    private void Start()
    {
        mainCamera = Camera.main;
        infoText.Back.SetActive(false);
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
        infoText.Back.SetActive(true);
        if(PlayerPrefs.GetInt(TranslationConstants.TranslateParametr) == 0)
            infoText.Text.text = "Нажмите на Е";
        else
            infoText.Text.text = "Press Е";
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

        infoText.Back.SetActive(false);
        isPromptVisible = false;
    }

    private void ShowInfoText()
    {
       
        if (currentObject == null) return;

        // Получаем компонент с описанием объекта, если есть
        ObjectDescription description = null;
        currentObject.TryGetComponent(out description);
        currentObject.TryGetComponent(out _note);
        DiskObject diskObject = null;
        currentObject.TryGetComponent(out diskObject);
        PickableDisk pickableDisk = null;
        currentObject.TryGetComponent(out pickableDisk);
        EmptyDisk emptyDisk = null;
        currentObject.TryGetComponent<EmptyDisk>(out emptyDisk);
        WrongDoor wrongDoor = null;
        currentObject.TryGetComponent(out wrongDoor);
        Rightdoor rightdoor = null;
        currentObject.TryGetComponent(out rightdoor);

        if (description != null && !string.IsNullOrEmpty(description.Description))
        {
            infoText.Text.text = description.Description;
        }
        else if (_note != null) 
        {
            _note.ActivateNote(true);
        }
        else if(diskObject != null)
        {
            diskObject.RotateDisk();
        }
        else if(pickableDisk != null)
        {
            pickableDisk.CollectDisk();
        }
        else if(emptyDisk !=null)
        {
            emptyDisk.InsertDisk();
        }
        else if (wrongDoor != null)
        {
            wrongDoor.OpenDoor();
        }
        else if (rightdoor != null)
        {
            rightdoor.OpenDoor();
        }

        else
        {
            if (PlayerPrefs.GetInt(TranslationConstants.TranslateParametr) == 0)
                infoText.Text.text = "Информация об объекте отсутствует";
            else
                infoText.Text.text = "sorry, I forgot to put text here";
        }
        infoText.Back.SetActive(true);


    }
}
