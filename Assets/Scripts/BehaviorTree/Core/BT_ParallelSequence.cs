using System;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 并行顺序节点
    /// 并发执行它的所有孩子节点
    ///（一真则真，全假则假）
    /// </summary>
    public class BT_ParallelSequence : BT_Parallel
    {
        private List<BT_Node> m_pWaitNodes;

        private bool m_pIsSuccess;

        public BT_ParallelSequence()
        {
            m_pWaitNodes = new List<BT_Node>();
            m_pIsSuccess = false;
        }

        private void Reset()
        {
            m_pWaitNodes.Clear();
            m_pIsSuccess = false;
        }

        private BT_Result CheckResult()
        {
            return m_pIsSuccess ? BT_Result.SUCCESSFUL : BT_Result.FAIL;
        }

        public override BT_Result Tick()
        {
            if (children == null || children.Count == 0)
                return BT_Result.SUCCESSFUL;

            BT_Result result = BT_Result.NONE;
            List<BT_Node> waitNodes = new List<BT_Node>();
            List<BT_Node> mainNodes = new List<BT_Node>();
            mainNodes = m_pWaitNodes.Count > 0 ? m_pWaitNodes : children;
            for (int i = 0; i < mainNodes.Count; i++)
            {
                result = mainNodes[i].Tick();
                switch (result)
                {
                    case BT_Result.SUCCESSFUL:
                        m_pIsSuccess = true;
                        break;
                    case BT_Result.RUNING:
                        waitNodes.Add(mainNodes[i]);
                        break;
                    default:
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
