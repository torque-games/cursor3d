using System;
using UnityEngine;

namespace FPSCursor
{
    [Serializable]
    public class FPSCursorData
    {
        public LayerMask layerMask;
        public float maxDistance = 1000;

        public FPSCursorData()
        {
        }

        public FPSCursorData(LayerMask layerMask, float maxDistance)
        {
            this.layerMask = layerMask;
            this.maxDistance = maxDistance;
        }
    }
}