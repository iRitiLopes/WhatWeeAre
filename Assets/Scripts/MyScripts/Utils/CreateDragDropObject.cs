using UnityEngine;
public class CreateDragDropObject {
    public static GameObject createWrapper(Transform parent, Canvas canvas, bool draggable = true){
        var wrapper = new GameObject("Wrapper");
        wrapper.transform.parent = parent;
        wrapper.AddComponent<RectTransform>();
        wrapper.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        wrapper.AddComponent<CanvasGroup>();
        wrapper.layer = 0;

        if(draggable){
            wrapper.AddComponent<DragDrop>();
            wrapper.GetComponent<DragDrop>().Canvas = canvas;
        }
        
        return wrapper;
    }
}