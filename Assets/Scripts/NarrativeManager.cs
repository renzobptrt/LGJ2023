using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    [SerializeField] int currentScene = 1;
    [SerializeField] Image background;
    [SerializeField] Image character;
    [SerializeField] TextMeshProUGUI tmpro;
    public List<Scene> scenes;
    public float timePerDialog = 3;
    public bool didCurrSceneEnd = false;

    void Awake()
    {
        background = GetComponent<Image>();
        ShowDialog();
    }

    public void ShowDialog()
    {
        didCurrSceneEnd = false;
        if(scenes[currentScene].background) background.sprite = scenes[currentScene].background;
        if(scenes[currentScene].character) character.sprite = scenes[currentScene].character;
        gameObject.SetActive(true);
        StartCoroutine( NextDialog() );
    }

    IEnumerator NextDialog()
    {
        foreach (string dialog in scenes[currentScene].dialogs)
        {
            tmpro.text = scenes[currentScene].charname + "\n" + dialog; 
            yield return new WaitForSeconds(timePerDialog);
        }
        currentScene ++;
        didCurrSceneEnd = true;
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
