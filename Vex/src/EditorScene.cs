using Midnight;
using Midnight.GUI;

namespace Vex;

public class EditorScene : Scene {
    //private GUIDisplayer _gui;
    //private float _time;
    //private bool _triggered;

    //private Font f;
    //private bool anim;
    //private TextDisplayer text;

    public override void Prepare() {
        base.Prepare();
        /*
         *     Window
         *       |
         *     Frame
         *    /     \
         *   /       \
         * ButtonA ButtonB
         */


        /* UI as Entity-Component
         *
         * Button: Interactable, MultiState, Transform
         * ToggleButton -> Button
         *
         * Window win = Entities.Create<Window>();
         * Frame frame = Entities.Create<Frame>();
         * Button buttonA = Entities.Create<Button>(),
         *        buttonB = Entities.Create<ToggleButton>();
         *
         * frame.AddChild(buttonA);
         * frame.AddChild(buttonB);
         * win.AddChild(frame);
         *
         * UserButton buttonC = Entities.Create<UserButton>();
         *
         */

        /* UI as Component
         *
         * (Frame) Entity: Frame, Container, Transform
         * (Button) Entity: Button, Interactable, MultiState, Transform
         * (ToggleButton) Entity: ToggleButton, Interactable, MultiState, Transform
         * or
         * (ToggleButton) Entity: Toggle, Button, Interactable, MultiState, Transform
         *
         * Window win = Entities.Create<Window>();
         * Frame frame = Entities.Create<Frame>();
         * Button buttonA = Entities.Create<Button>(),
         *        buttonB = Entities.Create<Button>().With<Toggle>();
         *
         * UserButton buttonC = Entities.Create<UserButton>();
         *
         * frame.AddChild(buttonA);
         * frame.AddChild(buttonB);
         * win.AddChild(frame);
         *
         *
         */

         /*
         *
         * GUIDisplayer displayer = new();
         * GUI.Design design = displayer.Design;
         *
         * // Indirect
         *
         * Frame frame = design.Create<Frame>();
         * Button buttonA = frame.Container.Create<Button>();
         * Button buttonB = frame.Container.Create<Button>();
         *
         * // Immediate
         *
         * design.Builder.Start();
         *
         * void BuildMyGUI(GUI.DesignBuilder b) {
         *     using (var f = b.Frame()) {
         *         if (f.Button("Ok")) {
         *             ButtonOkPressed();
         *         } elif (f.Button("Cancel")) {
         *             ButtonCancelPressed();
         *         }
         *     }
         * }
         *
         * design.Builder.End();
         *
         */

        /*
        text = new() {
            Font = font,
            Value = "The quick brown fox jumps over the lazy dog",
        };

        if (text.Material is MTSDFShaderMaterial mat) {
            mat.InnerOutlineThickness = 1.2f;
            mat.InnerOutlineColor = ColorF.Red;

            mat.OuterOutlineThickness = 1.5f;
            mat.OuterOutlineColor = ColorF.Black;

            //mat.Rounding = 2.0f;
            //mat.OutlineRounding = 0.0f;

            mat.GlowColor = ColorF.Magenta;
            mat.GlowLength = 1.5f;
            mat.GlowIntensity = 3.0f;
        }
        */

        /*
        Entities.Create()
                .With<GUIDisplayer>(out GUIDisplayer guiDisplayer)
                .Submit();
        */

        //guiDisplayer.Design.Builder.Build(Build);

        Design design = new();
        design.Builder.Build(Build);

        //RenderingServer.MainCamera.Position = new(RenderingServer.MainCamera.Size.ToVector2() / 2.0f);
    }

    public override void Start() {
        base.Start();
        //Program.Debug.Visible = false;
    }

    public override void Update(DeltaTime dt) {
        base.Update(dt);

        /*
        if (_triggered) {
            return;
        }

        _time += dt.Sec;

        if (_time > 3.0) {
            if (_gui.Design.Builder.Result is Frame frame) {
                Button btn = frame.Container.Find<Button>();
                System.Console.WriteLine($"Button '{btn.TreeToString()}' pressed!");
                btn.Press();
            }

            _triggered = true;
        }
        */

        /*
        if (anim) {
            _time += dt.Sec;

            var midpoint = (220 - 16) / 2.0f;
            f.Size = Math.Max(midpoint + midpoint * Math.Sin(120 * _time), 16);
            //System.Console.WriteLine($"Font size: {f.Size}");
        }
        */
    }

    public override void Render(DeltaTime dt) {
        base.Render(dt);
        //System.Console.WriteLine("== New Render == ");
        //_gui.Render(dt, r);

        /*
        // TODO  move it to a test, it's to see every font
        var trans = text.Entity.Components.Get<Transform2D>();
        float y = 0;

        for (int i = 8; i <= 120; i += 4) {
            f.Size = i;
            trans.Position = new(0, y);
            text.Render(dt, r);
            y += text.SizeEm.Height * f.Size;

            if (y >= Game.Rendering.MainCamera.Size.Height) {
                break;
            }
        }
        */
    }

    private void Build(DesignBuilder b) {
        using (var f = b.Frame()) {
            if (f.PushButton("Ok")) {
                ButtonOkPressed();
            } else if (f.PushButton("Cancel")) {
                ButtonCancelPressed();
            } else {
                Logger.Line("Editor: Nothing pressed");
            }
        }
    }

    private void ButtonOkPressed() {
        Logger.Line("Editor: Button OK pressed");
    }

    private void ButtonCancelPressed() {
        Logger.Line("Editor: Button Cancel pressed");
    }
}
