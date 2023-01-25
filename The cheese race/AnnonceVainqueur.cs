using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_cheese_race
{
    public partial class AnnonceVainqueur : Form
    {
        public AnnonceVainqueur()
        {
            InitializeComponent();
        }

        private void AnnonceVainqueur_Load(object sender, EventArgs e)
        {
            if (Donnees_publics.Many == false)
            {
                if (Donnees_publics.blueWin == true)
                    pctBoxSingle.Image = Properties.Resources.blue_mouse270;
                else if (Donnees_publics.greenWin == true)
                    pctBoxSingle.Image = Properties.Resources.green_mouse270;
                else if (Donnees_publics.purpleWin == true)
                    pctBoxSingle.Image = Properties.Resources.purple_mouse270;
                else if (Donnees_publics.redWin == true)
                    pctBoxSingle.Image = Properties.Resources.red_mouse270;

                pctBoxTitle.Image = Properties.Resources.WinnerIs;
                pctBoxSingle.Location = new Point(175, 130);
                pctBoxSingle.Size = new Size(400, 400);
                pctBoxSingle.BringToFront();
                pictureBox2.Image = null;
                pictureBox3.Image = null;
                pictureBox4.Image = null;
                buttonEllipse1.Hide();
                buttonEllipse2.BringToFront();
                buttonEllipse2.Text = "END";
            }
            else
            {
                pctBoxTitle.Image = Properties.Resources.ManyWin;
                pctBoxSingle.Location = new Point(258, 173);
                pctBoxSingle.Size = new Size(230, 210);
                buttonEllipse1.Show();
                buttonEllipse2.Text = "It's ok for us many winners";
                if (Donnees_publics.HasWin == 2)
                {
                    if (Donnees_publics.blueWin == true && Donnees_publics.greenWin == true)
                    {
                        pictureBox2.Image = Properties.Resources.blue_mouse270;
                        pictureBox3.Image = Properties.Resources.green_mouse270;
                    }
                    else if (Donnees_publics.blueWin == true && Donnees_publics.purpleWin == true)
                    {
                        pictureBox2.Image = Properties.Resources.blue_mouse270;
                        pictureBox3.Image = Properties.Resources.purple_mouse270;
                    }
                    else if (Donnees_publics.blueWin == true && Donnees_publics.redWin == true)
                    {
                        pictureBox2.Image = Properties.Resources.blue_mouse270;
                        pictureBox3.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Donnees_publics.greenWin == true && Donnees_publics.purpleWin == true)
                    {
                        pictureBox2.Image = Properties.Resources.green_mouse270;
                        pictureBox3.Image = Properties.Resources.purple_mouse270;
                    }
                    else if (Donnees_publics.greenWin == true && Donnees_publics.redWin == true)
                    {
                        pictureBox2.Image = Properties.Resources.green_mouse270;
                        pictureBox3.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Donnees_publics.purpleWin == true && Donnees_publics.redWin == true)
                    {
                        pictureBox2.Image = Properties.Resources.purple_mouse270;
                        pictureBox3.Image = Properties.Resources.red_mouse270;
                    }
                }
                else if (Donnees_publics.HasWin == 3)
                {
                    if (Donnees_publics.blueWin == true && Donnees_publics.greenWin == true && Donnees_publics.purpleWin == true)
                    {
                        pctBoxSingle.Image = Properties.Resources.blue_mouse270;
                        pictureBox2.Image = Properties.Resources.green_mouse270;
                        pictureBox3.Image = Properties.Resources.purple_mouse270;
                    }
                    else if (Donnees_publics.blueWin == true && Donnees_publics.greenWin == true && Donnees_publics.redWin == true)
                    {
                        pctBoxSingle.Image = Properties.Resources.blue_mouse270;
                        pictureBox2.Image = Properties.Resources.green_mouse270;
                        pictureBox3.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Donnees_publics.blueWin == true && Donnees_publics.purpleWin == true && Donnees_publics.redWin == true)
                    {
                        pctBoxSingle.Image = Properties.Resources.blue_mouse270;
                        pictureBox2.Image = Properties.Resources.purple_mouse270;
                        pictureBox3.Image = Properties.Resources.red_mouse270;
                    }
                    else if (Donnees_publics.greenWin == true && Donnees_publics.purpleWin == true && Donnees_publics.redWin == true)
                    {
                        pctBoxSingle.Image = Properties.Resources.green_mouse270;
                        pictureBox2.Image = Properties.Resources.purple_mouse270;
                        pictureBox3.Image = Properties.Resources.red_mouse270;
                    }
                }
                else if (Donnees_publics.HasWin == 4)
                {
                    pctBoxSingle.Image = Properties.Resources.blue_mouse270;
                    pictureBox2.Image = Properties.Resources.green_mouse270;
                    pictureBox3.Image = Properties.Resources.purple_mouse270;
                    pictureBox4.Image = Properties.Resources.red_mouse270;
                }
            }
        }

        private void buttonEllipse2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonEllipse1_Click(object sender, EventArgs e)
        {
            Donnees_publics.WantCompetition = true;
            this.Hide();
        }
    }
}
