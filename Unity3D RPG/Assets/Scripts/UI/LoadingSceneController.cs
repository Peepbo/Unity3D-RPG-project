using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingSceneController : Singleton<LoadingSceneController>
{
    protected LoadingSceneController() { }

    [SerializeField]
    GameObject SceneUIFactory;
    GameObject SceneUI;
    CanvasGroup canvasGroup;
    Image progressBar;
    public string loadSceneName;
    bool isDun = false;

    private void Awake()
    {
        //SceneUIFactory = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/LoadingUI.prefab", typeof(GameObject));
        DontDestroyOnLoad(gameObject);
        SceneUI = Instantiate(SceneUIFactory);
        DontDestroyOnLoad(SceneUI);
        
    }

    void Start()
    {
        canvasGroup = SceneUI.GetComponent<CanvasGroup>();
        progressBar = SceneUI.GetComponentsInChildren<Image>()[2];
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Load");
        SceneUI.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;
        yield return StartCoroutine(Fade(true));

        AsyncOperation _op = SceneManager.LoadSceneAsync(loadSceneName);
        _op.allowSceneActivation = false; 

        float _timer = 0f;
        while(!_op.isDone)
        {
            yield return null;

            if( _op.progress <0.9f)
            {
                progressBar.fillAmount = _op.progress;
            }
            else
            {
                _timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1.0f, _timer);

                if(progressBar.fillAmount >=1f)
                {
                    _op.allowSceneActivation = true;
                    if (loadSceneName != "" )
                    {
                        switch (loadSceneName)
                        {
                            case "TownScene":
                                SoundManager.Instance.BGMPlay("VillageBGM");
                                SoundManager.Instance.AMBPlay("Town_Amb");
                                isDun = false;
                                break;
                            case "Dungeon 1(light bake)":
                            case "Dungeon 2(light bake)":
                            case "D 1-3":
                            case "D 1-4":
                                if (!isDun)
                                {
                                    SoundManager.Instance.BGMPlay("Dungeon1_BGM");
                                    SoundManager.Instance.AMBPlay("Dungeon_Amb");
                                    isDun = true;
                                }
                                break;
                            case "BossRoom(light bake)":
                                SoundManager.Instance.BGMPlay("Boss1_BGM");
                                SoundManager.Instance.AMBPlay("Dungeon_Amb");
                                isDun = false;
                                break;
                        }
                    }
                   
                    yield break;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;

            SceneManager.LoadScene("UiScene", LoadSceneMode.Additive);
        }
    }
    private IEnumerator Fade(bool isFadeIn)
    {
        if (!isFadeIn)
        {
            SceneUI.GetComponentsInChildren<Image>()[1].color = Color.clear;
            SceneUI.GetComponentsInChildren<Image>()[2].color = Color.clear;
        }

        float _timer = 0f;
        while(_timer < 1f)
        {
            yield return null;
            _timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, _timer) : Mathf.Lerp(1f, 0f, _timer);
        }

        if (!isFadeIn)
        {
            SceneUI.SetActive(false);
        }
    }
}
