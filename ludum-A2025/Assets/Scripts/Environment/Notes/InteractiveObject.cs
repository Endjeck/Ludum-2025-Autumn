using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractiveObject : MonoBehaviour
{
    [Header("Outline Component (assign if not on same GameObject)")]
    public Outline outline;

    [Header("Image to show on interaction")]
    public Sprite interactionImage;

    private void Awake()
    {
        if (outline == null)
            outline = GetComponentInChildren<Outline>();

        if (outline != null)
            outline.enabled = false;
    }
}

public class InteractiveManager : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Тег интерактивных объектов")]
    public string interactiveTag = "Interactive";

    [Header("UI Elements (assign in Inspector)")]
    public TextMeshProUGUI promptText;
    public Image displayImage;
    public Button closeButton;

    private Camera mainCamera;
    private InteractiveObject currentInteractive;
    private bool isPromptVisible = false;
    private bool isImageShown = false;

    private void Start()
    {
        mainCamera = Camera.main;

        if (promptText != null)
            promptText.gameObject.SetActive(false);

        if (displayImage != null)
            displayImage.gameObject.SetActive(false);

        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(false);
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }
    }

    private void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            ClearCurrentInteractive();
            return;
        }

        if (!isImageShown)
            CheckInteractiveUnderCursor();

        if (isPromptVisible && Input.GetKeyDown(KeyCode.E))
        {
            ShowImageAndButton();
        }
    }

    private void CheckInteractiveUnderCursor()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.collider.gameObject;

            if (hitObj.CompareTag(interactiveTag))
            {
                InteractiveObject interactive = hitObj.GetComponentInChildren<InteractiveObject>();
                if (interactive != null)
                {
                    if (currentInteractive != interactive)
                    {
                        ClearCurrentInteractive();
                        SetCurrentInteractive(interactive);
                    }
                    return;
                }
            }
        }

        ClearCurrentInteractive();
    }

    private void SetCurrentInteractive(InteractiveObject interactive)
    {
        currentInteractive = interactive;

        if (currentInteractive.outline != null)
            currentInteractive.outline.enabled = true;

        if (promptText != null)
        {
            promptText.text = "Нажмите на Е";
            promptText.gameObject.SetActive(true);
        }

        isPromptVisible = true;
    }

    private void ClearCurrentInteractive()
    {
        if (currentInteractive != null && currentInteractive.outline != null)
        {
            currentInteractive.outline.enabled = false;
        }

        currentInteractive = null;

        if (promptText != null)
            promptText.gameObject.SetActive(false);

        isPromptVisible = false;
    }

    private void ShowImageAndButton()
    {
        if (currentInteractive == null)
            return;

        if (promptText != null)
            promptText.gameObject.SetActive(false);

        if (displayImage != null)
        {
            displayImage.sprite = currentInteractive.interactionImage;
            displayImage.gameObject.SetActive(true);
        }

        if (closeButton != null)
            closeButton.gameObject.SetActive(true);

        isImageShown = true;
        isPromptVisible = false;

        if (currentInteractive.outline != null)
            currentInteractive.outline.enabled = false;
    }

    private void OnCloseButtonClicked()
    {
        if (displayImage != null)
            displayImage.gameObject.SetActive(false);

        if (closeButton != null)
            closeButton.gameObject.SetActive(false);

        isImageShown = false;
    }
}