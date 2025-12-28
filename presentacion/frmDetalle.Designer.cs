
namespace presentacion
{
    partial class frmDetalle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvArticuloDetalles = new System.Windows.Forms.DataGridView();
            this.pbxDetalle = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticuloDetalles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvArticuloDetalles
            // 
            this.dgvArticuloDetalles.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgvArticuloDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArticuloDetalles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvArticuloDetalles.Location = new System.Drawing.Point(12, 3);
            this.dgvArticuloDetalles.MaximumSize = new System.Drawing.Size(700, 100);
            this.dgvArticuloDetalles.MinimumSize = new System.Drawing.Size(630, 90);
            this.dgvArticuloDetalles.MultiSelect = false;
            this.dgvArticuloDetalles.Name = "dgvArticuloDetalles";
            this.dgvArticuloDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvArticuloDetalles.Size = new System.Drawing.Size(637, 93);
            this.dgvArticuloDetalles.TabIndex = 0;
            // 
            // pbxDetalle
            // 
            this.pbxDetalle.Location = new System.Drawing.Point(207, 102);
            this.pbxDetalle.Name = "pbxDetalle";
            this.pbxDetalle.Size = new System.Drawing.Size(264, 244);
            this.pbxDetalle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxDetalle.TabIndex = 1;
            this.pbxDetalle.TabStop = false;
            // 
            // frmDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 358);
            this.Controls.Add(this.pbxDetalle);
            this.Controls.Add(this.dgvArticuloDetalles);
            this.Name = "frmDetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle de Artículo";
            this.Load += new System.EventHandler(this.frmDetalle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArticuloDetalles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxDetalle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvArticuloDetalles;
        private System.Windows.Forms.PictureBox pbxDetalle;
    }
}