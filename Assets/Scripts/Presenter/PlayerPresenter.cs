using UnityEngine;

public class PlayerPresenter
{
    private Playermodel model;
    private PlayerView view;
    private GameModel gamemodel;
    private bool isInputEnabled = true;
    public event System.Action OnPlayerWin;
    public PlayerPresenter(Playermodel model, PlayerView view)
    {
        this.model = model;
        this.view = view;
        view.SetPosition(model.Position);
    }
    public void SetInputEnabled(bool enabled)
    {
        isInputEnabled = enabled;
    }
    public void OnInput(Vector3 input)
    {
        if (!isInputEnabled) return;
        if (model == null || input == Vector3.zero) return;

        model.Move(input);
        view.SetPosition(model.Position);
        model.UpdateDirection(input);
        view.UpdateSprite(model.LastDirection);
        SoundManager.Instance.PlaySFX(view.stepclip);
        if (model.CheckWin())
        {
            OnPlayerWin?.Invoke();
        }
    }
}