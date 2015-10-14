using Microsoft.Xna.Framework;
using Patrik.GameProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrik.GameProject
{
    public class Segment
    {
        public Vector2 start, end;

        public Segment(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }

        public Segment(float sx, float sy, float ex, float ey)
        {
            this.start = new Vector2(sx, sy);
            this.end = new Vector2(ex, ey);
        }

        public Vector2 GetNormal()
        {
            var len = (end - start).Length();
            float dx = (end.X - start.X) / len;
            float dy = (end.Y - start.Y) / len;

            //(-dy, dx) and (dy, -dx)
            if (dx < 0)
            {
                return new Vector2(dy, -dx);
            }
            else
            {
                return new Vector2(-dy, dx);
            }
        }

        public float GetRadians()
        {
            return (float)Math.Atan2((end - start).Y, (end - start).X);
        }

        public bool Collide(Rectangle obj)
        {
            float dst = DistanceFromPointToLineSegment(obj.Location.ToVector2(), start, end);
            if (dst <= Math.Max(obj.Width, obj.Height) / 3.2f)
            {
                return true;
            }
            return false;
        }

        private float DistanceFromPointToLineSegment(Vector2 point, Vector2 anchor, Vector2 end)
        {
            Vector2 d = end - anchor;
            float length = d.Length();
            if (d == Vector2.Zero) return (point - anchor).Length();
            d.Normalize();
            float intersect = Vector2.Dot((point - anchor), d);
            if (intersect < 0) return (point - anchor).Length();
            if (intersect > length) return (point - end).Length();
            return (point - (anchor + d * intersect)).Length();
        }
    }
}
