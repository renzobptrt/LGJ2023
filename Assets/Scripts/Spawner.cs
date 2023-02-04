using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public List<GameObject> blocksPrefabs;
    [SerializeField] float bound;
    [SerializeField] float yTop;
    [SerializeField] bool won = false;
    float speed = 64f;

    void Awake()
    {
        // float camHalfHeight = Camera.main.orthographicSize;
        // float camHalfWidth = Camera.main.aspect * camHalfHeight;
        // Vector3 bSize = blocksPrefabs[0].GetComponent<Renderer>().bounds.size;
        // bound = camHalfWidth - (bSize.x/2);
        // yTop = camHalfHeight - (bSize.y/2);
        Vector2 currSize = GetComponent<RectTransform>().sizeDelta;
        Vector2 bSize = blocksPrefabs[0].GetComponent<RectTransform>().sizeDelta;
        bound = (Screen.width + currSize.x - bSize.x)/2;
        yTop = (Screen.height + currSize.y - bSize.y)/2;
        // transform.position = new Vector3(0, yTop, 0);

        
    }

    void Start()
    {
        StartCoroutine( Spawn() );
    }

    IEnumerator Spawn()
    {
        while(!won)
        {
            int typeBlock = Random.Range(0,blocksPrefabs.Count);
            GameObject block = blocksPrefabs[typeBlock];
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
