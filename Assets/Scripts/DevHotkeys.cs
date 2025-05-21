using UnityEngine;
using UnityEngine.SceneManagement;

public class DevHotkeys : MonoBehaviour
{
    [SerializeField] private UpgradeManager upgradeManager;

    private void Awake()
    {
        upgradeManager = FindFirstObjectByType<UpgradeManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            upgradeManager.SetGold(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            upgradeManager.SetGold(99999);
        }
    }
}
