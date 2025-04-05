using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission_Data", menuName = "Mission_Data")]
public class Mission_Data : ScriptableObject
{
    [SerializeField] private int sceneIndex;

    [SerializeField, TextArea(3, 10)] public string text_Letter;
    [SerializeField] public string text_FromLetter;
}
