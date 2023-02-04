using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameLevel : MonoBehaviour
{
    public static GameLevel instance;
    //Data
    public DataLevelStatsClass m_DataLevelStats = new DataLevelStatsClass();

    //UI
    [SerializeField] private List<Button> m_ListItems = new List<Button>();
    [SerializeField] private TextMeshProUGUI m_TextToShowReference = null;
    [SerializeField] private TextMeshProUGUI m_TextToShowCombo = null;

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
                    //Pasar al siguiente nivel, añadir transicion con DOTween
                    m_CurrentIndexOfReference = 0;
                    Debug.Log("Pasaste siguiente nivel");
                }
            }
            SetRandomIndex();
        }
        else
        {
            m_CurrentIndexOfReference = m_CurrentIndexOfReference == 0 ? 0 : m_CurrentIndexOfReference - 1;
        }

        m_TextToShowCombo.text = "x"+m_CurrentIndexOfReference.ToString();
    }

    public void SetRandomIndex()
    {
        m_CurrentRandom = Random.Range(0,3);
        m_TextToShowReference.text = m_CurrentRandom == 0 ? "Negro" : (m_CurrentRandom == 1 ? "Rojo" : "Verde");
    }

    //Private variables
    private int m_CurrentSubLevel= 0;
    private int m_CurrentIndexOfReference = 0;
    private int m_CurrentRandom = 0;

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
    }

}
