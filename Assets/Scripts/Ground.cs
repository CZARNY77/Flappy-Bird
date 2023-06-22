using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ground : MonoBehaviour
{
    MeshRenderer meshRenderer;
    [SerializeField] bool background;
    [SerializeField] Material materialDay;
    [SerializeField] Material materialNight;
    void Start()
    {
        meshRenderer= GetComponent<MeshRenderer>();
        if (background)
        {
            int hour = DateTime.Now.Hour;
            if (hour > 6 && hour < 19)
                meshRenderer.material = materialDay;
            else
                meshRenderer.material = materialNight;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float speed = GameManager.instance.speedWorld / transform.localScale.x;
        if (background) speed /= 10;
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
