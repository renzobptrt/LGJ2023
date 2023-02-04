using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameLevel : MonoBehaviour
{
    //Data
    public DataLevelStatsClass m_DataLevelStats = new DataLevelStatsClass();

    //UI
    [SerializeField] private List<Button> m_ListItems = new List<Button>();
    [SerializeField] private TextMeshProUGUI m_TextToShowReference = null;

    private void Start() {
       for(int i=0; i< m_ListItems.Count; i++)
       {
            int temp = i;
            m_ListItems[temp].onClick.RemoveAllListeners();
            m_ListItems[temp].onClick.AddListener(
                ()=> CheckIndexOfReference(temp)
            );
       } 

       SetRandomIndex();
       //Prueba de DOTween
       Transform parent = m_TextToShowReference.transform.parent.transform.parent;
       parent.GetComponent<RectTransform>().DOAnchorPosY(200,4f,true).SetDelay(2f);
    }

    private void SetSubLevel(int i){
        m_CurrentSubLevel = i;
    }

    public void CheckIndexOfReference(int index){
        if(index == m_CurrentRandom){
            m_CurrentIndexOfReference++;
            if(m_CurrentIndexOfReference>=m_DataLevelStats.m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect){
                m_CurrentSubLevel++;
                if(m_CurrentSubLevel>=m_DataLevelStats.m_NumSubLevelsData.Count){
                    //Salir a pantalla final
                    Debug.Log("Salvaste a la planta");
                }else{
                    //Pasar al siguiente nivel
                    m_CurrentIndexOfReference = 0;
                    Debug.Log("Pasaste siguiente nivel");
                }
            }
            SetRandomIndex();
        }else{
            m_CurrentIndexOfReference = m_CurrentIndexOfReference == 0 ? 0 : m_CurrentIndexOfReference-1;
        }
       Debug.Log("Racha: " + m_CurrentIndexOfReference);
    }


    public void SetRandomIndex(){
        m_CurrentRandom = Random.Range(0,m_ListItems.Count);
        m_TextToShowReference.text = m_CurrentRandom.ToString();
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
