using UnityEngine;
using Input = NativeKeyboardHandler;

namespace DefaultNamespace
{
    public class PauseState : IState
    {
        readonly GameObject _pauseMenu;

        public PauseState(GameObject pauseMenu)
        {
            _pauseMenu = pauseMenu;
        }
        
        public void Enter()
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            Music.Instance.PauseMusic();
        }
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StateMachineService.Instance.SetDefaultState();
            }
        }
        public void Exit()
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            Music.Instance.PlayMusic();
        }
    }
}
