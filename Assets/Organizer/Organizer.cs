using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.Experimental.AssetBundlePatching;


public class RenameMaterial
{
    [MenuItem("Organizer/Rename Materials in Folder #F1")]
    public static void RenameMaterials()
    {
        Object selectedObject = Selection.activeObject;
        string selectedPath = AssetDatabase.GetAssetPath(selectedObject);
        string[] splitedPath = selectedPath.Split('/');
        List<string> splitedPathList = ArrayToList<string>(splitedPath);
        splitedPathList.Remove(splitedPathList.Last());
        selectedPath = string.Join("/",splitedPathList);

        Object[] allMaterials = Resources.FindObjectsOfTypeAll(typeof(Material));
        List<Object> finalMaterialsList = new List<Object>(); 
        foreach (Object material in allMaterials)
        {
            string matPath = AssetDatabase.GetAssetPath(material);
            matPath = RemoveLastElementOfPath(matPath);
            
            if(matPath == selectedPath)
            {
                //Debug.Log($"material path : {matPath} , selected materials path {selectedPath}");
                finalMaterialsList.Add(material);
            }

        }
        foreach(Object o in finalMaterialsList)
        {
            Debug.LogWarning(o.name);
        }
        //Debug


        MaterialRenameWindow.Open(selectedPath,finalMaterialsList);
    }

    static List<T> ArrayToList<T>(T[] array)
    {
        List<T> returnList = new List<T>();
        foreach(T element in array)
        {
            returnList.Add(element);

        }
        return returnList;
    }

    static string RemoveLastElementOfPath(string input)
    {
        string[] splitedMatPath = input.Split('/');
        List<string> splitedList = ArrayToList<string>(splitedMatPath);
        splitedList.Remove(splitedList.Last());
        string path = string.Join("/", splitedList);
        return path;
    }
}

