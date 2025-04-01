using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    
    [Header("Components")]
    [SerializeField] private Image textBoxSprite;
    [SerializeField] TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject Inventory;
    [Header("textbox sprite")]
    [SerializeField] private Sprite talkingTextBox;
    [SerializeField] private Sprite thinkingTextBox;
    [SerializeField] private Sprite exclaimingTextBox;
    [Header("Dialog Entry")]
    [SerializeField] DialogData diaData;
    [SerializeField] CinemachineCamera cam;
    [Header("Status")]
    [SerializeField] bool isActive = false;
    int dialogIndex;
    private Coroutine typingCoroutine;
    public bool isTyping = false;
    public static DialogSystem instance;

    private void Awake()
    {
        
        instance = this;
    }

    private void Update()
    {
        if (diaData == null || cam == null)
        {
            return;
        }
        runDialog(isActive);
    }
    #region runDialog

    public void nextButton()
    {
        dialogIndex++;
        isTyping = false;
    }

    private void runDialog(bool isRunning)
    {
        if (isRunning)
        {
            Cursor.lockState = CursorLockMode.None;
            dialogUI.SetActive(true);
            Inventory.SetActive(false);
            InputControl.Instance.playerMovement(false);
            cam.Priority = 1;

            if (diaData.dialogEntries.Length == 0)
            {
                
                Inventory.SetActive(true);
                dialogUI.SetActive(false);
                cam.Priority = 0;
                return;
            }

            if (dialogIndex >= diaData.dialogEntries.Length)
            {
                Cursor.lockState = CursorLockMode.Locked;
                isActive = false;
                dialogUI.SetActive(false);
                Inventory.SetActive(true);
                InputControl.Instance.playerMovement(true);
                cam.Priority = 0;
                removeDialogData();
                return;
            }

            DialogEntry selectedEntry = diaData.dialogEntries[dialogIndex];
            StartTypewriter(selectedEntry.text);

            switch (selectedEntry.type)
            {
                case DialogType.talking:
                    textBoxSprite.sprite = talkingTextBox;
                    break;

                case DialogType.thinking:
                    textBoxSprite.sprite = thinkingTextBox;
                    break;

                case DialogType.exclaiming:
                    textBoxSprite.sprite = exclaimingTextBox;
                    break;

                default:
                    Debug.Log("Unknown dialog type.");
                    break;
            }
        }
        else
        {
            dialogUI.SetActive(false);
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
                typingCoroutine = null;
            }
        }
    }

    public void StartTypewriter(string message)
    {
        if (isTyping) return;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); 
        }

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    IEnumerator TypeText(string message)
    {
        isTyping = true;
        dialogText.text = ""; 

        foreach (char letter in message)
        {
            dialogText.text += letter; 
            yield return new WaitForSeconds(0.05f); 
        }
        
    }
    #endregion

    public void activateDialog(bool active)
    {
        isActive = active;
    }


    public void removeDialogData()
    {
        diaData = null;
        cam = null;
    }

    public void addDialogData(DialogData dialogData, CinemachineCamera focusCam)
    {
        diaData = dialogData;
        cam = focusCam;
    }
}
