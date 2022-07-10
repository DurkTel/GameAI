using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public static int NODE_START = 1;
    public static int NODE_End = 2;
    public static int NODE_BLOCK = 3;

    public int X;

    public int Y;

    public bool Visited;

    public int status;

    public PathNode Parent;

    public void SetData(int x, int y, int status)
    {
        X = x;
        Y = y;
        Parent = null;
        Visited = false;
        this.status = status;
    }
}

public class AstarPathNode : PathNode
{
    /// <summary>
    /// 与终点的距离
    /// </summary>
    public int H;
    /// <summary>
    /// 与起点的距离
    /// </summary>
    public int G;
    /// <summary>
    /// 总距离 越小越优
    /// </summary>
    public int F { get { return H + G; } }

    /// <summary>
    /// 获得H值
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public int GetH(AstarPathNode target)
    {
        if (target == null)
            return 0;

        int a = Mathf.Abs(X - target.X);
        int b = Mathf.Abs(Y - target.Y);

        return (int)Mathf.Sqrt(a * a + b * b) * 10;
    }

    /// <summary>
    /// 获得G值
    /// </summary>
    /// <param name="current"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public int GetG(AstarPathNode target)
    {
        if (target == null)
            return 0;

        if (X == target.X && Y == target.Y)
            return 0;

        if (Y == target.Y || X == target.Y)
            return 10;

        return 14;
    }
}
