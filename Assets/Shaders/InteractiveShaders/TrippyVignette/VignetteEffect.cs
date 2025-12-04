using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable, VolumeComponentMenu("Custom/VignetteEffect")]
public class VignetteEffect : VolumeComponent, IPostProcessComponent
{
    public BoolParameter _enableEffect = new BoolParameter(false);

    public bool IsActive() => _enableEffect.value;
    public bool IsTileCompatible() => false;
}
