using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelConfig : MonoBehaviour
{
    // //Data
    // public DataLevelStatsClass m_DataLevelStats = new DataLevelStatsClass();
    // public List<GameObject> m_ListItems;

    // //Private variables
    // private int m_CurrentSubLevel = 0;
    // private int m_CurrentIndexOfReference = 0;
    // private int m_CurrentRandom = 0;
    

    // private void Start()
    // {
    //    SetRandomIndex();
    //    //Prueba de DOTween
    //    Transform parent = m_TextToShowReference.transform.parent.transform.parent;
    //    parent.GetComponent<RectTransform>().DOAnchorPosY(200,4f,true).SetDelay(2f);
    // }

    // private void Update() {
        
    // }

    // private void SetSubLevel(int i){
    //     m_CurrentSubLevel = i;
    // }

    // public void CheckIndexOfReference(int index){
    //     if(index == m_CurrentRandom){
    //         m_CurrentIndexOfReference++;
    //         if(m_CurrentIndexOfReference>=m_DataLevelStats.m_NumSubLevelsData[m_CurrentSubLevel].m_NumCorrect){
    //             m_CurrentSubLevel++;
    //             if(m_CurrentSubLevel>=m_DataLevelStats.m_NumSubLevelsData.Count){
    //                 //Salir a pantalla final
    //                 Debug.Log("Salvaste a la planta");
    //             }else{
    //                 //Pasar al siguiente nivel
    //                 m_CurrentIndexOfReference = 0;
    //                 Debug.Log("Pasaste siguiente nivel");
    //             }
    //         }
    //         SetRandomIndex();
    //     }else{
    //         m_CurrentIndexOfReference = m_CurrentIndexOfReference == 0 ? 0 : m_CurrentIndexOfReference-1;
    //     }
    //    Debug.Log("Racha: " + m_CurrentIndexOfReference);
    // }


    // public void SetRandomIndex()
    // {
    //     m_CurrentRandom = Random.Range(0,m_ListItems.Count);
    // }


    // //SubClasses
    // [System.Serializable]
    // public class DataLevelStatsClass
    // {
    //     public List<DataSubLevelStatsClass> m_NumSubLevelsData = new List<DataSubLevelStatsClass>();
    // }

    // [System.Serializable]
    // public class DataSubLevelStatsClass
    // {
    //     public int m_NumCorrect = 0;
    // }

}
