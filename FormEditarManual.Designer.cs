using System.Windows.Forms;

namespace WinFormsManual
{
    partial class FormEditarManual
    {
        private Label etiquetaTituloManual;
        private TextBox cuadroTituloManual;
        private ListBox listaPasos;
        private Label etiquetaTituloPaso;
        private TextBox cuadroTituloPaso;
        private Label etiquetaDescripcionPaso;
        private RichTextBox cuadroDescripcionPaso;
        private Label etiquetaRutaImagen;
        private TextBox cuadroRutaImagen;
        private Button botonBuscarImagen;
        private Button botonAgregarPaso;
        private Button botonAceptar;
        private Button botonCancelar;

        private void InitializeComponent()
        {
            etiquetaTituloManual = new Label();
            cuadroTituloManual = new TextBox();
            listaPasos = new ListBox();
            etiquetaTituloPaso = new Label();
            cuadroTituloPaso = new TextBox();
            etiquetaDescripcionPaso = new Label();
            cuadroDescripcionPaso = new RichTextBox();
            etiquetaRutaImagen = new Label();
            cuadroRutaImagen = new TextBox();
            botonBuscarImagen = new Button();
            botonAgregarPaso = new Button();
            botonAceptar = new Button();
            botonCancelar = new Button();
            SuspendLayout();

            etiquetaTituloManual.AutoSize = true;
            etiquetaTituloManual.Location = new System.Drawing.Point(12, 15);
            etiquetaTituloManual.Text = "Título del manual:";

            cuadroTituloManual.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cuadroTituloManual.Location = new System.Drawing.Point(120, 12);
            cuadroTituloManual.Size = new System.Drawing.Size(452, 23);

            listaPasos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            listaPasos.Location = new System.Drawing.Point(12, 50);
            listaPasos.Size = new System.Drawing.Size(220, 290);
            listaPasos.SelectedIndexChanged += listaPasos_SelectedIndexChanged;

            etiquetaTituloPaso.AutoSize = true;
            etiquetaTituloPaso.Location = new System.Drawing.Point(244, 50);
            etiquetaTituloPaso.Text = "Título del paso:";

            cuadroTituloPaso.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cuadroTituloPaso.Location = new System.Drawing.Point(244, 68);
            cuadroTituloPaso.Size = new System.Drawing.Size(328, 23);

            etiquetaDescripcionPaso.AutoSize = true;
            etiquetaDescripcionPaso.Location = new System.Drawing.Point(244, 100);
            etiquetaDescripcionPaso.Text = "Descripción del paso:";

            cuadroDescripcionPaso.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cuadroDescripcionPaso.Location = new System.Drawing.Point(244, 118);
            cuadroDescripcionPaso.Size = new System.Drawing.Size(328, 120);

            etiquetaRutaImagen.AutoSize = true;
            etiquetaRutaImagen.Location = new System.Drawing.Point(244, 245);
            etiquetaRutaImagen.Text = "Nombre de archivo de imagen:";

            cuadroRutaImagen.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cuadroRutaImagen.Location = new System.Drawing.Point(244, 263);
            cuadroRutaImagen.Size = new System.Drawing.Size(247, 23);

            botonBuscarImagen.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonBuscarImagen.Location = new System.Drawing.Point(497, 262);
            botonBuscarImagen.Size = new System.Drawing.Size(75, 25);
            botonBuscarImagen.Text = "Buscar...";
            botonBuscarImagen.Click += botonBuscarImagen_Click;

            botonAgregarPaso.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonAgregarPaso.Location = new System.Drawing.Point(416, 294);
            botonAgregarPaso.Size = new System.Drawing.Size(156, 25);
            botonAgregarPaso.Text = "Agregar paso";
            botonAgregarPaso.Click += botonAgregarPaso_Click;

            botonAceptar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonAceptar.Location = new System.Drawing.Point(404, 335);
            botonAceptar.Size = new System.Drawing.Size(75, 27);
            botonAceptar.Text = "Aceptar";
            botonAceptar.Click += botonAceptar_Click;

            botonCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            botonCancelar.Location = new System.Drawing.Point(497, 335);
            botonCancelar.Size = new System.Drawing.Size(75, 27);
            botonCancelar.Text = "Cancelar";
            botonCancelar.Click += botonCancelar_Click;

            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 374);
            Controls.Add(etiquetaTituloManual);
            Controls.Add(cuadroTituloManual);
            Controls.Add(listaPasos);
            Controls.Add(etiquetaTituloPaso);
            Controls.Add(cuadroTituloPaso);
            Controls.Add(etiquetaDescripcionPaso);
            Controls.Add(cuadroDescripcionPaso);
            Controls.Add(etiquetaRutaImagen);
            Controls.Add(cuadroRutaImagen);
            Controls.Add(botonBuscarImagen);
            Controls.Add(botonAgregarPaso);
            Controls.Add(botonAceptar);
            Controls.Add(botonCancelar);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar manual";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
