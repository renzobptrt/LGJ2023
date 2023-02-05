using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeManager : MonoBehaviour
{
    public List<Scene> scenes;
    [SerializeField] int currentScene = 0;
    public float timePerDialog = 3;

    void Awake()
    {
        // StartCoroutine(  );
    }

    IEnumerator ShowDialog()
    {
        
    }

    [System.Serializable]
    public class Scene
    {
        public Image background;
        public Image character;
        public string charname;
        public List<string> dialogs;
    }
}
