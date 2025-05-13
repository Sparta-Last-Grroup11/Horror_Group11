using UnityEngine;

public class UITestButton : MonoBehaviour
{
    public GameObject dyingUIPrefab;

    //public void TestShowClearUI()
    //{
    //    UIManager.Instance.show<ClearUI>();
    //}

    public void TestShowDyingUI()
    {
        dyingUIPrefab.SetActive(true);
    }
}
