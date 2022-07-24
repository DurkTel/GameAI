using System;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 并行选择节点
    /// 并发执行它的所有孩子节点
    ///（一假则假，全真则真）
    /// </summary>
    public class BT_ParallelSelector : BT_Parallel
    {
        private List<BT_Node> m_pWaitNodes;

        private bool m_pIsFail;

        public BT_ParallelSelector()
        {
            m_pWaitNodes = new List<BT_Node>();
            m_pIsFail = false;
        }

        private void Reset()
        {
            m_pWaitNodes.Clear();
            m_pIsFail = false;
        }

        private BT_Result CheckResult()
        {
            return m_pIsFail ? BT_Result.FAIL : BT_Result.SUCCESSFUL;
        }

        public override BT_Result Tick()
        {
            if (children == null || children.Count == 0)
                return BT_Result.SUCCESSFUL;

            BT_Result result = BT_Result.NONE;
            List<BT_Node> waitNodes = new List<BT_Node>();
            List<BT_Node> mainNodes = new List<BT_Node>();
            //必须等待所有节点都为True
            mainNodes = m_pWaitNodes.Count > 0 ? m_pWaitNodes : children;
            for (int i = 0; i < mainNodes.Count; i++)
            {
                result = mainNodes[i].Tick();
                switch (result)
                {
                    case BT_Result.SUCCESSFUL:
                        break;
                    case BT_Result.RUNING:
                        waitNodes.Add(mainNodes[i]);
                        break;
                    default:
                        m_pIsFail = true;
                        break;
                }
            }

            //如果有等待节点就返回等待
            if (waitNodes.Count > 0)
            {
                m_pWaitNodes = waitNodes;
                return BT_Result.RUNING;
            }

            result = CheckResult();
            Reset();
            return result;
        }
    }
}
