using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static Callbacks;

public class GameManager : MonoBehaviour
{
    
    //Elements
    public Button m_ButtonLestGo = null;
    [SerializeField] private TextMeshProUGUI m_TextCount = null;

    void Start()
    {
        m_ButtonLestGo.onClick.RemoveAllListeners();
        m_ButtonLestGo.onClick.AddListener(StartCanvaReady);
        canvas =  m_PanelUI.GetComponent<CanvasGroup>();
        //StartCanvaReady();
    }

    void StartCanvaReady()
    {
        //Transform parent = m_TextToShowReference.transform.parent.transform.parent;
        //parent.GetComponent<RectTransform>().DOAnchorPosY(200,4f,true).SetDelay(2f);
        m_ButtonLestGo.interactable = false;
        canvas.DOFade(1f,0.5f).OnComplete(()=>{
            AnimationCount(3);
        });
    }

    void AnimationCount(int numnber)
    {
        if(numnber>0)
        {
            m_TextCount.text = numnber.ToString();
            m_TextCount.transform.GetComponent<RectTransform>().DOScale(1,0.5f).OnComplete(()=>{
                m_TextCount.transform.GetComponent<RectTransform>().DOScale(0,0.5f).OnComplete(()=>{
                    numnber--;
                    AnimationCount(numnber);
                });
            });
        }
        else{
            canvas.DOFade(0f,0.5f).OnComplete(()=>{
                canvas.blocksRaycasts = false;
            });
        }
    }

    void StartGameSubLevel()
    {
        canvas.blocksRaycasts = true;
        canvas.DOFade(1f,0.5f).OnComplete(()=>{
            m_ButtonLestGo.interactable = true;
        });
    }

    public RectTransform m_PanelUI = null;
    public CanvasGroup canvas = null;
}
