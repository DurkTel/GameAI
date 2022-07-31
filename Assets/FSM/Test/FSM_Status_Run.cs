using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Status_Run : FSM_Status
{
    public override void Action()
    {
        Debug.Log("当前是Run状态");
    }

    public override void EnterStatus()
    {
        base.EnterStatus();
        Debug.Log("进入Run状态");
        dataBase.GetComponent<Animator>().SetBool("run", true);
    }

    public override void ExitStatus()
    {
        base.ExitStatus();
        dataBase.GetComponent<Animator>().SetBool("run", false);
        Debug.Log("退出Run状态");
    }
}
