using UnityEngine;
public class CreateDragDropObject {
    public static GameObject createWrapper(Transform parent, Canvas canvas){
        var wrapper = new GameObject("Wrapper");
        wrapper.transform.parent = parent;
        wrapper.AddComponent<RectTransform>();
        wrapper.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        wrapper.AddComponent<CanvasGroup>();
        wrapper.AddComponent<DragDrop>();
        wrapper.GetComponent<DragDrop>().Canvas = canvas;
        wrapper.layer = 0;
        return wrapper;
    }
}