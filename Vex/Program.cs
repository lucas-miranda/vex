using Midnight;
using Vex;

App app = new App(MidnightConfig.Default with {
    Organization = "Luke Raccoon",
    Application = "Vex",
    Graphics = MidnightConfig.Default.Graphics with {
        BackBuffer = new() {
            Width = 1920 / 2,
            Height = 1080 / 2,
        }
    },
});

app.LoadScene<EditorScene>();
app.Run();
