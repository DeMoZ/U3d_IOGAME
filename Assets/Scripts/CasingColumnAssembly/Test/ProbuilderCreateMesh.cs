using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

namespace Test
{
    public class ProbuilderCreateMesh : MonoBehaviour
    {
        public float _angle = 180f;
        public float _radius = 2f;
        public float _width = 0.2f;
        public float _depth = 8f;
        public int _radialCuts = 10;

        private float __angle = float.MaxValue;
        private float __radius = float.MaxValue;
        private float __width = float.MaxValue;
        private float __depth = float.MaxValue;
        private int __radialCuts = int.MaxValue;

        ProBuilderMesh m_mesh;
        // Start is called before the first frame update
        void Start()
        {
            // CreateMesh();
            // каждую интервал проверяю не менялись ли какие то параметры, чтобы пересобрать меш
            InvokeRepeating("InvokedMethod", 0, 1);
            // m_LastExtrudedFace = m_Mesh.faces[0];


            //// Create a new quad facing forward.
            //ProBuilderMesh quad = ProBuilderMesh.Create(
            //    new Vector3[] {
            //new Vector3(0f, 0f, 0f),
            //new Vector3(1f, 0f, 0f),
            //new Vector3(0f, 1f, 0f),
            //new Vector3(1f, 1f, 0f)
            //    },
            //    new Face[] { new Face(new int[] { 0, 1, 2, 1, 3, 2 } )
            //});


        }

        public void CreateMesh()
        {
            Debug.Log("Create");


            if (m_mesh) Destroy(m_mesh.gameObject);

            m_mesh = ShapeGenerator.GenerateArch(
                PivotLocation.FirstVertex,
                angle: _angle,
                radius: _radius,
                width: _width,
                depth: _depth,
                radialCuts: _radialCuts,
                insideFaces: true,
                outsideFaces: true,
                frontFaces: true,
                backFaces: true,
                endCaps: true);

            m_mesh.GetComponent<MeshRenderer>().sharedMaterial = BuiltinMaterials.defaultMaterial;

            m_mesh.transform.rotation = Quaternion.Euler(Vector3.right * 90);

            IList<Face> faces = m_mesh.faces;

            foreach (Face face in m_mesh.faces)
            {
                //face.smoothingGroup = 1;            
                face.textureGroup = 1;
            }

            Smoothing.ApplySmoothingGroups(m_mesh, faces, 90);
        }

        private void InvokedMethod()
        {
            if (IfValueChanged()) CreateMesh();
        }
        private bool IfValueChanged()
        {
            bool rezult = false;
            if (_angle != __angle)
            {
                __angle = _angle;
                rezult = true;
            }

            if (_radius != __radius)
            {
                __radius = _radius;
                rezult = true;
            }

            if (_width != __width)
            {
                __width = _width;
                rezult = true;
            }

            if (_depth != __depth)
            {
                __depth = _depth;
                rezult = true;
            }

            if (_radialCuts != __radialCuts)
            {
                __radialCuts = _radialCuts;
                rezult = true;
            }

            return rezult;
        }
    }
}