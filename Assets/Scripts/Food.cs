using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Food : MonoBehaviour
{
    public FoodType foodType;

    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private TextEffect _textEffect;
    [SerializeField] private float _foodLifeTime;

    private void OnEnable()
    {
        StartCoroutine(DisableFood());
    }

    private IEnumerator DisableFood()
    {
        yield return new WaitForSeconds(_foodLifeTime);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(_rigidbody.isKinematic)
        {
            _rigidbody.MovePosition(transform.position + _direction * _speed * Time.deltaTime);
        }
    }

    public void Drop()
    {
        _textEffect.StartAnimation();
        _rigidbody.isKinematic = false;
        transform.SetParent(null);
        StopAllCoroutines();
    }

    public void FreezeFoodInBasket(Transform basket)
    {
        transform.SetParent(basket);
    }
}

public enum FoodType
{
    cacke,
    jelly,
    banana
}
