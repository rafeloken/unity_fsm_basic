using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public enum State { Initialize, SetupNewGame, Game, GameOver, Restart, Quit }
    public enum GameState { Idle, InGameMenu }

    InGameMenu igm;

    State _state = State.Initialize;
    State _prevState;

    GameState _gameState = GameState.Idle;
    GameState _prevGameState;

#region Basic Getters/Setters
    public State CurrentState {
        get { return _state; }
    }

    public State PrevState {
        get { return _prevState; }
    }

    public GameState CurrentGameState {
        get { return _gameState; }
    }

    public GameState PrevGameState {
        get { return _prevGameState; }
    }
#endregion

    void Awake() {
        // Do any general system initialization stuff here.
        igm = GetComponent<InGameMenu>();

        SetState(State.SetupNewGame);
    }

    // NOTE: Async version of Start.
	IEnumerator Start() {
	    while(true) {
            switch(_state) {
                case State.Initialize:
                    break;
                case State.SetupNewGame:
                    SetupNewGame();
                    break;
                case State.Game:
                    if(Input.GetKeyDown(KeyCode.G)) {
                        SetState(State.GameOver);
                    }
                    if(Input.GetKeyDown(KeyCode.M)) {
                        igm.enabled = !igm.enabled;
                    }

                    InGame();
                    break;
                case State.GameOver:
                    break;
                case State.Restart:
                    if(Input.GetKeyDown(KeyCode.R)) {
                        SetState(State.SetupNewGame);
                    }
                    if(Input.GetKeyDown(KeyCode.Q)) {
                        SetState(State.Quit);
                    }
                    break;
                case State.Quit:
                    break;
            }
            yield return null;
        }
	}

    void InGame() {
        switch(_gameState) {
            case GameState.Idle:
                break;
            case GameState.InGameMenu:
                break;
        }
    }

	void Update() {
                
	}

    // This coroutine is really just kinda simulating some sort of inner game logic loop.
    IEnumerator InitializeGameLogicStuff() {
        yield return new WaitForSeconds(1);
        while(true) {
            yield return new WaitForSeconds(5);

            if(_state == State.GameOver) {
                SetState(State.Restart);
                break;
            }
        }
    }

    void SetupNewGame() {
        StartCoroutine(InitializeGameLogicStuff());
        SetState(State.Game);
    }

    public void SetState(State newState) {
        _prevState = _state;
        _state = newState;
        PrintState();
    }

    public void SetGameState(GameState newState) {
        _prevGameState = _gameState;
        _gameState = newState;
        PrintState();
    }

    void PrintState() {
        Debug.Log("main state: " + _state.ToString() +
                  " | game state: " + _gameState.ToString());
    }
}
