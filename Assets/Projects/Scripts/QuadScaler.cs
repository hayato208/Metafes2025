using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class QuadScaler : MonoBehaviour
{
    public Texture2D texture; // Texture2D �ɂ��邱�Ƃ� width/height �����m

    [SerializeField]
    private float aspect;

    void Start()
    {
        if (texture == null) return;

        Debug.Log("width" + (float)texture.width);
        Debug.Log("height" + (float)texture.height);

        // �V�����}�e���A�����쐬
        Material mat = new Material(Shader.Find("Unlit/Transparent"));
        mat.mainTexture = texture;

        // Quad �Ɋ��蓖��
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = mat;
        }

        // �A�X�y�N�g��ɍ��킹�ăX�P�[������
        Debug.Log("width"+ (float)texture.width);
        Debug.Log("height"+ (float)texture.height);

        aspect = (float)texture.width / (float)texture.height;
        Vector3 scale = transform.localScale;
        scale.x = scale.y * aspect;
        transform.localScale = scale;
    }
}
