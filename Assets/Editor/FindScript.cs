using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FindScript
{
    [MenuItem("查找所有组件/Start")]
    static void Find()
    {
        string prefabPath = "Assets/Prefabs";
        if (Directory.Exists(prefabPath))
        {
            string[] allPath = Directory.GetFiles(prefabPath, "*.prefab", SearchOption.AllDirectories);
            for (int i = 0; i < allPath.Length; i++)
            {
                string path = allPath[i].Replace('\\', '/');
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                Animator[] animators = prefab.GetComponentsInChildren<Animator>();
                if (animators.Length != 0)
                {
                    foreach (Animator animator in animators)
                    {
                        if (animator.applyRootMotion)
                        {
                            Debug.Log(animator.gameObject.name);
                            break;
                        }
                    }
                }
            }
        }
    }
}
