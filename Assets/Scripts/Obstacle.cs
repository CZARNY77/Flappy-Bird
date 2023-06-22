using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float leftEnge;
    Transform[] obstacles;
    void Start()
    {
        leftEnge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        obstacles = transform.GetComponentsInChildren<Transform>();
        float points = GameManager.instance.GetPoint();

        points = points <= 125? points/100: 1.25f;
        foreach(Transform t in obstacles)
        {
            float newY = t.position.y >= 0 ? t.position.y - points : t.position.y + points;
            t.position = new Vector3(t.position.x, newY, t.position.z);
        }
        
    }
    void Update()
    {
        transform.position += Vector3.left * GameManager.instance.speedWorld * Time.deltaTime;
        if (transform.position.x < leftEnge) Destroy(gameObject);
    }
}
