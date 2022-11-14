using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
public class TutorialScript : MonoBehaviour
{

    #region Variables
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] VideoClip[] _listOfVideo;
    [SerializeField]
    string[] _listDescriptionTuto =
    {
        "1. Hand out exam papers",
        "2. Don't let the students cheat",
        "3. Mark the papers with fairness",
    };
    [SerializeField] TextMeshProUGUI _descriptionTextUI;
    [SerializeField] int _currentStepTuto = 0;
    [SerializeField] Button _backButton;
    [SerializeField] TextMeshProUGUI _nextButtonText;
    [SerializeField] Player _player;
    int _n;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _n =_listOfVideo.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextButton()
    {
        if (_currentStepTuto < _n)
        {
            if (_currentStepTuto == _n - 1)
            {
                _nextButtonText.text = "End Tuto";
            }
            _videoPlayer.clip = _listOfVideo[++_currentStepTuto];
            _descriptionTextUI.text = _listDescriptionTuto[_currentStepTuto];
            
        }
        else if(_currentStepTuto == _n - 1)
        {
            SkipButton();
        }
        _backButton.gameObject.SetActive(true);
    }
    public void BackButton()
    {
        if (_currentStepTuto == 1)
        {
            _backButton.gameObject.SetActive(false);
        }
        if (_currentStepTuto > 0)
        {
            _videoPlayer.clip = _listOfVideo[--_currentStepTuto];
            _descriptionTextUI.text = _listDescriptionTuto[_currentStepTuto];
        }
        _nextButtonText.text = "Next";
    }
    public void SkipButton()
    {
        _player.CanPlay = true;
        _currentStepTuto = 0;
        _videoPlayer.clip = _listOfVideo[_currentStepTuto];
        _descriptionTextUI.text = _listDescriptionTuto[_currentStepTuto];
        _videoPlayer.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
