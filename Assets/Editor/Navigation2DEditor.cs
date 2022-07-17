using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AI;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(Navigation2D))]
public class Navigation2DEditor : Editor
{
    private bool mapEdit;

    private Navigation2D navigation;

    private BoxCollider boxCollider;

    private bool showInfo;

    private int nodeValue = 0;

    private int[] nodeValueSizes = { 0, 3 };

    private string[] nodeValueName = { "可导航点", "障碍物" };

    private void OnEnable()
    {
        navigation = (Navigation2D)target;
        if (!navigation.gameObject.TryGetComponent(out boxCollider))
        {
            boxCollider = navigation.gameObject.AddComponent<BoxCollider>();
        }

        boxCollider.enabled = false;
    }

    public override void OnInspectorGUI()
    {

        showInfo = EditorGUILayout.Foldout(showInfo, "基础信息");
        if (showInfo)
        {
            EditorGUI.indentLevel++;
            navigation.width = EditorGUILayout.IntField("宽度(X):", navigation.width);

            navigation.length = EditorGUILayout.IntField("长度(Y):", navigation.length);

            navigation.nodeSize = EditorGUILayout.FloatField("格子大小(SIZE):", navigation.nodeSize);
        }

        EditorGUI.indentLevel--;


        EditorGUI.BeginChangeCheck();
        mapEdit = EditorGUILayout.Toggle("启动编辑", mapEdit);
        if (EditorGUI.EndChangeCheck())
        {
            if (mapEdit)
            {
                boxCollider.enabled = true;
                boxCollider.size = new Vector3(navigation.width * navigation.nodeSize, navigation.length * navigation.nodeSize, 0.1f);
                boxCollider.center = new Vector3(navigation.width * navigation.nodeSize / 2f, navigation.length * navigation.nodeSize / 2f);
            }
            else
                boxCollider.enabled = false;

        }

        if(mapEdit)
        {
            nodeValue = EditorGUILayout.IntPopup("编辑类型：", nodeValue, nodeValueName, nodeValueSizes);
        }
    }

    public void OnSceneGUI()
    {
        if (mapEdit)
        {
            
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            Event e = Event.current;

            if (e.isMouse && e.button == 0 && e.clickCount == 1 || e.shift)
            {
                Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 2000))
                {
                    int x = (int)(hitInfo.point.x / navigation.nodeSize);
                    int y = (int)(hitInfo.point.y / navigation.nodeSize);
                    navigation.SetData(x, y, nodeValue);
                }
            }
        }
        else
        {
            HandleUtility.Repaint();
        }
    }

    [DrawGizmo(GizmoType.InSelectionHierarchy)]
    private static void DrawGridMap(Navigation2D target, GizmoType type)
    {
        if (target.width <= 0 || target.length <= 0 || target.map == null)
            return;
        
        Gizmos.color = Color.white;

        //行
        for (int i = 0; i < target.width + 1; i++)
        {
            Vector3 start = new Vector3(i * target.nodeSize, 0);
            Vector3 end = new Vector3(i * target.nodeSize, target.length * target.nodeSize);
            Gizmos.DrawLine(start, end);
        }

        //列
        for (int i = 0; i < target.length + 1; i++)
        {
            Vector3 start = new Vector3(0, i * target.nodeSize);
            Vector3 end = new Vector3(target.width * target.nodeSize, i * target.nodeSize);
            Gizmos.DrawLine(start, end);
        }

        for (int x = 0; x < target.map.GetLength(0); x++)
        {
            for (int y = 0; y < target.map.GetLength(1); y++)
            {
                if (target.map[x, y].status == PathNode.NODE_BLOCK)
                {
                    Gizmos.color = new Color(1, 0, 0, 0.5f);
                    Vector3 start = new Vector3(x * target.nodeSize + target.nodeSize / 2, y * target.nodeSize + target.nodeSize / 2);
                    Gizmos.DrawCube(start, Vector3.one * target.nodeSize * 0.95f);
                }
            }
        }
    }
}
