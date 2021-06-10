using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerAnimation : MonoBehaviour
{
    public Vector2 position;
    private void Start()
    {
        Point();
    }
    public void Point()
    {
        transform.LeanMoveLocal(position, 0.5f).setEaseInCubic().setLoopPingPong();
    }
}
