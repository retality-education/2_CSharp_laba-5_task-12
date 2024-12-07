using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        /* поле для работы с содержанием файла*/
       

        TextBox textBox_1 = new TextBox();
        TextBox textBox_2 = new TextBox();
        string text;
        
        string filename_1 = "";
        string filename_2 = "";
        ToolStripMenuItem save_as_1 = new ToolStripMenuItem("Сохранить файл 1 как");
        ToolStripMenuItem save_as_2 = new ToolStripMenuItem("Сохранить файл 2 как");

        public Form1()
        {
            InitializeComponent();

            Text = "Лаба 5 задача 11";
            BackColor = Color.FromArgb(51, 51, 51);
            Font = new System.Drawing.Font("Sans serif", 12);
            WindowState = FormWindowState.Maximized;

            /* создание формы как меню*/
            MenuStrip menuStrip1 = new MenuStrip();
            textBox_1.Visible = false;
            textBox_1.ScrollBars = ScrollBars.Both;
            textBox_1.Multiline = true;

            textBox_2.Visible = false;
            textBox_2.ScrollBars = ScrollBars.Both;
            textBox_2.Multiline = true;

            /* добавление кнопок меню*/
            ToolStripMenuItem fileItem = new ToolStripMenuItem("Файл");
            ToolStripMenuItem newItem = new ToolStripMenuItem("Создать");
            ToolStripMenuItem saveItem_1 = new ToolStripMenuItem("Сохранить файл 1");
            ToolStripMenuItem saveItem_2 = new ToolStripMenuItem("Сохранить файл 2");
            ToolStripMenuItem openItem_1 = new ToolStripMenuItem("Открыть файл 1");
            ToolStripMenuItem openItem_2 = new ToolStripMenuItem("Открыть файл 2");
            ToolStripMenuItem closeItem_1 = new ToolStripMenuItem("Закрыть файл 1");
            ToolStripMenuItem closeItem_2 = new ToolStripMenuItem("Закрыть файл 2");
            fileItem.DropDownItems.Add(newItem);
            fileItem.DropDownItems.Add(openItem_1);
            fileItem.DropDownItems.Add(openItem_2);
            fileItem.DropDownItems.Add(saveItem_1);
            fileItem.DropDownItems.Add(saveItem_2);
            fileItem.DropDownItems.Add(save_as_1);
            fileItem.DropDownItems.Add(save_as_2);
            fileItem.DropDownItems.Add(closeItem_1);
            fileItem.DropDownItems.Add(closeItem_2);
            newItem.Click += createfile_Click;
            openItem_1.Click += openfile_1_Click;
            openItem_2.Click += openfile_2_Click;
            saveItem_1.Click += savefile_1_Сlick;
            saveItem_2.Click += savefile_2_Сlick;
            save_as_1.Click += save_as_1_Click;
            save_as_2.Click += save_as_2_Click;
            closeItem_1.Click += closeItem_1_Click;
            closeItem_2.Click += closeItem_2_Click;

            menuStrip1.Items.Add(fileItem);

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("О программе");
            ToolStripMenuItem task = new ToolStripMenuItem("Задача");
            ToolStripMenuItem abouttask = new ToolStripMenuItem("Условие");
            ToolStripMenuItem do_task = new ToolStripMenuItem("Выполнить");
            task.DropDownItems.Add(abouttask);
            task.DropDownItems.Add(do_task);
            aboutItem.Click += aboutItem_Сlick;
            abouttask.Click += abouttask_Сlick;
            do_task.Click += do_task_Click;
            menuStrip1.Items.Add(aboutItem);
            menuStrip1.Items.Add(task);
            Controls.Add(menuStrip1);
        }

        private void closeItem_1_Click(object sender, EventArgs e)
        {
            if (filename_1 != "")
            {
                textBox_1.Visible = false;
                textBox_1.Text = "";
                filename_1 = "";
                notice("Файл закрыт");
            }
            else
                notice("Никакой файл не был открыт");
        }
        private void closeItem_2_Click(object sender, EventArgs e)
        {
            if (filename_2 != "")
            {
                textBox_2.Visible = false;
                textBox_2.Text = "";
                filename_2 = "";
                notice("Файл закрыт");
            }
            else
                notice("Никакой файл не был открыт");
        }

        private void save_as_1_Click(object sender, EventArgs e)
        {
            /*сохранение файла с другим именем*/
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename_1 = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename_1, textBox_1.Text);
            notice("Файл сохранён");

        }
        private void save_as_2_Click(object sender, EventArgs e)
        {
            /*сохранение файла с другим именем*/
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename_2 = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename_2, textBox_2.Text);
            notice("Файл сохранён");

        }
        private void createfile_Click(object sender, EventArgs e)
        {
            /*Создание файла, которого ещё нет*/

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            notice("Файл создан");
        }

        private void openfile_1_Click(object sender, EventArgs e)
        {
            /*открытие существующего файла*/
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            filename_1 = openFileDialog1.FileName;

            string fileText = File.ReadAllText(filename_1);
            textBox_1.Text = fileText;
            text = textBox_1.Text;
            textBox_1.BackColor = Color.White;
            textBox_1.Location = new Point(50, 50);
            textBox_1.Size = new Size { Width = 700, Height = 700 };
            textBox_1.Visible = true;

            notice("Файл открыт");
            Controls.Add(textBox_1);
        }

        private void openfile_2_Click(object sender, EventArgs e)
        {
            /*открытие существующего файла*/
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (openFileDialog2.ShowDialog() == DialogResult.Cancel)
                return;

            filename_2 = openFileDialog2.FileName;

            string fileText = File.ReadAllText(filename_2);
            textBox_2.Text = fileText;
            text = textBox_2.Text;
            textBox_2.BackColor = Color.White;
            textBox_2.Location = new Point(800, 50);
            textBox_2.Size = new Size { Width = 700, Height = 700 };
            textBox_2.Visible = true;

            notice("Файл открыт");
            Controls.Add(textBox_2);
        }

        private void savefile_1_Сlick(object sender, EventArgs e)
        {
            /*  "сохранение изменений файла"*/
            if (filename_1 != "")
            {
                System.IO.File.WriteAllText(filename_1, textBox_1.Text);
                notice("Файл сохранён");
            }
            else
                save_as_1.PerformClick();
        }
        private void savefile_2_Сlick(object sender, EventArgs e)
        {
            /*  "сохранение изменений файла"*/
            if (filename_2 != "")
            {
                System.IO.File.WriteAllText(filename_2, textBox_2.Text);
                notice("Файл сохранён");
            }
            else
                save_as_2.PerformClick();
        }


        private void aboutItem_Сlick(object sender, EventArgs e)
        {
            /*информация выводящая правила правила работы программы*/
            string info = "1. Функциональность должна быть реализована в виде меню.\r\n" +
                "2. Программа должна позволять:\r\na. создавать новый файл;\r\nb. открывать" +
                " существующий;\r\nc. сохранять его;\r\nd. сохранять под другим именем;\r\ne." +
                " обрабатывать открытый файл;\r\nf. сохранять результат обработки (в случае" +
                " необходимости);\r\ng. очищать результат обработки.\r\n";
            notice(info);
        }

        private void abouttask_Сlick(object sender, EventArgs e)
        {
            /*информация о задаче, которую необходимо выполнить*/
            string info = "1. Даны два текстовых файла. Написать программу, которая сравнивала бы их " +
                "на совпадение и выводила бы в качестве результата номер строки и номер" +
                " символа, где встретилось первое отличие.\r\n";
            notice(info);
        }

        private async void do_task_Click(object sender, EventArgs e)
        {

            /* Выполнение задачи */
            if (filename_1 != "" && filename_2 != "")
            {
                if (filename_1 != filename_2)
                {
                    using (StreamReader reader1 = new StreamReader(filename_1))
                    using (StreamReader reader2 = new StreamReader(filename_2))
                    {
                        string line1;
                        string line2;
                        int lineNumber = 0;

                        while ((line1 = reader1.ReadLine()) != null && (line2 = reader2.ReadLine()) != null)
                        {
                            lineNumber++;
                            for (int charIndex = 0; charIndex < Math.Min(line1.Length, line2.Length); charIndex++)
                            {
                                if (line1[charIndex] != line2[charIndex])
                                {
                                    // Report the first difference found
                                    notice($"Первое отличие найдено в строке {lineNumber}, символ {charIndex + 1}");
                                    return;
                                }
                            }

                            // Check for additional characters in longer lines
                            if (line1.Length != line2.Length)
                            {
                                notice($"Первое отличие найдено в строке {lineNumber}, символ {Math.Min(line1.Length, line2.Length) + 1}");
                                return;
                            }
                        }

                        notice("Файлы идентичны или один из них короче другого.");
                    }
                }
                else
                {
                    notice("Открыт один и тот же файл дважды");
                }
            }
            else
            {
                notice("Отсутствует исходный файл");
            }
        }
        private void notice(string message)
        {
            MessageBox.Show(
              message,
              "Сообщение",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
        }
    }
}
