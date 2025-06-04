using UnityEngine;

public class PlayerPresenter
{
    private Playermodel model;
    private PlayerView view;
    public PlayerPresenter(Playermodel Model, PlayerView View)
    {
        this.model = Model;
        this.view = View;
        view.SetPosition(model.Position);
    }
    public void updatePresenter()
    {
        
        Vector3 input = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W)) { input = Vector3.up; }
        if (Input.GetKeyDown(KeyCode.S)) { input = Vector3.down; }
        if (Input.GetKeyDown(KeyCode.A)) { input = Vector3.left; }
        if (Input.GetKeyDown(KeyCode.D)) { input = Vector3.right; }

        if(input != Vector3.zero)
        {
            model.Move(input);
            view.SetPosition(model.Position);
            model.UpdateDirection(input);
            view.UpdateSprite(model.LastDirection);
        }
    }
 
}
