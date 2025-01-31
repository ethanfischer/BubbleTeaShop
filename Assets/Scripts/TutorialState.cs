using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialState : MonoBehaviour, IState
{
    [SerializeField]
    IngredientsInstructions _ingredientsInstructions;
    public static bool IsTutorialActive { get; private set; } = false;

    private Level _level;
    private Level Level
    { get
    {
        if (_level == null)
        {
            _level = Level.Instance;
        }
        return _level;
    } }

    public static bool[] CompletedTutorials { get; private set; } =
    {
        false,
        false,
        true, //Level 2 doesn't need a tutorial
        false,
        false,
        false
    };

    public void Enter()
    {
        IsTutorialActive = true;
        Debug.Log("Showing tutorial");
    }

    void IState.Update()
    {
        if (!IsTutorialActive) return;

        ShowIngredientToKeyInstructions(Level.LevelIndex);

        if (_ingredientsInstructions.DidCompleteTutorial)
        {
            IsTutorialActive = false;
            CompletedTutorials[Level.LevelIndex] = true;
            StateMachineService.Instance.SetState(new DefaultState());
        }
    }
    
    public void Exit()
    {
    }

    void ShowIngredientToKeyInstructions(int i)
    {
        var order = OrderSystem.Instance.Orders.First();
        _ingredientsInstructions.ShowIngredientToKeyInstructions(order, i);
    }

}
