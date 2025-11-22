namespace EMULATOR_GUI
{
    partial class Form1
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
            splitContainer1 = new SplitContainer();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            bEmg = new Button();
            groupBox1 = new GroupBox();
            checkBox20 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox19 = new CheckBox();
            checkBox10 = new CheckBox();
            checkBox9 = new CheckBox();
            checkBox1 = new CheckBox();
            checkBox11 = new CheckBox();
            checkBox8 = new CheckBox();
            checkBox18 = new CheckBox();
            checkBox12 = new CheckBox();
            checkBox7 = new CheckBox();
            checkBox13 = new CheckBox();
            checkBox6 = new CheckBox();
            checkBox17 = new CheckBox();
            checkBox14 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox15 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox16 = new CheckBox();
            label8 = new Label();
            l_EMG = new WinFormsControlLibrary1.Indicator();
            label7 = new Label();
            l_XHome = new WinFormsControlLibrary1.Indicator();
            labelStatus = new Label();
            lbliEnc = new Label();
            btnStart = new Button();
            label6 = new Label();
            l_Adpt = new WinFormsControlLibrary1.Indicator();
            lbldEnc = new Label();
            lblsEnc = new Label();
            label4 = new Label();
            l_dHome = new WinFormsControlLibrary1.Indicator();
            label5 = new Label();
            l_dOver = new WinFormsControlLibrary1.Indicator();
            label3 = new Label();
            l_sHome = new WinFormsControlLibrary1.Indicator();
            label2 = new Label();
            l_sOver = new WinFormsControlLibrary1.Indicator();
            label1 = new Label();
            l_sOut = new WinFormsControlLibrary1.Indicator();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = SystemColors.ActiveBorder;
            splitContainer1.Panel1.Controls.Add(button3);
            splitContainer1.Panel1.Controls.Add(button2);
            splitContainer1.Panel1.Controls.Add(button1);
            splitContainer1.Panel1.Controls.Add(bEmg);
            splitContainer1.Panel1.Controls.Add(groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = SystemColors.ActiveCaption;
            splitContainer1.Panel2.Controls.Add(label8);
            splitContainer1.Panel2.Controls.Add(l_EMG);
            splitContainer1.Panel2.Controls.Add(label7);
            splitContainer1.Panel2.Controls.Add(l_XHome);
            splitContainer1.Panel2.Controls.Add(labelStatus);
            splitContainer1.Panel2.Controls.Add(lbliEnc);
            splitContainer1.Panel2.Controls.Add(btnStart);
            splitContainer1.Panel2.Controls.Add(label6);
            splitContainer1.Panel2.Controls.Add(l_Adpt);
            splitContainer1.Panel2.Controls.Add(lbldEnc);
            splitContainer1.Panel2.Controls.Add(lblsEnc);
            splitContainer1.Panel2.Controls.Add(label4);
            splitContainer1.Panel2.Controls.Add(l_dHome);
            splitContainer1.Panel2.Controls.Add(label5);
            splitContainer1.Panel2.Controls.Add(l_dOver);
            splitContainer1.Panel2.Controls.Add(label3);
            splitContainer1.Panel2.Controls.Add(l_sHome);
            splitContainer1.Panel2.Controls.Add(label2);
            splitContainer1.Panel2.Controls.Add(l_sOver);
            splitContainer1.Panel2.Controls.Add(label1);
            splitContainer1.Panel2.Controls.Add(l_sOut);
            splitContainer1.Size = new Size(950, 551);
            splitContainer1.SplitterDistance = 316;
            splitContainer1.TabIndex = 0;
            // 
            // button3
            // 
            button3.Location = new Point(12, 442);
            button3.Name = "button3";
            button3.Size = new Size(70, 73);
            button3.TabIndex = 4;
            button3.Text = "Main ON/ OFF";
            button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(232, 339);
            button2.Name = "button2";
            button2.Size = new Size(70, 73);
            button2.TabIndex = 3;
            button2.Text = "Door Lock";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(122, 339);
            button1.Name = "button1";
            button1.Size = new Size(70, 73);
            button1.TabIndex = 2;
            button1.Text = "KeySw";
            button1.UseVisualStyleBackColor = true;
            // 
            // bEmg
            // 
            bEmg.Location = new Point(12, 339);
            bEmg.Name = "bEmg";
            bEmg.Size = new Size(70, 73);
            bEmg.TabIndex = 1;
            bEmg.Text = "EMG";
            bEmg.UseVisualStyleBackColor = true;
            bEmg.Click += bEmg_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox20);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox19);
            groupBox1.Controls.Add(checkBox10);
            groupBox1.Controls.Add(checkBox9);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Controls.Add(checkBox11);
            groupBox1.Controls.Add(checkBox8);
            groupBox1.Controls.Add(checkBox18);
            groupBox1.Controls.Add(checkBox12);
            groupBox1.Controls.Add(checkBox7);
            groupBox1.Controls.Add(checkBox13);
            groupBox1.Controls.Add(checkBox6);
            groupBox1.Controls.Add(checkBox17);
            groupBox1.Controls.Add(checkBox14);
            groupBox1.Controls.Add(checkBox5);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(checkBox15);
            groupBox1.Controls.Add(checkBox4);
            groupBox1.Controls.Add(checkBox16);
            groupBox1.Location = new Point(12, 9);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(291, 306);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Adapters";
            // 
            // checkBox20
            // 
            checkBox20.AutoSize = true;
            checkBox20.Location = new Point(183, 261);
            checkBox20.Name = "checkBox20";
            checkBox20.Size = new Size(88, 19);
            checkBox20.TabIndex = 38;
            checkBox20.Text = "checkBox20";
            checkBox20.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(6, 61);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(82, 19);
            checkBox2.TabIndex = 20;
            checkBox2.Text = "checkBox2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox19
            // 
            checkBox19.AutoSize = true;
            checkBox19.Location = new Point(183, 236);
            checkBox19.Name = "checkBox19";
            checkBox19.Size = new Size(88, 19);
            checkBox19.TabIndex = 37;
            checkBox19.Text = "checkBox19";
            checkBox19.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            checkBox10.AutoSize = true;
            checkBox10.Location = new Point(6, 261);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(88, 19);
            checkBox10.TabIndex = 28;
            checkBox10.Text = "checkBox10";
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(6, 236);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(82, 19);
            checkBox9.TabIndex = 27;
            checkBox9.Text = "checkBox9";
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(6, 36);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(82, 19);
            checkBox1.TabIndex = 19;
            checkBox1.Text = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            checkBox11.AutoSize = true;
            checkBox11.Location = new Point(183, 42);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new Size(88, 19);
            checkBox11.TabIndex = 29;
            checkBox11.Text = "checkBox11";
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Location = new Point(6, 211);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(82, 19);
            checkBox8.TabIndex = 26;
            checkBox8.Text = "checkBox8";
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox18
            // 
            checkBox18.AutoSize = true;
            checkBox18.Location = new Point(183, 213);
            checkBox18.Name = "checkBox18";
            checkBox18.Size = new Size(88, 19);
            checkBox18.TabIndex = 36;
            checkBox18.Text = "checkBox18";
            checkBox18.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            checkBox12.AutoSize = true;
            checkBox12.Location = new Point(183, 67);
            checkBox12.Name = "checkBox12";
            checkBox12.Size = new Size(88, 19);
            checkBox12.TabIndex = 30;
            checkBox12.Text = "checkBox12";
            checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(6, 186);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(82, 19);
            checkBox7.TabIndex = 25;
            checkBox7.Text = "checkBox7";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox13
            // 
            checkBox13.AutoSize = true;
            checkBox13.Location = new Point(183, 92);
            checkBox13.Name = "checkBox13";
            checkBox13.Size = new Size(88, 19);
            checkBox13.TabIndex = 31;
            checkBox13.Text = "checkBox13";
            checkBox13.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(6, 161);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(82, 19);
            checkBox6.TabIndex = 24;
            checkBox6.Text = "checkBox6";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox17
            // 
            checkBox17.AutoSize = true;
            checkBox17.Location = new Point(183, 188);
            checkBox17.Name = "checkBox17";
            checkBox17.Size = new Size(88, 19);
            checkBox17.TabIndex = 35;
            checkBox17.Text = "checkBox17";
            checkBox17.UseVisualStyleBackColor = true;
            // 
            // checkBox14
            // 
            checkBox14.AutoSize = true;
            checkBox14.Location = new Point(183, 117);
            checkBox14.Name = "checkBox14";
            checkBox14.Size = new Size(88, 19);
            checkBox14.TabIndex = 32;
            checkBox14.Text = "checkBox14";
            checkBox14.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(6, 136);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(82, 19);
            checkBox5.TabIndex = 23;
            checkBox5.Text = "checkBox5";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(6, 86);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(82, 19);
            checkBox3.TabIndex = 21;
            checkBox3.Text = "checkBox3";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox15
            // 
            checkBox15.AutoSize = true;
            checkBox15.Location = new Point(183, 142);
            checkBox15.Name = "checkBox15";
            checkBox15.Size = new Size(88, 19);
            checkBox15.TabIndex = 33;
            checkBox15.Text = "checkBox15";
            checkBox15.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(6, 111);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(82, 19);
            checkBox4.TabIndex = 22;
            checkBox4.Text = "checkBox4";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox16
            // 
            checkBox16.AutoSize = true;
            checkBox16.Location = new Point(183, 167);
            checkBox16.Name = "checkBox16";
            checkBox16.Size = new Size(88, 19);
            checkBox16.TabIndex = 34;
            checkBox16.Text = "checkBox16";
            checkBox16.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(541, 187);
            label8.Name = "label8";
            label8.Size = new Size(38, 15);
            label8.TabIndex = 20;
            label8.Text = "EMEG";
            // 
            // l_EMG
            // 
            l_EMG.Location = new Point(523, 113);
            l_EMG.Name = "l_EMG";
            l_EMG.Size = new Size(63, 63);
            l_EMG.TabIndex = 19;
            l_EMG.Value = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(237, 161);
            label7.Name = "label7";
            label7.Size = new Size(81, 15);
            label7.TabIndex = 18;
            label7.Text = "Indexer Home";
            // 
            // l_XHome
            // 
            l_XHome.Location = new Point(165, 135);
            l_XHome.Name = "l_XHome";
            l_XHome.Size = new Size(59, 59);
            l_XHome.TabIndex = 17;
            l_XHome.Value = false;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelStatus.Location = new Point(23, 17);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(81, 25);
            labelStatus.TabIndex = 16;
            labelStatus.Text = "Not Live";
            // 
            // lbliEnc
            // 
            lbliEnc.AutoSize = true;
            lbliEnc.Location = new Point(94, 161);
            lbliEnc.Name = "lbliEnc";
            lbliEnc.Size = new Size(36, 15);
            lbliEnc.TabIndex = 15;
            lbliEnc.Text = "x_Enc";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(500, 9);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(99, 47);
            btnStart.TabIndex = 14;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(231, 92);
            label6.Name = "label6";
            label6.Size = new Size(49, 15);
            label6.TabIndex = 13;
            label6.Text = "Adapter";
            // 
            // l_Adpt
            // 
            l_Adpt.Location = new Point(165, 70);
            l_Adpt.Name = "l_Adpt";
            l_Adpt.Size = new Size(59, 59);
            l_Adpt.TabIndex = 12;
            l_Adpt.Value = false;
            // 
            // lbldEnc
            // 
            lbldEnc.AutoSize = true;
            lbldEnc.Location = new Point(272, 500);
            lbldEnc.Name = "lbldEnc";
            lbldEnc.Size = new Size(33, 15);
            lbldEnc.TabIndex = 11;
            lbldEnc.Text = "dEnc";
            // 
            // lblsEnc
            // 
            lblsEnc.AutoSize = true;
            lblsEnc.Location = new Point(55, 500);
            lblsEnc.Name = "lblsEnc";
            lblsEnc.Size = new Size(31, 15);
            lblsEnc.TabIndex = 10;
            lblsEnc.Text = "sEnc";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(272, 461);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 9;
            label4.Text = "dHome";
            // 
            // l_dHome
            // 
            l_dHome.Location = new Point(260, 399);
            l_dHome.Name = "l_dHome";
            l_dHome.Size = new Size(59, 59);
            l_dHome.TabIndex = 8;
            l_dHome.Value = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(260, 350);
            label5.Name = "label5";
            label5.Size = new Size(70, 15);
            label5.TabIndex = 7;
            label5.Text = "dOverShoot";
            // 
            // l_dOver
            // 
            l_dOver.Location = new Point(260, 288);
            l_dOver.Name = "l_dOver";
            l_dOver.Size = new Size(59, 59);
            l_dOver.TabIndex = 6;
            l_dOver.Value = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(59, 461);
            label3.Name = "label3";
            label3.Size = new Size(45, 15);
            label3.TabIndex = 5;
            label3.Text = "sHome";
            // 
            // l_sHome
            // 
            l_sHome.Location = new Point(55, 399);
            l_sHome.Name = "l_sHome";
            l_sHome.Size = new Size(59, 59);
            l_sHome.TabIndex = 4;
            l_sHome.Value = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(55, 350);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 3;
            label2.Text = "sOverShoot";
            // 
            // l_sOver
            // 
            l_sOver.Location = new Point(55, 288);
            l_sOver.Name = "l_sOver";
            l_sOver.Size = new Size(59, 59);
            l_sOver.TabIndex = 2;
            l_sOver.Value = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(231, 232);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 1;
            label1.Text = "sOut";
            // 
            // l_sOut
            // 
            l_sOut.Location = new Point(165, 209);
            l_sOut.Name = "l_sOut";
            l_sOut.Size = new Size(59, 59);
            l_sOut.TabIndex = 0;
            l_sOut.Value = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 551);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Label label3;
        private WinFormsControlLibrary1.Indicator l_sHome;
        private Label label2;
        private WinFormsControlLibrary1.Indicator l_sOver;
        private Label label1;
        private WinFormsControlLibrary1.Indicator l_sOut;
        private Label label4;
        private WinFormsControlLibrary1.Indicator l_dHome;
        private Label label5;
        private WinFormsControlLibrary1.Indicator l_dOver;
        private Label label6;
        private WinFormsControlLibrary1.Indicator l_Adpt;
        private Label lbldEnc;
        private Label lblsEnc;
        private Button btnStart;
        private Label lbliEnc;
        private Label labelStatus;
        private Label label7;
        private WinFormsControlLibrary1.Indicator l_XHome;
        private CheckBox checkBox20;
        private CheckBox checkBox19;
        private CheckBox checkBox18;
        private CheckBox checkBox17;
        private CheckBox checkBox16;
        private CheckBox checkBox15;
        private CheckBox checkBox14;
        private CheckBox checkBox13;
        private CheckBox checkBox12;
        private CheckBox checkBox11;
        private CheckBox checkBox10;
        private CheckBox checkBox9;
        private CheckBox checkBox8;
        private CheckBox checkBox7;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private Button button1;
        private Button bEmg;
        private Button button2;
        private Button button3;
        private Label label8;
        private WinFormsControlLibrary1.Indicator l_EMG;
    }
}
