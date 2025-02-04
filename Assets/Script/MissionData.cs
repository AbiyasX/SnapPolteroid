using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission_Data", menuName = "Mission_Data")]
public class Mission_Data : ScriptableObject
{
    [SerializeField] public GameObject m_NpcHouse;
    [SerializeField] public GameObject m_Npc;
   
    [SerializeField, TextArea(3, 10)] public string text_Letter;
}
