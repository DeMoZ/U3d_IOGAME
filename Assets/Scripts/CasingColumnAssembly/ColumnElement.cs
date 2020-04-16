using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;


namespace CasingColumnAssembly
{
    /// <summary>
    /// настройки элемента Колонны
    /// </summary>
    public class ColumnElement : MonoBehaviour
    {
        [Tooltip("Верхняя точка прикрепления элемента")]
        public Transform m_JointTop;
        [Tooltip("Нижняя точка прикрепления элемента")]
        public Transform m_JointBottom;

        [SerializeField] private Material m_metaltMaterial;
        [SerializeField] private Material m_concreteMaterial;

        private Transform m_CCElement;
        /// <summary>
        /// Префаб элемента конструкции
        /// </summary>
        public Transform GetCCElement
        {
            get
            {
                if (!m_CCElement)
                    throw new System.Exception($" пытаешься получить генерируемый элемент колонны, до того, как он сгененрирован");

                return m_CCElement;
            }
        }
        /// <summary>
        /// Генерируемый меш элемента
        /// </summary>
        ProBuilderMesh m_meshMetal;
        /// <summary>
        /// Гененируемый мешь бетонной части для элемента
        /// </summary>
        ProBuilderMesh m_meshConcrete;

        float m_radius = 2f;
        float m_width = 0.2f;
        float m_depth = 8f;

        private ProceduralParameters m_proceduralParameters;
        /// <summary>
        /// Параметры для создания процедурного элемента
        /// </summary>
        public ProceduralParameters GetPrceduralParameters
        {
            get
            {
                return m_proceduralParameters;
            }
            private set
            {
                m_proceduralParameters = value;
            }
        }
        
        ///// <summary>
        ///// создаст элемент с указанными параметрами
        ///// </summary>       
        public Transform Build(ProceduralParameters parameters)
        {
            GetPrceduralParameters = parameters;

            Transform metal = CreateElement(m_meshMetal, "Metal", m_metaltMaterial);
            m_CCElement = metal;

            Transform concrete = CreateElement(m_meshConcrete, "Concrete", m_concreteMaterial, GetPrceduralParameters.Radius * 0.1f, GetPrceduralParameters.Thiknes * 0.5f);
            // добавил небольшое смещение, чтобы металическая часть конструкции была чуть спереди
            concrete.position = new Vector3(concrete.position.x, -0.05f, -0.9f); 

            return metal;
        }

        /// <summary>
        /// Элемент колонны будет создан ме
        /// </summary>
        private Transform CreateElement(ProBuilderMesh mesh, string name, Material material, float addRaduis = 0, float addThiknes = 0)
        {
            if (mesh) Destroy(mesh.gameObject);

            // создаю арку
            mesh = ShapeGenerator.GenerateArch(
                PivotLocation.FirstVertex,
                angle: GetPrceduralParameters.Angle,
                radius: GetPrceduralParameters.Radius + addRaduis,
                width: GetPrceduralParameters.Thiknes + addThiknes,
                depth: GetPrceduralParameters.Length,
                radialCuts: GetPrceduralParameters.RadialCuts,
                insideFaces: true,
                outsideFaces: true,
                frontFaces: true,
                backFaces: true,
                endCaps: true);

            mesh.name = name;

            // сглаживание внут и наруж изгибов
            Smoothing.ApplySmoothingGroups(mesh, mesh.faces, 50);
            mesh.Refresh();

            // разворачиваю в вертикальное положение
            Transform meshT = mesh.transform;
            meshT.rotation = Quaternion.Euler(Vector3.right * 90);
            meshT.parent = gameObject.transform;
            meshT.localPosition += new Vector3(1, 0, -1);

            // вешаю материал (с текстурой)
            mesh.GetComponent<MeshRenderer>().sharedMaterial = material;

            // Unity UVs
            HardcodedUVs(mesh);

            return mesh.transform;
        }

        /// <summary>
        /// создание UV развертки методами Unity
        /// </summary>
        /// <param name="proMesh"></param>
        private void HardcodedUVs(ProBuilderMesh proMesh)
        {
            // с помощью ProBuilder, в эдиторе узнал индексы полигонов. 
            // на основе этих индексов создаю списки полигонов, относящихся к одному острову
            List<int> inFaces = new List<int> { 18, 16, 14, 12, 10, 8, 6, 4, 1 };// 1, 4, 6, 8, 10, 12, 14, 16, 18 };
            List<int> outFaces = new List<int> { 0, 3, 5, 7, 9, 11, 13, 15, 17 };// { 17, 15, 13, 11, 9, 7, 5, 3, 0 };
            List<int> cutFaces = new List<int> { 19, 2 };
            List<int> topFaces = new List<int> { 21, 23, 25, 27, 29, 31, 33, 35, 37 }; // { 37, 35, 33, 31, 29, 27, 25, 23, 21 };
            List<int> bottomFaces = new List<int> { 20, 22, 24, 26, 28, 30, 32, 34, 36 };

            // uv для внут 
            float u = Mathf.PI / 10 * GetPrceduralParameters.Radius;
            float v = GetPrceduralParameters.Length;
            // uv для внеш части
            float uo = Mathf.PI / 10 * (GetPrceduralParameters.Radius + GetPrceduralParameters.Thiknes);
            // uv для разреза.
            float uc = GetPrceduralParameters.Thiknes;


            Mesh mesh = proMesh.GetComponent<MeshFilter>().mesh;

            // все вертиксы модели
            Vector3[] vertices = mesh.vertices;

            // массив UV для каждого вертекса
            Vector2[] uvs = new Vector2[vertices.Length];

            int faceIndex = 0;

            // прохожу по массиву вертексов, прыжками по 4 элемена.
            // каждые 4 элемента оносятся к своему полигону.
            // получаю ингдекс полигона для каждой четверки и смотрю к какому острову относится полигон.
            // выставляю координаты для вертексов, с учитыванием индекса полигона в списке полигонов
            for (int i = 0; i < uvs.Length; i += 4)
            {
                faceIndex = i / 4;

                if (inFaces.Contains(faceIndex)) // внутренняя сторона
                {
                    int j = inFaces.IndexOf(faceIndex);

                    uvs[i] = new Vector2(u * j, 0);
                    uvs[i + 1] = new Vector2(u * (j + 1), 0);
                    uvs[i + 2] = new Vector2(u * j, v);
                    uvs[i + 3] = new Vector2(u * (j + 1), v);
                }
                else if (outFaces.Contains(faceIndex)) // внешняя сторона
                {
                    int j = outFaces.IndexOf(faceIndex);

                    uvs[i] = new Vector2(uo * j, 0);
                    uvs[i + 1] = new Vector2(uo * (j + 1), 0);
                    uvs[i + 2] = new Vector2(uo * j, v);
                    uvs[i + 3] = new Vector2(uo * (j + 1), v);
                }
                else if (cutFaces.Contains(faceIndex)) // продольные срезы
                {
                    int j = cutFaces.IndexOf(faceIndex);

                    uvs[i] = new Vector2(uc * j, 0);
                    uvs[i + 1] = new Vector2(uc * (j + 1), 0);
                    uvs[i + 2] = new Vector2(uc * j, v);
                    uvs[i + 3] = new Vector2(uc * (j + 1), v);
                }
                else if (topFaces.Contains(faceIndex))
                {
                    int j = topFaces.IndexOf(faceIndex);

                    uvs[i] = new Vector2(1 - 0.025f + 0.025f * j, 0.1f * i);
                    uvs[i + 1] = new Vector2(1 - 0.025f + 0.025f * (j + 1), 0.1f * i);
                    uvs[i + 2] = new Vector2(1 - 0.025f + 0.025f * j, 0.1f + 0.1f * i);
                    uvs[i + 3] = new Vector2(1 - 0.025f + 0.025f * (j + 1), 0.1f + 0.1f * i);
                }
                else if (bottomFaces.Contains(faceIndex))
                {
                    int j = bottomFaces.IndexOf(faceIndex);

                    uvs[i] = new Vector2(1 - 0.05f + 0.025f * j, 0.1f * i);
                    uvs[i + 1] = new Vector2(1 - 0.05f + 0.025f * (j + 1), 0.1f * i);
                    uvs[i + 2] = new Vector2(1 - 0.05f + 0.025f * j, 0.1f + 0.1f * i);
                    uvs[i + 3] = new Vector2(1 - 0.05f + 0.025f * (j + 1), 0.1f + 0.1f * i);
                }
                else
                {
                    uvs[i] = Vector2.zero;
                    uvs[i + 1] = Vector2.zero;
                    uvs[i + 2] = Vector2.zero;
                    uvs[i + 3] = Vector2.zero;
                }

                faceIndex++;
            }

            mesh.uv = uvs;

            for (int i = 0; i < mesh.uv.Length; i++)
            {
                Debug.Log($"mesh.uv[{i}] {mesh.uv[i]}");
            }
        }

        /// <summary>
        /// Метод требует разработки.
        /// Расставляет джоинты согласно границам элемента
        /// </summary>
        public void ArrangeJoints()
        {
            // GetCCElement.localPosition = Vector3.zero;
            //
            // Renderer renderer = GetCCElement.GetComponent<Renderer>();

            // Vector3 extents = renderer.bounds.extents;
            // Debug.Log("ColumnElement extentY = " + extentY);

            // TODO : надо умножать на поворот, потом двигать...
            // m_JointTop.localPosition = GetCCElement.localPosition + new Vector3(0, extents.x, 0);
            // m_JointBottom.localPosition = GetCCElement.localPosition - new Vector3(0, extents.z, 0);
        }
    }
}
