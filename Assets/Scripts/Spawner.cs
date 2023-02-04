using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> blocksPrefabs;
    [SerializeField] float bound;
    [SerializeField] float yTop;
    [SerializeField] bool won = false;
    float speed = 3f;

    void Awake()
    {
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = Camera.main.aspect * camHalfHeight;
        Vector3 bSize = blocksPrefabs[0].GetComponent<Renderer>().bounds.size;
        bound = camHalfWidth - (bSize.x/2);
        yTop = camHalfHeight - (bSize.y/2);
        transform.position = new Vector3(0, yTop, 0);
        StartCoroutine( Spawn() );
    }

    // void Update()
    // {
        
    // }

    IEnumerator Spawn()
    {
        while(!won)
        {
            int typeBlock = Random.Range(0,blocksPrefabs.Count);
            GameObject block = blocksPrefabs[typeBlock];
            block.GetComponent<Serpent>().speed = speed;
            float xPos = Random.Range(-bound,bound);
            Vector3 pos = new Vector3( xPos, yTop, 0);
            Instantiate(block, pos, block.transform.rotation, gameObject.transform);
            float t = Random.Range(1,2);
            yield return new WaitForSeconds(t);
        }
    }
}
