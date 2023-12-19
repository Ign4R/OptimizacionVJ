using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager: Updateable
{
    [SerializeField]
    private GameObject _splashScreen;
    [SerializeField]
    private Animation _animScreen;
    private static bool _SKIP;

    public override void Start()
    {
        base.Start();
        if (!_SKIP)
        {
            _splashScreen.SetActive(true);
        }    
    }
    public override void CustomUpdate()
    {     
        if (!_SKIP)
        {
            var _ANY_KEY = Input.anyKey;
            if (_ANY_KEY)
            {
                _animScreen.Play();
                _SKIP = true;
            }
        }

    }
    public void RunGame()
    {
        SceneManager.LoadScene("Game");
    }
}
