using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;


public class Game_Manager : MonoBehaviour
{

    private static Game_Manager instance = null;
    public static Game_Manager sharedInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Game_Manager>();
            }
            return instance;
        }
    }

    #region Variables
    [SerializeField] Player _player;
    public static bool GameIsPaused = false;
    public static bool PartOneHasEnded = false;
    public static int PartGame = -1;
    public static float DistributionTime = 0f;
    public float ExamTime = 110f;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _endingPanel;
    [SerializeField] TextMeshProUGUI _distributionScore;
    [SerializeField] TextMeshProUGUI _examTimer;
    [SerializeField] TextMeshProUGUI _cheaterScore;
    [SerializeField] TextMeshProUGUI _markScore;
    public static int CurrentPaperMarked = 0;
    [SerializeField] TextMeshProUGUI _ZoomPaperText;
    [SerializeField] GameObject _ZoomPaperUI;
    [SerializeField] Graph _Graph;
    [SerializeField] GameObject _crossHair;
    public  AudioSource _BackgroundMusic;
    public  AudioClip[] _backGroundListMusic;
    IA_Manager _myIAmanager;
    #endregion

    private void Awake()
    {
        GameIsPaused = false;
        PartOneHasEnded = false;
        PartGame = -1;
        DistributionTime = 0f;
        CurrentPaperMarked = 0;
    }

        // Start is called before the first frame update
        void Start()
    {
        
        _myIAmanager = GetComponent<IA_Manager>();
        _BackgroundMusic.clip = _backGroundListMusic[0];
        _BackgroundMusic.Play();
        Debug.Log("Game is Starting");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Health < 0) GameOver();
        if (!GameIsPaused && ControllerManager.EscapeKey) PauseGame();
        if (GameIsPaused && ControllerManager.EscapeKey) ResumeGame();
        if(!GameIsPaused && ControllerManager.TabKey) ScoreBoard();
        if ((_myIAmanager.NbCheater == 0 || ExamTime < 0.2f) && !PartOneHasEnded) PartOneEnding();
        if(PartGame == 0)
        {
            if (!_crossHair.activeSelf) _crossHair.SetActive(true);
            DistributionTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(DistributionTime / 60);
            int seconds = Mathf.FloorToInt(DistributionTime % 60);
            _distributionScore.text = "Handing out time : " + (minutes > 0 ? minutes + "m" + seconds + "s" : seconds + "s");
        }
        else if(PartGame == 1)
        {
            _cheaterScore.text = "Cheater exposed : " + (_myIAmanager.CheaterArray.Length - _myIAmanager.NbCheater) + "/"+ _myIAmanager.CheaterArray.Length;
            ExamTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(ExamTime / 60);
            int seconds = Mathf.FloorToInt(ExamTime % 60);
            _examTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if(PartGame ==2)
        {
            if(CurrentPaperMarked < PaperPlacement.PaperPlacedList.Count)
            {
                if (PaperPlacement.PaperPlacedList[CurrentPaperMarked] == null) CurrentPaperMarked++;
                _ZoomPaperText.text = PaperPlacement.PaperPlacedList[CurrentPaperMarked].textPaper.text;
            }
            else if(_ZoomPaperUI.activeSelf)
            {
                PartTwoEnding();
            }
        }
    }
    async void PartOneEnding()
    {
        _examTimer.text = string.Format("{0:00}:{1:00}", 0, 0);
        PartOneHasEnded = true;
        PartGame++;
        await UniTask.Delay(TimeSpan.FromSeconds(5)); //Le temps que tous les élèves partent
        _player.CanPlay = false;
        await _player.GoToLastStage(); //On attend que le joueur arrive devant les escaliers
        _ZoomPaperUI.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        _player.CanPlay = true;
        _BackgroundMusic.clip = _backGroundListMusic[2];
        _BackgroundMusic.Play();
    }
    void PartTwoEnding()
    {
        Cursor.visible = true;
        _ZoomPaperUI.SetActive(false);
        _Graph.gameObject.SetActive(true);
        _Graph.ShowGraph(PaperPlacement.ListMark);
        _endingPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "JOB'S FINISHED";
        _markScore.text = "Mark Curve : is it Gaussian though.. ?  ";
        _endingPanel.SetActive(true);
        _endingPanel.transform.GetChild(1).gameObject.SetActive(true);
        _endingPanel.transform.GetChild(2).gameObject.SetActive(true);
        _endingPanel.transform.GetChild(3).gameObject.SetActive(true);
        _player.CanPlay = false;
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void GameOver()
    {
        Cursor.visible = true;
        _crossHair.SetActive(false);
        _examTimer.text = string.Format("{0:00}:{1:00}", 0, 0);
        PartOneHasEnded = true;
        _player.CanPlay = false;
        _endingPanel.SetActive(true);
        _endingPanel.transform.GetChild(1).gameObject.SetActive(true);
        _endingPanel.transform.GetChild(2).gameObject.SetActive(true);
        _endingPanel.transform.GetChild(3).gameObject.SetActive(true);
        _markScore.text = "YOU SHOT TOO MANY INNOCENT STUDENTS !\n" +
            "YOU'VE BEEN FIRED ! ";
    }
    public void PauseGame()
    {
        Cursor.visible = true;
        ControllerManager.EveryKeyFalse();
        if (PartGame == -1) return;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        GameIsPaused = true;
        _pauseMenu.SetActive(true);

    }
    public void ResumeGame()
    {
        Cursor.visible = false;
        ControllerManager.EveryKeyFalse();
        Time.timeScale = 1;
        AudioListener.pause = false;
        GameIsPaused = false;
        _pauseMenu.SetActive(false);
    }
    
    public void ScoreBoard()
    {
        ControllerManager.EveryKeyFalse();
        if (PartGame == -1) return;
        if(_endingPanel.activeSelf) _endingPanel.SetActive(false);
        else _endingPanel.SetActive(true);
    }
    public void LoadMenu()
    {
        ResumeGame();
        Debug.Log("Back to tuto");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        _endingPanel.transform.GetChild(8).gameObject.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }
}
