<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Btn_dtMill_quit = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.TextBox14 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.homeTitle = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBox6 = New System.Windows.Forms.TextBox()
        Me.DssatRun = New System.Windows.Forms.Button()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.msgErr_expDssat_export = New System.Windows.Forms.Label()
        Me.Btn_Concat_Dssat = New System.Windows.Forms.Button()
        Me.Btn_expDssat_export = New System.Windows.Forms.Button()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.TextBox7 = New System.Windows.Forms.TextBox()
        Me.SticsRun = New System.Windows.Forms.Button()
        Me.TextBox8 = New System.Windows.Forms.TextBox()
        Me.TextBox9 = New System.Windows.Forms.TextBox()
        Me.Btn_Concat_Stics = New System.Windows.Forms.Button()
        Me.msgErr_expStics_export = New System.Windows.Forms.Label()
        Me.Btn_expStics_export = New System.Windows.Forms.Button()
        Me.TabPage14 = New System.Windows.Forms.TabPage()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.CelsiusRun = New System.Windows.Forms.Button()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.msgErr_expCelsius_export = New System.Windows.Forms.Label()
        Me.Btn_Concat_Celsius = New System.Windows.Forms.Button()
        Me.Btn_expCelsius_export = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.fileToImport = New System.Windows.Forms.OpenFileDialog()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.TextBox13 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.NbCore = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ForceClimate = New System.Windows.Forms.CheckBox()
        Me.TabControl1.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage14.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NbCore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Btn_dtMill_quit
        '
        Me.Btn_dtMill_quit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_dtMill_quit.Location = New System.Drawing.Point(1000, 509)
        Me.Btn_dtMill_quit.Name = "Btn_dtMill_quit"
        Me.Btn_dtMill_quit.Size = New System.Drawing.Size(70, 40)
        Me.Btn_dtMill_quit.TabIndex = 1
        Me.Btn_dtMill_quit.Text = "Quit"
        Me.Btn_dtMill_quit.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage14)
        Me.TabControl1.Location = New System.Drawing.Point(240, 119)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(840, 369)
        Me.TabControl1.TabIndex = 5
        '
        'TabPage5
        '
        Me.TabPage5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.TabPage5.Controls.Add(Me.TextBox14)
        Me.TabPage5.Controls.Add(Me.TextBox3)
        Me.TabPage5.Controls.Add(Me.TextBox2)
        Me.TabPage5.Controls.Add(Me.TextBox1)
        Me.TabPage5.Controls.Add(Me.PictureBox2)
        Me.TabPage5.Controls.Add(Me.Button2)
        Me.TabPage5.Controls.Add(Me.Label12)
        Me.TabPage5.Controls.Add(Me.homeTitle)
        Me.TabPage5.Location = New System.Drawing.Point(4, 28)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(832, 337)
        Me.TabPage5.TabIndex = 2
        Me.TabPage5.Text = "Home"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'TextBox14
        '
        Me.TextBox14.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox14.Font = New System.Drawing.Font("Book Antiqua", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox14.Location = New System.Drawing.Point(188, 218)
        Me.TextBox14.Multiline = True
        Me.TextBox14.Name = "TextBox14"
        Me.TextBox14.Size = New System.Drawing.Size(638, 116)
        Me.TextBox14.TabIndex = 14
        Me.TextBox14.Text = resources.GetString("TextBox14.Text")
        Me.TextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox3.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TextBox3.Font = New System.Drawing.Font("Stencil", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.ForeColor = System.Drawing.Color.LightCoral
        Me.TextBox3.Location = New System.Drawing.Point(188, 73)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(30)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(638, 73)
        Me.TextBox3.TabIndex = 13
        Me.TextBox3.Text = "ACME-Agile Crop Model Ensemble" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(DataMill for ARISE)"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox2
        '
        Me.TextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox2.Font = New System.Drawing.Font("Book Antiqua", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.Location = New System.Drawing.Point(186, 5)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(640, 60)
        Me.TextBox2.TabIndex = 12
        Me.TextBox2.Text = resources.GetString("TextBox2.Text")
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Font = New System.Drawing.Font("Book Antiqua", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(188, 155)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(638, 57)
        Me.TextBox1.TabIndex = 11
        Me.TextBox1.Text = resources.GetString("TextBox1.Text")
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(179, 331)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 10
        Me.PictureBox2.TabStop = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(58, 531)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(392, 86)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Création de la table des champs DataMill pour Arise"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(54, 421)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(1162, 137)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = resources.GetString("Label12.Text")
        '
        'homeTitle
        '
        Me.homeTitle.AutoSize = True
        Me.homeTitle.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.homeTitle.Location = New System.Drawing.Point(54, 361)
        Me.homeTitle.Name = "homeTitle"
        Me.homeTitle.Size = New System.Drawing.Size(983, 19)
        Me.homeTitle.TabIndex = 1
        Me.homeTitle.Text = "DATAMILL : un outil informatique spécifiquement développé pour automatiser l’accè" &
    "s des modèles internationaux aux bases de données du Cirad."
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TextBox6)
        Me.TabPage2.Controls.Add(Me.DssatRun)
        Me.TabPage2.Controls.Add(Me.TextBox5)
        Me.TabPage2.Controls.Add(Me.TextBox4)
        Me.TabPage2.Controls.Add(Me.msgErr_expDssat_export)
        Me.TabPage2.Controls.Add(Me.Btn_Concat_Dssat)
        Me.TabPage2.Controls.Add(Me.Btn_expDssat_export)
        Me.TabPage2.Location = New System.Drawing.Point(4, 28)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(832, 337)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "DSSAT"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBox6
        '
        Me.TextBox6.Location = New System.Drawing.Point(10, 190)
        Me.TextBox6.Multiline = True
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New System.Drawing.Size(336, 85)
        Me.TextBox6.TabIndex = 36
        Me.TextBox6.Text = "Use the ""Built simulation outputs"" button to export simulation output in a standa" &
    "rd format in the ""SummaryOutput"" table of the ""MasterInput"" database"
        '
        'DssatRun
        '
        Me.DssatRun.Location = New System.Drawing.Point(352, 134)
        Me.DssatRun.Name = "DssatRun"
        Me.DssatRun.Size = New System.Drawing.Size(125, 51)
        Me.DssatRun.TabIndex = 35
        Me.DssatRun.Text = "Run simulations"
        Me.DssatRun.UseVisualStyleBackColor = True
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(10, 100)
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(336, 85)
        Me.TextBox5.TabIndex = 34
        Me.TextBox5.Text = "Use the ""Run simulations"" button to run model simulations with the model inputs g" &
    "enerated with the ""Build model inputs"""
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(10, 10)
        Me.TextBox4.Multiline = True
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(336, 85)
        Me.TextBox4.TabIndex = 33
        Me.TextBox4.Text = "Use the ""Build model inputs"" button to build model-specific input files as inform" &
    "ed in the ""SimUnitList"" table in the MasterInput database"
        '
        'msgErr_expDssat_export
        '
        Me.msgErr_expDssat_export.ForeColor = System.Drawing.Color.Red
        Me.msgErr_expDssat_export.Location = New System.Drawing.Point(6, 278)
        Me.msgErr_expDssat_export.Name = "msgErr_expDssat_export"
        Me.msgErr_expDssat_export.Size = New System.Drawing.Size(400, 26)
        Me.msgErr_expDssat_export.TabIndex = 30
        Me.msgErr_expDssat_export.Text = "messageExport"
        Me.msgErr_expDssat_export.Visible = False
        '
        'Btn_Concat_Dssat
        '
        Me.Btn_Concat_Dssat.Enabled = False
        Me.Btn_Concat_Dssat.Location = New System.Drawing.Point(352, 223)
        Me.Btn_Concat_Dssat.Name = "Btn_Concat_Dssat"
        Me.Btn_Concat_Dssat.Size = New System.Drawing.Size(125, 52)
        Me.Btn_Concat_Dssat.TabIndex = 32
        Me.Btn_Concat_Dssat.Text = "Build simulation outputs"
        Me.Btn_Concat_Dssat.UseVisualStyleBackColor = True
        '
        'Btn_expDssat_export
        '
        Me.Btn_expDssat_export.Enabled = False
        Me.Btn_expDssat_export.Location = New System.Drawing.Point(352, 44)
        Me.Btn_expDssat_export.Name = "Btn_expDssat_export"
        Me.Btn_expDssat_export.Size = New System.Drawing.Size(125, 51)
        Me.Btn_expDssat_export.TabIndex = 26
        Me.Btn_expDssat_export.Text = "Build model inputs "
        Me.Btn_expDssat_export.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.TextBox7)
        Me.TabPage6.Controls.Add(Me.SticsRun)
        Me.TabPage6.Controls.Add(Me.TextBox8)
        Me.TabPage6.Controls.Add(Me.TextBox9)
        Me.TabPage6.Controls.Add(Me.Btn_Concat_Stics)
        Me.TabPage6.Controls.Add(Me.msgErr_expStics_export)
        Me.TabPage6.Controls.Add(Me.Btn_expStics_export)
        Me.TabPage6.Location = New System.Drawing.Point(4, 28)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(832, 337)
        Me.TabPage6.TabIndex = 3
        Me.TabPage6.Text = "STICS"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'TextBox7
        '
        Me.TextBox7.Location = New System.Drawing.Point(10, 190)
        Me.TextBox7.Multiline = True
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New System.Drawing.Size(336, 85)
        Me.TextBox7.TabIndex = 40
        Me.TextBox7.Text = "Use the ""Built simulation outputs"" button to export simulation output in a standa" &
    "rd format in the ""SummaryOutput"" table of the ""MasterInput"" database"
        '
        'SticsRun
        '
        Me.SticsRun.Location = New System.Drawing.Point(350, 135)
        Me.SticsRun.Name = "SticsRun"
        Me.SticsRun.Size = New System.Drawing.Size(125, 51)
        Me.SticsRun.TabIndex = 39
        Me.SticsRun.Text = "Run simulations"
        Me.SticsRun.UseVisualStyleBackColor = True
        '
        'TextBox8
        '
        Me.TextBox8.Location = New System.Drawing.Point(10, 100)
        Me.TextBox8.Multiline = True
        Me.TextBox8.Name = "TextBox8"
        Me.TextBox8.Size = New System.Drawing.Size(336, 85)
        Me.TextBox8.TabIndex = 38
        Me.TextBox8.Text = "Use the ""Run simulations"" button to run model simulations with the model inputs g" &
    "enerated with the ""Build model inputs"""
        '
        'TextBox9
        '
        Me.TextBox9.Location = New System.Drawing.Point(10, 10)
        Me.TextBox9.Multiline = True
        Me.TextBox9.Name = "TextBox9"
        Me.TextBox9.Size = New System.Drawing.Size(336, 85)
        Me.TextBox9.TabIndex = 37
        Me.TextBox9.Text = "Use the ""Build model inputs"" button to build model-specific input files as inform" &
    "ed in the ""SimUnitList"" table in the MasterInput database"
        '
        'Btn_Concat_Stics
        '
        Me.Btn_Concat_Stics.Enabled = False
        Me.Btn_Concat_Stics.Location = New System.Drawing.Point(350, 224)
        Me.Btn_Concat_Stics.Name = "Btn_Concat_Stics"
        Me.Btn_Concat_Stics.Size = New System.Drawing.Size(125, 51)
        Me.Btn_Concat_Stics.TabIndex = 31
        Me.Btn_Concat_Stics.Text = "Build simulation outputs"
        Me.Btn_Concat_Stics.UseVisualStyleBackColor = True
        '
        'msgErr_expStics_export
        '
        Me.msgErr_expStics_export.ForeColor = System.Drawing.Color.Red
        Me.msgErr_expStics_export.Location = New System.Drawing.Point(6, 278)
        Me.msgErr_expStics_export.Name = "msgErr_expStics_export"
        Me.msgErr_expStics_export.Size = New System.Drawing.Size(400, 19)
        Me.msgErr_expStics_export.TabIndex = 30
        Me.msgErr_expStics_export.Text = "messageExport"
        Me.msgErr_expStics_export.Visible = False
        '
        'Btn_expStics_export
        '
        Me.Btn_expStics_export.Enabled = False
        Me.Btn_expStics_export.Location = New System.Drawing.Point(352, 43)
        Me.Btn_expStics_export.Name = "Btn_expStics_export"
        Me.Btn_expStics_export.Size = New System.Drawing.Size(123, 52)
        Me.Btn_expStics_export.TabIndex = 26
        Me.Btn_expStics_export.Text = "Build model inputs"
        Me.Btn_expStics_export.UseVisualStyleBackColor = True
        '
        'TabPage14
        '
        Me.TabPage14.Controls.Add(Me.TextBox10)
        Me.TabPage14.Controls.Add(Me.CelsiusRun)
        Me.TabPage14.Controls.Add(Me.TextBox11)
        Me.TabPage14.Controls.Add(Me.TextBox12)
        Me.TabPage14.Controls.Add(Me.msgErr_expCelsius_export)
        Me.TabPage14.Controls.Add(Me.Btn_Concat_Celsius)
        Me.TabPage14.Controls.Add(Me.Btn_expCelsius_export)
        Me.TabPage14.Location = New System.Drawing.Point(4, 28)
        Me.TabPage14.Name = "TabPage14"
        Me.TabPage14.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage14.Size = New System.Drawing.Size(832, 337)
        Me.TabPage14.TabIndex = 5
        Me.TabPage14.Text = "Celsius"
        Me.TabPage14.UseVisualStyleBackColor = True
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(10, 190)
        Me.TextBox10.Multiline = True
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(336, 85)
        Me.TextBox10.TabIndex = 44
        Me.TextBox10.Text = "Use the ""Built simulation outputs"" button to export simulation output in a standa" &
    "rd format in the ""SummaryOutput"" table of the ""MasterInput"" database"
        '
        'CelsiusRun
        '
        Me.CelsiusRun.Location = New System.Drawing.Point(350, 135)
        Me.CelsiusRun.Name = "CelsiusRun"
        Me.CelsiusRun.Size = New System.Drawing.Size(125, 51)
        Me.CelsiusRun.TabIndex = 43
        Me.CelsiusRun.Text = "Run simulations"
        Me.CelsiusRun.UseVisualStyleBackColor = True
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(10, 100)
        Me.TextBox11.Multiline = True
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(336, 85)
        Me.TextBox11.TabIndex = 42
        Me.TextBox11.Text = "Use the ""Run simulations"" button to run model simulations with the model inputs g" &
    "enerated with the ""Build model inputs"""
        '
        'TextBox12
        '
        Me.TextBox12.Location = New System.Drawing.Point(10, 10)
        Me.TextBox12.Multiline = True
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.Size = New System.Drawing.Size(336, 85)
        Me.TextBox12.TabIndex = 41
        Me.TextBox12.Text = "Use the ""Build model inputs"" button to build model-specific input files as inform" &
    "ed in the ""SimUnitList"" table in the MasterInput database"
        '
        'msgErr_expCelsius_export
        '
        Me.msgErr_expCelsius_export.ForeColor = System.Drawing.Color.Red
        Me.msgErr_expCelsius_export.Location = New System.Drawing.Point(6, 278)
        Me.msgErr_expCelsius_export.Name = "msgErr_expCelsius_export"
        Me.msgErr_expCelsius_export.Size = New System.Drawing.Size(400, 19)
        Me.msgErr_expCelsius_export.TabIndex = 30
        Me.msgErr_expCelsius_export.Text = "msgErr_expCelsius_export"
        Me.msgErr_expCelsius_export.Visible = False
        '
        'Btn_Concat_Celsius
        '
        Me.Btn_Concat_Celsius.Enabled = False
        Me.Btn_Concat_Celsius.Location = New System.Drawing.Point(352, 227)
        Me.Btn_Concat_Celsius.Name = "Btn_Concat_Celsius"
        Me.Btn_Concat_Celsius.Size = New System.Drawing.Size(123, 48)
        Me.Btn_Concat_Celsius.TabIndex = 33
        Me.Btn_Concat_Celsius.Text = "Build simulation outputs"
        Me.Btn_Concat_Celsius.UseVisualStyleBackColor = True
        '
        'Btn_expCelsius_export
        '
        Me.Btn_expCelsius_export.Enabled = False
        Me.Btn_expCelsius_export.Location = New System.Drawing.Point(352, 46)
        Me.Btn_expCelsius_export.Name = "Btn_expCelsius_export"
        Me.Btn_expCelsius_export.Size = New System.Drawing.Size(123, 49)
        Me.Btn_expCelsius_export.TabIndex = 26
        Me.Btn_expCelsius_export.Text = "Build model inputs"
        Me.Btn_expCelsius_export.UseVisualStyleBackColor = True
        '
        'FolderBrowserDialog1
        '
        Me.FolderBrowserDialog1.ShowNewFolderButton = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(236, 1)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(844, 112)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(-3, 1)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(237, 556)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 7
        Me.PictureBox3.TabStop = False
        '
        'TextBox13
        '
        Me.TextBox13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TextBox13.Location = New System.Drawing.Point(295, 524)
        Me.TextBox13.Name = "TextBox13"
        Me.TextBox13.Size = New System.Drawing.Size(405, 26)
        Me.TextBox13.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(727, 502)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 19)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Cores to use"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(240, 494)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(52, 58)
        Me.Button1.TabIndex = 10
        Me.Button1.UseVisualStyleBackColor = True
        '
        'NbCore
        '
        Me.NbCore.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.NbCore.Location = New System.Drawing.Point(731, 525)
        Me.NbCore.Maximum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.NbCore.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NbCore.Name = "NbCore"
        Me.NbCore.Size = New System.Drawing.Size(82, 26)
        Me.NbCore.TabIndex = 11
        Me.NbCore.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(298, 502)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(185, 19)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "MasterInput Database folder"
        '
        'ForceClimate
        '
        Me.ForceClimate.Location = New System.Drawing.Point(839, 509)
        Me.ForceClimate.Name = "ForceClimate"
        Me.ForceClimate.Size = New System.Drawing.Size(138, 43)
        Me.ForceClimate.TabIndex = 15
        Me.ForceClimate.Text = "Force overwrite Climate files"
        Me.ForceClimate.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1084, 561)
        Me.Controls.Add(Me.ForceClimate)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.NbCore)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox13)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Btn_dtMill_quit)
        Me.Controls.Add(Me.TabControl1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 5, 3, 5)
        Me.Name = "Form1"
        Me.Text = "DataMill"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        Me.TabPage14.ResumeLayout(False)
        Me.TabPage14.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NbCore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Btn_dtMill_quit As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents fileToImport As System.Windows.Forms.OpenFileDialog
    Friend WithEvents msgErr_expDssat_export As System.Windows.Forms.Label
    Friend WithEvents Btn_expDssat_export As System.Windows.Forms.Button
    Friend WithEvents msgErr_expStics_export As System.Windows.Forms.Label
    Friend WithEvents Btn_expStics_export As System.Windows.Forms.Button
    Friend WithEvents homeTitle As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Button2 As Button
    Friend WithEvents TabPage14 As TabPage
    Friend WithEvents msgErr_expCelsius_export As Label
    Friend WithEvents Btn_expCelsius_export As Button
    Friend WithEvents Btn_Concat_Stics As Button
    Friend WithEvents Btn_Concat_Dssat As Button
    Friend WithEvents Btn_Concat_Celsius As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox6 As TextBox
    Friend WithEvents DssatRun As Button
    Friend WithEvents TextBox7 As TextBox
    Friend WithEvents SticsRun As Button
    Friend WithEvents TextBox8 As TextBox
    Friend WithEvents TextBox9 As TextBox
    Friend WithEvents TextBox10 As TextBox
    Friend WithEvents CelsiusRun As Button
    Friend WithEvents TextBox11 As TextBox
    Friend WithEvents TextBox12 As TextBox
    Friend WithEvents TextBox13 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents NbCore As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox14 As TextBox
    Friend WithEvents ForceClimate As CheckBox
End Class
