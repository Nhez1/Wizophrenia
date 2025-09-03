public interface ISpell
{
    string Name { get; }
    float ManaCost { get; }
    Mana Mana { get; }

    void Cast();
}
