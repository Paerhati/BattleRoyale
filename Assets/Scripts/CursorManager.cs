using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public Texture2D DefaultWorldCursor;
    public Vector2 DefaultWorldCursorHotspot;

    public Texture2D EnemyCursor;
    public Vector2 EnemyCursorHotspot;

    void Start()
    {
        SetDefaultWorldCursor();
    }

    void Update()
    {
        if (IsMouseOverUI())
        {
            SetDefaultUICursor();
        }
        else
        {
            HandleWorldCursor();
        }
    }

    void HandleWorldCursor()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Enemy")
            {
                SetEnemyWorldCursor();
            }
            else
            {
                SetDefaultWorldCursor();
            }
        }
        else
        {
            SetDefaultWorldCursor();
        }
    }

    void SetDefaultUICursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void SetDefaultWorldCursor()
    {
        Cursor.SetCursor(
            DefaultWorldCursor,
            DefaultWorldCursorHotspot,
            CursorMode.Auto);
    }

    void SetEnemyWorldCursor()
    {
        Cursor.SetCursor(
            EnemyCursor,
            EnemyCursorHotspot,
            CursorMode.Auto);
    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

