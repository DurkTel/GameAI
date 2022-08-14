using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Status_Run : FSM_Status<string>
{
    public override void OnAction()
    {
        Debug.Log("当前是Run状态");
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("进入Run状态");
        dataBase.GetComponent<Animator>().SetBool("run", true);
    }

    public override void OnExit()
    {
        base.OnExit();
        dataBase.GetComponent<Animator>().SetBool("run", false);
        Debug.Log("退出Run状态");
    }
}
