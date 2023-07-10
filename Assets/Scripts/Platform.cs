using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public GameObject diamond;
    public GameObject nitro;
    private float distance = 1f;
    void Start()
    {
        int randDiamond = Random.Range(0, 5);

        Vector3 diamondPos = transform.position;
        diamondPos.y += 1f;

        if (randDiamond < 1)
        {
            GameObject diamondInstace = Instantiate(diamond, diamondPos, diamond.transform.rotation);
            diamondInstace.transform.SetParent(gameObject.transform);
            
        }

        int randNitro = Random.Range(0, 50);

        Vector3 nitroPos = transform.position;
        nitroPos.y = 1f;
        nitroPos.z = diamondPos.z + distance;

        if (randNitro < 1)
        {
            GameObject nitroInstance = Instantiate(nitro, nitroPos, nitro.transform.rotation);
            nitroInstance.transform.SetParent(gameObject.transform);
        }


    }

    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            Invoke("Fall", 0.2f);
        }
    }
    void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 1f);
    }
}
