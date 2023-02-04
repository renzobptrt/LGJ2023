using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameLevel : MonoBehaviour
{
    
    public List<GameObject> blocksPrefabs;
    // public List<GameObject> combos;
    public static GameLevel instance;
    //Data
    public DataLevelStatsClass m_DataLevelStats = new DataLevelStatsClass();

    //UI
    [SerializeField] private List<Button> m_ListItems = new List<Button>();
    [SerializeField] private Image background;
    [SerializeField] private Image rootOk;
    [SerializeField] private Image rootSick;
    [SerializeField] private GameObject comboPrefabToDo;
    [SerializeField] private GameObject comboPrefabDone;
    [SerializeField] private GameObject comboBar;

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

    private void Start()
    {
       /*for(int i=0; i< m_ListItems.Count; i++)
       {
            int temp = i;
            m_ListItems[temp].onClick.RemoveAllListeners();
            m_ListItems[temp].onClick.AddListener(
                ()=> CheckIndexOfReference(temp)
            );
       } */

       SetRandomIndex();
       //Prueba de DOTween
       //Transform parent = m_TextToShowReference.transform.parent.transform.parent;
       //parent.GetComponent<RectTransform>().DOAnchorPosY(200,4f,true).SetDelay(2f);
    }

    public void AddListItems(Button newButton, int index)
    {
        int temp = index;
        newButton.onClick.RemoveAllListeners();
        newButton.onClick.AddListener(
            ()=> 
            {
                CheckIndexOfReference(temp);
                m_ListItems.Remove(newButton);
                Destroy(newButton.transform.gameObject);
            }
        );

        m_ListItems.Add(newButton);
    }

    public void CheckIndexOfReference(int index)
    {   
        Debug.Log(index + "  " + m_CurrentRandom);
        if(index == m_CurrentRandom)
        {
            m_CurrentIndexOfReference++;
            if(m_CurrentIndexOfReference >= m_DataLevelStats.m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect)
            {
                m_CurrentSubLevel++;
                if(m_CurrentSubLevel >= m_DataLevelStats.m_NumSubLevelsData.Count)
                {
                    //Salir a pantalla final
                    Debug.Log("Salvaste a la planta");
                }
                else
                {
                    //Pasar al siguiente nivel
                    m_CurrentIndexOfReference = 0;
                    DataSubLevelStatsClass currLevel = m_DataLevelStats.m_NumSubLevelsData[m_CurrentSubLevel];
                    // combos = new List<GameObject>();
                    // for (int i = 0; i < currLevel.m_NumCorrect; i++)
                    // {
                    //     combos.Add( Instantiate(comboPrefabToDo, Vector2.zero, comboPrefabToDo.transform.rotation, comboBar.transform) );
                    // }
                    background.sprite = currLevel.background;
                    rootOk.sprite = currLevel.rootOk;
                    rootSick.sprite = currLevel.rootSick;
                    blocksPrefabs.Add(currLevel.newSnake);
                    Debug.Log("Pasaste siguiente nivel");
                }
            }
            SetRandomIndex();
        }
        else
        {
            m_CurrentIndexOfReference = m_CurrentIndexOfReference == 0 ? 0 : m_CurrentIndexOfReference - 1;
        }
        //Debug.Log("Racha: " + m_CurrentIndexOfReference);
        // m_TextToShowCombo.text = "x"+m_CurrentIndexOfReference.ToString();
    }

    public void SetRandomIndex()
    {
        m_CurrentRandom = Random.Range(0,blocksPrefabs.Count);
        m_currentCombImg.sprite = blocksPrefabs[m_CurrentRandom].gameObject.GetComponent<Image>().sprite;
        // m_TextToShowReference.text = m_CurrentRandom == 0 ? "Negro" : (m_CurrentRandom == 1 ? "Rojo" : "Verde");
    }

    //Private variables
    private int m_CurrentSubLevel= 0;
    private int m_CurrentIndexOfReference = 0;
    private int m_CurrentRandom = 0;
    [SerializeField] Image m_currentCombImg;

    //SubClasses
    [System.Serializable]
    public class DataLevelStatsClass
    {
        public List<DataSubLevelStatsClass> m_NumSubLevelsData = new List<DataSubLevelStatsClass>();
    }

    [System.Serializable]
    public class DataSubLevelStatsClass
    {
        public int m_NumCorrect = 0;
        public Sprite background;
        public Sprite rootSick;
        public Sprite rootOk;
        public GameObject newSnake;
    }

}
