using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public FSM_StateMachine FSM;
    void Start()
    {
        FSM = new FSM_StateMachine();
        FSM_DataBase dataBase = gameObject.AddComponent<FSM_DataBase>();
        FSM.dataBase = dataBase;

        FSM_Status idle = new FSM_Status((p) => { print("进入待机"); }, (p) => { print("退出待机"); }, (p) => { print("待机中"); });
        FSM_Status walk = new FSM_Status((p) => { print("进入走路"); }, (p) => { print("退出走路"); }, (p) => { print("走路中"); });
        FSM_Status run = new FSM_Status((p) => { print("进入跑步"); }, (p) => { print("退出跑步"); }, (p) => { print("跑步中"); });

        FSM_StateMachine subNormalFSM = new FSM_StateMachine();
        FSM_StateMachine subMoveFSM = new FSM_StateMachine();

        subNormalFSM.AddStatus("idle", idle);

        subMoveFSM.AddStatus("walk", walk);
        subMoveFSM.AddStatus("run", run);

        FSM.AddStatus("normal", subNormalFSM);
        FSM.AddStatus("move", subMoveFSM);

        FSM_Transition<string> transition1 = new FSM_Transition<string>("normal", "move");
        transition1.AddCondition(new FSM_Condition_Booler("w", FSM_Condition_Booler.BoolerCondition.True));

        FSM_Transition<string> transition2 = new FSM_Transition<string>("move", "normal");
        transition2.AddCondition(new FSM_Condition_Booler("w", FSM_Condition_Booler.BoolerCondition.False));

        FSM_Transition<string> transition3 = new FSM_Transition<string>("walk", "run");
        transition3.AddCondition(new FSM_Condition_Booler("d", FSM_Condition_Booler.BoolerCondition.True));

        FSM_Transition<string> transition4 = new FSM_Transition<string>("run", "walk");
        transition4.AddCondition(new FSM_Condition_Booler("d", FSM_Condition_Booler.BoolerCondition.False));

        subMoveFSM.AddTransition(transition3);
        subMoveFSM.AddTransition(transition4);

        FSM.AddTransition(transition1);
        FSM.AddTransition(transition2);

        FSM.OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Mathf.Abs(Input.GetAxis("Horizontal"));
        float ver = Mathf.Abs(Input.GetAxis("Vertical"));

        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool d = Input.GetKey(KeyCode.D);

        //FSM.dataBase.SetData<float>("seed", (hor + ver));
        FSM.dataBase.SetData<bool>("w", w);
        FSM.dataBase.SetData<bool>("a", a);
        FSM.dataBase.SetData<bool>("d", d);


        FSM.OnAction();
    }
}
