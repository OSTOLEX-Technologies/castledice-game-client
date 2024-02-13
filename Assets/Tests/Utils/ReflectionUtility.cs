using UnityEditor;
using UnityEngine;

namespace Tests
{
    public static class ReflectionUtility
    {
#if UNITY_EDITOR

        public static void AddObjectReferenceValueToSerializedProperty<U, T>(U gameObject, string propertyName, T value) where T : Object 
            where U: Object
        {
            var serializedObject = new SerializedObject(gameObject);
            serializedObject.FindProperty(propertyName).objectReferenceValue = value;
            serializedObject.ApplyModifiedProperties();
        }
#endif
        
        public static T GetPrivateField<T>(this object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (T) field.GetValue(obj);
        }
        
        public static void SetPrivateField<T>(this object obj, string fieldName, T value)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field.SetValue(obj, value);
        }
        
        public static void SetPrivateFieldNull(this object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field.SetValue(obj, null);
        }
    }
}