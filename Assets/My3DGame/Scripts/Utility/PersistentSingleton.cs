using UnityEngine;

namespace My3DGame
{
    /// <summary>
    /// 씬 전환 시 파괴되지 않고 유지되는 싱글톤 클래스입니다.
    /// </summary>
    public class PersistentSingleton<T> : Singleton<T> where T : Singleton<T>
    {
        #region Unity Event Methods
        protected override void Awake()
        {
            // 부모 클래스의 Awake를 호출하여 인스턴스를 지정하거나 중복 인스턴스를 정리합니다.
            base.Awake();

            // 씬 전환 시 파괴되지 않도록 설정합니다.
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion
    }
}
