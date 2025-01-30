using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Alchemy.Hierarchy;

namespace _Scripts
{
    [CustomEditor(typeof(HierarchyHeader))]
    public class HierarchyHeaderEditor : UnityEditor.Editor
    {
        private List<GameObject> createdCubes = new List<GameObject>();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            GUIStyle gulStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                normal =
                {
                    textColor = Color.white
                },
                fixedHeight = 30
            };

            GUI.backgroundColor = Color.cyan;

            if (GUILayout.Button("Kirill just press here", gulStyle))
            {
                BuildCubes();
            }

            if (createdCubes.Count > 0)
            {
                GUI.backgroundColor = Color.red;

                if (GUILayout.Button("Delete all cubes", gulStyle))
                {
                    DeleteCubes();
                }
            }

            GUI.backgroundColor = Color.white;
        }

        private void BuildCubes()
        {
            Vector3[] positions =
            {
                new Vector3(0, 0, -1), new Vector3(0, 0, 1), new Vector3(0, 1, 0), new Vector3(0, 2, 0),
                new Vector3(0, 3, 0)
            };

            foreach (var pos in positions)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = pos;
                cube.name = "GeneratedCube";
                createdCubes.Add(cube);
            }
        }

        private void DeleteCubes()
        {
            foreach (var cube in createdCubes)
            {
                if (cube != null)
                {
                    GameObject.DestroyImmediate(cube);
                }
            }

            createdCubes.Clear();
        }
    }
}