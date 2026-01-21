using System.Windows.Forms;

namespace WinFormsManual
{
    partial class FormDocumentacion
    {
        private TextBox txtBusqueda;
        private Label lblBusqueda;
        private ListBox lstDocumentos;
        private ComboBox comboIdioma;
        private Panel panelContenido;
        private WebBrowser webContenido;
        private Button btnCerrar;
        private Button btnFavorito;
        private ComboBox comboFiltro;
        private Label lblDocumentos;
        private Label lblContenido;

        private void InitializeComponent()
        {
            txtBusqueda = new TextBox();
            lstDocumentos = new ListBox();
            comboIdioma = new ComboBox();
            lblBusqueda = new Label();
            lblDocumentos = new Label();
            lblContenido = new Label();
            btnCerrar = new Button();
            btnFavorito = new Button();
            comboFiltro = new ComboBox();
            panelContenido = new Panel();
            webContenido = new WebBrowser();

            SuspendLayout();

            // Panel principal para el contenido
            panelContenido.BorderStyle = BorderStyle.FixedSingle;
            panelContenido.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            panelContenido.Location = new System.Drawing.Point(328, 31);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new System.Drawing.Size(450, 355);
            panelContenido.TabIndex = 7;

            // WebBrowser para contenido con formato profesional
            webContenido.Dock = DockStyle.Fill;
            webContenido.Location = new System.Drawing.Point(0, 0);
            webContenido.MinimumSize = new System.Drawing.Size(20, 20);
            webContenido.Name = "webContenido";
            webContenido.Size = new System.Drawing.Size(448, 353);
            webContenido.TabIndex = 0;

            // Agregar WebBrowser al panel
            panelContenido.Controls.Add(webContenido);

            // comboFiltro - Dropdown para filtrar favoritos
            comboFiltro.DropDownStyle = ComboBoxStyle.DropDownList;
            comboFiltro.FormattingEnabled = true;
            comboFiltro.Items.AddRange(new object[] { "Todos", "Favoritos ★", "No favoritos" });
            comboFiltro.Location = new System.Drawing.Point(218, 9);
            comboFiltro.Name = "comboFiltro";
            comboFiltro.Size = new System.Drawing.Size(120, 23);
            comboFiltro.TabIndex = 1;
            comboFiltro.SelectedIndexChanged += comboFiltro_SelectedIndexChanged;
            comboFiltro.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboFiltro.SelectedIndex = 0;

            // comboIdioma
            comboIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            comboIdioma.FormattingEnabled = true;
            comboIdioma.Items.AddRange(new object[] { "Inglés", "Español" });
            comboIdioma.Location = new System.Drawing.Point(12, 9);
            comboIdioma.Name = "comboIdioma";
            comboIdioma.Size = new System.Drawing.Size(200, 23);
            comboIdioma.TabIndex = 0;
            comboIdioma.SelectedIndexChanged += comboIdioma_SelectedIndexChanged;
            comboIdioma.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboIdioma.SelectedIndex = 0;

            // lblBusqueda
            lblBusqueda.AutoSize = true;
            lblBusqueda.Location = new System.Drawing.Point(12, 42);
            lblBusqueda.Name = "lblBusqueda";
            lblBusqueda.Size = new System.Drawing.Size(180, 15);
            lblBusqueda.TabIndex = 1;
            lblBusqueda.Text = "Buscar (puedes escribir parcial):";
            lblBusqueda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            // txtBusqueda
            txtBusqueda.Location = new System.Drawing.Point(12, 64);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new System.Drawing.Size(300, 27);
            txtBusqueda.TabIndex = 2;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            txtBusqueda.Text = "Ej: zool, zork, prince...";
            txtBusqueda.ForeColor = System.Drawing.Color.Gray;
            txtBusqueda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            txtBusqueda.BorderStyle = BorderStyle.FixedSingle;

            // lstDocumentos
            lstDocumentos.FormattingEnabled = true;
            lstDocumentos.ItemHeight = 16;
            lstDocumentos.Location = new System.Drawing.Point(12, 105);
            lstDocumentos.Name = "lstDocumentos";
            lstDocumentos.Size = new System.Drawing.Size(300, 281);
            lstDocumentos.TabIndex = 3;
            lstDocumentos.SelectedIndexChanged += lstDocumentos_SelectedIndexChanged;
            lstDocumentos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lstDocumentos.BorderStyle = BorderStyle.FixedSingle;
            lstDocumentos.BackColor = System.Drawing.Color.White;

            // lblDocumentos
            lblDocumentos.AutoSize = true;
            lblDocumentos.Location = new System.Drawing.Point(12, 85);
            lblDocumentos.Name = "lblDocumentos";
            lblDocumentos.Size = new System.Drawing.Size(147, 15);
            lblDocumentos.TabIndex = 4;
            lblDocumentos.Text = "Documentos disponibles:";
            lblDocumentos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            // lblContenido
            lblContenido.AutoSize = true;
            lblContenido.Location = new System.Drawing.Point(328, 9);
            lblContenido.Name = "lblContenido";
            lblContenido.Size = new System.Drawing.Size(93, 15);
            lblContenido.TabIndex = 5;
            lblContenido.Text = "Documentación:";
            lblContenido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            // btnFavorito - Botón para agregar/quitar favoritos
            btnFavorito.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFavorito.Location = new System.Drawing.Point(12, 398);
            btnFavorito.Name = "btnFavorito";
            btnFavorito.Size = new System.Drawing.Size(120, 23);
            btnFavorito.TabIndex = 7;
            btnFavorito.Text = "Agregar a favoritos";
            btnFavorito.UseVisualStyleBackColor = true;
            btnFavorito.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnFavorito.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            btnFavorito.ForeColor = System.Drawing.Color.Black;
            btnFavorito.FlatStyle = FlatStyle.Flat;
            btnFavorito.FlatAppearance.BorderSize = 0;
            btnFavorito.Click += btnFavorito_Click;

            // btnCerrar
            btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCerrar.Location = new System.Drawing.Point(719, 398);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new System.Drawing.Size(75, 23);
            btnCerrar.TabIndex = 6;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            btnCerrar.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnCerrar.ForeColor = System.Drawing.Color.White;
            btnCerrar.FlatStyle = FlatStyle.Flat;
            btnCerrar.FlatAppearance.BorderSize = 0;
            btnCerrar.Click += btnCerrar_Click;

            // FormDocumentacion
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 433);
            Controls.Add(panelContenido);
            Controls.Add(btnCerrar);
            Controls.Add(btnFavorito);
            Controls.Add(comboFiltro);
            Controls.Add(lblContenido);
            Controls.Add(lblDocumentos);
            Controls.Add(lstDocumentos);
            Controls.Add(lblBusqueda);
            Controls.Add(txtBusqueda);
            Controls.Add(comboIdioma);
            MinimumSize = new System.Drawing.Size(820, 480);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Documentación - WinUAE";
            BackColor = System.Drawing.Color.White;

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
