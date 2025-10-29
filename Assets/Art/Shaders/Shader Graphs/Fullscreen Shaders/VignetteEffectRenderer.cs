using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteEffectRenderer : ScriptableRendererFeature
{
    class Pass : ScriptableRenderPass
    {
        static readonly string kTag = "Custom Vignette";
        Material material;
        VignetteEffect settings;

        public Pass(Material mat) => material = mat;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (settings == null || !settings.IsActive()) return;

            var cmd = CommandBufferPool.Get(kTag);
            material.SetFloat("_Intensity", settings.intensity.value);
            Blit(cmd, ref renderingData, material, 0);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public void SetEffect(VignetteEffect settings) => this.settings = settings;
    }

    public Material vignetteMaterial;
    Pass pass;

    public override void Create()
    {
        pass = new Pass(vignetteMaterial)
        {
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var stack = VolumeManager.instance.stack;
        var settings = stack.GetComponent<VignetteEffect>();
        pass.SetEffect(settings);
        renderer.EnqueuePass(pass);
    }
}
