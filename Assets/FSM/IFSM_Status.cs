using System.Collections;
using System.Collections.Generic;

public interface IFSM_Status<TStateID>
{
    TStateID name { get; set; }
    void OnAction();
    void OnEnter();
    void OnExit();
    void Tick();
}

