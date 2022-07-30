using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Base : MonoBehaviour
{

    public FSM_Status currentStatus;

    public FSM_Status lastStatus;

    public List<FSM_Status> allStatus = new List<FSM_Status>();

    void Start()
    {
        
    }

    void Update()
    {
        if (currentStatus.Tick(out int newStatusID))
        {
            ChangeState(newStatusID);
        }

        currentStatus.Action();
    }

    public void ChangeState(int newStatusID)
    {
        currentStatus.ExitStatus();
        lastStatus = currentStatus;
        currentStatus = allStatus.Find(p => p.statusID == newStatusID);
        currentStatus.EnterStatus();
    }
}
