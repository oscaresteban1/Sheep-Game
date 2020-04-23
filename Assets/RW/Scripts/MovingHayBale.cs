using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHayBale : MonoBehaviour
{
    public Vector3 speed;
    public Space space;


    void Update()
    {
        transform.Translate(speed * Time.deltaTime, space);
    }
}
