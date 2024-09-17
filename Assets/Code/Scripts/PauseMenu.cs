using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _ui;
    private bool _paused = false;



    // Start is called before the first frame update
    void Start() {
        _pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        PauseHandler();
    }

    private void PauseHandler(){
        if(Input.GetKeyDown(KeyCode.Escape) && !_paused) {
            Pause();
        } else if (Input.GetKeyDown(KeyCode.Escape) && _paused){
            Resume();
        }
    }

    public void Resume(){
        Time.timeScale = 1.0f;
        _paused = false;
        _pausePanel.SetActive(_paused);
        _ui.SetActive(!_paused);
    }

    private void Pause(){
        Time.timeScale = 0.0f;
        _paused = true;
        _pausePanel.SetActive(_paused);
        _ui.SetActive(!_paused);
    }

    public void Quit(){
        Application.Quit();
    }
}
