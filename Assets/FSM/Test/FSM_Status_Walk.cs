using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Status_Walk : FSM_Status
{
    public override void Action()
    {
        Debug.Log("当前是Walk状态");
    }

    public override void EnterStatus()
    {
        base.EnterStatus();
        //Debug.Log("进入Walk状态");
        dataBase.GetComponent<Animator>().SetBool("walk", true);

    }

    public override void ExitStatus()
    {
        base.ExitStatus();
        dataBase.GetComponent<Animator>().SetBool("walk", false);
        //Debug.Log("退出Walk状态");
    }
}
