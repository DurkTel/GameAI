using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Navigation2D : MonoBehaviour
    {
        private int m_width;
        public int width 
        { 
            get 
            { 
                return m_width; 
            } 
            set 
            {
                if (m_width != value && value > 0)
                { 
                    m_width = value;
                    InitMap();
                }
            } 
        }

        private int m_length;
        public int length 
        {
            get
            {
                return m_length;
            }
            set 
            {
                if (m_length != value && value > 0)
                {
                    m_length = value;
                    InitMap();
                }
            } 
        }

        private float m_nodeSize;
        public float nodeSize 
        { 
            get 
            { 
                return m_nodeSize;
            } 
            set 
            {
                if (value > 0)
                {
                    m_nodeSize = value;
                }
            } 
        }

        private PathNode[,] m_map;
        public PathNode[,] map { get { return m_map; } }


        /// <summary>
        /// 初始化地图
        /// </summary>
        /// <param name="map"></param>
        public void InitMap()
        {
            m_map = new PathNode[m_width, m_length];
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_length; y++)
                {
                    PathNode node = new AstarPathNode();
                    node.SetData(x, y, PathNode.NODE_NONE);
                    m_map[x, y] = node;
                }
            }
        }

        public void SetData(int x, int y, int value)
        {
            if (m_width <= x || m_length <= y)
                return;

            m_map[x, y].status = value;
        }


    }
}
