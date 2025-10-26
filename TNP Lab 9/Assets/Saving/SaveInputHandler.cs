using UnityEngine;
using UnityEngine.InputSystem;

public class SaveInputHandler : MonoBehaviour
{
    public TransformSaver transformSaver; // Assign in Inspector
    private SaveInputs saveInputs;

    private void Awake()
    {
        saveInputs = new SaveInputs();
    }

    private void OnEnable()
    {
        saveInputs.Game.Enable();
        saveInputs.Game.Save.performed += OnSavePerformed;
        saveInputs.Game.Load.performed += OnLoadPerformed;
    }

    private void OnDisable()
    {
        saveInputs.Game.Save.performed -= OnSavePerformed;
        saveInputs.Game.Load.performed -= OnLoadPerformed;
        saveInputs.Game.Disable();
    }

    private void OnSavePerformed(InputAction.CallbackContext context)
    {
        if (transformSaver != null)
            transformSaver.SaveTransforms();
    }

    private void OnLoadPerformed(InputAction.CallbackContext context)
    {
        if (transformSaver != null)
            transformSaver.LoadTransforms();
    }
}
