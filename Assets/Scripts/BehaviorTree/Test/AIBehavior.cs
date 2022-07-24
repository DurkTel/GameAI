using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class AIBehavior : BT_Tree
{
    protected override void Init()
    {
        base.Init();

        root = new BT_Selector();

        BT_Sequence bT_Sequence1 = new BT_Sequence();
        BT_Sequence bT_Sequence2 = new BT_Sequence();

        Test_Condition_Idle idleCondition = new Test_Condition_Idle();
        Test_Condition_Run runCondition = new Test_Condition_Run();

        Test_Idle idle = new Test_Idle();
        Test_Run run = new Test_Run();
        bT_Sequence1.Add(runCondition);
        bT_Sequence1.Add(run);

        bT_Sequence2.Add(idleCondition);
        bT_Sequence2.Add(idle);


        root.Add(bT_Sequence1);
        root.Add(bT_Sequence2);
    }
}
