using UnityEngine;

public class InGameMenu : MonoBehaviour {
    GameManager mgr;

    void OnEnable() {
        mgr = GetComponent<GameManager>();
        mgr.SetGameState(GameManager.GameState.InGameMenu);
        Time.timeScale = 0f;
        Debug.Log("Paused");
        _setup();
    }

    void OnDisable() {
        mgr.SetGameState(mgr.PrevGameState);
        Time.timeScale = 1f;        
        _teardown();
        Debug.Log("Resumed");
    }

    void _setup() {
        Debug.Log("Do menu setup stuff");
    }

    void _teardown() {
        Debug.Log("Do menu teardown stuff");
    }
}
