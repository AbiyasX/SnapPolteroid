using UnityEngine;


public enum DialogType
{
    talking,
    thinking,
    exclaiming
}

public enum Target
{
    Player,
    Npc
}

[System.Serializable]
public struct DialogEntry
{
    public DialogType type;
    public Target Target;
    [TextArea(5,20)]
    public string text;   
}

[CreateAssetMenu(fileName = "DialogData", menuName = "DialogMenu/DialogData")]
public class DialogData : ScriptableObject
{
    public DialogEntry[] dialogEntries;
}
   
