using UnityEngine;
using System.Collections;

public static class Effects
{
    public static IEnumerator SimpleDamageEffect(Renderer[] renderers, float duration = 0.1f)
    {
        foreach(var rend in renderers)
        {
            Color previousColor = rend.material.color;
            rend.material.color = Color.red;
            yield return new WaitForSeconds(duration);
            rend.material.color = previousColor;
        }
    }
}