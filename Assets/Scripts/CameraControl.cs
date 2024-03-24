/*
    Oyuncu kontrolü sýrasýnda yukarý aþaðýya bakabilmek için gerekli script.
 */

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float sensitivity = 1f,
                  curXRot;

    void Update()
    {
        if (GameManager.instance.gameOn)
        {
            float y = Input.GetAxisRaw("Mouse Y") * sensitivity;
            curXRot += y;
            curXRot = Mathf.Clamp(curXRot, -20f, 20f);
            transform.localEulerAngles = new Vector3(-curXRot, 0f,0f);
        }
    }
}
