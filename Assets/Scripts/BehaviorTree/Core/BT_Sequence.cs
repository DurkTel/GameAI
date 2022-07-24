using System;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 顺序节点
    /// 执行本节点时，将从头到尾迭代执行自己的孩子节点
    /// 当遇到一个孩子节点执行后返回False，将停止迭代，本节点也向自己的父节点返回False
    /// 否则所有孩子节点都返回True，本节点也向自己的父节点返回True
    ///（一假则假，全真则真）
    /// </summary>
    public class BT_Sequence : BT_Node
    {
        private int m_index;

        public BT_Sequence()
        {
            Reset();
        }

        private void Reset()
        {
            m_index = 0;
        }

        public override BT_Result Tick()
        {
            if (children == null || children.Count == 0)
                return BT_Result.SUCCESSFUL;

            if (m_index >= children.Count)
                Reset();

            //依次执行每条分支 对分支的结果进行判断
            BT_Result result = BT_Result.NONE;
            for (int length = children.Count; m_index < length; ++m_index)
            {
                result = children[m_index].Tick();

                if (result == BT_Result.FAIL)
                {
                    Reset();
                    return result;
                }
                else if (result == BT_Result.RUNING)
                    return result;
                else
                    continue;
            }

            Reset();
            return BT_Result.SUCCESSFUL;
        }
    }
}
