using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Status_Idle : FSM_Status<string>
{
    public override void OnAction()
    {
        Debug.Log("当前是Idle状态");
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入Idle状态");
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("退出Idle状态");
    }
}
