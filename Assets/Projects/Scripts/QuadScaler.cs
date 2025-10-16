using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class QuadScaler : MonoBehaviour
{
    public Texture2D texture; // Texture2D にすることで width/height が正確

    [SerializeField]
    private float aspect;

    void Start()
    {
        if (texture == null) return;

        Debug.Log("width" + (float)texture.width);
        Debug.Log("height" + (float)texture.height);

        // 新しいマテリアルを作成
        Material mat = new Material(Shader.Find("Unlit/Transparent"));
        mat.mainTexture = texture;

        // Quad に割り当て
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = mat;
        }

        // アスペクト比に合わせてスケール調整
        Debug.Log("width"+ (float)texture.width);
        Debug.Log("height"+ (float)texture.height);

        aspect = (float)texture.width / (float)texture.height;
        Vector3 scale = transform.localScale;
        scale.x = scale.y * aspect;
        transform.localScale = scale;
    }
}
