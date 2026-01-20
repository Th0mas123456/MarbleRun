using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float vel = 2;
    Rigidbody2D rigBody;
    float t;

    // Start is called before the first frame update
    void Start()
    {
       rigBody = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (Input.touchCount > 0)
        {

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;


            Vector2 direction = (mouseWorldPos - transform.position).normalized;

            if (t < 5)
            {
                rigBody.velocity = (direction * vel);
            }
        }
        else
        {
            if (t > 5)
            {
                rigBody.velocity = new Vector2(0f, 0f);
                t = 0;
            }
        }
    }
}
