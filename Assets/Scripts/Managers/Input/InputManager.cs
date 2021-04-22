
using UnityEngine;

public static class InputManager
{
    public static bool HasClick()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        if (Input.touchCount > 0) return true;

        return false;
    }

    public static Vector3 GetClickPosition()
    {
        if (Input.GetMouseButtonDown(0))
            return Input.mousePosition;

        if (Input.touchCount > 0)
            return Input.touches[0].position;

        return Vector3.zero;
    }
}
