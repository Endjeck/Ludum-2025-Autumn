using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RAINMOVER : MonoBehaviour
{
    public Transform Player;
    public float Offset;


    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position + new Vector3(0, Offset, 0);
    }
}
