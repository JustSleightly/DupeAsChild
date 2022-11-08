#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JustSleightly
{
    public class DuplicateAsChild : MonoBehaviour
    {
        [MenuItem("GameObject/Duplicate As Child %#d", false, -753)] //Add to GameObject Context Menu
        private static void DupeAsChild()
        {
            var selectedObjects = Selection.GetFiltered<GameObject>(SelectionMode.Editable); //Locally Store Selected Editable Objects

            for (var i = 0; i < selectedObjects.Length; i++) //For Each Selected Editable Object
                if (selectedObjects[i] != null) //Check if Selection Exists
                {
                    var newChild = Instantiate(selectedObjects[i], Vector3.zero, Quaternion.identity, selectedObjects[i].transform); //Duplicate GameObject and Set Transform/Parent
                    newChild.name = selectedObjects[i].name; //Set Name of Child
                    Undo.RegisterCreatedObjectUndo(newChild, "Duplicate " + selectedObjects[i].name + " as Child"); //Add Undo Record

                    selectedObjects[i] = newChild; //Replace Selection with New Duplicate
                }

            Selection.objects = selectedObjects; //Change Selection to New Objects
        }
    }
}
#endif