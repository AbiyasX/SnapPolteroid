using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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
    [SerializeField] CinemachineCamera playerCam;
    [SerializeField] CinemachineCamera npcCam;
    [Header("Status")]
    [SerializeField] bool isActive = false;
    int dialogIndex;
    private Coroutine typingCoroutine;
    public bool isTyping = false;
    public static DialogSystem instance;
    bool playVFX = false;
    bool hasNPC;

    [SerializeField] UnityEvent onStartDialog;
    [SerializeField] UnityEvent onExitDialog;
    private void Awake()
    {
        
        instance = this;
    }

    private void Update()
    {
        if (diaData == null || playerCam == null)
        {
            return;
        }
        hasNPC = npcCam != null;
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
            onStartDialog?.Invoke();
            if (diaData.dialogEntries.Length == 0)
            {
                
                Inventory.SetActive(true);
                dialogUI.SetActive(false);
                
                return;
            }

            if (dialogIndex >= diaData.dialogEntries.Length)
            {
                Cursor.lockState = CursorLockMode.Locked;
                isActive = false;
                dialogUI.SetActive(false);
                Inventory.SetActive(true);
                InputControl.Instance.playerMovement(true);
                playerCam.Priority = 0;
                NpcCamera(0);
                onExitDialog?.Invoke();
                playObtainVFX(playVFX);
                dialogIndex = 0;
                removeDialogData();
                return;
            }

            DialogEntry selectedEntry = diaData.dialogEntries[dialogIndex];
            StartTypewriter(selectedEntry.text);

            switch (selectedEntry.Target)
            {
                case Target.Player:
                    playerCam.Priority = 1;
                    NpcCamera(0);
                    break;
                case Target.Npc:
                    NpcCamera(1);
                    playerCam.Priority = 0;
                    break;
                default: 
                    playerCam.Priority = 0;
                    npcCam.Priority = 0;
                    break;
            }


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

    public void NpcCamera(int value)
    {
        if (hasNPC) npcCam.Priority = value;
    }

    public void activateDialog(bool active)
    {
        isActive = active;
    }

    public void removeDialogData()
    {
        diaData = null;
        playerCam = null;
        npcCam = null;
        playVFX = false;
    }

    public void playObtainVFX(bool active)
    {
        playVFX = active;
        if (!isActive && playVFX) UI_InventorySystem.instance.VFX();
    }

    public void addDialogData(DialogData dialogData, CinemachineCamera Player_Camera, CinemachineCamera NPC_Camera)
    {
        diaData = dialogData;
        playerCam = Player_Camera;
        npcCam = NPC_Camera;
    }
}
