using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Tests
{
    public static class ReflectionUtility
    {
#if UNITY_EDITOR

        public static void AddObjectReferenceValueToSerializedProperty<U, T>(U gameObject, string propertyName, T value)
            where T : Object
            where U : Object
        {
            var serializedObject = new SerializedObject(gameObject);
            serializedObject.FindProperty(propertyName).objectReferenceValue = value;
            serializedObject.ApplyModifiedProperties();
        }
#endif

        public static T GetPrivateField<T>(this object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            return (T)field.GetValue(obj);
        }

        public static void SetPrivateField<T>(this object obj, string fieldName, T value)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(obj, value);
        }

        public static void SetPrivateFieldNull(this object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(obj, null);
        }

        public static void CallPrivateStaticMethod(this object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            method.Invoke(obj, parameters);
        }

        public static void SetPublicField(this object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);
            field.SetValue(obj, value);
        }

        public static T GetPrivateStaticField<T>(this object obj, string constName)
        {
            var field = obj.GetType().GetField(constName, BindingFlags.NonPublic | BindingFlags.Static);
            return (T)field.GetValue(obj);
        }

        public static void CallProtectedMethod(this object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(obj, parameters);
        }

        public static async Task CallAsyncProtectedMethod(this object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method.Invoke(obj, parameters);
        }
}
}