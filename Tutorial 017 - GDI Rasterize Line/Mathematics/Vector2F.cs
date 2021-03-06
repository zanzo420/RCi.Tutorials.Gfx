﻿using System;
using System.Runtime.InteropServices;

namespace RCi.Tutorials.Gfx.Mathematics
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vector2F
    {
        #region // static

        public static readonly Vector2F Zero = new Vector2F(0, 0);

        #endregion

        #region // storage

        public float X { get; }

        public float Y { get; }

        #endregion

        #region // ctor

        public Vector2F(float x, float y) => (X, Y) = (x, y);

        #endregion

        #region // operators

        public static Vector2F operator +(in Vector2F left, in Vector2F right)
        {
            return new Vector2F
            (
                left.X + right.X,
                left.Y + right.Y
            );
        }

        public static Vector2F operator -(in Vector2F left, in Vector2F right)
        {
            return new Vector2F
            (
                left.X - right.X,
                left.Y - right.Y
            );
        }

        public static Vector2F operator *(in Vector2F left, float right)
        {
            return new Vector2F
            (
                left.X * right,
                left.Y * right
            );
        }

        public static Vector2F operator /(in Vector2F left, float right)
        {
            return new Vector2F
            (
                left.X / right,
                left.Y / right
            );
        }

        #endregion

        #region // routines

        public override string ToString() => $"{X:0.000000}, {Y:0.000000}";

        #endregion
    }
}
