// WaypointManagerWindow.cs
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Tools/Waypoint Editor")]
    public static void Open() => GetWindow<WaypointManagerWindow>("Waypoint Editor");

    public Transform waypointRoot;
    SerializedObject _so;

    void OnEnable() => _so = new SerializedObject(this);

    void OnGUI()
    {
        _so.Update();
        EditorGUILayout.PropertyField(_so.FindProperty(nameof(waypointRoot)));

        if (!waypointRoot)
        {
            EditorGUILayout.HelpBox("Assign a root transform for your waypoints.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();
        }

        _so.ApplyModifiedProperties();
    }

    void DrawButtons()
    {
        if (GUILayout.Button("Create Waypoint"))
            CreateWaypoint();

        if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<Waypoint>())
        {
            EditorGUILayout.Space(3);
            if (GUILayout.Button("Create Waypoint Before"))
                CreateWaypointBefore();
            if (GUILayout.Button("Create Waypoint After"))
                CreateWaypointAfter();
            EditorGUILayout.Space(3);
            if (GUILayout.Button("Remove Waypoint"))
                RemoveWaypoint();
        }

        // Branch linking: exactly two Waypoints selected
        var sel = Selection.transforms;
        if (sel.Length == 2
            && sel[0].GetComponent<Waypoint>() != null
            && sel[1].GetComponent<Waypoint>() != null)
        {
            EditorGUILayout.Space(3);
            if (GUILayout.Button("Create Branch"))
                CreateBranch(sel[0].GetComponent<Waypoint>(), sel[1].GetComponent<Waypoint>());
        }
    }

    void CreateWaypoint()
    {
        var go = new GameObject($"Waypoint {waypointRoot.childCount + 1}", typeof(Waypoint));
        Undo.RegisterCreatedObjectUndo(go, "Create Waypoint");
        go.transform.SetParent(waypointRoot, false);
        var wp = go.GetComponent<Waypoint>();

        if (waypointRoot.childCount > 1)
        {
            var prev = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            wp.previousWaypoint      = prev;
            prev.nextWaypoint        = wp;
            go.transform.position    = prev.transform.position;
            go.transform.rotation    = prev.transform.rotation;
        }

        Selection.activeGameObject = go;
        EditorGUIUtility.PingObject(go);
    }

    void CreateWaypointBefore()
    {
        // (existing implementation; unchanged)
        /* … */
        MarkSceneDirty();
    }

    void CreateWaypointAfter()
    {
        // (existing implementation; unchanged)
        /* … */
        MarkSceneDirty();
    }

    void RemoveWaypoint()
    {
        // (existing implementation; unchanged)
        /* … */
        MarkSceneDirty();
    }

    void CreateBranch(Waypoint source, Waypoint target)
    {
        Undo.RecordObject(source, "Create Branch");
        source.alternateNextWaypoint = target;
        EditorUtility.SetDirty(source);
        MarkSceneDirty();
    }

    void MarkSceneDirty()
    {
        EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }
}
