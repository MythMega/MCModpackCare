namespace ModpackManagement
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.cboxModpack = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.lblSelectedModpack = new System.Windows.Forms.Label();
            this.gbOptionals = new System.Windows.Forms.GroupBox();
            this.btnOpenModFolder = new System.Windows.Forms.Button();
            this.btnOpenMPFolder = new System.Windows.Forms.Button();
            this.btnExecMC = new System.Windows.Forms.Button();
            this.gbModList = new System.Windows.Forms.GroupBox();
            this.btnRefreshListModpack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboxModpack
            // 
            this.cboxModpack.Font = new System.Drawing.Font("Montserrat", 11F);
            this.cboxModpack.FormattingEnabled = true;
            this.cboxModpack.Location = new System.Drawing.Point(17, 39);
            this.cboxModpack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboxModpack.Name = "cboxModpack";
            this.cboxModpack.Size = new System.Drawing.Size(323, 29);
            this.cboxModpack.TabIndex = 1;
            this.cboxModpack.SelectedIndexChanged += new System.EventHandler(this.cboxModpack_SelectedIndexChanged);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.CadetBlue;
            this.btnDownload.FlatAppearance.BorderSize = 0;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDownload.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDownload.Location = new System.Drawing.Point(384, 39);
            this.btnDownload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(273, 28);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Re-download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // lblSelectedModpack
            // 
            this.lblSelectedModpack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedModpack.AutoSize = true;
            this.lblSelectedModpack.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedModpack.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSelectedModpack.Location = new System.Drawing.Point(309, 9);
            this.lblSelectedModpack.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedModpack.Name = "lblSelectedModpack";
            this.lblSelectedModpack.Size = new System.Drawing.Size(129, 16);
            this.lblSelectedModpack.TabIndex = 4;
            this.lblSelectedModpack.Text = "Selected Modpack :";
            this.lblSelectedModpack.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbOptionals
            // 
            this.gbOptionals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this.gbOptionals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbOptionals.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbOptionals.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gbOptionals.Location = new System.Drawing.Point(17, 71);
            this.gbOptionals.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOptionals.Name = "gbOptionals";
            this.gbOptionals.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbOptionals.Size = new System.Drawing.Size(359, 458);
            this.gbOptionals.TabIndex = 5;
            this.gbOptionals.TabStop = false;
            this.gbOptionals.Text = "Optionals";
            // 
            // btnOpenModFolder
            // 
            this.btnOpenModFolder.BackColor = System.Drawing.Color.CadetBlue;
            this.btnOpenModFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenModFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenModFolder.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenModFolder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpenModFolder.Location = new System.Drawing.Point(16, 5);
            this.btnOpenModFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenModFolder.Name = "btnOpenModFolder";
            this.btnOpenModFolder.Size = new System.Drawing.Size(132, 28);
            this.btnOpenModFolder.TabIndex = 6;
            this.btnOpenModFolder.Text = "Open Mod Folder";
            this.btnOpenModFolder.UseVisualStyleBackColor = false;
            this.btnOpenModFolder.Click += new System.EventHandler(this.btnOpenModFolder_Click);
            // 
            // btnOpenMPFolder
            // 
            this.btnOpenMPFolder.BackColor = System.Drawing.Color.CadetBlue;
            this.btnOpenMPFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenMPFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenMPFolder.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenMPFolder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnOpenMPFolder.Location = new System.Drawing.Point(156, 5);
            this.btnOpenMPFolder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenMPFolder.Name = "btnOpenMPFolder";
            this.btnOpenMPFolder.Size = new System.Drawing.Size(131, 28);
            this.btnOpenMPFolder.TabIndex = 7;
            this.btnOpenMPFolder.Text = "Open MP Folder";
            this.btnOpenMPFolder.UseVisualStyleBackColor = false;
            this.btnOpenMPFolder.Click += new System.EventHandler(this.btnOpenMPFolder_Click);
            // 
            // btnExecMC
            // 
            this.btnExecMC.BackColor = System.Drawing.Color.CadetBlue;
            this.btnExecMC.FlatAppearance.BorderSize = 0;
            this.btnExecMC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExecMC.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecMC.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExecMC.Location = new System.Drawing.Point(17, 537);
            this.btnExecMC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExecMC.Name = "btnExecMC";
            this.btnExecMC.Size = new System.Drawing.Size(640, 28);
            this.btnExecMC.TabIndex = 8;
            this.btnExecMC.Text = "Execute Minecraft";
            this.btnExecMC.UseVisualStyleBackColor = false;
            this.btnExecMC.Click += new System.EventHandler(this.btnExecMC_Click);
            // 
            // gbModList
            // 
            this.gbModList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
            this.gbModList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbModList.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbModList.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gbModList.Location = new System.Drawing.Point(384, 71);
            this.gbModList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbModList.Name = "gbModList";
            this.gbModList.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbModList.Size = new System.Drawing.Size(273, 458);
            this.gbModList.TabIndex = 9;
            this.gbModList.TabStop = false;
            this.gbModList.Text = "Enabled mods";
            // 
            // btnRefreshListModpack
            // 
            this.btnRefreshListModpack.BackColor = System.Drawing.Color.CadetBlue;
            this.btnRefreshListModpack.FlatAppearance.BorderSize = 0;
            this.btnRefreshListModpack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshListModpack.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshListModpack.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefreshListModpack.Image = global::ModpackManagement.Properties.Resources.refreshicon_small;
            this.btnRefreshListModpack.Location = new System.Drawing.Point(348, 39);
            this.btnRefreshListModpack.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshListModpack.Name = "btnRefreshListModpack";
            this.btnRefreshListModpack.Size = new System.Drawing.Size(28, 28);
            this.btnRefreshListModpack.TabIndex = 10;
            this.btnRefreshListModpack.UseVisualStyleBackColor = false;
            this.btnRefreshListModpack.Click += new System.EventHandler(this.btnRefreshListModpack_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(674, 574);
            this.Controls.Add(this.btnRefreshListModpack);
            this.Controls.Add(this.gbModList);
            this.Controls.Add(this.btnExecMC);
            this.Controls.Add(this.btnOpenMPFolder);
            this.Controls.Add(this.btnOpenModFolder);
            this.Controls.Add(this.gbOptionals);
            this.Controls.Add(this.lblSelectedModpack);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.cboxModpack);
            this.Font = new System.Drawing.Font("Montserrat", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmMain";
            this.Opacity = 0.97D;
            this.Text = "MCModCare";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cboxModpack;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Label lblSelectedModpack;
        private System.Windows.Forms.GroupBox gbOptionals;
        private System.Windows.Forms.Button btnOpenModFolder;
        private System.Windows.Forms.Button btnOpenMPFolder;
        private System.Windows.Forms.Button btnExecMC;
        private System.Windows.Forms.GroupBox gbModList;
        private System.Windows.Forms.Button btnRefreshListModpack;
    }
}

