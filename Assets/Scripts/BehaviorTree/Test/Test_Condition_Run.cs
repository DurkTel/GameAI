using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class Test_Condition_Run : BT_Condition
{
    public override BT_Result Tick()
    {
        return BT_Result.RUNING;
    }
}
