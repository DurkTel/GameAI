using System.Collections;
using System.Collections.Generic;

public class FSM_Transition
{
    /// <summary>
    /// 当前状态
    /// </summary>
    private int m_formStatusID;
    public int formStatusID { get { return m_formStatusID; } }
    /// <summary>
    /// 要切换的状态
    /// </summary>
    private int m_toStatusID;
    public int toStatusID { get { return m_toStatusID; } }
    /// <summary>
    /// 排序权重
    /// </summary>
    private int m_weightOrder;
    public int weightOrder { get { return m_weightOrder; } }
    /// <summary>
    /// 切换条件
    /// </summary>
    public List<IFSM_Condition> conditions = new List<IFSM_Condition>();
    public FSM_Transition(int formStatus, int toStatus, int weightOrder)
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
    /// 是否存在
    /// </summary>
    /// <param name="formStatus"></param>
    /// <param name="toStatus"></param>
    /// <returns></returns>
    public bool Contains(int formStatus, int toStatus)
    {
        return formStatus == m_formStatusID && toStatus == m_toStatusID;
    }
}
