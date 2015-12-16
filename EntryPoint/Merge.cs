using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace EntryPoint
{
    class Merge
    {
        public List<Tuple<Vector2, float>> mergesort(List<Tuple<Vector2, float>> a)
        {
            if (a.Count <= 1) return a;
            int midpoint = a.Count / 2;
            List<Tuple<Vector2, float>> Left = new List<Tuple<Vector2, float>>();
            List<Tuple<Vector2, float>> Right = new List<Tuple<Vector2, float>>();

            for (int i = 0; i < midpoint; i++)
                Left.Add(a[i]);
            for (int i = midpoint; i < a.Count; i++)
                Right.Add(a[i]);

            Left = mergesort(Left);
            Right = mergesort(Right);

            return merge(Left, Right);
        }

        public static List<Tuple<Vector2, float>> merge(List<Tuple<Vector2, float>> Left, List<Tuple<Vector2, float>> Right)
        {
            List<Tuple<Vector2, float>> result = new List<Tuple<Vector2, float>>();
            while (Left.Count > 0 && Right.Count > 0)
            {
                if (Left[0].Item2 < Right[0].Item2)
                {
                    result.Add(Left[0]);
                    Left.RemoveAt(0);
                }
                else
                {
                    result.Add(Right[0]);
                    Right.RemoveAt(0);
                }

            }
            while (Left.Count > 0)
            {
                result.Add(Left[0]);
                Left.RemoveAt(0);
            }
            while (Right.Count > 0)
            {
                result.Add(Right[0]);
                Right.RemoveAt(0);
            }
            return result;
        }


    }
}
