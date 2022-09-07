using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour {

    [SerializeField]
    GameObject button;
    private void Start() {
        button.GetComponent<Button>().onClick.AddListener(Quit);
    }

    private void Update() {
    }

    public void Quit() {
        Application.Quit();
    }

}