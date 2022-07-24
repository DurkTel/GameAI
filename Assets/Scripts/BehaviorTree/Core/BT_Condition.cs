using System;
using System.Collections.Generic;

namespace AI
{
    /// <summary>
    /// 条件节点
    /// 满足某个条件时就可以继续执行
    /// 一般只有True和False 不会有Running状态
    /// </summary>
    public class BT_Condition : BT_Node
    {
        public override BT_Result Tick()
        {
            return BT_Result.FAIL;
        }
    }
}
