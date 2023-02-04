using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serpent : MonoBehaviour
{
    public float speed;
    //TODO: get current
    
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        
        if (transform.position.y < -(Camera.main.orthographicSize + 5)) {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
            Destroy(gameObject);
    }
}
