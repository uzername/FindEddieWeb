using UnityEngine;
/// <summary>
/// Adjust tiling of material's textures in Unity. 
/// Changing tiling updates immediately. Works in Edit Mode and Play Mode
/// Attach this script to 3D object (like a Cube) to set up tiling value.
/// Tiling in material applieas to all objects with that material while this script lets you set tiling per-object.
/// it's some advanced 3D wowow
/// </summary>
[ExecuteAlways]
public class PerObjectTiling : MonoBehaviour
{
    public Vector2 tiling = Vector2.one;

    Renderer _renderer;
    MaterialPropertyBlock _block;

    void Apply()
    {
        if (_renderer == null)
            _renderer = GetComponent<Renderer>();

        if (_block == null)
            _block = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_block);
        _block.SetVector("_BaseMap_ST", new Vector4(
            tiling.x, tiling.y, 0, 0
        ));
        _renderer.SetPropertyBlock(_block);
    }

    void OnValidate()
    {
        Apply();
    }

    void OnEnable()
    {
        Apply();
    }
}
