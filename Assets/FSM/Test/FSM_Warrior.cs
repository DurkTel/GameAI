using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Warrior : FSM_Base
{
    protected override void Init()
    {
        base.Init();

        FSM_Status_Idle idle = new FSM_Status_Idle();
        FSM_Status_Walk walk = new FSM_Status_Walk();
        FSM_Status_Run run = new FSM_Status_Run();


        AddStatus(idle);
        AddStatus(walk);
        AddStatus(run);

        //idle ——> walk
        FSM_Transition IdleToWalk = idle.AddTransition(walk.statusID);
        FSM_Condition_Float floatCondition = new FSM_Condition_Float();
        floatCondition.dataName = "seed";
        floatCondition.target = 0.1f;
        floatCondition.condition = FSM_Condition_Float.FloatCondition.Greater;
        IdleToWalk.AddCondition(floatCondition);


        //idle ——> run
        FSM_Transition IdleToRun = idle.AddTransition(run.statusID,1);
        IdleToRun.AddCondition(new FSM_Condition_Float("seed", 0.1f, FSM_Condition_Float.FloatCondition.Greater));

        //walk ——> idle
        FSM_Transition WalkToIdle = walk.AddTransition(idle.statusID);
        WalkToIdle.AddCondition(new FSM_Condition_Float("seed", 0.1f, FSM_Condition_Float.FloatCondition.Less));

        //run ——> idle
        FSM_Transition RunToIdle = run.AddTransition(idle.statusID);
        RunToIdle.AddCondition(new FSM_Condition_Float("seed", 0.1f, FSM_Condition_Float.FloatCondition.Less));

    }
}
