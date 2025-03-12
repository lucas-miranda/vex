using Midnight;

namespace Vex;

[SystemRegistry]
public class GuideRenderSystem : EntitySystem {
    private LineDrawable? _line;

    public override void Setup(Scene scene) {
        Subscribe<RenderStepEvent>()
            .Submit(Render);

        _line = new LineDrawable() {
            Color = 0x333333FF,
            Opacity = .03f,
            Width = 2.0f,
        };
    }

    private void Render(RenderStepEvent ev) {
        if (_line == null) {
            return;
        }

        Camera camera = RenderingServer.MainCamera;

        // horizontal line
        _line.PointA = new(camera.Position.X, 0.0f);
        _line.PointB = new(camera.Size.Width, 0.0f);
        _line.Draw(ev.DeltaTime);

        // vertical line
        _line.PointA = new(0.0f, camera.Position.Y);
        _line.PointB = new(0.0f, camera.Size.Height);
        _line.Draw(ev.DeltaTime);
    }
}
