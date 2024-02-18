using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace CoolGraphicsApp
{
    internal class PointsManager
    {
        private static PointPairList _pointPairs = new PointPairList();

        public static long SwapCount { get; private set; } = 0;
        public static long CompareCount { get; private set; } = 0;
        public static PointPairList GetPairs() => _pointPairs;

        public static void IncreaseSwap() => SwapCount++;
        public static void IncreaseCompare() => CompareCount++;

        public static void RegisterPoint(double x, double y)
        {
            _pointPairs.Add(new PointPair(x, y));
        }
    }
}
