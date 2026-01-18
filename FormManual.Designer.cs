using System.Windows.Forms;

namespace WinFormsManual
{
    partial class FormManual
    {
        private ComboBox comboManuales;
        private Button botonAgregarManual;
        private ListBox listaPasos;
        private RichTextBox cuadroTextoPaso;
        private PictureBox imagenPaso;
        private Button botonImagenAnterior;
        private Button botonImagenSiguiente;
        private Label etiquetaIndiceImagen;
        private MenuStrip menuPrincipal;
        private ToolStripMenuItem menuArchivo;
        private ToolStripMenuItem menuSalir;
        private ToolStripMenuItem menuManuales;
        private ToolStripMenuItem menuAgregarManual;
        private ToolStripMenuItem menuCheatCodes;

        private void InitializeComponent()
        {
            comboManuales = new ComboBox();
            botonAgregarManual = new Button();
            listaPasos = new ListBox();
            cuadroTextoPaso = new RichTextBox();
            imagenPaso = new PictureBox();
            botonImagenAnterior = new Button();
            botonImagenSiguiente = new Button();
            etiquetaIndiceImagen = new Label();
            menuPrincipal = new MenuStrip();
            menuArchivo = new ToolStripMenuItem();
            menuSalir = new ToolStripMenuItem();
            menuManuales = new ToolStripMenuItem();
            menuAgregarManual = new ToolStripMenuItem();
            menuCheatCodes = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)imagenPaso).BeginInit();
            SuspendLayout();

            // Combo de manuales debajo del menú
            comboManuales.DropDownStyle = ComboBoxStyle.DropDownList;
            comboManuales.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboManuales.Location = new System.Drawing.Point(12, 36);
            comboManuales.Width = 660;
            comboManuales.SelectedIndexChanged += comboManuales_SelectedIndexChanged;

            // Botón para editar el manual seleccionado
            botonAgregarManual.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            botonAgregarManual.Location = new System.Drawing.Point(678, 35);
            botonAgregarManual.Size = new System.Drawing.Size(96, 24);
            botonAgregarManual.Text = "Editar manual";
            botonAgregarManual.Click += botonEditarManual_Click;

            listaPasos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listaPasos.Location = new System.Drawing.Point(12, 74);
            listaPasos.Size = new System.Drawing.Size(220, 364);
            listaPasos.SelectedIndexChanged += listaPasos_SelectedIndexChanged;

            // Texto en una franja superior centrada
            cuadroTextoPaso.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cuadroTextoPaso.Location = new System.Drawing.Point(240, 74);
            cuadroTextoPaso.Size = new System.Drawing.Size(532, 90);
            cuadroTextoPaso.ReadOnly = true;

            // Imagen grande debajo del texto ocupando todo el área central/derecha
            imagenPaso.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            imagenPaso.Location = new System.Drawing.Point(240, 194);
            imagenPaso.Size = new System.Drawing.Size(532, 244);
            imagenPaso.SizeMode = PictureBoxSizeMode.Zoom;
            imagenPaso.Cursor = Cursors.Hand;
            imagenPaso.Click += imagenPaso_Click;

            // Botones de navegación de imágenes
            botonImagenAnterior.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            botonImagenAnterior.Location = new System.Drawing.Point(240, 174);
            botonImagenAnterior.Size = new System.Drawing.Size(30, 20);
            botonImagenAnterior.Text = "<";
            botonImagenAnterior.Click += botonImagenAnterior_Click;

            botonImagenSiguiente.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            botonImagenSiguiente.Location = new System.Drawing.Point(340, 174);
            botonImagenSiguiente.Size = new System.Drawing.Size(30, 20);
            botonImagenSiguiente.Text = ">";
            botonImagenSiguiente.Click += botonImagenSiguiente_Click;

            etiquetaIndiceImagen.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            etiquetaIndiceImagen.Location = new System.Drawing.Point(272, 177);
            etiquetaIndiceImagen.AutoSize = true;
            etiquetaIndiceImagen.Text = string.Empty;

            // Menú principal
            menuPrincipal.Items.AddRange(new ToolStripItem[] { menuArchivo, menuManuales, menuCheatCodes });
            menuPrincipal.Location = new System.Drawing.Point(0, 0);
            menuPrincipal.Name = "menuPrincipal";
            menuPrincipal.Size = new System.Drawing.Size(784, 24);

            // Menú "Archivo"
            menuArchivo.Text = "Archivo";
            menuArchivo.DropDownItems.AddRange(new ToolStripItem[] { menuSalir });

            menuSalir.Text = "Salir";
            menuSalir.Click += menuSalir_Click;

            // Menú "Manuales"
            menuManuales.Text = "Manuales";
            menuManuales.DropDownItems.AddRange(new ToolStripItem[] { menuAgregarManual });

            // Opción "Agregar manual"
            menuAgregarManual.Text = "Agregar manual";
            menuAgregarManual.Click += botonAgregarManual_Click;

            // Menú "Cheat Codes" (ahora es un menú principal)
            menuCheatCodes.Text = "Cheat Codes";
            menuCheatCodes.Click += menuCheatCodes_Click;

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 461);
            Controls.Add(menuPrincipal);
            Controls.Add(comboManuales);
            Controls.Add(botonAgregarManual);
            Controls.Add(listaPasos);
            Controls.Add(cuadroTextoPaso);
            Controls.Add(botonImagenAnterior);
            Controls.Add(etiquetaIndiceImagen);
            Controls.Add(botonImagenSiguiente);
            Controls.Add(imagenPaso);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manual de WinUAE";

            ((System.ComponentModel.ISupportInitialize)imagenPaso).EndInit();
            ResumeLayout(false);
        }
    }
}
