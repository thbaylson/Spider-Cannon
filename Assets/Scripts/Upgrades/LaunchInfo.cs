using UnityEngine;

[CreateAssetMenu(fileName = "Launch Info Upgrade", menuName = "Spider Upgrades/Launch Info")]
public class LaunchInfo : Upgrade
{
    public GameObject launchInfoContainer;
    public override void Apply(SpiderLauncher spiderLauncher)
    {
        spiderLauncher.showLaunchInfo = true;
        GameObject[] iAmAshamed = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach(GameObject obj in iAmAshamed)
        {
            if (obj.tag == "LaunchInfoContainer")
            {
                obj.SetActive(true);
                //i feel like this is the dumbest way to do this. IM SO SHIT. 
            }
        }
        
        
    }
}
