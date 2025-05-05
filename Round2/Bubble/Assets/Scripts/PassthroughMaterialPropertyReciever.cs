using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Rendering;
    /// <summary>
    /// Apply affordance to material property block color property.
    /// </summary>
[RequireComponent(typeof(MaterialPropertyBlockHelper))]
public class PassthroughMaterialPropertyReciever : ColorAffordanceReceiver
{
    [SerializeField]
    [Tooltip("Material Property Block Helper component reference used to set material properties.")]
    MaterialPropertyBlockHelper m_MaterialPropertyBlockHelper;

    /// <summary>
    /// Material Property Block Helper component reference used to set material properties.
    /// </summary>
    public MaterialPropertyBlockHelper materialPropertyBlockHelper
    {
        get => m_MaterialPropertyBlockHelper;
        set => m_MaterialPropertyBlockHelper = value;
    }

    [SerializeField]
    [Tooltip("Shader property name to set the color of. When empty, the component will attempt to use the default for the current render pipeline.")]
    string m_PassthroughAlpha;

    /// <summary>
    /// Shader property name to set the color of.
    /// </summary>
    public string colorPropertyName
    {
        get => m_PassthroughAlpha;
        set
        {
            m_PassthroughAlpha = value;
            UpdatePassthroughAlpha();
        }
    }

    int m_AlphaValue;

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    protected void OnValidate()
    {
        if (m_MaterialPropertyBlockHelper == null)
            m_MaterialPropertyBlockHelper = GetComponent<MaterialPropertyBlockHelper>();
    }

    /// <inheritdoc/>
    protected override void Awake()
    {
        base.Awake();

        if (m_MaterialPropertyBlockHelper == null)
            m_MaterialPropertyBlockHelper = GetComponent<MaterialPropertyBlockHelper>();

        UpdatePassthroughAlpha();
    }

    /// <inheritdoc/>
    /// Recieves color and translates to alpha value. Which is kind of a hack.
    protected override void OnAffordanceValueUpdated(Color newValue)
    {
        m_MaterialPropertyBlockHelper.GetMaterialPropertyBlock()?.SetFloat(m_PassthroughAlpha, newValue.b);
        base.OnAffordanceValueUpdated(newValue);
    }

    /// <inheritdoc/>
    protected override Color GetCurrentValueForCapture()
    {
        Color colOut = Color.white;
        colOut.b = m_MaterialPropertyBlockHelper.GetMaterialPropertyBlock().GetFloat(m_PassthroughAlpha);
        return colOut;
    }

    void UpdatePassthroughAlpha() {
        if (!string.IsNullOrEmpty(m_PassthroughAlpha)) {
            m_AlphaValue = Shader.PropertyToID(m_PassthroughAlpha);
        } else {
            m_AlphaValue = Shader.PropertyToID("_InvertedAlpha");
        }
    }
}