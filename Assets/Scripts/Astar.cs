using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar
{
    private AstarNode[,] m_map;

    private int m_mapLengh;

    private int m_mapWidth;

    public void InitMap(int[,] map)
    {
        m_mapLengh = map.GetLength(0); //行数
        m_mapWidth = map.GetLength(1); //列数
        m_map = new AstarNode[m_mapLengh, m_mapWidth];
        for (int x = 0; x < m_mapLengh; x++)
        {
            for (int y = 0; y < m_mapWidth; y++)
            {
                m_map[x, y] = new AstarNode(x, y, map[x, y]);
            }
        }
    }


    private List<AstarNode> openList = new List<AstarNode>();
    List<AstarNode> closeList = new List<AstarNode>();
    public List<AstarNode> AStar_Finding(Vector2Int startNode, Vector2Int targetNode)
    {
        openList.Clear();
        closeList.Clear();

        //重置地图数据
        //ResetMap();
        //标记起点和终点
        AstarNode sNode = m_map[startNode[0], startNode[1]];
        sNode.status = AstarNode.NODE_START;
        AstarNode eNode = m_map[targetNode[0], targetNode[1]];
        eNode.status = AstarNode.NODE_End;

        sNode.G = 0;
        sNode.H = GetH(sNode, eNode);
        sNode.Parent = null;

        openList.Add(sNode);
        while (openList.Count > 0)
        {
            AstarNode minNode = GetMinFNode();
            closeList.Add(minNode);
            openList.Remove(minNode);

            if(minNode.status == AstarNode.NODE_End)
                break;

            List<AstarNode> neighor = GetNeighor(minNode);
            foreach (AstarNode PathNode in neighor)
            {
                if (PathNode.status != AstarNode.NODE_BLOCK && !openList.Contains(PathNode) && !closeList.Contains(PathNode))
                {
                    PathNode.H = GetH(PathNode, eNode);
                    PathNode.G = GetG(PathNode, minNode) + minNode.G;
                    PathNode.Parent = minNode;

                    openList.Add(PathNode);
                }
            }
        }

        //向上递归父节点得出路径
        List<AstarNode> path = new List<AstarNode>();
        AstarNode target = eNode;
        while (target.status != AstarNode.NODE_START)
        {
            if (target.Parent == null)
                break;
            path.Add(target);
            target = target.Parent;
        }


        return path;

    }

    /// <summary>
    /// 获得H值
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private int GetH(AstarNode current, AstarNode target)
    {
        if (current == null || target == null)
            return 0;

        int a = Mathf.Abs(current.X - target.X);
        int b = Mathf.Abs(current.Y - target.Y);

        return (int)Mathf.Sqrt(a * a + b * b) * 10;
    }

    /// <summary>
    /// 获得G值
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    private int GetG(AstarNode current, AstarNode target)
    {
        if (current == null || target == null)
            return 0;

        if (current.X == target.X && current.Y == target.Y)
            return 0;

        if (current.Y == target.Y || current.X == target.Y)
            return 10;

        return 14;
    }

    /// <summary>
    /// 从开启列表中获得F值最小的
    /// </summary>
    private AstarNode GetMinFNode()
    {
        AstarNode minNode = null;
        int minFCount = int.MaxValue;

        foreach (AstarNode PathNode in openList)
        {
            if (PathNode.F < minFCount)
            {
                minFCount = PathNode.F;
                minNode = PathNode;
            }
        }

        return minNode;
    }


    /// <summary>
    /// 获得相邻的节点
    /// </summary>
    /// <param name="currentNode"></param>
    /// <returns></returns>
    private List<AstarNode> GetNeighor(AstarNode currentNode)
    {
        List<AstarNode> nodes = new List<AstarNode>();
        int x = currentNode.X;
        int y = currentNode.Y;

        //左
        if (x - 1 >= 0)
            nodes.Add(m_map[x - 1, y]);
        //右
        if (x + 1 < m_mapLengh && x + 1 >= 0)
            nodes.Add(m_map[x + 1, y]);
        //下
        if (y - 1 >= 0)
            nodes.Add(m_map[x, y - 1]);
        //上
        if (y + 1 < m_mapWidth && y + 1 >= 0)
            nodes.Add(m_map[x, y + 1]);
        //左下
        if (x - 1 >= 0 && y - 1 >= 0)
            nodes.Add(m_map[x - 1, y - 1]);
        //左上
        if (x - 1 >= 0 && y + 1 < m_mapWidth && y + 1 >= 0)
            nodes.Add(m_map[x - 1, y + 1]);
        //右下
        if (x + 1 < m_mapLengh && x + 1 >= 0 && y - 1 >= 0)
            nodes.Add(m_map[x + 1, y - 1]);
        //右上
        if (x + 1 < m_mapLengh && x + 1 >= 0 && y + 1 < m_mapWidth && y + 1 >= 0)
            nodes.Add(m_map[x + 1, y + 1]);

        return nodes;
    }


    public class AstarNode
    {
        public static int NODE_START = 1;

        public static int NODE_End = 2;

        public static int NODE_BLOCK = 3;

        public int X;

        public int Y;

        public int status;

        public AstarNode Parent;

        public int H;

        public int G;

        public int F { get { return H + G; } }

        public AstarNode(int x, int y, int status)
        {
            X = x;
            Y = y;
            this.status = status;
        }
    }
}
