using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Status_Idle : FSM_Status
{
    public override void Action()
    {
        Debug.Log("当前是Idle状态");
    }

    public override void EnterStatus()
    {
        base.EnterStatus();
        //Debug.Log("进入Idle状态");
    }

    public override void ExitStatus()
    {
        base.ExitStatus();
        //Debug.Log("退出Idle状态");
    }
}
