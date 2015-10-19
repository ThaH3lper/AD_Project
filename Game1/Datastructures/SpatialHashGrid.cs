
using Game1.Datastructures.ADT;
using Game1.Datastructures.Implementations;
using Microsoft.Xna.Framework;
using Patrik.GameProject;
using Patrik.GameProject.Datastructures.Implementations;
using System;
using Game1.Entitys;
using System.Linq;

namespace Game1.Datastructures
{
    /// <summary>
    /// TODO Explain
    /// Insert O(1) (hashtable)
    /// Remove Average O(1)
    /// Clear O(1)
    /// 
    /// </summary>
    class SpatialHashGrid
    {
        private Hashtable<int, IList<GameObject>> Buckets;

        private int Rows;
        private int Cols;
        private int SceneWidth;
        private int SceneHeight;
        private int CellSize;

        public void Setup(int scenewidth, int sceneheight, int cellsize)
        {
            Cols = scenewidth / cellsize;
            Rows = sceneheight / cellsize;
            Buckets = new Hashtable<int, IList<GameObject>>(Cols * Rows);

            for (int i = 0; i < Cols * Rows; i++)
            {
                Buckets.Put(i, new LinkedList<GameObject>());
            }

            SceneWidth = scenewidth;
            SceneHeight = sceneheight;
            CellSize = cellsize;
        }


        public void ClearBuckets()
        {
            //Buckets.Clear();
            for (int i = 0; i < Cols * Rows; i++)
            {
                Buckets.Get(i).Clear();
            }
        }

        public void AddObject(GameObject obj)
        {
            var cellIds = GetIdForObj(obj);
            foreach (var item in cellIds)
            {
                Buckets.Get(item).Add(obj);
            }
        }

        public void AddObject(System.Collections.Generic.IEnumerable<GameObject> objs)
        {
            foreach (var item in objs)
            {
                AddObject(item);
            }
        }

        private IList<int> GetIdForObj(GameObject obj)
        {
            var bucketsObjIsIn = new LinkedList<int>();

            Vector2 min = new Vector2(
                obj.GetPosition().X - (obj.GetMaxRadius()),
                obj.GetPosition().Y - (obj.GetMaxRadius()));

            Vector2 max = new Vector2(
                obj.GetPosition().X + (obj.GetMaxRadius()),
                obj.GetPosition().Y + (obj.GetMaxRadius()));

            float width = Cols;

            //TopLeft
            AddBucket(min, width, bucketsObjIsIn);

            //TopRight
            AddBucket(new Vector2(max.X, min.Y), width, bucketsObjIsIn);

            //BottomRight
            AddBucket(new Vector2(max.X, max.Y), width, bucketsObjIsIn);

            //BottomLeft
            AddBucket(new Vector2(min.X, max.Y), width, bucketsObjIsIn);

            return bucketsObjIsIn;
        }

        private void AddBucket(Vector2 vector, float width, IList<int> buckettoaddto)
        {
            int cellPosition = (int)((Math.Floor(vector.X / CellSize)) + (Math.Floor(vector.Y / CellSize)) * width);

            if (!buckettoaddto.Contains(cellPosition))
                buckettoaddto.Add(cellPosition);

        }

        public GameObject[] GetPossibleColliders(GameObject obj)
        {
            var objects = new LinkedList<GameObject>();
            var bucketIds = GetIdForObj(obj);
            foreach (var item in bucketIds)
            {
                objects.AddRange(Buckets.Get(item));
            }
            return objects.Distinct().ToArray();
        }


    }
}
