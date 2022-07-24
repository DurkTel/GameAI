using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Test_Idle : BT_Action
{
    public override BT_Result Tick()
    {
        Debug.Log("执行Idle");
        return BT_Result.SUCCESSFUL;
    }
}
