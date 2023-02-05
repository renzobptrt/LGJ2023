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
    public NarrativeManager narrativeManager = null;
    //Data
    public List<DataLevelStatsClass> m_DataLevelStats = new List<DataLevelStatsClass>();
    float rootHealHeight = 0;

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

        //HEIGHT | Altura por cantidad de combos del nivel actual
        rootHealHeight = rmComboHealth.canvasRect.height / m_DataLevelStats[m_currentLevel].m_NumSubLevelsData.Count;
        rmComboHealth.padding = new Vector4( 0,0,0,rmComboHealth.canvasRect.height );
    }

    private void Start()
    {
        //Set First Index
        SetRandomIndex();
        GameManager.instance.StartListSpritesForPower(m_DataLevelStats[m_currentLevel].m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect);
        GameManager.instance.SetLevelText((m_currentLevel+1).ToString(), (m_CurrentSubLevel+1).ToString());
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

    public void RemoveListButton(Button currentButton)
    {
        m_ListItems.Remove(currentButton);
    }

    public void CheckIndexOfReference(int index)
    {   
        //TODO: FIX INDEX OUT OF RANGE
        if(index == m_CurrentRandom)
        {
            GameManager.instance.PlaySfxSound("RightChoice");
            GameManager.instance.SetSpritesForPower(m_CurrentIndexOfReference,1);
            m_CurrentIndexOfReference++;
            if(m_CurrentIndexOfReference >= m_DataLevelStats[m_currentLevel].m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect)
            {
                //Resetear Numero de Combos Acertados
                m_CurrentIndexOfReference = 0;

                //Subir siguiente sub nivel
                m_CurrentSubLevel++;
                
                rmComboHealth.padding = new Vector4( 0,0,0,rmComboHealth.padding.w - rootHealHeight );
                GameManager.instance.PlaySfxSound("CompleteSubLevel");
                //Cambiar sprite
                
                if(m_CurrentSubLevel >= m_DataLevelStats[m_currentLevel].m_NumSubLevelsData.Count)
                {
                    //Pasar al siguiente nivel

                    //Eliminar a la ardilla
                    if (m_currentLevel == 0) blocksPrefabs.RemoveAt(0);
                    
                    narrativeManager.ShowDialog();
                    DataLevelStatsClass currLevel = m_DataLevelStats[m_currentLevel];
                    background.sprite = currLevel.background;
                    rootOk.sprite = currLevel.rootOk;
                    rootSick.sprite = currLevel.rootSick;
                        
                    if(currLevel.newSnake!= null) blocksPrefabs.Add(currLevel.newSnake);

                    //Resetear Numero el Subnivel Actual del Nivel
                    m_CurrentSubLevel = 0;
                    m_currentLevel++;

                    if(m_currentLevel >= m_DataLevelStats.Count)
                    {
                        Debug.Log("GANASTE EL JUEGO");
                    }else
                    {
                        GameManager.instance.StartCanvaReady();
                    }
                }
                else
                {
                    GameManager.instance.StartCanvaReady();
                }
                GameManager.instance.StartListSpritesForPower(m_DataLevelStats[m_currentLevel].m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect);
                GameManager.instance.SetLevelText((m_currentLevel+1).ToString(), (m_CurrentSubLevel+1).ToString());
            }
            SetRandomIndex();
        }
        else
        {
            GameManager.instance.PlaySfxSound("WrongChoice");
            if(m_CurrentIndexOfReference == 0 )
            {
                //Quitar vida
                bool isRestart = GameManager.instance.CheckWrongChoice() ;
                if (isRestart) 
                {
                    GameManager.instance.StartListSpritesForPower(m_DataLevelStats[m_currentLevel].m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect);
                }
            }else
            {
                m_CurrentIndexOfReference = m_CurrentIndexOfReference == 0 ? 0 : m_CurrentIndexOfReference - 1;
                GameManager.instance.SetSpritesForPower(m_CurrentIndexOfReference,0);
            }
        }
    }

    public void SetRandomIndex()
    {
        m_CurrentRandom = Random.Range(0,blocksPrefabs.Count);
        m_currentCombImg.sprite = blocksPrefabs[m_CurrentRandom].gameObject.GetComponent<Image>().sprite;
    }

    //Private variables
    public int m_currentLevel {private set; get;}
    public int m_CurrentSubLevel {private set; get;}
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
        public Sprite background;
        public Sprite rootSick;
        public Sprite rootOk;
        public GameObject newSnake;
        public float speed;
    }

    [System.Serializable]
    public class DataSubLevelStatsClass
    {
        public int m_NumCorrect = 0;
    }

}
