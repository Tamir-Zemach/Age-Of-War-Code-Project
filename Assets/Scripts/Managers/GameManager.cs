using Assets.Scripts;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private CharacterButton[] characterButtons;
    [Tooltip("The Spawn point for the Characters to diploy")]
    [SerializeField] private Transform _characterSpawnPoint;

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        Test();
    }

    public int _moneyToAdd;
    public int _moneyToSubstract;
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerCurrency.Instance.AddMoney(_moneyToAdd);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerCurrency.Instance.SubtractMoney(_moneyToSubstract);
        }
        PlayerCurrency.Instance.DisplyMoneyInConsole();

    }

    public void CharacterButtonPressed(CharacterButton button)
    {
        int selectedIndex = System.Array.IndexOf(characterButtons, button);
        if (selectedIndex != -1)
        {
            if (button.CanDeploy())
            {
                Diploy(button._characterPrefab);    
                PlayerCurrency.Instance.SubtractMoney(button._cost);
            }
            //Debug.Log($"Selected Character index is: {selectedIndex}, character cost : {button._cost}, can diploy : {button.CanDeploy()}");
        }
        else
        {
            Debug.LogError("Button not found in the array!");
        }
    }


    private void Diploy(GameObject characterPrefab)
    {
        Instantiate(characterPrefab, _characterSpawnPoint.position, _characterSpawnPoint.rotation);
    }

}
