namespace Chemformulatron
{
    partial class ChemformulatronForm
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
            this.dgvElementInfo = new System.Windows.Forms.DataGridView();
            this.btnSortByName = new System.Windows.Forms.Button();
            this.btnSingleCharSymbols = new System.Windows.Forms.Button();
            this.btnSortByAtomicNum = new System.Windows.Forms.Button();
            this.lblChemicalFormula = new System.Windows.Forms.Label();
            this.lblMolarMassTxt = new System.Windows.Forms.Label();
            this.tboxChemicalFormula = new System.Windows.Forms.TextBox();
            this.lblMolarMass = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvElementInfo
            // 
            this.dgvElementInfo.AllowUserToAddRows = false;
            this.dgvElementInfo.AllowUserToDeleteRows = false;
            this.dgvElementInfo.AllowUserToOrderColumns = true;
            this.dgvElementInfo.AllowUserToResizeRows = false;
            this.dgvElementInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvElementInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvElementInfo.Location = new System.Drawing.Point(12, 12);
            this.dgvElementInfo.Name = "dgvElementInfo";
            this.dgvElementInfo.ReadOnly = true;
            this.dgvElementInfo.RowHeadersVisible = false;
            this.dgvElementInfo.Size = new System.Drawing.Size(1069, 735);
            this.dgvElementInfo.TabIndex = 0;
            // 
            // btnSortByName
            // 
            this.btnSortByName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSortByName.Location = new System.Drawing.Point(1087, 12);
            this.btnSortByName.Name = "btnSortByName";
            this.btnSortByName.Size = new System.Drawing.Size(160, 23);
            this.btnSortByName.TabIndex = 1;
            this.btnSortByName.Text = "Sort By Name";
            this.btnSortByName.UseVisualStyleBackColor = true;
            this.btnSortByName.Click += new System.EventHandler(this.btnSortByName_Click);
            // 
            // btnSingleCharSymbols
            // 
            this.btnSingleCharSymbols.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSingleCharSymbols.Location = new System.Drawing.Point(1087, 41);
            this.btnSingleCharSymbols.Name = "btnSingleCharSymbols";
            this.btnSingleCharSymbols.Size = new System.Drawing.Size(160, 23);
            this.btnSingleCharSymbols.TabIndex = 2;
            this.btnSingleCharSymbols.Text = "Single Character Symbols";
            this.btnSingleCharSymbols.UseVisualStyleBackColor = true;
            this.btnSingleCharSymbols.Click += new System.EventHandler(this.btnSingleCharSymbols_Click);
            // 
            // btnSortByAtomicNum
            // 
            this.btnSortByAtomicNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSortByAtomicNum.Location = new System.Drawing.Point(1087, 70);
            this.btnSortByAtomicNum.Name = "btnSortByAtomicNum";
            this.btnSortByAtomicNum.Size = new System.Drawing.Size(160, 23);
            this.btnSortByAtomicNum.TabIndex = 3;
            this.btnSortByAtomicNum.Text = "Sort By Atomic #";
            this.btnSortByAtomicNum.UseVisualStyleBackColor = true;
            this.btnSortByAtomicNum.Click += new System.EventHandler(this.btnSortByAtomicNum_Click);
            // 
            // lblChemicalFormula
            // 
            this.lblChemicalFormula.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChemicalFormula.AutoSize = true;
            this.lblChemicalFormula.Location = new System.Drawing.Point(12, 770);
            this.lblChemicalFormula.Name = "lblChemicalFormula";
            this.lblChemicalFormula.Size = new System.Drawing.Size(93, 13);
            this.lblChemicalFormula.TabIndex = 4;
            this.lblChemicalFormula.Text = "Chemical Formula:";
            // 
            // lblMolarMassTxt
            // 
            this.lblMolarMassTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMolarMassTxt.AutoSize = true;
            this.lblMolarMassTxt.Location = new System.Drawing.Point(978, 770);
            this.lblMolarMassTxt.Name = "lblMolarMassTxt";
            this.lblMolarMassTxt.Size = new System.Drawing.Size(103, 13);
            this.lblMolarMassTxt.TabIndex = 5;
            this.lblMolarMassTxt.Text = "Approx. Molar Mass:";
            // 
            // tboxChemicalFormula
            // 
            this.tboxChemicalFormula.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxChemicalFormula.Location = new System.Drawing.Point(111, 767);
            this.tboxChemicalFormula.Name = "tboxChemicalFormula";
            this.tboxChemicalFormula.Size = new System.Drawing.Size(806, 20);
            this.tboxChemicalFormula.TabIndex = 6;
            this.tboxChemicalFormula.TextChanged += new System.EventHandler(this.tboxChemicalFormula_TextChanged);
            // 
            // lblMolarMass
            // 
            this.lblMolarMass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMolarMass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMolarMass.Location = new System.Drawing.Point(1087, 765);
            this.lblMolarMass.Name = "lblMolarMass";
            this.lblMolarMass.Size = new System.Drawing.Size(160, 23);
            this.lblMolarMass.TabIndex = 8;
            this.lblMolarMass.Text = "0 g/mol";
            this.lblMolarMass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChemformulatronForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 811);
            this.Controls.Add(this.lblMolarMass);
            this.Controls.Add(this.tboxChemicalFormula);
            this.Controls.Add(this.lblMolarMassTxt);
            this.Controls.Add(this.lblChemicalFormula);
            this.Controls.Add(this.btnSortByAtomicNum);
            this.Controls.Add(this.btnSingleCharSymbols);
            this.Controls.Add(this.btnSortByName);
            this.Controls.Add(this.dgvElementInfo);
            this.KeyPreview = true;
            this.Name = "ChemformulatronForm";
            this.Text = "Chemformulatron";
            ((System.ComponentModel.ISupportInitialize)(this.dgvElementInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvElementInfo;
        private System.Windows.Forms.Button btnSortByName;
        private System.Windows.Forms.Button btnSingleCharSymbols;
        private System.Windows.Forms.Button btnSortByAtomicNum;
        private System.Windows.Forms.Label lblChemicalFormula;
        private System.Windows.Forms.Label lblMolarMassTxt;
        private System.Windows.Forms.TextBox tboxChemicalFormula;
        private System.Windows.Forms.Label lblMolarMass;
    }
}

