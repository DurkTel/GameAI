using System;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 并行节点
    /// 同时执行多个节点，分为并行选择节点（Parallel Selector Node）、并行顺序节点（Parallel Sequence Node）
    /// 提供了并发，提高性能 不需要向选择节点和顺序节点那样判断那个孩子该摆前，哪个该摆后
    /// 常用于：1 用于并行多棵Action子树 2 在并行节点下挂一棵子树，并挂上多个Condition Node
    /// </summary>
    public class BT_Parallel : BT_Node
    {
        public BT_Parallel()
        {

        }
    }
}
