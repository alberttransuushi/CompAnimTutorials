using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathManager))]
public class PathManagerEditor : Editor
{
    [SerializeField]
    PathManager pathManager;

    [SerializeField]
    List<Waypoint> thePath;

    List<int> toDelete;
    Waypoint selectedPoint = null;
    bool doRepaint = true;

    private void OnSceneGUI()
    {
        thePath = pathManager.GetPath();
        DrawPath(thePath);
    }

    private void OnEnable()
    {
        pathManager = target as PathManager;
        toDelete = new List<int>();
    }

    public override void OnInspectorGUI()
    {
        this.serializedObject.Update();
        thePath = pathManager.GetPath();

        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Path:");

        DrawGUIForPoints();

        // Button for adding a point to the path
        if (GUILayout.Button("Add Point to Path"))
        {
            pathManager.CreateAddPoint();
        }

        EditorGUILayout.EndVertical();
        SceneView.RepaintAll();
    }

    void DrawGUIForPoints()
    {
        if (thePath != null && thePath.Count > 0)
        {
            for (int i = 0; i < thePath.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                Waypoint p = thePath[i];

                Vector3 oldPos = p.pos;
                Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                if (EditorGUI.EndChangeCheck())
                {
                    p.SetPos(newPos);
                }

                // The Delete button
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    // Do something for deletion
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }

    public void DrawPath(List<Waypoint> path)
    {
        void DrawGUIForPoints()
        {
            if (thePath != null && thePath.Count > 0)
            {
                for (int i = 0; i < thePath.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    Waypoint p = thePath[i];

                    Vector3 oldPos = p.GetPos();
                    Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                    if (EditorGUI.EndChangeCheck())
                    {
                        p.SetPos(newPos);
                    }

                    // The Delete button
                    if (GUILayout.Button("-", GUILayout.Width(25)))
                    {
                        toDelete.Add(i);  // add the index to our delete list
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }

            if (toDelete.Count > 0)
            {
                foreach (int i in toDelete)
                {
                    thePath.RemoveAt(i); // remove from the path
                }
                toDelete.Clear(); // clear the delete list for the next time
            }
        }

    }
}
