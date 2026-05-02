using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBoundsChecker : MonoBehaviour
{
    public Vector2 minXZ;
    public Vector2 maxXZ;
    public float minHeight = -10f;

    void Update()
    {
        Vector3 pos = transform.position;

        // Check XZ bounds
        if (pos.x < minXZ.x || pos.x > maxXZ.x ||
            pos.z < minXZ.y || pos.z > maxXZ.y)
        {
            HandleOutOfBounds();
        } else 

        // Check height (water / falling)
        if (pos.y < minHeight)
        {
            HandleBelowHeight();
        }
    }

    void HandleOutOfBounds()
    {
        Debug.Log("Player left map bounds");
        MySceneOptions.endingOption = GameEndings.Falling;
        SceneManager.LoadScene(1);
    }

    void HandleBelowHeight()
    {
        Debug.Log("Player fell too low / into water");
        MySceneOptions.endingOption = GameEndings.Swimming;
        SceneManager.LoadScene(1);
    }
}