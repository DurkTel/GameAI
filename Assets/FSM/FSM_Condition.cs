using System.Collections;
using System.Collections.Generic;

public class FSM_Condition
{
    /// <summary>
    /// 返回条件是否满足
    /// </summary>
    /// <returns></returns>
    public virtual bool Tick()
    {
        return false;
    }
}
