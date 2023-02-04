using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameLevel gLevel;
    [SerializeField] float bound;
    [SerializeField] float yTop;
    [SerializeField] bool won = false;
    float speed = 64f;

    void Awake()
    {
        Vector2 currSize = GetComponent<RectTransform>().sizeDelta;
        Vector2 bSize = gLevel.blocksPrefabs[0].GetComponent<RectTransform>().sizeDelta;
        bound = (Screen.width + currSize.x - bSize.x)/2;
        yTop = (Screen.height + currSize.y + bSize.y)/2;
    }

    void Start()
    {
        StartCoroutine( Spawn() );
    }

    IEnumerator Spawn()
    {
        while(!won)
        {
            int typeBlock = Random.Range(0,gLevel.blocksPrefabs.Count);
            GameObject block = gLevel.blocksPrefabs[typeBlock];
            block = Instantiate(block, Vector2.zero, block.transform.rotation, gameObject.transform);
            block.GetComponent<Serpent>().speed = speed;

            GameLevel.instance.AddListItems(block.GetComponent<Button>(),typeBlock);

            float xPos = Random.Range(-bound,bound);
            block.GetComponent<RectTransform>().localPosition = new Vector3( xPos, yTop, 0);
            float t = Random.Range(1,2);
            yield return new WaitForSeconds(t);
        }
    }
}
