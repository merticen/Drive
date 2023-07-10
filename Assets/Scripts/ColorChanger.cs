using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color[] colors;
    
    void Start()
    {
        StartCoroutine("ColorChange");
    }

    
    void Update()
    {
        
    }

    IEnumerator ColorChange()
    {
        while (true)
        {
            int randColor = Random.Range(0, 5);
            Camera.main.backgroundColor = colors[randColor];
            yield return new WaitForSeconds(10f);
        }
    }
}
