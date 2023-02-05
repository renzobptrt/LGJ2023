using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
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
        
        m_ButtonContinue.onClick.RemoveAllListeners();
        m_ButtonContinue.onClick.AddListener(()=>{
            m_ButtonContinue.interactable = false;
            m_CanvasRestart.DOFade(0f,0.5f).OnComplete(()=>{
                m_CanvasRestart.blocksRaycasts = false;
            });
            StartCanvaReady();
        });

        //Canvas
        canvas =  m_PanelUI.GetComponent<CanvasGroup>();
        m_CanvasRestart = m_PanelRestart.GetComponent<CanvasGroup>();

        m_NumLifes = m_ListLifes.Count;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public bool CheckWrongChoice()
    {
        m_NumLifes--;

        if(m_NumLifes<0)
        {
            //Pantalla fin
            m_CanvasRestart.DOFade(1f,0.5f).OnComplete(()=>{
                m_ButtonContinue.interactable = true;
                m_CanvasRestart.blocksRaycasts = true;
                RestartLife();
            });

            return true;
        }
        else
        {
            float newAlpha =  0f;
            Color newColor =  m_ListLifes[m_NumLifes].color;
            newColor.a = newAlpha;
            m_ListLifes[m_NumLifes].color = newColor;
            return false;
        }
    }

    public void RestartLife()
    {
        m_NumLifes = m_ListLifes.Count;
        foreach(Image im in m_ListLifes)
        {
            float newAlpha =  1f;
            Color newColor = im.color;
            newColor.a = newAlpha;
            im.color = newColor;
        }
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

    public void SetSpritesForPower(int index, int isDone)
    {
        m_ListPowerToDo[index].sprite = m_ListSpritesForPower[isDone];
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
            if(m_rmComboHealth.padding == Vector4.zero)
            {
                m_rmComboHealth.padding = new Vector4( 0,0,0, m_rmComboHealth.canvasRect.height );
            }
            onComplete();
        }
    }

    public void SetLevelText(string newLevelText, string newSubLevelText)
    {
        m_TextLevel.text = "Level " + newLevelText + " - " + newSubLevelText;
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
    [SerializeField] private TextMeshProUGUI m_TextLevel = null;
    [SerializeField] private RectMask2D m_rmComboHealth = null;
    [SerializeField] private List<Image> m_ListLifes = new List<Image>();
    [SerializeField] private RectTransform m_PanelRestart= null;
    [SerializeField] private Button m_ButtonContinue = null;
    private CanvasGroup canvas = null;
    private CanvasGroup m_CanvasRestart = null;
    private CanvasGroup m_CanvasFinish = null;
    private int m_NumLifes = 3;
}
