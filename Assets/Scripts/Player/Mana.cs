using UnityEngine;

[System.Serializable]
public class Mana
{
    [Tooltip("Maximum MP")]
    [SerializeField] private float _maxMP = 100;
    [Tooltip("Mana Points")]
    [SerializeField] private float _mp;

    public float MaxMP { get { return _maxMP; } private set { } }
    public float MP { get { return _mp; } private set => _mp = Mathf.Clamp(value, 0f, _maxMP); }

    public Mana()
    {
        MP = _maxMP;
    }

    public void ManaSpend(float amount)
    {
        MP -= amount;
    }

    /// <summary>
    /// Restore a specified amount of Mana to the player.
    /// </summary>
    public void ManaRegain(float amount)
    {
        if (_mp < _maxMP)
        {
            MP += amount;
            MP = Mathf.Min(_maxMP); // Limita el mana al máximo
        }

    }

    /// <summary>
    /// Reduce the player's maximum MP by a specified amount.
    /// </summary>
    /// <param name="amount">How much maxMP you want to take away from the player.</param>
    public void ReduceMaxMP(float amount)
    {
        if (amount >= _maxMP) _maxMP = 0;
        else _maxMP -= amount;
    }

    public void SetMP(float amount) => MP = amount;
    public void SetMaxMP(float amount) => MaxMP = amount;
}
