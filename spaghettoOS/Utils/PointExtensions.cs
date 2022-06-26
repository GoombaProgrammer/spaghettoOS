using Cosmos.System.Graphics;
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
    }
}
