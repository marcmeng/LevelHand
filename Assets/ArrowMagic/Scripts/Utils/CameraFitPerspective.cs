using UnityEngine;

namespace PixelBug.ArrowMagic
{
    public static class CameraFitPerspective
    {
        public static float ComputeDistanceToFitRect(
            Camera cam,
            float worldWidth,
            float worldHeight,
            float padding = 0f)
        {
            float w = worldWidth + padding * 2f;
            float h = worldHeight + padding * 2f;

            float halfH = h * 0.5f;
            float halfW = w * 0.5f;

            float vFov = cam.fieldOfView * Mathf.Deg2Rad;
            float hFov = 2f * Mathf.Atan(Mathf.Tan(vFov * 0.5f) * cam.aspect);

            float distByHeight = halfH / Mathf.Tan(vFov * 0.5f);
            float distByWidth = halfW / Mathf.Tan(hFov * 0.5f);

            return Mathf.Max(distByHeight, distByWidth);
        }

        public static void FrameBoard(
            Camera cam,
            Vector3 boardCenter,
            float boardWorldWidth,
            float boardWorldHeight,
            float padding = 0.5f)
        {
            float dist = ComputeDistanceToFitRect(
                cam,
                boardWorldWidth,
                boardWorldHeight,
                padding
            );

            cam.transform.position = new Vector3(
                boardCenter.x,
                boardCenter.y,
                boardCenter.z - dist
            );

            cam.transform.rotation = Quaternion.identity; // looking straight down -Z
        }
    }
}