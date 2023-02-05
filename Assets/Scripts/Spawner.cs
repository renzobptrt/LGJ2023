using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameLevel gLevel;
    [SerializeField] float bound;
    [SerializeField] float yTop;
    [SerializeField] float prevLvl;
    [SerializeField] bool won = false;

    void Awake()
    {
        Vector2 currSize = GetComponent<RectTransform>().sizeDelta;
        Vector2 bSize = gLevel.blocksPrefabs[0].GetComponent<RectTransform>().sizeDelta;
        bound = (Screen.width + currSize.x - bSize.x)/2;
        yTop = (Screen.height + currSize.y + bSize.y)/2;
        prevLvl = gLevel.m_currentLevel;
    }

    public void Start()
    {
        StartCoroutine( Spawn() );
    }

    IEnumerator Spawn()
    {
        while(!won)
        {
            float speed = gLevel.m_CurrentSubLevel * 10;
            speed += gLevel.m_DataLevelStats[gLevel.m_currentLevel].speed;

            UpdateBlockSpeeds(speed);

            int q = gLevel.m_currentLevel * (2 + gLevel.m_CurrentSubLevel);
            q = Random.Range(0, q+2);
            for (int i = 0; i < q; i++) InstantiateBlock(speed);
            
            yield return new WaitForSeconds(0.2f);
        }
    }

    void UpdateBlockSpeeds(float speed)
    {
        if (prevLvl != gLevel.m_currentLevel)
        {
            foreach (Transform enemy in transform)
            {
                enemy.gameObject.GetComponent<Serpent>().ySpeed = speed;
            }
        }
    }

    void InstantiateBlock(float speed)
    {
        int typeBlock = Random.Range(0,gLevel.blocksPrefabs.Count);
        GameObject block = gLevel.blocksPrefabs[typeBlock];
        block = Instantiate(block, Vector2.zero, block.transform.rotation, gameObject.transform);
        block.GetComponent<Serpent>().ySpeed = speed;

        GameLevel.instance.AddListItems(block.GetComponent<Button>(),typeBlock);

        float xPos = Random.Range(-bound,bound);
        block.GetComponent<RectTransform>().localPosition = new Vector3( xPos, yTop, 0);
    }
}
