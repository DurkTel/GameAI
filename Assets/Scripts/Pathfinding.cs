using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Pathfinding
    {
        private static PathNode[,] m_map;

        private static int m_mapLengh;

        private static int m_mapWidth;


        /// <summary>
        /// 重置地图
        /// </summary>
        public static void ResetMap()
        {
            if (m_map == null)
                return;

            foreach (PathNode PathNode in m_map)
            {
                PathNode.Visited = false;
                PathNode.Parent = null;
            }
        }

        #region BFS

        /// <summary>
        /// BFS 广度优先算法
        /// </summary>
        /// <param name="startNode">起点</param>
        /// <param name="targetNode">终点</param>
        /// <returns>最短路径</returns>
        public static List<PathNode> BFS_Finding(Vector2Int startNode, Vector2Int targetNode)
        {
            //重置地图数据
            ResetMap();
            //标记起点和终点
            PathNode sNode = m_map[startNode[0], startNode[1]];
            sNode.status = PathNode.NODE_START;
            PathNode eNode = m_map[targetNode[0], targetNode[1]];
            if (eNode.status == PathNode.NODE_BLOCK)
            {
                Error();
                return null;
            }
            eNode.status = PathNode.NODE_End;


            //添加起点
            Queue<PathNode> queue = new Queue<PathNode>();
            queue.Enqueue(sNode);

            while (queue.Count > 0)
            {
                PathNode head = queue.Dequeue();
                //获取四周的点
                List<PathNode> neighor = GetNeighor(head);
                foreach (PathNode PathNode in neighor)
                {
                    //如果没访问过并是可访问的点
                    if (!PathNode.Visited && PathNode.status != PathNode.NODE_BLOCK)
                    {
                        //改变状态
                        PathNode.Visited = true;
                        PathNode.Parent = head;
                        //添加到下一轮
                        queue.Enqueue(PathNode);
                        //如果这个就是终点 跳出
                        if (PathNode.status == PathNode.NODE_End)
                            break;
                    }
                }
            }

            List<PathNode> path = GetPath(eNode);

            return path;
        }

        #endregion

        #region DFS

        /// <summary>
        /// DFS 深度优先算法
        /// </summary>
        /// <param name="startNode">起点</param>
        /// <param name="targetNode">终点</param>
        /// <returns>可抵达路径</returns>
        public static List<PathNode> DFS_Finding(Vector2Int startNode, Vector2Int targetNode)
        {
            //重置地图数据
            ResetMap();
            //标记起点和终点
            PathNode sNode = m_map[startNode[0], startNode[1]];
            sNode.status = PathNode.NODE_START;
            PathNode eNode = m_map[targetNode[0], targetNode[1]];
            if (eNode.status == PathNode.NODE_BLOCK)
            {
                Error();
                return null;
            }
            eNode.status = PathNode.NODE_End;

            DFSRecursion(sNode.X, sNode.Y);

            List<PathNode> path = GetPath(eNode);

            return path;
        }

        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private static void DFSRecursion(int x, int y)
        {
            PathNode currentNode = m_map[x, y];
            currentNode.Visited = true;

            List<PathNode> neighor = GetNeighor(currentNode);

            foreach (PathNode PathNode in neighor)
            {
                if (!PathNode.Visited && PathNode.status != PathNode.NODE_BLOCK)
                {
                    PathNode.Visited = true;
                    PathNode.Parent = currentNode;

                    DFSRecursion(PathNode.X, PathNode.Y);
                }
            }
        }

        #endregion

        #region A星

        private static List<AstarPathNode> m_openList = new List<AstarPathNode>();

        private static List<AstarPathNode> m_closeList = new List<AstarPathNode>();
        public static List<PathNode> AStar_Finding(Vector2Int startNode, Vector2Int targetNode)
        {
            //重置地图数据
            m_openList.Clear();
            m_closeList.Clear();
            ResetMap();
            //标记起点和终点
            AstarPathNode sNode = m_map[startNode[0], startNode[1]] as AstarPathNode;
            sNode.status = AstarPathNode.NODE_START;
            AstarPathNode eNode = m_map[targetNode[0], targetNode[1]] as AstarPathNode;
            eNode.status = AstarPathNode.NODE_End;

            //重置起点 将父节点置空
            sNode.G = 0;
            sNode.H = sNode.GetH(eNode);
            sNode.Parent = null;

            m_openList.Add(sNode);
            while (m_openList.Count > 0)
            {
                //取到开启列表中最小F值的点
                AstarPathNode minNode = GetMinFNode();
                //移到关闭列表
                m_closeList.Add(minNode);
                m_openList.Remove(minNode);

                if (minNode.status == AstarPathNode.NODE_End)
                    break;

                List<PathNode> neighor = GetNeighor(minNode);
                foreach (AstarPathNode PathNode in neighor)
                {
                    if (PathNode.status != AstarPathNode.NODE_BLOCK && !m_openList.Contains(PathNode) && !m_closeList.Contains(PathNode))
                    {
                        //与终点的距离
                        PathNode.H = PathNode.GetH(eNode);
                        //与起点的距离 要加上父节点
                        PathNode.G = PathNode.GetG(minNode) + minNode.G;
                        PathNode.Parent = minNode;

                        m_openList.Add(PathNode);
                    }
                }
            }


            List<PathNode> path = GetPath(eNode);

            return path;

        }

        /// <summary>
        /// 从开启列表中获得F值最小的
        /// </summary>
        private static AstarPathNode GetMinFNode()
        {
            AstarPathNode minNode = null;
            int minFCount = int.MaxValue;

            foreach (AstarPathNode PathNode in m_openList)
            {
                if (PathNode.F < minFCount)
                {
                    minFCount = PathNode.F;
                    minNode = PathNode;
                }
            }

            return minNode;
        }

        #endregion

        private static List<PathNode> GetPath(PathNode eNode)
        {
            //向上递归父节点得出路径
            List<PathNode> path = new List<PathNode>();
            PathNode target = eNode;
            while (target.status != PathNode.NODE_START)
            {
                if (target.Parent == null)
                    break;
                path.Add(target);
                target = target.Parent;
            }

            if (path.Count <= 0)
                Error();

            return path;
        }

        /// <summary>
        /// 获得相邻的节点
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private static List<PathNode> GetNeighor(PathNode currentNode)
        {
            List<PathNode> nodes = new List<PathNode>();
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



        private static void Error()
        {
            Debug.LogError("当前寻路不可达");
        }

    }
}