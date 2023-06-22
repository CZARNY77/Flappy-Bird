using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    CharacterController characterController;
    float gravity = 9.81f;
    [SerializeField] float flyForce;
    [SerializeField] float rotationSpeed = 50f;
    Vector3 direction;
    [SerializeField] float verticalAmplitude = 1f;
    [SerializeField] float verticalFrequency = 5f;
    public Vector3 startPosition;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        direction = Vector3.zero;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.startGame)
        {
            FlyIdle();
            if (Input.GetButtonDown("Fire1"))
            {
                GameManager.instance.startGame = true;
                GameManager.instance.tuturialImage.gameObject.SetActive(false);
            }
        }
        Fly();
    }

    void Fly()
    {
        if (!GameManager.instance.dead && GameManager.instance.startGame)
        {
            direction += Vector3.down * gravity * 2 * Time.deltaTime;
            if (Input.GetButtonDown("Fire1"))
            {
                direction = Vector3.up * flyForce;
                Quaternion flyRotation = Quaternion.Euler(0, 0, 30f);
                transform.rotation = flyRotation;
                rotationSpeed = 50f;
            }
            Quaternion targetRotation = Quaternion.Euler(0, 0, -90f);
            Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = newRotation;
            rotationSpeed += 100f * Time.deltaTime;
            characterController.Move(direction * Time.deltaTime);
        }

    }

    void FlyIdle()
    {
        float verticalOffset = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude * Time.deltaTime;

        Vector3 targetPosition = transform.position + new Vector3(0f, verticalOffset, 0f);

        transform.position = targetPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Ground"))
        {
            GameManager.instance.GameOver();
        }
        if (other.CompareTag("Point"))
        {
            GameManager.instance.AddPoint();
        }
    }
}
