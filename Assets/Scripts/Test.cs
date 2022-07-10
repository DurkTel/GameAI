using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    private static int[,] map;

    private GameObject[,] gameMap;

    public Material red;

    public Material green;

    public Material blue;

    public Material black;

    public Material white;

    public GameObject cube;

    void Start()
    {
        map = new int[10, 10]{           // 初始的map_maze  
                { 1, 0, 0, 3, 0, 3, 0, 0, 0, 0 },
                { 0, 0, 3, 0, 0, 3, 0, 3, 0, 3 },
                { 3, 0, 0, 0, 0, 3, 3, 3, 0, 3 },
                { 3, 0, 3, 0, 0, 0, 0, 0, 0, 3 },
                { 3, 0, 0, 0, 0, 3, 0, 3, 0, 3 },
                { 3, 0, 0, 3, 0, 0, 0, 3, 0, 3 },
                { 3, 0, 3, 0, 0, 3, 3, 0, 0, 0 },
                { 0, 0, 3, 0, 0, 0, 0, 0, 0, 0 },
                { 3, 3, 3, 0, 0, 3, 0, 3, 0, 3 },
                { 3, 0, 0, 0, 0, 3, 3, 3, 0, 3 },
            };

        getMapStrings();
        Pathfinding.InitMap(map);
        List<PathNode> path = Pathfinding.AStar_Finding(new Vector2Int(0, 0), new Vector2Int(6, 8));

        foreach (PathNode PathNode in path)
        {
            GameObject go = gameMap[PathNode.X, PathNode.Y];
            MeshRenderer goMat = go.GetComponent<MeshRenderer>();
            goMat.materials = new Material[1] { green };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getMapStrings()
    {
        int mapLength = map.GetLength(0);
        int mapWidth = map.GetLength(1);
        float spacing = 0.1f;
        gameMap = new GameObject[mapLength, mapWidth];
        for (int i = 0; i < mapLength; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                Material material = white;
                switch (map[i, j])
                {
                    case 0:
                        material = white;
                        break;
                    case 1:
                        material = blue;
                        break;
                    case 2:
                        material = red;
                        break;
                    case 3:
                        material = black;
                        break;
                    //case 4:
                    //    Console.Write(".");
                    //    break;
                    //case 8:
                    //    Console.Write("A");
                    //    break;
                }
                GameObject go = Instantiate(cube);
                go.name = string.Format("{0}-{1}", i, j);
                MeshRenderer goMat = go.GetComponent<MeshRenderer>();
                goMat.materials = new Material[1] { material };
                go.transform.position = new Vector3(i + i * spacing, j + j * spacing, 0);
                gameMap[i, j] = go;
            }


        }
    }

}
