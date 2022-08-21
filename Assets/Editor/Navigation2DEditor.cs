using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AI;
using UnityEditor.AnimatedValues;
using System.IO;

[CustomEditor(typeof(Navigation2D))]
public class Navigation2DEditor : Editor
{
    private bool mapEdit;

    private Navigation2D navigation;

    private SerializedProperty navigationData;

    private BoxCollider boxCollider;

    private bool showInfo = true;

    private string mapName;

    private int nodeValue = 0;

    private int[] nodeValueSizes = { 0, 3 };

    private string[] nodeValueName = { "可导航点", "障碍物" };

    private string assetPath = @"Assets/SO/NavigationData/";

    private static int width;

    private static int length;

    private static float nodeSize;

    private static PathNode[,] map;

    private void OnEnable()
    {
        navigation = (Navigation2D)target;
        if (!navigation.gameObject.TryGetComponent(out boxCollider))
        {
            boxCollider = navigation.gameObject.AddComponent<BoxCollider>();
        }

        boxCollider.enabled = false;

        navigationData = serializedObject.FindProperty("navigationData");

        if (navigationData.objectReferenceValue != null)
        {
            NavigationData data = navigationData.objectReferenceValue as NavigationData;
            width = data.width;
            length = data.length;
            nodeSize = data.nodeSize;
            map = data.map;
            mapName = data.name;
        }
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(navigationData, new GUIContent("导航数据"));
        if (EditorGUI.EndChangeCheck())
        {
            if (navigationData.objectReferenceValue != null)
            {
                NavigationData data = navigationData.objectReferenceValue as NavigationData;
                width = data.width;
                length = data.length;
                nodeSize = data.nodeSize;
                map = data.map;
                mapName = data.name;
                navigation.navigationData = data;
            }
            else
            {
                width = 0;
                length = 0;
                nodeSize = 0;
                map = null;
                navigation.navigationData = null;
            }
        }

        mapName = EditorGUILayout.TextField("地图名字", mapName);

        showInfo = EditorGUILayout.Foldout(showInfo, "基础信息");
        if (showInfo)
        {
            EditorGUI.indentLevel++;
            EditorGUI.BeginChangeCheck();

            width = EditorGUILayout.IntField("宽度(X):", width);

            length = EditorGUILayout.IntField("长度(Y):", length);
            if (EditorGUI.EndChangeCheck())
            {
                InitMap();
            }

            nodeSize = EditorGUILayout.FloatField("格子大小(SIZE):", nodeSize);
        }

        EditorGUI.indentLevel--;


        EditorGUI.BeginChangeCheck();
        mapEdit = EditorGUILayout.Toggle("启动编辑", mapEdit);
        if (EditorGUI.EndChangeCheck())
        {
            if (mapEdit)
            {
                boxCollider.enabled = true;
                boxCollider.size = new Vector3(width * nodeSize, length * nodeSize, 0.1f);
                boxCollider.center = new Vector3(width * nodeSize / 2f, length * nodeSize / 2f);
            }
            else
                boxCollider.enabled = false;

        }

        if(mapEdit)
        {
            nodeValue = EditorGUILayout.IntPopup("编辑类型：", nodeValue, nodeValueName, nodeValueSizes);
        }

        if (GUILayout.Button("保存信息"))
        {
            SaveData();
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
                    int x = (int)(hitInfo.point.x / nodeSize);
                    int y = (int)(hitInfo.point.y / nodeSize);
                    SetData(x, y, nodeValue);
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
        if (width <= 0 && length <= 0)
            return;

        Gizmos.color = Color.white;

        //行
        for (int i = 0; i < width + 1; i++)
        {
            Vector3 start = new Vector3(i * nodeSize, 0);
            Vector3 end = new Vector3(i * nodeSize, length * nodeSize);
            Gizmos.DrawLine(start, end);
        }

        //列
        for (int i = 0; i < length + 1; i++)
        {
            Vector3 start = new Vector3(0, i * nodeSize);
            Vector3 end = new Vector3(width * nodeSize, i * nodeSize);
            Gizmos.DrawLine(start, end);
        }

        if (map != null && map.GetLength(0) > 0 && map.GetLength(1) > 0)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].status == PathNode.NODE_BLOCK)
                    {
                        Gizmos.color = new Color(1, 0, 0, 0.5f);
                        Vector3 start = new Vector3(x * nodeSize + nodeSize / 2, y * nodeSize + nodeSize / 2);
                        Gizmos.DrawCube(start, Vector3.one * nodeSize * 0.95f);
                    }
                }
            }

        }

    }

    /// <summary>
    /// 初始化地图
    /// </summary>
    /// <param name="map"></param>
    private void InitMap()
    {
        map = new PathNode[width, length];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                PathNode node = new AstarPathNode();
                node.SetData(x, y, PathNode.NODE_NONE);
                map[x, y] = node;
            }
        }
    }

    private void SetData(int x, int y, int value)
    {
        if (width <= x || length <= y)
            return;

        map[x, y].status = value;
    }

    private void SaveData()
    {
        if(string.IsNullOrEmpty(mapName))
        {
            Debug.LogError("请输入导航数据名称");
            return;
        }

        if (!Directory.Exists(assetPath))
            Directory.CreateDirectory(assetPath);

        var so = ScriptableObject.CreateInstance<NavigationData>();

        so.width = width;
        so.length = length;
        so.nodeSize = nodeSize;
        so.map = map;

        navigation.navigationData = so;


        AssetDatabase.CreateAsset(so, assetPath + mapName + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
