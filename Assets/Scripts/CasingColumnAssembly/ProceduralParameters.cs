using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly
{
    /// <summary>
    /// параметры, по которым гененируется элемент колонны
    /// </summary>
    public class ProceduralParameters
    {
        public float Angle { get; set; }
        public int RadialCuts { get; set; }

        public float Radius { get; set; }
        public float Thiknes { get; set; }
        public float Length { get; set; }

        public ProceduralParameters(float radius, float thicknes, float length, int radialCuts = 10, float angle = 180)
        {
            Angle = angle;
            RadialCuts = radialCuts;
            Radius = radius;
            Thiknes = thicknes;
            Length = length;
        }
    }
}