using System;

namespace My3DGame
{
    /// <summary>
    /// 배열 조작을 돕는 헬퍼 클래스입니다.
    /// C#의 고정 크기 배열에 요소를 추가하거나 제거하는 편의 기능을 제공합니다.
    /// </summary>
    public static class ArrayHelper
    {
        #region Custom Methods
        /// <summary>
        /// 기존 배열에 요소를 추가하여 새로운 크기의 배열을 반환합니다.
        /// </summary>
        public static T[] Add<T>(T[] source, T item)
        {
            int originalLength = (source != null) ? source.Length : 0;
            T[] newArray = new T[originalLength + 1];

            if (originalLength > 0)
            {
                Array.Copy(source, newArray, originalLength);
            }

            newArray[originalLength] = item;
            return newArray;
        }

        /// <summary>
        /// 배열의 특정 인덱스 요소를 제거한 새 배열을 반환합니다.
        /// </summary>
        public static T[] RemoveAt<T>(T[] source, int index)
        {
            if (source == null || source.Length == 0)
            {
                return new T[0];
            }

            if (index < 0 || index >= source.Length)
            {
                return source;
            }

            T[] newArray = new T[source.Length - 1];
            int newIndex = 0;

            for (int i = 0; i < source.Length; i++)
            {
                if (i == index) continue;
                newArray[newIndex++] = source[i];
            }

            return newArray;
        }

        /// <summary>
        /// 배열의 특정 요소를 찾아 제거한 새 배열을 반환합니다.
        /// </summary>
        public static T[] Remove<T>(T[] source, T item) where T : class
        {
            if (source == null || source.Length == 0)
            {
                return new T[0];
            }

            int index = Array.IndexOf(source, item);
            if (index < 0)
            {
                return source;
            }

            return RemoveAt(source, index);
        }
        #endregion
    }
}
