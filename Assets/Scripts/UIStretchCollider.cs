using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class UIStretchCollider : MonoBehaviour
{
    void Awake()
    {
        ResizeCollider();
    }

    void ResizeCollider()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        GetComponent<BoxCollider2D>().size = new Vector2(rectTransform.rect.width,
                                                         rectTransform.rect.height);
    }

#if UNITY_EDITOR
    void Update()
    {
        ResizeCollider();
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneView;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneView;
    }

    void OnSceneView(SceneView view)
    {
        Update();
    }
#endif
}