using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorImg; //바꿀 커서 이미지
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware); //cursorImg 텍스쳐로 커서 변경
    }
}
