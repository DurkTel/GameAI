using System;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 选择节点
    /// 执行本节点时，将从头到尾迭代执行自己的孩子节点
    /// 当遇到一个孩子节点执行后返回True，将停止迭代，本节点也向自己的父节点返回True
    /// 否则所有孩子节点都返回False，本节点也向自己的父节点返回False
    /// (一真则真，全假则假)
    /// </summary>
    public class BT_Selector : BT_Node
    {
        private int m_index;

        public BT_Selector()
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

                if (result == BT_Result.SUCCESSFUL)
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
            return BT_Result.FAIL;
        }

    }
}
