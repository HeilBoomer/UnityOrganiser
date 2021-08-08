using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroupMaster))]
public class GroupMasterEditor : Editor
{
    private void OnSceneGUI()
    {
        GroupMaster master = (GroupMaster)target;
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

        GroupMaster master = (GroupMaster) target;
        GUILayout.Space(20);

        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = Color.white;
        

        GUILayout.BeginHorizontal();
        GUI.backgroundColor = new Color(.645f, .645f, .665f, 1);

        if (!master.recording)
        {
            GUI.backgroundColor = new Color(0.6185604f, 1, 0.5707547f, 1);

            if (GUILayout.Button("Start Selecting GameObjects", style, GUILayout.MinHeight(30)))
            {
                master.StartSelect();
            }
            GUI.backgroundColor = new Color(0.9433962f, 0.5494924f, 0.5295479f, 1);

            if (GUILayout.Button("Start Removing GameObjects", GUILayout.MinHeight(30)))
            {
                master.StartRemove();
            }

        }
        else
        {
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Stop recording", style, GUILayout.MinHeight(30)))
            {
                master.RecordEvents();
            }
        }
        GUI.backgroundColor = new Color(.645f, .645f, .665f, 1);


        GUILayout.EndHorizontal();
        GUILayout.Space(30);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Curve Color");
        master.curveColor = EditorGUILayout.ColorField(master.curveColor);
        GUILayout.EndHorizontal();
        master.drawAlways = EditorGUILayout.Toggle("Draw Always", master.drawAlways);
        master.drawIconsAlways = EditorGUILayout.Toggle("Draw Icons Always", master.drawIconsAlways);
        master.draw2D = EditorGUILayout.Toggle("Draw2D", master.draw2D);
        GUILayout.Space(20);
        master.bezierCurve = GUILayout.Toggle(master.bezierCurve, new GUIContent("Bezier Curve","What about making it a bit more 'curvy' B)"));


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
        GUI.backgroundColor = new Color(.845f, .845f, .865f, 1);

        if (GUILayout.Button("Carry"))
        {
            master.Carry();
        }
    }
}

