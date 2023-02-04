using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serpent : MonoBehaviour
{
    public float speed;
    public int m_IndexBlock = 0;

    void Awake()
    {
        // maxY = Camera.main.orthographicSize;
        Vector2 currSize = GetComponent<RectTransform>().sizeDelta;
        maxY = currSize.y;
        
        //
        //btn.onClick.AddListener(OnMouseDown);
    }

    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        //if(btn != null)
            //GameLevel.instance.AddListItems(btn,m_IndexBlock);
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

    //Private
    [SerializeField] private float maxY;

}
