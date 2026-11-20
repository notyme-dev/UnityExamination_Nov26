using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private DialogWindow dw;
    [SerializeField] InputActionAsset inputActionAsset;
    private InputAction interactAction;
    public NPCDialog current_dialogue;

    private bool is_talking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactAction = inputActionAsset.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactAction != null && dw != null && current_dialogue != null)
        {
            if (interactAction.triggered)
            {
                if (!is_talking)
                {
                    dw.ShowDialog(true);
                    GlobalPlayerInfo gpi = GlobalPlayerInfo.Instance;
                    if (gpi != null)
                    {
                        if (gpi.collectedAllPoints)
                        {
                            dw.SetText(current_dialogue.win_text);
                        }
                        else
                        {
                            dw.SetText(current_dialogue.lose_text);
                        }
                    }

                    is_talking = true;
                }
                else
                {
                    dw.ShowDialog(false);
                    is_talking = false;
                }
                
            }
        }
    }

    public void ForceClose()
    {
        is_talking = false;
        dw.ShowDialog(false);
    }
}
