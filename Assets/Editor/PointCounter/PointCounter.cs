using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PointCounter))]
public class PointCounterExecutor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var mesh = (target as PointCounter).getMesh();

        if (!mesh) {
            return;
        }

        GUILayout.Label("頂点データ");
        GUILayout.BeginVertical("Box");
        {
            GUILayout.Label("Name\t\t\t" + mesh.name);
            GUILayout.Label("vertices\t\t\t" + mesh.vertices.Length);
            GUILayout.Label("colors\t\t\t" + mesh.colors.Length);
            GUILayout.Label("colors32\t\t" + mesh.colors32.Length);
            GUILayout.Label("normals\t\t\t" + mesh.normals.Length);
            GUILayout.Label("tangents\t\t" + mesh.tangents.Length);
            GUILayout.Label("triangles\t\t" + mesh.triangles.Length);
            GUILayout.Label("uv\t\t\t" + mesh.uv.Length);
            GUILayout.Label("uv2\t\t\t" + mesh.uv2.Length);
            GUILayout.Label("uv3\t\t\t" + mesh.uv3.Length);
            GUILayout.Label("uv4\t\t\t" + mesh.uv4.Length);
            GUILayout.Label("uv5\t\t\t" + mesh.uv5.Length);
            GUILayout.Label("uv6\t\t\t" + mesh.uv6.Length);
            GUILayout.Label("uv7\t\t\t" + mesh.uv7.Length);
            GUILayout.Label("uv8\t\t\t" + mesh.uv8.Length);
            GUILayout.Label("vertexAttributeCount\t" + mesh.vertexAttributeCount);
        }
        GUILayout.EndVertical();

        GUILayout.Label("バウンディング情報");
        GUILayout.BeginVertical("Box");
        {
            GUILayout.Label("center\t\t\t" + mesh.bounds.center);
            GUILayout.Label("max\t\t\t" + mesh.bounds.max);
            GUILayout.Label("min\t\t\t" + mesh.bounds.min);
            GUILayout.Label("size\t\t\t" + mesh.bounds.size);
            GUILayout.Label("extents\t\t\t" + mesh.bounds.extents);
        }
        GUILayout.EndVertical();
    }

    private string makeTableRow(string title, int count) {
        return title + "\t" + count.ToString();
    }
}


[CreateAssetMenu(menuName = "ScriptableObject/PointCounter", fileName = "PointCounter")]
public class PointCounter : ScriptableObject {
    [SerializeField] private Mesh mesh;

    public Mesh getMesh() {
        return mesh;
    }
}