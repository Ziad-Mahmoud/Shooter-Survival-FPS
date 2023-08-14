using UnityEngine;

public class RemoveMissingScripts : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}
