using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawTools : Editor
{
	[DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
	static void DrawGameObjectName(Transform transform, GizmoType gizmoType)
	{
		if(transform.gameObject.GetComponent<ShowBornPos>()!=null)
			Handles.Label(transform.position, transform.gameObject.name + ":" + transform.gameObject.name);
	}
}
