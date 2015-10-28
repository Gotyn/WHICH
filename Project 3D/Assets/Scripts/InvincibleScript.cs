using UnityEngine;
using System.Collections;

/// <summary>
/// This will be used to store some settings in case we reload the scene.
/// </summary>
public class InvincibleScript : MonoBehaviour {
    private static InvincibleScript instance;

    public float volume = 1.0f;
    public bool showSplash = true;
    public bool dialogsEnabled = true;
    
    public static InvincibleScript Instance {
        get {
            
            if (instance == null) {
                instance = new GameObject("InvincibleScript").AddComponent<InvincibleScript>();
            } 
            return instance;
        }
    }
}
