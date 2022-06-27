using Cosmos.System.Graphics;
using spaghettoOS.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaghettoOS {
    public static class PointExtensions {
        public static Point Add(this Point point, Point other) {
            return new Point(point.X + other.X, point.Y + other.Y);
        }

        public static Point Sub(this Point point, Point other) {
            return new Point(point.X - other.X, point.Y - other.Y);
        }

        public static Point Mul(this Point point, Point other) {
            return new Point(point.X * other.X, point.Y * other.Y);
        }

        public static Point Div(this Point point, Point other) {
            return new Point(point.X / other.X, point.Y / other.Y);
        }

        public static bool IsInBounds(this Point point, Rect bounds) {
            return (point.X, point.Y).IsInBounds(bounds);
        }

        public static bool IsInBounds(this (int X, int Y) point, Rect bounds) {
            return (point.X > bounds.X && point.X < bounds.X + bounds.Width &&
                point.Y > bounds.Y && point.Y < bounds.Y + bounds.Height);
        }
    }
}
