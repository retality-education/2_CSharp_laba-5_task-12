using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;



namespace WindowsFormsApp3
{

    public partial class Form1 : Form
    {
        /* поле для работы с содержанием файла*/
        public const uint CNT_OF_FILES = 2;

        TextBox[] textBoxes = new TextBox[2];

        string text;

        string[] filenames = new string[CNT_OF_FILES];
        ToolStripMenuItem[] save_as = new ToolStripMenuItem[CNT_OF_FILES];


        public Form1()
        {
            InitializeComponent();

            Text = "Лаба 5 задача 11";
            BackColor = Color.FromArgb(51, 51, 51);
            Font = new System.Drawing.Font("Sans serif", 12);
            WindowState = FormWindowState.Maximized;

            for (int i = 0; i < CNT_OF_FILES; i++)
            {
                textBoxes[i] = new TextBox();
                textBoxes[i].Visible = false;
                textBoxes[i].ScrollBars = ScrollBars.Both;
                textBoxes[i].Multiline = true;
                textBoxes[i].Tag = i;

                save_as[i] = new ToolStripMenuItem($"Сохранить файл {i + 1} как");
                save_as[i].Tag = i;
            }
            /* создание формы как меню*/
            MenuStrip menuStrip1 = new MenuStrip();

            /* добавление кнопок меню*/
            ToolStripMenuItem fileItem = new ToolStripMenuItem("Файл");
            ToolStripMenuItem newItem = new ToolStripMenuItem("Создать");
            ToolStripMenuItem[] saveItems = new ToolStripMenuItem[CNT_OF_FILES];
            ToolStripMenuItem[] openItems = new ToolStripMenuItem[CNT_OF_FILES];
            ToolStripMenuItem[] closeItems = new ToolStripMenuItem[CNT_OF_FILES];
            for (int i = 0; i < CNT_OF_FILES; i++)
            {
                saveItems[i] = new ToolStripMenuItem($"Сохранить файл {i + 1}");
                openItems[i] = new ToolStripMenuItem($"Открыть файл {i + 1}");
                closeItems[i] = new ToolStripMenuItem($"Закрыть файл {i + 1}");
                saveItems[i].Tag = i;
                openItems[i].Tag = i;
                closeItems[i].Tag = i;
            }

            fileItem.DropDownItems.Add(newItem);
            for (int i = 0; i < CNT_OF_FILES; i++)
            {
                fileItem.DropDownItems.Add(openItems[i]);
                fileItem.DropDownItems.Add(saveItems[i]);
                fileItem.DropDownItems.Add(save_as[i]);
                fileItem.DropDownItems.Add(closeItems[i]);
            }
            for (int i = 0; i < CNT_OF_FILES; i++)
            {
                openItems[i].Click += openfile_Click;
                saveItems[i].Click += savefile_Сlick;
                save_as[i].Click += save_as_Click;
                closeItems[i].Click += closeItem_Click;
            }
            newItem.Click += createfile_Click;


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

        private void closeItem_Click(object sender, EventArgs e)
        {
            int idx = (int)((ToolStripMenuItem)sender).Tag;
            if (filenames[idx] != "")
            {
                textBoxes[idx].Visible = false;
                textBoxes[idx].Text = "";

                filenames[idx] = "";
                notice("Файл закрыт");
            }
            else
                notice("Никакой файл не был открыт");
        }


        private void save_as_Click(object sender, EventArgs e)
        {
            /*сохранение файла с другим именем*/
            int idx = (int)((ToolStripMenuItem)sender).Tag;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filenames[idx] = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filenames[idx], textBoxes[idx].Text);
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

        private void openfile_Click(object sender, EventArgs e)
        {
            int idx = (int)((ToolStripMenuItem)sender).Tag;
            /*открытие существующего файла*/
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            filenames[idx] = openFileDialog1.FileName;

            string fileText = File.ReadAllText(filenames[idx]);
            textBoxes[idx].Text = fileText;
            text = textBoxes[idx].Text;
            textBoxes[idx].BackColor = Color.White;
            textBoxes[idx].Location = new Point(50 , 50 + 375 * idx);
            textBoxes[idx].Size = new Size { Width = 350, Height = 350 };
            textBoxes[idx].Visible = true;

            notice("Файл открыт");
            Controls.Add(textBoxes[idx]);
        }

        private void savefile_Сlick(object sender, EventArgs e)
        {
            int idx = (int)((ToolStripMenuItem)sender).Tag;
            /*  "сохранение изменений файла"*/
            if (filenames[idx] != "")
            {
                System.IO.File.WriteAllText(filenames[idx], textBoxes[idx].Text);
                notice("Файл сохранён");
            }
            else
                save_as[idx].PerformClick();
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
            string info = "1. Даны n текстовых файла. Написать программу, которая сравнивала бы их " +
                "на совпадение и выводила бы в качестве результата номер строки и номер" +
                " символа, где встретилось первое отличие.\r\n";
            notice(info);
        }

        private void do_task_Click(object sender, EventArgs e)
        {

            /* Выполнение задачи */
            string[] lines = new string[CNT_OF_FILES];

            bool all_files_is_not_null = true;
            for (int i = 0; i < CNT_OF_FILES; i++)
                all_files_is_not_null &= filenames[i] != "";

            bool all_files_is_different = true;
            for (int i = 0; i < CNT_OF_FILES; i++)
                for (int j = i + 1; j < CNT_OF_FILES; j++)
                    all_files_is_different &= filenames[i] != filenames[j];

            if (all_files_is_not_null)
            {
                if (all_files_is_different)
                {
                    StreamReader[] sr = new StreamReader[CNT_OF_FILES];
                    for (int i = 0; i < CNT_OF_FILES; i++)
                        sr[i] = new StreamReader(filenames[i]);

                    bool while_reading = true;
                    for (int i = 0; i < CNT_OF_FILES; i++)
                        while_reading &= (lines[i] = sr[i].ReadLine()) != null;

                    int lineNumber = 0;
                    while (while_reading)
                    {
                        lineNumber++;
                        for (int charIndex = 0; charIndex < lines.Min(line => lines.Length); charIndex++)
                        {
                            int first;
                            int second;
                            for (int i = 0; i < CNT_OF_FILES; i++)
                                for (int j = i + 1; j < CNT_OF_FILES; j++)
                                    if (lines[i][charIndex] != lines[j][charIndex])
                                    {
                                        first = i;
                                        second = j;
                                        notice($"Первое отличие найдено в файлах {filenames[first]}, {filenames[second]} в строке {lineNumber}, символ {charIndex + 1} ");
                                        return;
                                    }

                        }

                        bool is_all_lines_have_the_same_length = true;
                        for (int i = 0; i < CNT_OF_FILES - 1; i++)
                            is_all_lines_have_the_same_length &= lines[i].Length == lines[i + 1].Length;

                        if (is_all_lines_have_the_same_length)
                        {
                            notice($"Все файлы одинаковы до строки {lineNumber}, и символа {lines.Min(line => line.Length) + 1}, но некоторые из файлов длинее остальных.");
                            return;
                        }
                        notice("Все файлы идентичны.");
                    }

                }else
                    notice("Среди открытых файлов есть открытые несколько раз.");
            }else
                notice("Один из числа файлов не открыт"); 
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
