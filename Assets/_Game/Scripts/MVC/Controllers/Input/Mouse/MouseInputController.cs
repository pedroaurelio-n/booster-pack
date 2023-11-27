using UnityEngine;

public class MouseInputController
{
    readonly IMouseInputModel _model;
    readonly MouseInputView _view;
    
    public MouseInputController (
        IMouseInputModel model,
        MouseInputView view
    )
    {
        _model = model;
        _view = view;
    }

    public void Initialize ()
    {
        AddViewListeners();
    }

    void AddViewListeners ()
    {
        _view.OnPositionChanged += HandlePositionChanged;
        _view.OnLeftClick += HandleLeftClick;
        _view.OnRightClick += HandleRightClick;
    }
    
    void RemoveViewListeners ()
    {
        _view.OnPositionChanged -= HandlePositionChanged;
        _view.OnLeftClick -= HandleLeftClick;
        _view.OnRightClick -= HandleRightClick;
    }

    void HandlePositionChanged (Vector3 position) => _model.UpdatePosition(position);

    void HandleLeftClick () => _model.LeftClick();
    
    void HandleRightClick () => _model.RightClick();
}