using UnityEngine;

namespace My3DGame
{
    /// <summary>
    /// 제네릭 싱글톤 클래스
    /// MonoBehaviour를 상속받는 싱글톤 클래스의 부모 클래스입니다.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        #region Variables
        private static T instance; // 싱글톤 인스턴스
        #endregion

        #region Properties
        // 외부에서 인스턴스에 접근할 수 있는 프로퍼티
        public static T Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region Unity Event Methods
        protected virtual void Awake()
        {
            // 이미 인스턴스가 존재하면 새로 생성된 오브젝트를 파괴합니다.
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            // 인스턴스를 지정합니다.
            instance = (T)this;
        }
        #endregion
    }
}
