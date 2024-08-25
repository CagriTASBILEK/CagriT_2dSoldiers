using UnityEngine;

/// <summary>
/// LayerMaskUtilities class provides utility methods for working with LayerMasks.
/// </summary>
public static class LayerMaskUtilities
{
    /// <summary>
    /// Converts a LayerMask to a single layer.
    /// </summary>
    /// <param name="mask">The LayerMask to convert.</param>
    /// <returns>The single layer represented by the LayerMask, or -1 if no layer is found.</returns>
    public static int ToSingleLayer(this LayerMask mask)
    {
        int value = mask.value;
        if (value == 0) return 0;
        for (int l = 1; l < 32; l++)
        {
            if ((value & (1 << l)) != 0) return l;
        }
        return -1;
    }
}