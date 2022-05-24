namespace UI;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Form1";
        
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.colorDlg = new System.Windows.Forms.ColorDialog(); 
        this.Palette = new System.Windows.Forms.Button();
        this.Clear = new System.Windows.Forms.Button();
        this.panel1 = new System.Windows.Forms.Panel();
        this.panel2 = new System.Windows.Forms.Panel();
        this.ColorGreen = new System.Windows.Forms.Button();
        this.ColorYellow = new System.Windows.Forms.Button();
        this.ColorRed = new System.Windows.Forms.Button();
        this.ColorBlack = new System.Windows.Forms.Button();
        this.itemsPanel = new System.Windows.Forms.FlowLayoutPanel();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.panel1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.SuspendLayout();
        // 
        // pictureBox1
        // 
        this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pictureBox1.BackColor = Color.Empty;
        this.pictureBox1.Location = new System.Drawing.Point(0, 600);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(1200, 200);
        this.pictureBox1.TabIndex = 0;
        this.pictureBox1.TabStop = false;
        this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
        this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
        this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
        // 
        // Clear
        // 
        this.Clear.Location = new System.Drawing.Point(23, 131);
        this.Clear.Name = "Clear";
        this.Clear.Size = new System.Drawing.Size(75, 23);
        this.Clear.TabIndex = 2;
        this.Clear.Text = "Clear";
        this.Clear.UseVisualStyleBackColor = true;
        this.Clear.Click += new EventHandler(this.Clear_Click);
        // 
        // panel1
        // 
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.panel1.Controls.Add(this.panel2);
        this.panel1.Controls.Add(this.Clear);
        this.panel1.Controls.Add(this.Palette);
        this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
        this.panel1.Location = new System.Drawing.Point(1096, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(120, 400);
        this.panel1.TabIndex = 3;
        // 
        // panel2
        // 
        this.panel2.Controls.Add(this.Palette);
        this.panel2.Controls.Add(this.ColorGreen);
        this.panel2.Controls.Add(this.ColorYellow);
        this.panel2.Controls.Add(this.ColorRed);
        this.panel2.Controls.Add(this.ColorBlack);
        this.panel2.Location = new System.Drawing.Point(3, 3);
        this.panel2.Name = "panel2";
        this.panel2.Size = new System.Drawing.Size(111, 116);
        this.panel2.TabIndex = 4;
        // 
        // Palette
        // 
        this.Palette.Dock = System.Windows.Forms.DockStyle.Top;
        this.Palette.Location = new System.Drawing.Point(0, 92);
        this.Palette.Name = "Palette";
        this.Palette.Size = new System.Drawing.Size(111, 23);
        this.Palette.TabIndex = 4;
        this.Palette.UseVisualStyleBackColor = false;
        this.Palette.Click += new EventHandler(this.Palette_Click);
        // 
        // ColorGreen
        // 
        this.ColorGreen.BackColor = System.Drawing.Color.ForestGreen;
        this.ColorGreen.Dock = System.Windows.Forms.DockStyle.Top;
        this.ColorGreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.ColorGreen.Location = new System.Drawing.Point(0, 69);
        this.ColorGreen.Name = "ColorGreen";
        this.ColorGreen.Size = new System.Drawing.Size(111, 23);
        this.ColorGreen.TabIndex = 3;
        this.ColorGreen.UseVisualStyleBackColor = false;
        this.ColorGreen.Click += new EventHandler(this.ColorGreen_Click);
        // 
        // ColorYellow
        // 
        this.ColorYellow.BackColor = System.Drawing.Color.Yellow;
        this.ColorYellow.Dock = System.Windows.Forms.DockStyle.Top;
        this.ColorYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.ColorYellow.Location = new System.Drawing.Point(0, 46);
        this.ColorYellow.Name = "ColorYellow";
        this.ColorYellow.Size = new System.Drawing.Size(111, 23);
        this.ColorYellow.TabIndex = 2;
        this.ColorYellow.UseVisualStyleBackColor = false;
        this.ColorYellow.Click += new EventHandler(this.ColorYellow_Click);
        // 
        // ColorRed
        // 
        this.ColorRed.BackColor = System.Drawing.Color.Firebrick;
        this.ColorRed.Dock = System.Windows.Forms.DockStyle.Top;
        this.ColorRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.ColorRed.Location = new System.Drawing.Point(0, 23);
        this.ColorRed.Name = "ColorRed";
        this.ColorRed.Size = new System.Drawing.Size(111, 23);
        this.ColorRed.TabIndex = 1;
        this.ColorRed.UseVisualStyleBackColor = false;
        this.ColorRed.Click += new EventHandler(this.ColorRed_Click);
        // 
        // ColorBlack
        // 
        this.ColorBlack.BackColor = System.Drawing.Color.Black;
        this.ColorBlack.Dock = System.Windows.Forms.DockStyle.Top;
        this.ColorBlack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.ColorBlack.Location = new System.Drawing.Point(0, 0);
        this.ColorBlack.Name = "ColorBlack";
        this.ColorBlack.Size = new System.Drawing.Size(111, 23);
        this.ColorBlack.TabIndex = 0;
        this.ColorBlack.UseVisualStyleBackColor = false;
        this.ColorBlack.Click += new EventHandler(this.ColorBlack_Click);
        //
        // itemsPanel
        //
        this.itemsPanel.BackColor = SystemColors.ControlLight;
        this.itemsPanel.AutoScroll = true;
        this.itemsPanel.Location = new System.Drawing.Point(0, 50);
        this.itemsPanel.Name = "itemsPanel";
        this.itemsPanel.Size = new System.Drawing.Size(132, 515);
        this.itemsPanel.TabIndex = 0;
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1216, 772);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.pictureBox1);
        this.Controls.Add(this.itemsPanel);
        this.Name = "MainForm";
        this.Text = "Form1";
        this.Load += new System.EventHandler(this.MainForm_Load);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.panel1.ResumeLayout(false);
        this.panel2.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion
    
    private PictureBox pictureBox1;
    private ColorDialog colorDlg;
    private Button Palette;
    private Button Clear;
    private Panel panel1;
    private Button ColorGreen;
    private Button ColorYellow;
    private Button ColorRed;
    private Button ColorBlack;
    private Panel panel2;
    private FlowLayoutPanel itemsPanel;
}