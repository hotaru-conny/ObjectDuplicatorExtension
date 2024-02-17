using UnityEditor;
using UnityEngine;

public class ObjectDuplicator : EditorWindow
{
    private GameObject originalObject;
    private int numberOfCopies = 1;
    private Vector3 offset = Vector3.zero;

    //Toolsから呼び出す
    [MenuItem("Tools/ObjectDuplicator")]
    public static void ShowWindow()
    {
        GetWindow<ObjectDuplicator>("ObjectDuplicatorWindow");
    }

    //Windowに表示する内容
    private void OnGUI()
    {
        GUILayout.Label("Let's Duplicate!", EditorStyles.boldLabel);

        //コピー元オブジェクト、コピー数、オフセットをフィールドで受け取る
        originalObject = EditorGUILayout.ObjectField("Original Object", originalObject, typeof(GameObject), true) as GameObject;
        numberOfCopies = EditorGUILayout.IntField("Number of copies", numberOfCopies);
        offset = EditorGUILayout.Vector3Field("offset", offset);

        //ボタンを押したら複製
        if (GUILayout.Button("Duplicate！"))
        {
            DuplicateObjects();
        }
    }

    private void DuplicateObjects()
    {
        //複製対象がnullなら
        if (originalObject == null)
        {
            EditorUtility.DisplayDialog("ObjectDuplicator", "Original Objectが未設定です！", "OK");
            return;
        }
        
        //コピー数が0以下なら
        if (numberOfCopies <= 0)
        {
            EditorUtility.DisplayDialog("ObjectDuplicator", "コピー数が0以下です！", "OK");
            return;
        }

        //複製
        for (int i = 0; i < numberOfCopies; i++)
        {
            GameObject newObject = Instantiate(originalObject);
            newObject.transform.position = originalObject.transform.position + offset * (i+1);
            newObject.name = originalObject.name + "_Copy" + (i + 1);

            //Undoで取り消せるように登録
            Undo.RegisterCreatedObjectUndo(newObject, "Duplicate Object");
        }

        Debug.Log("Duplicated！");
    }
}
