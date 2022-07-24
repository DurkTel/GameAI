using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class BT_Tree : MonoBehaviour
    {
        protected BT_Node root;

        public bool isRunning = true;

        [HideInInspector] public BT_DataBase dataBase;

        private void Awake()
        {
            Init();

            root.Activate(dataBase);
        }

        private void Update()
        {
            if (!isRunning) return;

            if (root.Evaluate())
            {
                root.Tick();
            }
        }

        protected virtual void Init()
        {
            if (!TryGetComponent(out dataBase))
            {
                dataBase = gameObject.AddComponent<BT_DataBase>();
            }
        }
    }
}