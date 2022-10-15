using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public float speed = 2.5f;
    float startX;
    public float distance = 5;
    float addX;
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
       addX = Mathf.PingPong(Time.time * speed, distance);
       transform.position = new Vector3(startX + addX, transform.position.y, transform.position.z);
    }
}
