using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Base : MonoBehaviour
{
    /// <summary>
    /// 当前状态
    /// </summary>
    public FSM_Status currentStatus;
    /// <summary>
    /// 上次状态
    /// </summary>
    public FSM_Status lastStatus;
    /// <summary>
    /// 所以的状态
    /// </summary>
    public List<FSM_Status> allStatus = new List<FSM_Status>();
    /// <summary>
    /// 状态机的数据黑板
    /// </summary>
    public FSM_DataBase dataBase;
    /// <summary>
    /// 状态UID
    /// </summary>
    private int Uid;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        Tick();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    protected virtual void Init()
    {
        if (!TryGetComponent(out dataBase))
        {
            dataBase = gameObject.AddComponent<FSM_DataBase>();
        }
    }
    /// <summary>
    /// 刷新状态机
    /// </summary>
    private void Tick()
    {
        int newStatusID = currentStatus.Tick();
        if (newStatusID != -1)
        {
            ChangeState(newStatusID);
        }

        currentStatus.Action();
    }
    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="newStatusID"></param>
    public void ChangeState(int newStatusID)
    {
        currentStatus.ExitStatus();
        lastStatus = currentStatus;
        currentStatus = allStatus.Find(p => p.statusID == newStatusID);
        currentStatus.EnterStatus();
    }

    protected void AddStatus(FSM_Status status)
    {
        if (!allStatus.Contains(status))
        {
            allStatus.Add(status);
            status.Activate(dataBase);
            status.statusID = ++Uid;

            if (currentStatus == null)
                currentStatus = status;
        }
    }
}
