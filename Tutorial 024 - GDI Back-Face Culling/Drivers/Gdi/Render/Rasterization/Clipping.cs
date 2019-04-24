﻿using System;
using RCi.Tutorials.Gfx.Drivers.Gdi.Materials;

namespace RCi.Tutorials.Gfx.Drivers.Gdi.Render.Rasterization
{
    /// <summary>
    /// Flags representing whether vertex is inside or outside (possibly multiple) clipping planes.
    /// </summary>
    [Flags]
    public enum ClippingPlane
    {
        Inside = 0,
        Left = 1 << 0,
        Right = 1 << 1,
        Bottom = 1 << 2,
        Top = 1 << 3,
        Near = 1 << 4,
        Far = 1 << 5,
    }

    /// <summary>
    /// Clipping options.
    /// </summary>
    public static class Clipping
    {
        public static bool CLIP_NEAR_PLANE_AT_ZERO { get; set; } = true;
    }

    /// <summary>
    /// Clipping for <see cref="TVertex"/> primitives.
    /// </summary>
    public static class Clipping<TVertex>
        where TVertex : IPsIn<TVertex>
    {
        /// <summary>
        /// Is vertex outside given plane?
        /// </summary>
        public static bool IsOutside(ClippingPlane plane, in TVertex vertex)
        {
            switch (plane)
            {
                case ClippingPlane.Inside:
                    return false;

                case ClippingPlane.Left:
                    return vertex.Position.X < -vertex.Position.W;

                case ClippingPlane.Right:
                    return vertex.Position.X > vertex.Position.W;

                case ClippingPlane.Bottom:
                    return vertex.Position.Y < -vertex.Position.W;

                case ClippingPlane.Top:
                    return vertex.Position.Y > vertex.Position.W;

                case ClippingPlane.Far:
                    return vertex.Position.Z > vertex.Position.W;

                case ClippingPlane.Near:
                    if (Clipping.CLIP_NEAR_PLANE_AT_ZERO)
                    {
                        return vertex.Position.Z < 0;
                    }
                    else
                    {
                        return vertex.Position.Z < -vertex.Position.W;
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(plane), plane, default);
            }
        }

        /// <summary>
        /// Get alpha value where plane intersects given line (made from vertices).
        /// </summary>
        private static float GetAlpha(ClippingPlane plane, in TVertex vertex0, in TVertex vertex1)
        {
            switch (plane)
            {
                case ClippingPlane.Inside:
                    return float.NaN;

                case ClippingPlane.Left:
                    return (vertex0.Position.X + vertex0.Position.W) / (vertex0.Position.X - vertex1.Position.X + vertex0.Position.W - vertex1.Position.W);

                case ClippingPlane.Right:
                    return (vertex0.Position.X - vertex0.Position.W) / (vertex0.Position.X - vertex1.Position.X - vertex0.Position.W + vertex1.Position.W);

                case ClippingPlane.Bottom:
                    return (vertex0.Position.Y + vertex0.Position.W) / (vertex0.Position.Y - vertex1.Position.Y + vertex0.Position.W - vertex1.Position.W);

                case ClippingPlane.Top:
                    return (vertex0.Position.Y - vertex0.Position.W) / (vertex0.Position.Y - vertex1.Position.Y - vertex0.Position.W + vertex1.Position.W);

                case ClippingPlane.Far:
                    return (vertex0.Position.Z - vertex0.Position.W) / (vertex0.Position.Z - vertex1.Position.Z - vertex0.Position.W + vertex1.Position.W);

                case ClippingPlane.Near:
                    if (Clipping.CLIP_NEAR_PLANE_AT_ZERO)
                    {
                        return vertex0.Position.Z / (vertex0.Position.Z - vertex1.Position.Z);
                    }
                    else
                    {
                        return (vertex0.Position.Z + vertex0.Position.W) / (vertex0.Position.Z - vertex1.Position.Z + vertex0.Position.W - vertex1.Position.W);
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(plane), plane, default);
            }
        }

        /// <summary>
        /// Clip line by given plane (modify given vertices if clipping).
        /// </summary>
        /// <returns><see langword="true"/> - if at least one point is inside given plane, <see langword="false"/> - if both points are clipped out.</returns>
        public static bool ClipByPlane(ClippingPlane plane, ref TVertex vertex0, ref TVertex vertex1)
        {
            var inside0 = !IsOutside(plane, vertex0);
            var inside1 = !IsOutside(plane, vertex1);

            if (inside0 && inside1)
            {
                // both inside
                return true;
            }

            if (!inside0 && !inside1)
            {
                // both outside
                return false;
            }

            // v0 or v1 inside
            if (inside0)
            {
                // v0 inside
                vertex1 = vertex0.InterpolateLinear(vertex1, GetAlpha(plane, vertex0, vertex1));
            }
            else
            {
                // v1 inside
                vertex0 = vertex0.InterpolateLinear(vertex1, GetAlpha(plane, vertex0, vertex1));
            }

            return true;
        }
    }
}
