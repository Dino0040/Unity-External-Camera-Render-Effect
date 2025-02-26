using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class ExternalCamera : MonoBehaviour
{
    private static readonly int ExternalCameraMatrix = Shader.PropertyToID("_ExternalCameraMatrix");
    private static readonly int ExternalCameraDepth = Shader.PropertyToID("_ExternalCameraDepth");
    private static readonly int Dither = Shader.PropertyToID("_Dither");
    [SerializeField] private Texture2D ditherTexture;
    new Camera camera;
    UniversalCameraData cameraData;
    RenderTexture renderTexture;
    
    void Update()
    {
        if (!camera)
        {
            camera = GetComponent<Camera>();
            cameraData = GetComponent<UniversalCameraData>();
            if (!camera) return;
        }
        
        Shader.SetGlobalMatrix(ExternalCameraMatrix, camera.nonJitteredProjectionMatrix * camera.worldToCameraMatrix);
        Shader.SetGlobalTexture(ExternalCameraDepth, camera.targetTexture);
        if (ditherTexture)
        {
            Shader.SetGlobalTexture(Dither, ditherTexture);
        }
    }
}
