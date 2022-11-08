#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JustSleightly
{
    public class DuplicateAsChild : MonoBehaviour
    {
        [MenuItem("GameObject/Duplicate As Child %#d", false, -753)] //Add to GameObject Context Menu
        public static void DupeAsChild(MenuCommand command)
        {
            var targetObject = command.context as GameObject; //Detect if this was clicked from right-click context menu
            var selectedObjects = Selection.GetFiltered<GameObject>(SelectionMode.Editable); //Locally Store Selected Editable Objects

            for (var i = 0; i < selectedObjects.Length; i++) //For Each Selected Editable Object
                if (targetObject == null) //If using top toolbar context menu or keyboard shortcut
                    selectedObjects[i] = DupeChild(selectedObjects[i]); //Replace Selection with New Duplicate of All
                else if (selectedObjects[i] == targetObject) //If using right-click context menu on GameObject
                    selectedObjects[i] = DupeChild(targetObject); //Replace Selection with New Duplicate of Self

            Selection.objects = selectedObjects; //Change Selection to New Objects
        }
        public static GameObject DupeChild(GameObject obj)
        {
            if (obj != null) //Check if Selection Exists
            {
                var newChild = Instantiate(obj, Vector3.zero, Quaternion.identity, obj.transform); //Duplicate GameObject and Set Transform/Parent
                newChild.name = obj.name; //Set Name of Child
                Undo.RegisterCreatedObjectUndo(newChild, "Duplicate " + obj.name + " as Child"); //Add Undo Record
                return newChild; //Return Child
            }
            else
                return null; //Ignore
        }
    }
}
#endif