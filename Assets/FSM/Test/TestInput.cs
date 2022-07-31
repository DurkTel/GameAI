using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public FSM_Base FSM;
    void Start()
    {
        FSM = GetComponent<FSM_Base>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Mathf.Abs(Input.GetAxis("Horizontal"));
        float ver = Mathf.Abs(Input.GetAxis("Vertical"));
        
        FSM.dataBase.SetData<float>("seed", (hor + ver));
    }
}
