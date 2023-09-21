using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Transform Start;
    private Transform End;
    public Node(Transform Start,Transform End)
    {
        this.Start = Start;
        this.End = End;
    }
    public Transform getStart()
    {
        return Start;
    }
    public Transform getEnd()
    {
        return End;
    }
}
