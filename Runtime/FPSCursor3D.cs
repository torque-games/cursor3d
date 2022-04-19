using System;
using UniRx;
using UnityEngine;

namespace FPSCursor
{
    public class FPSCursor3D : ICursor3D
    {
        private static readonly Vector2 Half = new Vector2(0.5f, 0.5f);
        private readonly FPSCursorData _data;
        private Camera _cam;
        private IObservable<RaycastHit> _onClickSomething;

        public FPSCursor3D(FPSCursorData data)
        {
            _data = data;
            _cam = Camera.main;
        }


        public bool CustomRaycast(Ray ray, out RaycastHit hit, float maxDist)
        {
            return Physics.Raycast(ray, out hit, maxDist, _data.layerMask);
        }

        public bool Raycast(out RaycastHit hit)
        {
            _cam ??= Camera.main;
            return Physics.Raycast(_cam.ViewportPointToRay(Half), out hit, _data.maxDistance, _data.layerMask);
        }

        public int Raycast(RaycastHit[] hits)
        {
            _cam ??= Camera.main;
            return Physics.RaycastNonAlloc(_cam.ViewportPointToRay(Half), hits, _data.maxDistance, _data.layerMask);
        }

        public IObservable<RaycastHit> OnClickSomething()
        {
            return _onClickSomething ??= Observable
                .EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Select(_ => (hits: Raycast(out var hit), info: hit))
                .Where(p => p.hits)
                .Select(p => p.info);
        }
    }
}