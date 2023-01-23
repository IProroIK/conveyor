using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelTask : MonoBehaviour
{
    public event Action onWin;
    public FoodType levelFoodGoal { get; private set; } 

    [SerializeField] private List<string> _productsName;
    [SerializeField] private TextMeshProUGUI _levelGoal;
    [SerializeField] private Slider _levelProgress;
    [SerializeField] private int _amountToWin;
    private int _currentAmount = 0;    
    private void Awake()
    {
        SetLevelGoal();
    }

    private void SetLevelGoal()
    {
        FillList();
        _amountToWin = (int)UnityEngine.Random.Range(1, 6);
        int productIndex = (int)UnityEngine.Random.Range(0, _productsName.Count);
        levelFoodGoal = (FoodType) productIndex;
        _levelGoal.text = "Collect " + _amountToWin.ToString() + " " + _productsName[productIndex];
        _levelProgress.maxValue = _amountToWin;
    }
    private void FillList()
    {
        _productsName = new List<string>();

        foreach (string name in System.Enum.GetNames(typeof(FoodType)))
        {
            _productsName.Add(name);
        }
    }

    public void UpdateProgress()
    {
        _levelProgress.value = _currentAmount;
        if(_currentAmount == _amountToWin)
        {
            onWin?.Invoke();
        }
    }

    public void UpdateCurrentAmount()
    {
        _currentAmount++;
    }

}
