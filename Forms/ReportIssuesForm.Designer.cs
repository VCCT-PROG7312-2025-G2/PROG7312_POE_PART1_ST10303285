namespace MunicipalServicesApp
{
    partial class ReportIssuesForm 
    {
        private System.ComponentModel.IContainer components = null; // Required designer variable

        private Label lblHeader;
        private Label lblLocation;
        private TextBox txtLocation;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblDescription;
        private RichTextBox rtbDescription;
        private Button btnAttach;
        private Label lblAttachment;
        private ProgressBar pbEngagement;
        private Label lblEncouragement;
        private Button btnSubmit;
        private Button btnBack;

        protected override void Dispose(bool disposing) // Clean up any resources being used
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() // Method to initialize components
        {
            lblHeader = new Label();
            lblLocation = new Label();
            txtLocation = new TextBox();
            lblCategory = new Label();
            cmbCategory = new ComboBox();
            lblDescription = new Label();
            rtbDescription = new RichTextBox();
            btnAttach = new Button();
            lblAttachment = new Label();
            pbEngagement = new ProgressBar();
            lblEncouragement = new Label();
            btnSubmit = new Button();
            btnBack = new Button();
            SuspendLayout();


            // lblHeader
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblHeader.Location = new Point(24, 16);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(235, 41);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Report an Issue";
            
            // lblLocation
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(24, 70);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(179, 20);
            lblLocation.TabIndex = 1;
            lblLocation.Text = "Location (suburb / street):";
            
            // txtLocation
            txtLocation.Location = new Point(220, 66);
            txtLocation.Name = "txtLocation";
            txtLocation.Size = new Size(540, 27);
            txtLocation.TabIndex = 2;
            txtLocation.TextChanged += AnyInputChanged;
            
            // lblCategory
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(24, 116);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(72, 20);
            lblCategory.TabIndex = 3;
            lblCategory.Text = "Category:";
            
            // cmbCategory
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Items.AddRange(new object[] { "Sanitation", "Roads", "Utilities", "Street Lighting", "Public Safety", "Other" });
            cmbCategory.Location = new Point(220, 112);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(300, 28);
            cmbCategory.TabIndex = 4;
            cmbCategory.SelectedIndexChanged += AnyInputChanged;
           
            // lblDescription
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(24, 162);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(88, 20);
            lblDescription.TabIndex = 5;
            lblDescription.Text = "Description:";

            // rtbDescription
            rtbDescription.Location = new Point(220, 158);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.Size = new Size(540, 260);
            rtbDescription.TabIndex = 6;
            rtbDescription.Text = "";
            rtbDescription.TextChanged += AnyInputChanged;
             
            // btnAttach 
            btnAttach.Location = new Point(220, 436);
            btnAttach.Name = "btnAttach";
            btnAttach.Size = new Size(200, 30);
            btnAttach.TabIndex = 7;
            btnAttach.Text = "Attach Image / Document";
            btnAttach.Click += BtnAttach_Click;
        
            // lblAttachment
            lblAttachment.AutoSize = true;
            lblAttachment.Location = new Point(440, 441);
            lblAttachment.Name = "lblAttachment";
            lblAttachment.Size = new Size(116, 20);
            lblAttachment.TabIndex = 8;
            lblAttachment.Text = "No file attached";
           
            // pbEngagement
            pbEngagement.Location = new Point(220, 470);
            pbEngagement.Name = "pbEngagement";
            pbEngagement.Size = new Size(540, 20);
            pbEngagement.TabIndex = 9;
            
            // lblEncouragement
            lblEncouragement.Location = new Point(220, 498);
            lblEncouragement.Name = "lblEncouragement";
            lblEncouragement.Size = new Size(540, 34);
            lblEncouragement.TabIndex = 10;
            lblEncouragement.Text = "Complete fields to improve report quality.";
            
            // btnSubmit
            btnSubmit.BackColor = Color.FromArgb(40, 116, 166);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(220, 550);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(160, 30);
            btnSubmit.TabIndex = 11;
            btnSubmit.Text = "Submit Report";
            btnSubmit.UseVisualStyleBackColor = false;
            btnSubmit.Click += BtnSubmit_Click;
            
            // btnBack
            btnBack.Location = new Point(400, 550);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(160, 30);
            btnBack.TabIndex = 12;
            btnBack.Text = "Back to Menu";
            btnBack.Click += BtnBack_Click;
            
            
            // ReportIssuesForm
            BackColor = Color.White;
            ClientSize = new Size(1063, 633);
            Controls.Add(lblHeader);
            Controls.Add(lblLocation);
            Controls.Add(txtLocation);
            Controls.Add(lblCategory);
            Controls.Add(cmbCategory);
            Controls.Add(lblDescription);
            Controls.Add(rtbDescription);
            Controls.Add(btnAttach);
            Controls.Add(lblAttachment);
            Controls.Add(pbEngagement);
            Controls.Add(lblEncouragement);
            Controls.Add(btnSubmit);
            Controls.Add(btnBack);
            Name = "ReportIssuesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Report an Issue";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}