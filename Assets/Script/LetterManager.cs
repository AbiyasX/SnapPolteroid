using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;

public class LetterManager : MonoBehaviour
{
    [SerializeField] Mission_Data[] missionData;
    [SerializeField] GameObject Letter;
    [SerializeField] GameObject LetterUI;
    [SerializeField] TextMeshProUGUI Letter_Text;
    [SerializeField] TextMeshProUGUI fromLetter_Text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Letter_Active(true);
        }

    }

    public void Letter_Active(bool _Active)
    {
        Letter.SetActive(_Active);
    }

    public void LetterUI_Active(bool _Active) 
    {
        if (_Active)
        {
            int MissionIndex = Random.Range(0, missionData.Length);
            Letter_Text.text = missionData[MissionIndex].text_Letter;
            fromLetter_Text.text = "From " + missionData[MissionIndex].text_FromLetter + ",";
            LetterUI.SetActive(true);
           
        }
        else if (!_Active)
        {
            LetterUI.SetActive(false);
        }
        
    }

   
}
