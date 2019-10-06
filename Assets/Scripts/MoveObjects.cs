using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{
    [ContextMenu("Move")]
    public void Move()
    {
        float x = 0;
        foreach (Transform child in transform)
        {
            Vector3 pos = Vector3.zero;
            pos.x = (x += 1f);
            child.position = pos;
        }
    }
}
