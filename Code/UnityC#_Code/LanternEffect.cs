using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
[Serializable]
[PostProcess (typeof (LanternEffectRenderer), PostProcessEvent.AfterStack, "Custom/LanternEffect")]
public sealed class LanternEffect : PostProcessEffectSettings {
    [Range (0f, 1f), Tooltip ("Grayscale effect intensity.")]
    public FloatParameter lightIntensity = new FloatParameter { value = 0.5f };

    public FloatParameter aspectRatio = new FloatParameter { value = 1.33f };    
}

public sealed class LanternEffectRenderer : PostProcessEffectRenderer<LanternEffect> {
    public override void Render (PostProcessRenderContext context) {
        var sheet = context.propertySheets.Get (Shader.Find ("Hidden/Custom/LanternEffect"));
        sheet.properties.SetFloat ("_LightIntensity", settings.lightIntensity);
        sheet.properties.SetFloat ("_AspectRatio", settings.aspectRatio);        
        context.command.BlitFullscreenTriangle (context.source, context.destination, sheet, 0);
    }
}