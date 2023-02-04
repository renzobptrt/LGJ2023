using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    
    //Elements
    public Button m_ButtonLestGo = null;

    void Start()
    {
        m_ButtonLestGo.onClick.RemoveAllListeners();
        m_ButtonLestGo.onClick.AddListener(StartCanvaReady);
        //StartCanvaReady();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartCanvaReady()
    {
        CanvasGroup canvas = m_PanelUI.GetComponent<CanvasGroup>();
        canvas.blocksRaycasts = true;
        canvas.DOFade(0f,0.5f).OnComplete(()=>{
            canvas.blocksRaycasts = false;
        });
    }

    void StartGameSubLevel()
    {

    }

    [SerializeField] private RectTransform m_PanelUI = null;
}
