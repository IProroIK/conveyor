using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    [SerializeField] private LevelTask _levelTask;
    [SerializeField] private Transform _conveyor;
    [SerializeField] private Animator _camera;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private float _zOffset;
    [SerializeField] private float _animTime;
    [SerializeField] private Transform _foodPool;
    private void OnEnable()
    {
        _levelTask.onWin += StartWinAnimation;
        _levelTask.onWin += ActiveWinUI;
        _levelTask.onWin += DisableFood;
    }

    private void OnDisable()
    {
        _levelTask.onWin -= StartWinAnimation;
        _levelTask.onWin -= ActiveWinUI;
        _levelTask.onWin -= DisableFood;
    }


    private void StartWinAnimation()
    {
        _camera.SetTrigger("Win");
        _conveyor.DOMoveZ(_zOffset, _animTime);
    }

    private void ActiveWinUI()
    {
        _winText.text = "Level Passed";
        _nextLevelButton.gameObject.SetActive(true);
    }   

    private void DisableFood()
    {
        _foodPool.gameObject.SetActive(false);
    }
}
