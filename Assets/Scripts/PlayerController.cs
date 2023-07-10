using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject pickEffect;
    bool moveLeft;

    public float moveSpeed;
    public float increaseAmount = 0.1f;
    public float increaseInterval = 10f;
    private float timer = 0f;

    public float nitroBoostMultiplier = 0.1f;
    public float nitroBoostDuration = 2f;
    private bool isBoostActive = false;


    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.instance.gameStarted)
        {
            Move();
            CheckInput();
        }

        if (transform.position.y <= -2)
        {
            GameManager.instance.GameOver();
        }
        
    }

    void Move()
    {
        timer += Time.deltaTime;
        if (timer >= increaseInterval)
        {
            moveSpeed += increaseAmount;
            timer = 0f;
        }
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    void CheckInput()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            ChangeDirection();
        }
    }
    void ChangeDirection()
    {
        if (moveLeft)
        {
            moveLeft = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            moveLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            GameManager.instance.IncrementScore();

            Instantiate(pickEffect, other.transform.position, pickEffect.transform.rotation);
            
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Nitro")
        {
            ApplyNitroBoost();
            Instantiate(pickEffect, other.transform.position, pickEffect.transform.rotation);

            Destroy(other.gameObject);
        }
        
    }

    

    void ApplyNitroBoost()
    {
        if (!isBoostActive)
        {
            isBoostActive = true;
            GameManager.instance.NitroScore();
            moveSpeed *= nitroBoostMultiplier;
            StartCoroutine(DisableNitroBoost());
        }
    }

    IEnumerator DisableNitroBoost()
    {
        yield return new WaitForSeconds(nitroBoostDuration);
        moveSpeed /= nitroBoostMultiplier;
        isBoostActive = false;
    }

   
}
