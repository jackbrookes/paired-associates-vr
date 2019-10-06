using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ObjectMeshCorrector : MonoBehaviour
{
    public float guideBoxSize = 0.05f;
    Vector3 transformBoundsCenter, renderedBoundsCenter, boundsSize;

#if UNITY_EDITOR
    void Reset()
    {
        FlushVertex();
    }
#endif

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.up * guideBoxSize / 2f, Vector3.one * guideBoxSize);
    }

    [ContextMenu("Update mesh")]
    void UpdateMesh()
    {
        GetTransformCentre();
        GetRendererCentre();
        Vector3 error = -renderedBoundsCenter;
        Debug.LogFormat("Error: {0}, {1}, {2}", error.x, error.y, error.z);
        EditVertices(error);
        GetRendererCentre();
    }

    [ContextMenu("Flush vertex")]
    void FlushVertex()
    {
        float min = float.PositiveInfinity;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            min = Mathf.Min(vertices[i].y, min);
        }

        if (min != float.PositiveInfinity)
        {
            EditVertices(new Vector3(0, -min, 0));
        }
    }

    void GetTransformCentre()
    {
        transformBoundsCenter = GetComponent<Transform>().localPosition;
        Debug.LogFormat("The transform centre are: {0}, {1}, {2}", transformBoundsCenter.x, transformBoundsCenter.y, transformBoundsCenter.z);
    }

    void GetRendererCentre()
    {
        renderedBoundsCenter = GetComponent<MeshFilter>().mesh.bounds.center;
        Debug.LogFormat("The center is: {0}, {1}, {2}", renderedBoundsCenter.x, renderedBoundsCenter.y, renderedBoundsCenter.z);
    }

    void EditVertices(Vector3 error)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i] + error;
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        MeshCollider mc = GetComponent<MeshCollider>();
        if (mc) mc.sharedMesh = mesh;
        Debug.LogFormat("Mesh centre:  {0}, {1}, {2}", mesh.bounds.center.x, mesh.bounds.center.y, mesh.bounds.center.z);
    }
}
