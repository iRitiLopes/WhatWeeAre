using UnityEngine;

public class ChildFinder {
    public static Transform findWithStartName(Transform parent, string name){
        for(int i = 0; i < parent.childCount; i ++){
            var child = parent.transform.GetChild(i);
            if(child.name.StartsWith(name)){
                return child;
            }
        }
        return null;
    }

    public static bool existChildWithName(Transform parent, string name){
        for(int i = 0; i < parent.childCount; i ++){
            var child = parent.transform.GetChild(i);
            if(child.name.StartsWith(name)){
                return true;
            }
        }
        return false;
    }
}