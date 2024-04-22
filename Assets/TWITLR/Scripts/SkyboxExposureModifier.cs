// 2023-11-28 AI-Tag 
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using UnityEngine;

public class SkyboxExposureModifier : MonoBehaviour
{
    public float increment = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (RenderSettings.skybox != null)
        {
            RenderSettings.skybox.SetFloat("_Exposure", 0);
            DynamicGI.UpdateEnvironment();
        }
    }

    // This method will increment the skybox exposure
    public void ModifyExposure()
    {
        if (RenderSettings.skybox != null)
        {
            float currentExposure = RenderSettings.skybox.GetFloat("_Exposure");
            RenderSettings.skybox.SetFloat("_Exposure", currentExposure + increment);
            DynamicGI.UpdateEnvironment();
        }
    }
}
