using System;
using UnityEngine;

namespace FPSCursor
{
    public interface ICursor3D
    {
        bool CustomRaycast(Ray ray, out RaycastHit hit, float maxDist);
        bool Raycast(out RaycastHit hit);
        int Raycast(RaycastHit[] hit);
        IObservable<RaycastHit> OnClickSomething();
    }

    public static class Cursor3DExtension
    {
        public static bool RaycastType<T>(this ICursor3D cursor, out RaycastHit hit, out T t) where T : Component
        {
            t = null;
            return cursor.Raycast(out hit) && hit.collider.TryGetComponent(out t);
        }

        public static bool RaycastType<T>(this ICursor3D cursor, out T t) where T : Component
        {
            t = null;
            return cursor.Raycast(out var hit) && hit.collider.TryGetComponent(out t);
        }
    }
}