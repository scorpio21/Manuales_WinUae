using System.Windows.Forms;

namespace WinFormsManual
{
    partial class FormPrincipal
    {
        private Button botonAbrirManual;

        private void InitializeComponent()
        {
            botonAbrirManual = new Button();
            SuspendLayout();
            
            botonAbrirManual.Text = "Abrir manual WinUAE";
            botonAbrirManual.AutoSize = true;
            botonAbrirManual.Anchor = AnchorStyles.None;
            botonAbrirManual.Click += botonAbrirManual_Click;
            
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(600, 200);
            Controls.Add(botonAbrirManual);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manual WinUAE";

            botonAbrirManual.Left = (ClientSize.Width - botonAbrirManual.Width) / 2;
            botonAbrirManual.Top = (ClientSize.Height - botonAbrirManual.Height) / 2;
            
            ResumeLayout(false);
        }
    }
}
