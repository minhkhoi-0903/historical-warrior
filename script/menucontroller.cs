using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menucontroller : MonoBehaviour
{
    [SerializeField] private bool IsPause;
    [SerializeField] private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        IsPause = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPause)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void pauseGame()
    {
        IsPause = true;
    }

    public void Resume()
    {
        IsPause = false;
    }
}
