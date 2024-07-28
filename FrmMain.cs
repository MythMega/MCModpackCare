using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace ModpackManagement
{
    public partial class FrmMain : Form
    {
        private string FULL_APP_LANGUAGE = "EN";
        private string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JMD", "mcmodmanager", "config.cfg");
        private string modpackFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JMD", "mcmodmanager", "modpacks");
        private bool APP_STARTED = false;
        private bool APP_READY = false;
        private string currentProfileFile = "";
        private List<string> groupModList = new List<string>();
        private List<string> DisabledGoupModList = new List<string>();
        private List<string> modModList = new List<string>();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(ModpackDragEnter);
            this.DragDrop += new DragEventHandler(ModpackDragDrop);

            cboxModpack.DropDownStyle = ComboBoxStyle.DropDownList;

            if (!File.Exists(configFilePath))
            {
                // Le fichier n'existe pas, créons-le avec le contenu par défaut
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(configFilePath)); // Créer les répertoires si nécessaire
                    string defaultContent = @"selectedPack=
disabledmods=";
                    File.WriteAllText(configFilePath, defaultContent);
                    Console.WriteLine("Le fichier a été créé avec le contenu par défaut.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Une erreur est survenue lors de la création du fichier : " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Le fichier existe déjà.");
            }
            // Le fichier n'existe pas, créons-le avec le contenu par défaut
            try
            {
                Directory.CreateDirectory(modpackFilesPath); // Créer les répertoires si nécessaire
                Console.WriteLine("Le fichier a été créé avec le contenu par défaut.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur est survenue lors de la création du fichier : " + ex.Message);
            }

            loadComboBoxModpack();

            APP_STARTED = true;
            APP_READY = true;
        }

        private void cboxModpack_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentProfileFile = Path.Combine(modpackFilesPath, cboxModpack.SelectedItem.ToString());
            lblSelectedModpack.Text = $"Selected Modpack : {cboxModpack.SelectedItem}";

            if (!APP_STARTED)
            {
                return;
            }
            
            // Vérifiez si le fichier existe
            if (File.Exists(configFilePath))
            {
                // Lisez toutes les lignes du fichier
                string[] lines = File.ReadAllLines(configFilePath);

                // Parcourez chaque ligne
                for (int i = 0; i < lines.Length; i++)
                {
                    // Vérifiez si la ligne commence par 'selectedPack='
                    if (lines[i].StartsWith("selectedPack="))
                    {
                        // Modifiez la ligne pour y mettre la valeur de la combobox
                        lines[i] = "selectedPack=" + cboxModpack.SelectedItem.ToString();
                    }
                }

                // Écrivez les lignes modifiées dans le fichier
                File.WriteAllLines(configFilePath, lines);
            }
            else
            {
                MessageBox.Show("Le fichier n'existe pas: " + configFilePath);
            }

            setModlistTexts();
        }

        private void loadCheckboxes()
        {
            
            string[] lines = File.ReadAllLines(currentProfileFile);
            bool mod = true;

            int y = 20; // Position de départ pour la première case à cocher

            foreach (string line in lines)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (line.StartsWith("* mods"))
                {
                    mod = true;
                    continue;
                }
                if (line.StartsWith("* groups"))
                {
                    mod = false;
                    continue;
                }

                if (!mod)
                {
                    groupModList.Add(line.Split(';')[0]);

                    CheckBox checkBox = new CheckBox();
                    checkBox.Text = line.Split(';')[0];
                    checkBox.Location = new Point(10, y); // Positionne la case à cocher
                    gbOptionals.Controls.Add(checkBox);
                    checkBox.Tag = $"{line.Split(';')[1]}";

                    Button button = new Button();
                    button.Text = "Infos";
                    button.Location = new Point(200, y);
                    gbOptionals.Controls.Add(button);
                    button.Tag = checkBox.Text;


                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.BackColor = Color.CadetBlue;
                    button.ForeColor = Color.White;
                    button.Width = 80;

                    ProgressBar bar = new ProgressBar();
                    bar.Location = new Point(282, y);
                    gbOptionals.Controls.Add(bar);
                    bar.Width = 70;
                    bar.Minimum = 0;
                    bar.Maximum = 5;
                    bar.Value = getModPartWeight(checkBox.Text);

                    y += checkBox.Height + 5; // Met à jour la position y pour la prochaine case à cocher

                    checkBox.Checked = true;

                    checkBox.CheckedChanged += CheckModParts;
                    button.Click += ValueModParts;
                }
            }
            disableSavedDisabledGroups();
        }

        public void setModlistTexts()
        {
            gbModList.Controls.Clear();
            // Créer un FlowLayoutPanel
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            flowLayoutPanel.FlowDirection = FlowDirection.TopDown;

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(appPath, "*.*", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                string extension = Path.GetExtension(file);
                if (extension == ".jar" || extension == ".dis")
                {
                    Label label = new Label();
                    label.Text = Path.GetFileNameWithoutExtension(file);
                    label.ForeColor = (extension == ".jar") ? Color.LightGreen : Color.OrangeRed;
                    label.AutoSize = true;  // Ajuster la taille du label à son contenu

                    // Ajouter le label au FlowLayoutPanel
                    flowLayoutPanel.Controls.Add(label);
                }
            }

            // Ajouter le FlowLayoutPanel au GroupBox
            gbModList.Controls.Add(flowLayoutPanel);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            APP_READY = false;
            if (!APP_STARTED)
            {
                return;
            }

            if (currentProfileFile.Length > 1)
            {
                string[] lines = File.ReadAllLines(currentProfileFile);
                bool mod = true;

                int y = 20; // Position de départ pour la première case à cocher
                
                foreach (string line in lines)
                {
                    if(line.StartsWith("#"))
                    {
                        continue;
                    }
                    if (line.StartsWith("* mods"))
                    {
                        mod = true;
                        continue;
                    }
                    if (line.StartsWith("* groups"))
                    {
                        mod = false;
                        continue;
                    }
                    
                    if(mod)
                    {
                        downloadMod(line);
                        modModList.Add(line.Split(';')[0]);
                    }
                    else
                    {
                        groupModList.Add(line.Split(';')[0]);

                        CheckBox checkBox = new CheckBox();
                        checkBox.Text = line.Split(';')[0];
                        checkBox.Location = new Point(10, y); // Positionne la case à cocher
                        gbOptionals.Controls.Add(checkBox);
                        checkBox.Tag = $"{line.Split(';')[1]}";

                        Button button = new Button();
                        button.Text = "Infos";
                        button.Location = new Point(200, y);
                        gbOptionals.Controls.Add(button);
                        button.Tag = checkBox.Text;
                        
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderSize = 0;
                        button.BackColor = Color.CadetBlue;
                        button.ForeColor = Color.White;
                        button.Width = 80;

                        ProgressBar bar = new ProgressBar();
                        bar.Location = new Point(280, y);
                        gbOptionals.Controls.Add(bar);
                        bar.Width = 50;
                        bar.Minimum = 0;
                        bar.Maximum = 5;
                        bar.Value = getModPartWeight(checkBox.Text);

                        y += checkBox.Height + 5; // Met à jour la position y pour la prochaine case à cocher

                        checkBox.Checked = true;

                        checkBox.CheckedChanged += CheckModParts;
                        button.Click += ValueModParts;

                    }
                }
            }

            foreach (Control control in gbOptionals.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkbox = control as CheckBox;
                    checkbox.Checked = true;
                }
            }
            saveDisabledGroups();
            APP_READY = true;
        }

        private void ValueModParts(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string modpart = btn.Tag.ToString(); // on obtiens la catégorie de mod
            int modWeight = getModPartWeight(modpart); // on obtiens son poids
            List<string> mods = getModListFromModParts(modpart); // on obtiens la liste des mods correspondant
            string message = $"Mod list :\n";
            mods.ForEach(x => message += $"{x}\n");
            message += "\n\n";
            switch (modWeight)
            {
                default:
                case 0:
                    message += "No";
                    break;
                case 1:
                    message += "ultra light";
                    break;
                case 2:
                    message += "Light";
                    break;
                case 3:
                    message += "Medium";
                    break;
                case 4:
                    message += "heavy";
                    break;
                case 5:
                    message += "really heavy";
                    break;
            }
            message += " Impacts on performances";
            MessageBox.Show(message);
        }

        private List<string> getModListFromModParts(string modpart)
        {
            List<string> mods = new List<string>();
            string[] lines = File.ReadAllLines(currentProfileFile);
            bool mod = true;

            foreach (string line in lines)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (line.StartsWith("* mods"))
                {
                    mod = true;
                    continue;
                }
                if (line.StartsWith("* groups"))
                {
                    mod = false;
                    continue;
                }

                if (mod)
                {
                    if(modpart == line.Split(';')[2] && line.Split(';')[3] == "0")
                        mods.Add(line.Split(';')[0]);
                }
            }
            return( mods );
        }

        private int getModPartWeight(string groupName)
        {
            int weight = 0;
            bool mod = true;
            string[] lines = File.ReadAllLines(currentProfileFile);
            foreach (string line in lines)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (line.StartsWith("* mods"))
                {
                    mod = true;
                    continue;
                }
                if (line.StartsWith("* groups"))
                {
                    mod = false;
                    continue;
                }

                if (!mod && line.Split(';')[0] == groupName)
                {
                    weight = int.Parse(line.Split(';')[1]);
                }
            }
            return weight;
        }

        private void CheckModParts(object sender, EventArgs e)
        {
            if(!APP_READY)
            {
                return;
            }
            CheckBox checkBox = (CheckBox)sender;
            string modGroup = checkBox.Text;
            string[] lines = File.ReadAllLines(currentProfileFile);
            bool mod = true;
            foreach (string line in lines)
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }
                if (line.StartsWith("* mods"))
                {
                    mod = true;
                    continue;
                }
                if (line.StartsWith("* groups"))
                {
                    mod = false;
                    continue;
                }

                if (mod && line.Split(';')[2] == modGroup)
                {
                    if(!checkBox.Checked && !DisabledGoupModList.Contains(modGroup))
                    {
                        DisabledGoupModList.Add(modGroup);
                        
                    }
                    changeModState(line.Split(';')[0], checkBox.Checked);
                    saveDisabledGroups();
                }
            }
            setModlistTexts();
        }

        private void saveDisabledGroups()
        {
            string[] configLines = File.ReadAllLines(configFilePath);
            List<string> disabledLine = new List<string>();
            foreach (Control control in gbOptionals.Controls)
            {
                if (control is CheckBox)
                {
                    CheckBox checkbox = control as CheckBox;
                    if (!checkbox.Checked)
                    {
                        disabledLine.Add(checkbox.Text);
                    }
                }
            }
            string newLine = String.Join(";",disabledLine);

            // Lisez toutes les lignes du fichier
            string[] lines = File.ReadAllLines(configFilePath);

            // Parcourez chaque ligne
            for (int i = 0; i < lines.Length; i++)
            {
                // Vérifiez si la ligne commence par 'disabledmods='
                if (lines[i].StartsWith("disabledmods="))
                {
                    // Modifiez la ligne pour y mettre la valeur de la combobox
                    lines[i] = "disabledmods=" + newLine;
                }
            }

            // Écrivez les lignes modifiées dans le fichier
            File.WriteAllLines(configFilePath, lines);
        }

        public void disableSavedDisabledGroups()
        {
            string[] configLines = File.ReadAllLines(configFilePath);
            foreach (string l in configLines)
                if (l.StartsWith("disabledmods="))
                {
                    // Obtenez le nom du pack à partir de la ligne
                    string element = l.Substring("disabledmods=".Length);

                    List<string> disabledStuff = element.Split(';').ToList();

                    foreach (string s in disabledStuff)
                    {
                        foreach (Control control in gbOptionals.Controls)
                        {
                            if (control is CheckBox)
                            {
                                CheckBox checkbox = control as CheckBox;
                                if (checkbox.Text == s)
                                {
                                    checkbox.Checked = false;
                                }
                            }
                        }
                    }
                }
        }
        private void changeModState(string modName, bool enable)
        {
            string extension = enable ? ".jar" : ".dis";
            string newFileName = modName + extension;

            // Assurez-vous que le fichier existe avant de le renommer
            if (System.IO.File.Exists(modName + ".jar") || System.IO.File.Exists(modName + ".dis"))
            {
                try { 
                System.IO.File.Move(modName + ".jar", newFileName);
                }
                catch { }
                try { 
                System.IO.File.Move(modName + ".dis", newFileName);
                }
                catch { }

            }
            else
            {
                throw new System.IO.FileNotFoundException($"Le fichier {modName}.jar ou {modName}.dis n'a pas été trouvé.");
            }
        }

        private void downloadMod(string line)
        {
            string modName = line.Split(';')[0]+".jar";
            string modUrl = line.Split(';')[1];

            if(File.Exists(Path.Combine(Application.StartupPath, modName)) || File.Exists(Path.Combine(Application.StartupPath, line.Split(';')[0]+".dis")) )
            {
                try
                {
                    File.Delete(Path.Combine(Application.StartupPath, line.Split(';')[0] + ".dis"));
                } catch
                {
                    
                }
                try
                {
                    File.Delete(Path.Combine(Application.StartupPath, modName));
                } catch
                {
                    
                }
            }

            using (WebClient client = new WebClient())
            {
                try
                {
                    // Téléchargez le fichier
                    client.DownloadFile(modUrl, modName);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"mod : {modName}\nmodUrl : {modUrl}\nUne erreur s'est produite lors du téléchargement du fichier : " + ex.Message);
                }
            }
        }

        private void btnOpenModFolder_Click(object sender, EventArgs e)
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeFolder = System.IO.Path.GetDirectoryName(exePath);

            Process.Start("explorer.exe", exeFolder);
        }

        private void btnOpenMPFolder_Click(object sender, EventArgs e)
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeFolder = System.IO.Path.GetDirectoryName(exePath);

            Process.Start("explorer.exe", modpackFilesPath);
        }

        private void btnExecMC_Click(object sender, EventArgs e)
        {
            string a = "C:\\XboxGames\\Minecraft Launcher\\Content\\Minecraft.exe";
            Process.Start("explorer.exe", a);
        }

        private void ModpackDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                bool validFile = true;
                FileInfo fi = new FileInfo(file);
                if (fi.Length <= 50 * 1024) // Poids inférieur à 50Ko
                {
                    string[] lines = File.ReadAllLines(file);
                    if (lines.Any(line => line.Contains("* mods")) && lines.Any(line => line.Contains("* groups")))
                    {
                        int countLine = 0;
                        string typeLigne = "mod";
                        foreach (string line in lines)
                        {
                            countLine++;
                            if (line == "* mods")
                                typeLigne = "mod";
                            if (line == "* groups")
                                typeLigne = "group";

                            if (!line.StartsWith("#") && !line.StartsWith("*"))
                            {
                                string[] elements = line.Split(';');
                                if ((elements.Length == 4 && typeLigne == "mod") || (elements.Length == 2 && typeLigne == "group"))
                                {
                                    Console.WriteLine(line + " is valid.");
                                }
                                else
                                {
                                    validFile = false;
                                    switch(typeLigne)
                                    {
                                        case "mod":
                                            MessageBox.Show($"error in line \"{line}\" (line {countLine}), found {elements.Length} parts but 4 expected.");
                                            break;
                                        case "group":
                                            MessageBox.Show($"error in line \"{line}\" (line {countLine}), found {elements.Length} parts but 2 expected.");
                                            break;

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Invalid file content {Path.GetFileName(file)}. Make sure you have \"* mods\" and \"* groups\" inside file content.");
                    }
                }
                else
                {
                    MessageBox.Show("File size exceed 50Ko, if it's legit, drag and drop your file into modpack folder");
                }
                if(validFile)
                {
                    // Le fichier correspond à vos critères
                    string destinationPath = Path.Combine(modpackFilesPath, fi.Name);
                    File.Copy(file, destinationPath);
                    break;
                }
            }
        }

        private void ModpackDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void btnRefreshListModpack_Click(object sender, EventArgs e)
        {
            loadComboBoxModpack();
        }

        private void loadComboBoxModpack()
        {
            cboxModpack.Items.Clear();
            // Obtenez tous les fichiers dans le dossier
            string[] files = Directory.GetFiles(modpackFilesPath);

            // Parcourez chaque fichier
            foreach (string file in files)
            {
                // Obtenez le nom du fichier sans le chemin complet
                string fileName = Path.GetFileName(file);

                // Ajoutez le nom du fichier à la combobox
                cboxModpack.Items.Add(fileName);
            }
            // Vérifiez si le fichier existe
            if (File.Exists(configFilePath))
            {
                // Lisez toutes les lignes du fichier
                string[] lines = File.ReadAllLines(configFilePath);

                // Parcourez chaque ligne
                foreach (string line in lines)
                {
                    // Vérifiez si la ligne commence par 'selectedPack='
                    if (line.StartsWith("selectedPack="))
                    {
                        // Obtenez le nom du pack à partir de la ligne
                        string packName = line.Substring("selectedPack=".Length);

                        // Vérifiez si la combobox contient cet élément
                        if (cboxModpack.Items.Contains(packName))
                        {
                            // Sélectionnez cet élément dans la combobox
                            cboxModpack.SelectedItem = packName;
                        }
                    }
                    // Vérifiez si la ligne commence par 'selectedPack='
                    if (line.StartsWith("selectedPack="))
                    {
                        // Obtenez le nom du pack à partir de la ligne
                        string packName = line.Substring("selectedPack=".Length);

                        // Vérifiez si la combobox contient cet élément
                        if (cboxModpack.Items.Contains(packName))
                        {
                            // Sélectionnez cet élément dans la combobox
                            cboxModpack.SelectedItem = packName;
                            currentProfileFile = Path.Combine(modpackFilesPath, cboxModpack.SelectedItem.ToString());

                            loadCheckboxes();
                            setModlistTexts();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Le fichier n'existe pas: " + configFilePath);
            }
        }
    }
}
