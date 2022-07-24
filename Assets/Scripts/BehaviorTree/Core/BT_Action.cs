using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// 行为节点
    /// 完成具体的一次（或一个步骤）的行为
    /// 叶子节点 没有子节点
    /// </summary>
    public class BT_Action : BT_Node
    {
        public override void Add(BT_Node node)
        {
            Debug.LogError("行为节点不能操作子节点");
        }

        public override void Remove(BT_Node node)
        {
            Debug.LogError("行为节点不能操作子节点");
        }
    }
}
