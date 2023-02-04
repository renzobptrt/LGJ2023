using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static Callbacks;

public class GameManager : MonoBehaviour
{
    //Instance
    public static GameManager instance;

    //Elements
    public Button m_ButtonLestGo = null;
    public RectTransform m_PanelUI = null;

    public List<Image> m_ListPowerToDo = new List<Image>();
    public List<Sprite> m_ListSpritesForPower = new List<Sprite>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        m_ButtonLestGo.onClick.RemoveAllListeners();
        m_ButtonLestGo.onClick.AddListener(()=>{
            m_ButtonLestGo.interactable = false;
            AnimationCount(3,()=>{
                    canvas.DOFade(0f,0.5f).OnComplete(()=>{
                    canvas.blocksRaycasts = false;
                });
            });
        });
        canvas =  m_PanelUI.GetComponent<CanvasGroup>();
    }
    
    public void StartListSpritesForPower(int count)
    {
        float newAlpha = 0f;

        for(int i=0; i<m_ListPowerToDo.Count; i++)
        {
            newAlpha = (i < count) ? 1f : 0f;
            Color newColor = m_ListPowerToDo[i].color;
            newColor.a = newAlpha;
            m_ListPowerToDo[i].color = newColor;
            m_ListPowerToDo[i].sprite = m_ListSpritesForPower[0];
        }
    }

    public void StartCanvaReady()
    {
        m_ButtonLestGo.interactable = false;
        canvas.blocksRaycasts = true;
        m_TextCount.transform.GetComponent<RectTransform>().localScale = Vector3.one;
        canvas.DOFade(1f,0.5f).OnComplete(()=>{
            m_ButtonLestGo.interactable = true;
        });
    }

    void AnimationCount(int number, OnComplete onComplete = null)
    {   
        if(number>0)
        {
            m_TextCount.text = number.ToString();
            m_TextCount.transform.GetComponent<RectTransform>().DOScale(1,0.5f).OnComplete(()=>{
                m_TextCount.transform.GetComponent<RectTransform>().DOScale(0,0.5f).OnComplete(()=>{
                    number--;
                    AnimationCount(number,onComplete);
                });
            });
        }
        else{
            m_TextCount.text = "Ready?";
            onComplete();
        }
    }

    void StartGameSubLevel()
    {
        canvas.blocksRaycasts = true;
        canvas.DOFade(1f,0.5f).OnComplete(()=>{
            m_ButtonLestGo.interactable = true;
        });
    }


    //Private Vari
    [SerializeField] private TextMeshProUGUI m_TextCount = null;
    private CanvasGroup canvas = null;
}
