using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Serpent : MonoBehaviour
{
    public float ySpeed; //X
    public int m_IndexBlock = 0;
    float ySpeedBf;
    Vector2 translateVec;

    void Awake()
    {
        Vector2 currSize = GetComponent<RectTransform>().sizeDelta;
        maxY = currSize.y;
    }

    void Start()
    {
        m_Button = this.gameObject.GetComponent<Button>();
        SetTranslateVec();
    }

    void SetTranslateVec()
    {
        float xSpeed =  Random.Range(-ySpeed, ySpeed);
        translateVec = new Vector2(Time.deltaTime * xSpeed, - Time.deltaTime * ySpeed);
        ySpeedBf = ySpeed;
    }
    
    void Update()
    {
        if (ySpeedBf != ySpeed) SetTranslateVec();

        transform.Translate( translateVec );
        
        if (transform.position.y < -(maxY + 5)) {
            GameLevel.instance.RemoveListButton(m_Button);
            Destroy(gameObject);
        }
    }
    
    //Private
    [SerializeField] private float maxY;
    private Button m_Button;
}
