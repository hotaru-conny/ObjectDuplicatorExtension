using UnityEditor;
using UnityEngine;

public class ObjectDuplicator : EditorWindow
{
    private GameObject originalObject;
    private int numberOfCopies = 1;
    private Vector3 offset = Vector3.zero;

    [MenuItem("Tools/ObjectDuplicator")]
    public static void ShowWindow()
    {
        GetWindow<ObjectDuplicator>("ObjectDuplicatorWindow");
    }

    private void OnGUI()
    {
        GUILayout.Label("Let's Duplicate!", EditorStyles.boldLabel);

        originalObject = EditorGUILayout.ObjectField("Original Object", originalObject, typeof(GameObject), true) as GameObject;
        numberOfCopies = EditorGUILayout.IntField("Number of copies", numberOfCopies);
        offset = EditorGUILayout.Vector3Field("offset", offset);

        if (GUILayout.Button("Duplicate！"))
        {
            DuplicateObjects();
        }
    }

    private void DuplicateObjects()
    {
        //複製対象が
        if (originalObject == null)
        {
            //ダイアログを表示
            EditorUtility.DisplayDialog("ObjectDuplicator", "Original Objectが未設定です！", "OK");
            return;
        }
        
        if (numberOfCopies <= 0)
        {
            EditorUtility.DisplayDialog("ObjectDuplicator", "コピー数が0以下です！", "OK");
            return;
        }

        for (int i = 0; i < numberOfCopies; i++)
        {
            GameObject newObject = Instantiate(originalObject);
            newObject.transform.position = originalObject.transform.position + offset * (i+1);
            newObject.name = originalObject.name + "_Copy" + (i + 1);

            Undo.RegisterCreatedObjectUndo(newObject, "Duplicate Object");
        }

        Debug.Log("Duplicated！");
    }
}
