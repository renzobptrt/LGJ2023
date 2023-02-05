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

        Vector2 currSize = GetComponent<RectTransform>().sizeDelta;
        maxY = currSize.y;
    }

    void Start()
    {
        m_Button = this.gameObject.GetComponent<Button>();
    }
    
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        
        if (transform.position.y < -(maxY + 5)) {
            GameLevel.instance.RemoveListButton(m_Button);
            Destroy(gameObject);
        }
    }
    
    //Private
    [SerializeField] private float maxY;
    private Button m_Button;
}
