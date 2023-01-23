using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    public event Action OnItemGrab; 

    [SerializeField] private Transform _handIKController;
    [SerializeField] private Transform _boxDropPoint;
    [SerializeField] private Transform _idlePositonPoint;
    [SerializeField] private Transform _dropPoint;
    [SerializeField] private float _timeToGrab;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LevelTask _levelTask;
    [SerializeField] private Animator _anim;
    [SerializeField] private TwoBoneIKConstraint _rightHandIK;
    [SerializeField] private Transform _basket;
    private List<Rigidbody> _rds; 
    private bool isGrabing;

    private void OnEnable()
    {
        _levelTask.onWin += StartDanceAnim;
    }

    private void OnDisable()
    {
        _levelTask.onWin -= StartDanceAnim;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CheckFood();
        }
    }

    private void CheckFood()
    {
        RaycastHit hitInfo;
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            if(hitInfo.transform.TryGetComponent(out Food food) && !isGrabing)
            {
                GrabItem(food);
                CheckProductType(food);
            }
        }
    }

    private void GrabItem(Food item)
    {
        OnItemGrab?.Invoke();
        isGrabing = true;
        Sequence GrabItem = DOTween.Sequence();
        GrabItem.Append(_handIKController.DOMove(item.transform.position, _timeToGrab))
            .AppendCallback(() => item.transform.position = _handIKController.position)
            .AppendCallback(() => item.transform.SetParent(_handIKController))
            .Append(item.transform.DOMove(_boxDropPoint.position, _timeToGrab))
            .Join(_handIKController.DOMove(_dropPoint.position, _timeToGrab))
            .AppendCallback(() => item.Drop())
            .Append(_handIKController.DOMove(_idlePositonPoint.position, _timeToGrab))
            .AppendCallback(() => item.FreezeFoodInBasket(_basket))
            .OnComplete(() => isGrabing = false);
    }

    private void CheckProductType(Food food)
    {
        if(_levelTask.levelFoodGoal == food.foodType)
        {
            _levelTask.UpdateCurrentAmount();
            _levelTask.UpdateProgress();
        }
    }
    
    private void StartDanceAnim()
    {
        _anim.SetTrigger("Win");
        _rightHandIK.weight = 0;
    }
    
}
