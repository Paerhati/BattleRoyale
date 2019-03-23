using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Effects
{
    public static IEnumerator SimpleDamageEffect(Renderer[] renderers, float duration = 0.05f)
    {
        var previousColors = new Queue<Color>();

        foreach(var rend in renderers)
        {
            previousColors.Enqueue(rend.material.color);

            rend.material.color = Color.red;
        }

        yield return new WaitForSeconds(duration);

        foreach(var rend in renderers)
        {
            rend.material.color = previousColors.Dequeue();
        }
    }
}