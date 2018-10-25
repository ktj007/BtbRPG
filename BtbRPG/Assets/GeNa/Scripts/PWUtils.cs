using UnityEngine;

namespace GeNa
{
	public class PWUtils : MonoBehaviour
	{
		/// <summary>
		/// Get the nearest vertice of the target object to the source position provided in world units
		/// </summary>
		/// <param name="sourcePosition">Source we are checking from</param>
		/// <param name="targetObject">Target object we are processing</param>
		/// <returns>Closest vertice</returns>
		public static Vector3 GetNearestVertice(Vector3 sourcePosition, GameObject targetObject)
		{
			float closestDistance = float.MaxValue;
			Vector3 closestVertice = targetObject.transform.position;
			Vector3 meshWorldPosition = closestVertice;
			Vector3 meshScale = targetObject.transform.localScale;
			Vector3 meshRotation = targetObject.transform.eulerAngles;
			MeshFilter[] filters = targetObject.GetComponentsInChildren<MeshFilter>();
			for (int fIdx = 0; fIdx < filters.Length; fIdx++)
			{
				Mesh mesh = filters[fIdx].sharedMesh;
				if (mesh != null)
				{
					int vertLength = mesh.vertices.Length;
					Vector3[] vertices = mesh.vertices;
					for (int i = 0; i < vertLength; i++)
					{
						Vector3 vertexWorldPosition = meshWorldPosition + (Quaternion.Euler(meshRotation) * Vector3.Scale(vertices[i], meshScale));
						float actualDistance = Vector3.Distance(sourcePosition, vertexWorldPosition);
						if (actualDistance < closestDistance)
						{
							closestDistance = actualDistance;
							closestVertice = vertexWorldPosition;
						}
					}
				}
			}
			return closestVertice;
		}
	}
}
