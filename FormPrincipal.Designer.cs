using System.Windows.Forms;

namespace WinFormsManual
{
    partial class FormPrincipal
    {
        private Button botonAbrirManual;
        private Button botonCheatCodes;

        private void InitializeComponent()
        {
            botonAbrirManual = new Button();
            botonCheatCodes = new Button();
            SuspendLayout();
            
            botonAbrirManual.Text = "Abrir manual WinUAE";
            botonAbrirManual.AutoSize = true;
            botonAbrirManual.Anchor = AnchorStyles.None;
            botonAbrirManual.Click += botonAbrirManual_Click;
            
            botonCheatCodes.Text = "Cheat Codes";
            botonCheatCodes.AutoSize = true;
            botonCheatCodes.Anchor = AnchorStyles.None;
            botonCheatCodes.Click += botonCheatCodes_Click;
            
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(600, 250);
            Controls.Add(botonAbrirManual);
            Controls.Add(botonCheatCodes);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manual WinUAE";

            botonAbrirManual.Left = (ClientSize.Width - botonAbrirManual.Width) / 2;
            botonAbrirManual.Top = (ClientSize.Height - botonAbrirManual.Height) / 2 - 30;
            
            botonCheatCodes.Left = (ClientSize.Width - botonCheatCodes.Width) / 2;
            botonCheatCodes.Top = (ClientSize.Height - botonCheatCodes.Height) / 2 + 30;
            
            ResumeLayout(false);
        }
    }
}
