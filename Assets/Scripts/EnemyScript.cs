using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    public float distance;

    private bool movingRight;

    public Transform groundDetector;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        //Raycast - это луч, который строиться из нашего GroundDetector-а
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetector.position, Vector2.down, distance);

        // Когда луч не находит землю под собой, тот сразу разворачивает персонажан на 180 градусов по y
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}
