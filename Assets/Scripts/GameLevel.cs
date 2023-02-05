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
    public RectMask2D rmComboHealth;
    public static GameLevel instance;
    //Data
    public DataLevelStatsClass m_DataLevelStats = new DataLevelStatsClass();
    float rootHealHeight = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        //HEIGHT | Altura por cantidad de combos del nivel actual
        rootHealHeight = rmComboHealth.canvasRect.height / m_DataLevelStats.m_NumSubLevelsData.Count;
        rmComboHealth.padding = new Vector4( 0,0,0,rmComboHealth.canvasRect.height );
    }

    private void Start()
    {
        //Set First Index
        SetRandomIndex();
        GameManager.instance.StartListSpritesForPower(m_DataLevelStats.m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect);
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
        if(index == m_CurrentRandom)
        {
            GameManager.instance.SetSpritesForPower(m_CurrentIndexOfReference,1);
            m_CurrentIndexOfReference++;
            if(m_CurrentIndexOfReference >= m_DataLevelStats.m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect)
            {
                m_CurrentSubLevel++;
                rmComboHealth.padding = new Vector4( 0,0,0,rmComboHealth.padding.w - rootHealHeight );
                GameManager.instance.StartListSpritesForPower(m_DataLevelStats.m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect);
                //Cambiar sprite

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
                    background.sprite = currLevel.background;
                    rootOk.sprite = currLevel.rootOk;
                    rootSick.sprite = currLevel.rootSick;
                    blocksPrefabs.Add(currLevel.newSnake);
                    
                    GameManager.instance.StartCanvaReady();

                }
            }
            SetRandomIndex();
        }
        else
        {
            m_CurrentIndexOfReference = m_CurrentIndexOfReference == 0 ? 0 : m_CurrentIndexOfReference - 1;
            GameManager.instance.SetSpritesForPower(m_CurrentIndexOfReference,0);
        }

    }

    public void SetRandomIndex()
    {
        m_CurrentRandom = Random.Range(0,blocksPrefabs.Count);
        m_currentCombImg.sprite = blocksPrefabs[m_CurrentRandom].gameObject.GetComponent<Image>().sprite;
    }

    //Private variables
    private int m_CurrentSubLevel= 0;
    private int m_CurrentIndexOfReference = 0;
    private int m_CurrentRandom = 0;
    [SerializeField] Image m_currentCombImg;

    //UI
    [SerializeField] private List<Button> m_ListItems = new List<Button>();
    [SerializeField] private Image background;
    [SerializeField] private Image rootOk;
    [SerializeField] private Image rootSick;
    [SerializeField] private GameObject comboPrefabToDo;
    [SerializeField] private GameObject comboPrefabDone;
    [SerializeField] private GameObject comboBar;

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
