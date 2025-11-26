using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Spell Effects/FireBall")]
public class FireBallEffectSO : EffectSO
{
    [SerializeField] private GameEvent _onFireBallCast;

    private CastContext _context;
    private SpellSO _self;

    public override void Init(CastContext castContext)
    {
        _context = castContext;
        _self = castContext.Spell;
        castContext.Spell.canCast = false;
    }

    public override void OnCast()
    {
        _onFireBallCast.Raise(this, null);
        var spawnPos = _context.SpawnPoint.position;

        var fireBall = FireBallFactory.Instance.GetFireBall();
        fireBall.transform.SetPositionAndRotation(spawnPos, Camera.main.transform.rotation);
    }

    public void SwitchOff() => _self.canCast = false;
    public void SwitchOn() => _self.canCast = true;
}