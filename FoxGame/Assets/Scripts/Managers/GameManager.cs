using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //if (Input.GetKeyDown(InputManager.Instance.keyCodes["quitKey"]))
        //{
        //    Application.Quit();
        //}
    }
}
