using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable, VolumeComponentMenu("Custom/VignetteEffect")]
public class VignetteEffect : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter intensity = new(1f, 0f, 1f);

    public bool IsActive() => intensity.value > 0f;
    public bool IsTileCompatible() => false;
}
