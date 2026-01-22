using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float vel = 2f;
    const float moveDuration = 5f;

    Rigidbody2D rigBody;
    float timer;
    bool isMoving;
    Vector2 moveDirection;
    AudioSource PlayerRollSFX;

    void Start()
    {
        rigBody = GetComponent<Rigidbody2D>();
        PlayerRollSFX = GameObject.Find("PlayerRollSFX").GetComponent<AudioSource>();
    }

    void Update()
    {
        rollNoise();
        //touchControl();
        tiltControl();
    }

    void rollNoise()
    {
        if (PlayerRollSFX.isPlaying && !isMoving)
        {
            PlayerRollSFX.Stop();
        }
        else if (!PlayerRollSFX.isPlaying && isMoving)
        {
            PlayerRollSFX.Play();
        }
    }

    void touchControl() 
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(touch.position);
                worldPos.z = 0f;

                moveDirection = (worldPos - transform.position).normalized;

                isMoving = true;
                timer = 0f;
            }
        }

        else if (isMoving)
        {
            if (timer < moveDuration)
            {
                rigBody.velocity = moveDirection * vel;
                timer += Time.deltaTime;
            }
            else
            {

                if (timer < 8f)
                {
                    rigBody.velocity = (moveDirection * vel) / 2;
                    timer += Time.deltaTime;
                }
                else
                {
                    rigBody.velocity = Vector2.zero;
                    isMoving = false;
                }

            }
        }
    }

    void tiltControl()
    {
        Vector2 direction = Vector2.zero;

        direction.x = Input.acceleration.x;
        direction.y = Input.acceleration.y;

        rigBody.velocity = direction * vel;
        //transform.Translate(direction * vel);
    }
}