using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// 数据黑板
    /// 一个存放共享数据的地方，用于各个行为节点数据交流
    /// </summary>
    public class BT_DataBase : MonoBehaviour
    {
        //data列表和key列表时一一对应的关系
        private List<object> m_dataBase = new List<object>();

        private List<string> m_dataName = new List<string>();

        /// <summary>
        /// 获取这个key在列表中的id
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
        private int GetIndexOfDataId(string dataName)
        {
            for (int i = 0; i < m_dataName.Count; i++)
            {
                if (m_dataName[i].Equals(dataName))
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// 尝试获取这个key的id 没有就创建一个
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public int TryGetDataId(string dataName)
        {
            int dataId = GetIndexOfDataId(dataName);
            if (dataId == -1)
            {
                m_dataName.Add(dataName);
                m_dataBase.Add(null);
                dataId = m_dataName.Count - 1;
            }
            return dataId;
        }

        /// <summary>
        /// 通过string Key取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T GetData<T>(string name)
        {
            int dataId = GetIndexOfDataId(name);
            if (dataId == -1)
                Debug.LogError("DataBase中不存在name为：" + name + "的key");

            return (T)m_dataBase[dataId];
        }

        /// <summary>
        /// 通过下标取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public T GetData<T>(int dataId)
        { 
            return (T)m_dataBase[dataId];
        }

        /// <summary>
        /// 存放一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataName"></param>
        /// <param name="data"></param>
        public void SetData<T>(string dataName, T data)
        {
            int dataId = TryGetDataId(dataName);
            m_dataBase[dataId] = data;
        }

        /// <summary>
        /// 存放一个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataId"></param>
        /// <param name="data"></param>
        public void SetData<T>(int dataId, T data)
        {
            m_dataBase[dataId] = data;
        }

        /// <summary>
        /// 列表中是否有这个数据
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
        public bool Contains(string dataName)
        {
            return GetIndexOfDataId(dataName) != -1;
        }

    }
}
