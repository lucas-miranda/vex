using Midnight;

namespace Vex;

[SystemRegistry]
public class CameraSystem : EntitySystem {
    private bool _panning;
    private float _panningStrength = .05f;

    public override void Setup(Scene scene) {
        Subscribe<UpdateStepEvent>()
            .Submit(Update);

        Subscribe<MouseButtonEvent>()
            .Submit(HandleMouseButton);
    }

    private void Update(UpdateStepEvent ev) {
    }

    private void HandleMouseButton(MouseButtonEvent ev) {
        switch (ev.Button) {
            case MouseButton.Right:
                switch (ev.State) {
                    case ButtonState.JustPressed:
                        _panning = true;
                        break;
                    case ButtonState.Down:
                        if (_panning) {
                            RenderingServer.MainCamera.Position -= new Vector3(ev.Movement.ToFloat(), 0.0f) * _panningStrength;
                        }
                        break;
                    case ButtonState.JustReleased:
                        _panning = false;
                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
    }
}
