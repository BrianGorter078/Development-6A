using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EntryPoint

{
    public class KDNode
    {
        public Vector2 Value { get; set; }
        public KDNode Left { get; set; }
        public KDNode Right { get; set; }
    }

    class KDTree
    {
        public KDNode rootNode { get; set; }
        public List<Vector2> BuildingsinsideRange;

        public KDTree()
        {
            rootNode = null;
            BuildingsinsideRange = new List<Vector2>();
        }


        public KDNode CreateKDTree(List<Vector2> specialbuildings, int depth)
        {
            if (specialbuildings.Count() == 0)
            {
                return null;
            }

            List<Vector2> left = new List<Vector2>();
            List<Vector2> right = new List<Vector2>();

            var middleInSpecialbuildings = specialbuildings.Count() / 2;         
            var middle = specialbuildings[middleInSpecialbuildings];
            specialbuildings.Remove(middle);


            for (var i = 0; i < specialbuildings.Count(); i++)
            {
                var specialbuildingXY = specialbuildings[i].Y;
                var b = middle.Y;

                if (depth % 2 == 0)
                {
                    specialbuildingXY = specialbuildings[i].X;
                    b = middle.X;
                }

                if (specialbuildingXY <= b)
                {
                    left.Add(specialbuildings[i]);
                }
                else if (specialbuildingXY > b)
                {
                    right.Add(specialbuildings[i]);
                }
            }

            return new KDNode
            {
                Value = middle,
                Left = CreateKDTree(left, depth + 1),
                Right = CreateKDTree(right, depth + 1)
            };
        }



        public void RangeSearch(Vector2 house, KDNode root, float maxDistance, int depth)
        {
            if (root == null)
            {
                return;
            }

            var Root = root.Value.Y;
            var House = house.Y;

            if (depth % 2 == 0)
            {
                Root = root.Value.X;
                House = house.X;
            }
            if (Root < (House - maxDistance))
            {
                RangeSearch(house, root.Right, maxDistance, depth + 1);
            }
            else if (Root > (House + maxDistance))
            {
                RangeSearch(house, root.Left, maxDistance, depth + 1);
            }
            else if ((Root >= (House - maxDistance)) && (Root <= (House + maxDistance)))
            {
                BuildingsinsideRange.Add(root.Value);
                RangeSearch(house, root.Left, maxDistance, depth + 1);
                RangeSearch(house, root.Right, maxDistance, depth + 1);
            }
        }
    }
}