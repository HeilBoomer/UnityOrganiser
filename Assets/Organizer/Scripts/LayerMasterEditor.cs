using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LayerMaster))]
public class LayerMasterEditor : Editor
{
    private void OnSceneGUI()
    {
        LayerMaster master = (LayerMaster)target;
        Event c = Event.current;

        switch (c.type)
        {
            case EventType.KeyDown:
                switch (Event.current.keyCode)
                {
                    case KeyCode.C:
                        master.Carry();
                        break;
                }

                break;
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LayerMaster master = (LayerMaster) target;

        master.bezierCurve = GUILayout.Toggle(master.bezierCurve, new GUIContent("Bezier Curve", "What about making it a bit more 'curvy' B)"));

        if (master.bezierCurve)
        {
            GUILayout.Space(10);
            master.startTangentOffset = EditorGUILayout.Vector3Field("Start Tangent Offset",master.startTangentOffset);
            master.endTangentOffset = EditorGUILayout.Vector3Field("End Tangent Offset",master.endTangentOffset);
            GUILayout.Space(10);
            master.width = EditorGUILayout.FloatField("Curve Width",master.width);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Curve Texture");
            master.curveTexture = (Texture2D) EditorGUILayout.ObjectField(master.curveTexture, typeof(Texture2D), false);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            master.pointPathLock = GUILayout.Toggle(master.pointPathLock, new GUIContent("Custom Point Icon Path", "Change the point icon using a path, You must have your Icons on Assets/Gizmos folder!"));
            if (master.pointPathLock)
            {
                master.pointIconPath = EditorGUILayout.TextField(master.pointIconPath);
            }
            else
            {
                GUILayout.Box(master.pointIconPath, GUILayout.MinWidth(180),GUILayout.MaxHeight(1));
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        master.objectPathLock = GUILayout.Toggle(master.objectPathLock, new GUIContent("Custom Object Icon Path", "Change the object icon using a path, You must have your Icons on Assets/Gizmos folder!"));
        if (master.objectPathLock)
        {
            master.objectIconPath = EditorGUILayout.TextField(master.objectIconPath,GUILayout.MinWidth(340));
        }

        else
        {
            GUILayout.Box(master.objectIconPath, GUILayout.MinWidth(180), GUILayout.MaxHeight(1));
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(30);
        if (GUILayout.Button("Carry"))
        {
            master.Carry();

        }
    }
}

