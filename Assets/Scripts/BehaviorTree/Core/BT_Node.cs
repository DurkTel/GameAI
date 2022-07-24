using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 行为树父节点
    /// 任何节点执行后，必须向其父节点报告执行结果：成功/失败
    /// 报告的结果用于控制整棵树的决策方向
    /// 具体分为组合节点、装饰节点、条件节点、行为节点
    /// </summary>
    public abstract class BT_Node
    {
        /// <summary>
        /// 节点昵称
        /// </summary>
        public string name;

        /// <summary>
        /// 行为树的数据
        /// </summary>
        public BT_DataBase dataBase;
        
        /// <summary>
        /// 下一次执行的间隔
        /// </summary>
        public float interval = 0f;

        /// <summary>
        /// 上一次执行的时间
        /// </summary>
        private float m_lastTickTime = 0f;

        /// <summary>
        /// 该节点是否激活
        /// </summary>
        public bool activated;

        /// <summary>
        /// 节点的子树
        /// </summary>
        protected List<BT_Node> m_children;
        public List<BT_Node> children { get { return m_children; } }

        /// <summary>
        /// 激活该节点以及所有子节点
        /// </summary>
        /// <param name="dataBase"></param>
        public virtual void Activate(BT_DataBase dataBase = null)
        {
            if (activated) return;

            this.dataBase = dataBase;

            if (m_children != null)
            {
                foreach (BT_Node child in m_children)
                {
                    child.Activate(dataBase);
                }
            }

            activated = true;
        }

        /// <summary>
        /// 评估是否能进入该节点
        /// </summary>
        /// <returns></returns>
        public virtual bool Evaluate()
        {
            //该节点未激活
            if (!activated)
                return false;

            //未冷却
            if (Time.time - m_lastTickTime > interval)
            {
                m_lastTickTime = Time.time;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 执行节点
        /// </summary>
        /// <returns>返回执行结果</returns>
        public virtual BT_Result Tick()
        {
            return BT_Result.NONE;
        }

        public virtual void Add(BT_Node node)
        {
            if (m_children == null)
                m_children = new List<BT_Node>();

            if (node != null)
                m_children.Add(node);
        }

        public virtual void Remove(BT_Node node)
        {
            if (m_children != null && node != null)
                m_children.Remove(node);
        }
    }

    /// <summary>
    /// 行为树的执行结果
    /// </summary>
    public enum BT_Result
    { 
        NONE,
        SUCCESSFUL, //成功
        FAIL, //失败
        RUNING, //进行中
    }
}