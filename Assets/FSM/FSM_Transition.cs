using System.Collections;
using System.Collections.Generic;

public class FSM_Transition<TStateId>
{
    /// <summary>
    /// 当前状态
    /// </summary>
    private TStateId m_formStatusID;
    public TStateId formStatusID { get { return m_formStatusID; } }
    /// <summary>
    /// 要切换的状态
    /// </summary>
    private TStateId m_toStatusID;
    public TStateId toStatusID { get { return m_toStatusID; } }
    /// <summary>
    /// 排序权重
    /// </summary>
    private int m_weightOrder;
    public int weightOrder { get { return m_weightOrder; } }
    /// <summary>
    /// 切换条件
    /// </summary>
    public List<IFSM_Condition> conditions = new List<IFSM_Condition>();
    public FSM_Transition(TStateId formStatus, TStateId toStatus, int weightOrder = 0)
    {
        m_formStatusID = formStatus;
        m_toStatusID = toStatus;
        m_weightOrder = weightOrder;
    }
    /// <summary>
    /// 添加切换条件
    /// </summary>
    /// <param name="condition"></param>
    public void AddCondition(IFSM_Condition condition)
    {
        if (conditions.Contains(condition))
            return;

        conditions.Add(condition);

    }
    /// <summary>
    /// 刷新这条过渡线
    /// </summary>
    /// <returns>是否连通（条件满足可切换）</returns>
    public bool Tick(FSM_DataBase dataBase)
    {
        for (int i = 0; i < conditions.Count; i++)
        {
            if (!conditions[i].Tick(dataBase))
                return false;
        }

        return true;
    }
    /// <summary>
    /// 是否相同
    /// </summary>
    /// <param name="formStatus"></param>
    /// <param name="toStatus"></param>
    /// <returns></returns>
    public bool Equals(TStateId formStatus, TStateId toStatus)
    {
        return EqualityComparer<TStateId>.Default.Equals(formStatus, toStatus);
    }
}

public class FSM_Transition : FSM_Transition<string>
{ 
    public FSM_Transition(string formStatus, string toStatus, int weightOrder = 0) : base(formStatus, toStatus, weightOrder)
    {
    
    }

}
