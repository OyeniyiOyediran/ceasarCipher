using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace ceasarCipherGUI
{
    public partial class Form1 : Form
    {
        public List<char> allAlphabets = new List<char>()
            { '[', ']', '!', '|', 'a', 'b', 'Z', 'X', 'C', 'V', 'B', 'N', '0', 'G', 'H', 'J', 'K', 'L', '@', '#', '$', '%', '1', 'M',
            'Q', 'W', 'E', 'R', 'c', '\'', 'd', 'e', 'f', 'g', 'h','^', '&', '*', '(', ')', '-', 'T', 'Y', 'U', 'I', 'O', 'P',
            '=',  'i', '6', '7', 'j', 'k', 'l', 'm', 'n', '8', '9', 'o', 'p', '4', '5', 'q', '+', '_', '{', '}', '"', 'A', 'S',
            'D', 'F', 'r', 's', 't', 'u', 'v', '\\', 'w', '2', '3', 'x', 'y', 'z', ':', '<', '>', '?', '/', '.',',',';'
    };
        string fileName = default;
        int myKey = default;
        StreamReader myOldFile = default;
        FileStream myFileStream = default;
        StreamWriter myNewFile = default;
        public string cipherFile = default;
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            using(OpenFileDialog myFile = new OpenFileDialog())
            {
                result = myFile.ShowDialog();
                fileName = myFile.FileName;
            }
            if(result is DialogResult.OK && fileName.EndsWith(".txt") is false)
            {
                MessageBox.Show("Only \".txt\" files are permitted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                myFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                myOldFile = new StreamReader(myFileStream);
                textBox2.Text = fileName;
                textBox1.Enabled = true;
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            myKey = keyVerifier(textBox1.Text);
            if (Convert.ToInt32(myKey).GetType().ToString() == "System.Int32" && myKey.ToString() != "")
            {
                DialogResult choice = default;
                using (SaveFileDialog encryptedFile = new SaveFileDialog())
                {
                    choice = encryptedFile.ShowDialog();
                    cipherFile = encryptedFile.FileName;
                }

                if (choice is DialogResult.OK && cipherFile.EndsWith(".txt"))
                {
                    myFileStream = new FileStream(cipherFile, FileMode.Create, FileAccess.Write);
                    myNewFile = new StreamWriter(myFileStream);
                    StringBuilder encryptedText = new StringBuilder();
                    int nPosition = default;
                    string plaintext = myOldFile.ReadToEnd();
                    string [] bq = Regex.Split(plaintext, @"\s");
                    foreach (var h in bq)
                    {
                        if(h != "")
                        {
                            StringBuilder myText= new StringBuilder(h);
                            for (int i = 0; i < myText.Length; i++)
                            {
                                int g = allAlphabets.IndexOf(myText[i]);
                                nPosition = (g + myKey > allAlphabets.Count()-1) ? (g + myKey) - allAlphabets.Count() : g + myKey;
                                encryptedText.Append(allAlphabets[nPosition]);
                            }
                            try
                            {
                                myNewFile.Write(encryptedText + " ");
                                encryptedText.Clear();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error occured while writing to file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    try
                    {
                        myNewFile.Close();
                    }
                    catch (EncoderFallbackException)
                    {
                        MessageBox.Show("Unable to close file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Only \".txt\" files are permitted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Key must be a number and must not be more than \"10\" ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            myKey = keyVerifier(textBox1.Text);
            if (Convert.ToInt32(myKey).GetType().ToString() == "System.Int32" && myKey.ToString() != "")
            {
                DialogResult choice = default; 
                using (SaveFileDialog encryptedFile = new SaveFileDialog())
                {
                    choice = encryptedFile.ShowDialog();
                    cipherFile = encryptedFile.FileName;
                }

                if (choice is DialogResult.OK && cipherFile.EndsWith(".txt"))
                {
                    myFileStream = new FileStream(cipherFile, FileMode.Create, FileAccess.Write);
                    myNewFile = new StreamWriter(myFileStream);
                    StringBuilder decryptedText = new StringBuilder();
                    int nPosition = default;
                    StringBuilder encodedText = new StringBuilder(myOldFile.ReadToEnd());
                    string[] zx = Regex.Split(encodedText.ToString(), @"\s");
                    foreach (var q in zx)
                    {
                        if (q != "")
                        {
                            StringBuilder plainText = new StringBuilder(q);
                            for (int i = 0; i < plainText.Length; i++)
                            {
                                int g = allAlphabets.IndexOf(plainText[i]);
                                nPosition = (g - myKey < 0) ? (g - myKey) + allAlphabets.Count() : g - myKey;
                                decryptedText.Append(allAlphabets[nPosition]);
                            }
                            try
                            {
                                myNewFile.Write(decryptedText + " ");
                                decryptedText.Clear();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Error occured while writing to file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }                                            
                    try
                    {
                        myNewFile.Close();
                    }
                    catch (EncoderFallbackException)
                    {
                        MessageBox.Show("Unable to close file", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Only \".txt\" files are permitted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Key must be a number and must not be more than \"15\" ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public int keyVerifier(string k)
        {
            int myKey = default;
            if (!int.TryParse(k, out myKey) || Convert.ToInt32(k) > 10)
            {
                MessageBox.Show("Key must be a number and must not be more than \"10\" ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return myKey;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
        }
    }
}
