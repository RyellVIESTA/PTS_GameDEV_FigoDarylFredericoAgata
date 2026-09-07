using UnityEngine;

public class PenerimaEvent : MonoBehaviour
{
    void onEnable()
    {
        PemancarEvent.TombolDitekan += TombolDitekanHandler;
    }

    void onDisable()
    {
        PemancarEvent.TombolDitekan -= TombolDitekanHandler;
    }

    void TombolDitekanHandler()
    {
        Debug.Log("Tombol ditekan! : Terima event");
    }
    
}
