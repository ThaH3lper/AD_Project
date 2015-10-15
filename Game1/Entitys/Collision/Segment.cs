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
            return LineIntersectsRect(start, end, obj);
        }

        public bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }
        private bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);
            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
    }
}
