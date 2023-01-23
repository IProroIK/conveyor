using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private float _offsetY = 5;
    [SerializeField] private float _timeToMove = 0.5f;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _text;
    private Quaternion _startRotation;

    private void Awake()
    {
        SaveStartRotaion();
    }

    private void Update()
    {
        FreezeRotation();
    }

    private void SaveStartRotaion()
    {
        _startRotation = _canvas.transform.rotation;
    }

    private void FreezeRotation()
    {
        _canvas.transform.rotation = _startRotation;
    }

    public void StartAnimation()
    {
        _canvas.gameObject.SetActive(true);
        Sequence textEffect = DOTween.Sequence();
        textEffect.Append(_text.transform.DOLocalMove(new Vector3(_text.transform.position.x, _text.transform.position.y + _offsetY, _text.transform.position.z), _timeToMove))
            //.Join(_text.DOFade(0.5f, _timeToMove))
            .OnComplete(() => _canvas.gameObject.SetActive(false));

    }
}
