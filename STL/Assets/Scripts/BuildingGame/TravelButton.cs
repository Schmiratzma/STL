using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.UI;

public class TravelButton : MonoBehaviour
{
    Button button;

    [Scene]
    public string FlyingScene;
    // Start is called before the first frame update
    void Start()
    {

        button = GetComponent<Button>();
        button.onClick.AddListener(()=>SceneHandler.Instance.LoadScene(FlyingScene,LoadSceneMode.Single));
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
