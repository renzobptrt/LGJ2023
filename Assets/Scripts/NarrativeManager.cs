using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Callbacks;
using UnityEngine.SceneManagement;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] int currentScene;
    [SerializeField] Image background;
    [SerializeField] Image character;
    [SerializeField] TextMeshProUGUI tmpro;
    public List<Scene> scenes;
    public float timePerDialog;
    public bool didCurrSceneEnd = false;

    void Awake()
    {
        background = GetComponent<Image>();
        currentScene = 0;
        ShowDialog(2);
    }

    public void ShowDialog( int numScenes, OnComplete onComplete = null )
    {
        didCurrSceneEnd = false;
        if(scenes[currentScene].background) background.sprite = scenes[currentScene].background;
        if(scenes[currentScene].character)
        {
            character.sprite = scenes[currentScene].character;
            character.enabled = true;
        }
        else character.enabled = false;
        gameObject.SetActive(true);
        StartCoroutine( NextDialog( numScenes, onComplete ) );
    }

    IEnumerator NextDialog( int scenesLeft, OnComplete onComplete = null)
    {
        while (scenesLeft > 0)
        {
            foreach (string dialog in scenes[currentScene].dialogs)
            {
                tmpro.text = scenes[currentScene].charname + "\n" + dialog; 
                yield return new WaitForSeconds(timePerDialog);
            }
            currentScene ++;
            scenesLeft--;
        }

        onComplete();
        gameObject.SetActive(false);

        didCurrSceneEnd = true;


    }

    public void SkipDialog()
    {
        didCurrSceneEnd = true;
        currentScene ++;
        StopCoroutine( NextDialog(0) );
        gameObject.SetActive(false);
    }


    [System.Serializable]
    public class Scene
    {
        public Sprite background;
        public Sprite character;
        public string charname;
        public List<string> dialogs;
    }
}
