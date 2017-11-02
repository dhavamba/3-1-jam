using UnityEditor;
using UnityEngine;

public class MultiTagsEditor
{
    [MenuItem("MultiTags/Create Empty")]
    private static void Istantiate()
    {
        GameObject empty = new GameObject("GameObject");
        empty.AddComponent<MultiTags>();
    }
}
