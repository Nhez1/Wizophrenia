using UnityEngine;
using System;

public class StateOverlay : MonoBehaviour
{
    [Header("Overlays de Sangre")]
    [SerializeField] private GameObject _blood30;
    [SerializeField] private GameObject _blood50;
    [SerializeField] private GameObject _blood80;

    private PlayerLife _playerLife;

    private void Start()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        _playerLife = p.GetComponent<Player>().Life;            

        _playerLife.OnHealthChanged += UpdateBloodOverlay;
    }

    private void OnDisable()
    {
        _playerLife.OnHealthChanged -= UpdateBloodOverlay;
    }

    private void UpdateBloodOverlay(float currentHP)
    {
        _blood30.SetActive(false);
        _blood50.SetActive(false);
        _blood80.SetActive(false);

        if (currentHP <= 30) _blood30.SetActive(true);
        else if (currentHP <= 50) _blood50.SetActive(true);
        else if (currentHP <= 80) _blood80.SetActive(true);
    }
}
