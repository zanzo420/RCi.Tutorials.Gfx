﻿using System.Runtime.CompilerServices;
using MathNet.Spatial.Euclidean;

namespace RCi.Tutorials.Gfx.Mathematics.Extensions
{
    public static class Vector4FEx
    {
        #region // import

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this Point2D value, float z, float w) => new Vector4F((float)value.X, (float)value.Y, z, w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this Vector2D value, float z, float w) => new Vector4F((float)value.X, (float)value.Y, z, w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this Vector2F value, float z, float w) => new Vector4F(value.X, value.Y, z, w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this in Point3D value, float w) => new Vector4F((float)value.X, (float)value.Y, (float)value.Z, w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this in Vector3D value, float w) => new Vector4F((float)value.X, (float)value.Y, (float)value.Z, w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this in UnitVector3D value, float w) => new Vector4F((float)value.X, (float)value.Y, (float)value.Z, w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this Vector3F value, float w) => new Vector4F(value.X, value.Y, value.Z, w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this in Vector4D value) => new Vector4F((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this Vector4F value) => new Vector4F(value.X, value.Y, value.Z, value.W);

        #endregion

        #region // export

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double[] ToDoubles(this Vector4F value) => new double[] { value.X, value.Y, value.Z, value.W };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float[] ToFloats(this Vector4F value) => new[] { value.X, value.Y, value.Z, value.W };

        #endregion

        #region // colors

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F FromRgbaToVector4F(this int value)
        {
            var r = value & 0xFF;
            var g = (value >> 8) & 0xFF;
            var b = (value >> 16) & 0xFF;
            var a = (value >> 24) & 0xFF;
            return new Vector4F(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F FromArgbToVector4F(this int value)
        {
            var b = value & 0xFF;
            var g = (value >> 8) & 0xFF;
            var r = (value >> 16) & 0xFF;
            var a = (value >> 24) & 0xFF;
            return new Vector4F(r / 255f, g / 255f, b / 255f, a / 255f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToArgb(this Vector4F value)
        {
            var r = (byte)(value.X * byte.MaxValue);
            var g = (byte)(value.Y * byte.MaxValue);
            var b = (byte)(value.Z * byte.MaxValue);
            var a = (byte)(value.W * byte.MaxValue);
            return ((((a << 8) + r) << 8) + g << 8) + b;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToRgba(this Vector4F value)
        {
            var r = (byte)(value.X * byte.MaxValue);
            var g = (byte)(value.Y * byte.MaxValue);
            var b = (byte)(value.Z * byte.MaxValue);
            var a = (byte)(value.W * byte.MaxValue);
            return ((((r << 8) + g) << 8) + b << 8) + a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector4F ToVector4F(this System.Drawing.Color value)
        {
            return new Vector4F(value.R / 255f, value.G / 255f, value.B / 255f, value.A / 255f);
        }

        #endregion
    }
}