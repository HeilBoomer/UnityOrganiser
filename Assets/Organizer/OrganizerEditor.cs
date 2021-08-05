using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MaterialRenameWindow : EditorWindow
{
    public string path;
    public List<Object> materials;
    public string rootName;
    public static void Open(string path)
    {
        MaterialRenameWindow window = new MaterialRenameWindow();
        window.path = path;
        window.titleContent = new GUIContent("Rename Materials");
        
        window.ShowUtility();
        GUI.SetNextControlName("FirstFocus");

        GUI.FocusControl("FirstFocus");
    }

    public static void Open(string path, List<Object> mats)
    {
        MaterialRenameWindow window = new MaterialRenameWindow();
        window.path = path;
        window.titleContent = new GUIContent("Rename Materials");
        window.materials = mats;
        window.ShowUtility();

    }
    
    public void OnGUI()
    {

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Root Name ", GUILayout.MaxWidth(70));
        GUI.SetNextControlName("FocusOnText");
        bool firstCheck = false;
        if (!firstCheck){ EditorGUI.FocusTextInControl("FocusOnText"); firstCheck = true; }

        rootName = EditorGUILayout.TextField(rootName);
        EditorGUILayout.EndHorizontal();

        List<Object> materialList = materials;
        string matsString = string.Join("", materialList);

        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();


        if (GUILayout.Button("Rename Materials"))
        {
            int index = 0;
            foreach(Object objectToChange in materialList)
            {
                Debug.Log(objectToChange.name + " is the material in queue");
                string currentPath = AssetDatabase.GetAssetPath(objectToChange);
                Debug.Log("rename situation : " + AssetDatabase.RenameAsset(currentPath,rootName+index.ToString()));
                index++;
                
            }
            this.Close();
        }

        if (Event.current.isKey && Event.current.keyCode == KeyCode.Return)
        {
            int index = 0;
            foreach (Object objectToChange in materialList)
            {
                Debug.Log(objectToChange.name + " is the material in queue");
                string currentPath = AssetDatabase.GetAssetPath(objectToChange);
                Debug.Log("rename situation : " + AssetDatabase.RenameAsset(currentPath, rootName + index.ToString()));
                index++;

            }
            this.Close();
        }



        EditorGUILayout.EndHorizontal();

    }
}
