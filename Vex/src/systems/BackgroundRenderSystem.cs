using Midnight;

namespace Vex;

[SystemRegistry]
public class BackgroundRenderSystem : EntitySystem {
    private RectangleDrawable? _backgroundTile;

    public Size2 TileSize => _backgroundTile?.Size ?? Size2.Zero;

    public override void Setup(Scene scene) {
        Subscribe<RenderStepEvent>()
            .Submit(Render);

        _backgroundTile = new RectangleDrawable() {
            Color = 0x00000003,
            Filled = true,
            Size = new(32, 32),
        };
    }

    private void Render(RenderStepEvent ev) {
        if (_backgroundTile == null) {
            return;
        }

        Camera camera = RenderingServer.MainCamera;

        int columns = Math.CeilI(camera.Size.Width / TileSize.Width),
            rows = Math.CeilI(camera.Size.Height / TileSize.Height),
            startX = 0;

        DrawSettings settings = Midnight.DrawSettings.Default with {
            WorldViewProjection = camera.Projection,
        };

        // using camera to positioning
        Vector2 displacement = -(camera.Position.ToVec2() % TileSize);

        // calculate correction if tile is odd or even
        Vector2 tile = (camera.Position.ToVec2() / TileSize).Round(System.MidpointRounding.ToZero);
        displacement += (tile % new Vector2(2, 2)) * TileSize;

        for (int y = -1; y < rows + 1; y++) {
            for (int x = startX - 1; x < columns + 1; x += 2) {
                _backgroundTile.Transform.Position = displacement + new Vector2(x, y) * _backgroundTile.Size;
                _backgroundTile.Draw(
                    ev.DeltaTime,
                    new DrawParams { DrawSettings = settings }
                );

            }

            startX = (startX + 1) % 2;
        }
    }
}
