using System;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 装饰节点
    /// 修饰判断 “直到……成功” || “直到……失败” 类似于Until
    /// </summary>
    public class BT_Decorator : BT_Node
    {
        private BT_Node m_child;

        public BT_Decorator()
        {
            m_child = null;
        }

        protected void SetChild(BT_Node node)
        {
            m_child = node;
        }
    }
}
