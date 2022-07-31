using System.Collections;
using System.Collections.Generic;

public interface IFSM_Condition
{
    string dataName { get; set; }
    bool Tick(FSM_DataBase dataBase);
}

/// <summary>
/// 通用条件判断
/// </summary>
/// <typeparam name="T">条件判断类型</typeparam>
public class FSM_Condition<T> : IFSM_Condition
{
    /// <summary>
    /// 判断该条件所需数据名称
    /// </summary>
    public string dataName { get; set; }
    /// <summary>
    /// 条件数据的预期值
    /// </summary>
    public T target { get; set; }
    /// <summary>
    /// 当前条件数据的值
    /// </summary>
    public T curData { get; set; }

    public FSM_Condition() { }

    public FSM_Condition(string dataName, T target)
    {
        this.dataName = dataName;
        this.target = target;
    }
    /// <summary>
    /// 返回条件是否满足
    /// </summary>
    /// <returns></returns>
    public virtual bool Tick(FSM_DataBase dataBase)
    {
        curData = dataBase.GetData<T>(dataName);

        return false;
    }
}

/// <summary>
/// 浮点类型的限制
/// </summary>
public class FSM_Condition_Float : FSM_Condition<float>
{
    public enum FloatCondition
    {
        Greater,
        Less
    }
    public FloatCondition condition { get; set; }
    public FSM_Condition_Float() { }
    public FSM_Condition_Float(string dataName, float target, FloatCondition condition) : base(dataName, target) { this.condition = condition; }

    public override bool Tick(FSM_DataBase dataBase)
    {
        base.Tick(dataBase);
        switch (condition)
        {
            case FloatCondition.Greater:
                return curData > target;
            case FloatCondition.Less:
                return curData < target;
        }

        return false;
    }
}

/// <summary>
/// 整型的限制
/// </summary>
public class FSM_Condition_Int : FSM_Condition<int>
{
    public enum IntCondition
    {
        Greater,
        Less,
        Equals,
        NotEquals
    }
    public IntCondition condition { get; set; }
    public FSM_Condition_Int() { }
    public FSM_Condition_Int(string dataName, int target, IntCondition condition) : base(dataName, target) { this.condition = condition; }

    public override bool Tick(FSM_DataBase dataBase)
    {
        base.Tick(dataBase);
        switch (condition)
        {
            case IntCondition.Greater:
                return curData > target;
            case IntCondition.Less:
                return curData < target;
            case IntCondition.Equals:
                return curData == target;
            case IntCondition.NotEquals:
                return curData != target;
        }

        return false;
    }
}


/// <summary>
/// 布尔的限制
/// </summary>
public class FSM_Condition_Booler : FSM_Condition<bool>
{
    public enum BoolerCondition
    {
        True,
        False
    }
    public BoolerCondition condition { get; set; }
    public FSM_Condition_Booler() { }
    public FSM_Condition_Booler(string dataName, BoolerCondition condition)
    {
        this.dataName = dataName;
        this.condition = condition;
    }

    public override bool Tick(FSM_DataBase dataBase)
    {
        base.Tick(dataBase);
        switch (condition)
        {
            case BoolerCondition.True:
                return curData == true;
            case BoolerCondition.False:
                return curData == false;
        }

        return false;
    }
}


/// <summary>
/// 触发器的限制
/// </summary>
public class FSM_Condition_Trigger : FSM_Condition<bool>
{
    public FSM_Condition_Trigger() { }
    public FSM_Condition_Trigger(string dataName)
    {
        this.dataName = dataName;
    }

    public override bool Tick(FSM_DataBase dataBase)
    {
        base.Tick(dataBase);
        dataBase.SetData<bool>(dataName, false);
        return curData;
    }
}