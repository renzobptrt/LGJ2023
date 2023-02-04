using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serpent : MonoBehaviour
{
    public float speed;
    //TODO: get current
    [SerializeField] float maxY;

    void Awake()
    {
        // maxY = Camera.main.orthographicSize;
        Vector2 currSize = GetComponent<RectTransform>().sizeDelta;
        maxY = currSize.y;
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnMouseDown);
    }
    
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        
        if (transform.position.y < -(maxY + 5)) {
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
