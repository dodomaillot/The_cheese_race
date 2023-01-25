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
    public partial class RulesRO : Form
    {
        public RulesRO()
        {
            InitializeComponent();
        }

        #region Platou
        private void pctBoxMenu_MouseMove(object sender, MouseEventArgs e)
        {
            lbMenu.Text = "În menu, se poate alege numărul\rde jucători, numele jucătorilor,\rmodurile jocului, și se poate citi\rregulile.";
            lbMenu.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxMenu_MouseLeave(object sender, EventArgs e)
        {
            lbMenu.Text = null;
            lbMenu.BorderStyle = BorderStyle.None;
        }

        private void pctBoxTitle_MouseMove(object sender, MouseEventArgs e)
        {
            lbTitle.Text = "Este titlul jocului, dar din momentul\rîn care nu mai sunt cașcavaluri pe\rplatoul, va fi scris: jocul este gata,\rdar în engleză.";
            lbTitle.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxTitle_MouseLeave(object sender, EventArgs e)
        {
            lbTitle.Text = null;
            lbTitle.BorderStyle = BorderStyle.None;
        }

        private void pctBoxBlue_MouseMove(object sender, MouseEventArgs e)
        {
            lbBlue.Text = "Casa/Punctul de plecare a\rșoarecelui albastru.";
            lbBlue.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxBlue_MouseLeave(object sender, EventArgs e)
        {
            lbBlue.Text = null;
            lbBlue.BorderStyle = BorderStyle.None;
        }

        private void pctBoxGreen_MouseMove(object sender, MouseEventArgs e)
        {
            lbGreen.Text = "Casa/Punctul de plecare a\rșoarecelui verde.";
            lbGreen.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxGreen_MouseLeave(object sender, EventArgs e)
        {
            lbGreen.Text = null;
            lbGreen.BorderStyle = BorderStyle.None;
        }

        private void pctBoxPurple_MouseMove(object sender, MouseEventArgs e)
        {
            lbPurple.Text = "Casa/Punctul de plecare a\rșoarecelui mov.";
            lbPurple.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxPurple_MouseLeave(object sender, EventArgs e)
        {
            lbPurple.Text = null;
            lbPurple.BorderStyle = BorderStyle.None;
        }

        private void pctBoxRed_MouseMove(object sender, MouseEventArgs e)
        {
            lbRed.Text = "Casa/Punctul de plecare a\rșoarecelui roșu.";
            lbRed.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxRed_MouseLeave(object sender, EventArgs e)
        {
            lbRed.Text = null;
            lbRed.BorderStyle = BorderStyle.None;
        }

        private void pctBoxSound_MouseMove(object sender, MouseEventArgs e)
        {
            lbSound.Text = "Se apasă o dată pe el pentru a\rpune jocul pe mut(să nu mai\rfie zgomote atunci când șoarecii\rmănânc un cașcaval sau când se\ranunță câștigătorul).";
            lbSound.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxSound_MouseLeave(object sender, EventArgs e)
        {
            lbSound.Text = null;
            lbSound.BorderStyle = BorderStyle.None;
        }

        private void pctboxChooseMove_MouseMove(object sender, MouseEventArgs e)
        {
            lbChooseMove.Text = "Butoanele care vor permite\rconstrucția secvenței de\racțiune.";
            lbChooseMove.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctboxChooseMove_MouseLeave(object sender, EventArgs e)
        {
            lbChooseMove.Text = null;
            lbChooseMove.BorderStyle = BorderStyle.None;
        }

        private void pctBoxSequence_MouseMove(object sender, MouseEventArgs e)
        {
            lbSequence.Text = "Aici se vor afișa acțiunile \ralese si pe care șoarecele\rle va executa.";
            lbSequence.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxSequence_MouseLeave(object sender, EventArgs e)
        {
            lbSequence.Text = null;
            lbSequence.BorderStyle = BorderStyle.None;
        }

        private void pctBoxPlayers_MouseMove(object sender, MouseEventArgs e)
        {
            lbPlayers.Text = "Numele jucătorilor.";
            lbPlayers.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxPlayers_MouseLeave(object sender, EventArgs e)
        {
            lbPlayers.Text = null;
            lbPlayers.BorderStyle = BorderStyle.None;
        }

        private void pctBoxNbDes_MouseMove(object sender, MouseEventArgs e)
        {
            lbNbDes.Text = "Locul unde se va afișa numărul \rfăcut de zarul.";
            lbNbDes.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxNbDes_MouseLeave(object sender, EventArgs e)
        {
            lbNbDes.Text = null;
            lbNbDes.BorderStyle = BorderStyle.None;
        }

        private void pctBoxError_MouseMove(object sender, MouseEventArgs e)
        {
            lbError.Text = "Aici se vor afișa erorile,\rîn cazul in care apar.";
            lbError.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxError_MouseLeave(object sender, EventArgs e)
        {
            lbError.Text = null;
            lbError.BorderStyle = BorderStyle.None;
        }

        private void pctBoxThrowDice_MouseMove(object sender, MouseEventArgs e)
        {
            lbThrowDice.Text = "Butonul care, când va fi \rapăsat, va arunca sau opri\rzarul.";
            lbThrowDice.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxThrowDice_MouseLeave(object sender, EventArgs e)
        {
            lbThrowDice.Text = null;
            lbThrowDice.BorderStyle = BorderStyle.None;
        }

        private void pctBoxRun_MouseMove(object sender, MouseEventArgs e)
        {
            lbRun.Text = "Butonul care, când va fi \rapăsat, va executa secvența\rde acțiune aleasa.";
            lbRun.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pctBoxRun_MouseLeave(object sender, EventArgs e)
        {
            lbRun.Text = null;
            lbRun.BorderStyle = BorderStyle.None;
        }


        #endregion Platou

        #region Action

        int coteRight = 1, temps = 1, coteLeft = 1, coteBack = 1;
        private void pctBoxTryLeft_Click(object sender, EventArgs e)
        {
            if (coteLeft == 1)
            {
                pctBoxLeft.Image = Properties.Resources.Blue90;
                coteLeft++;
            }
            else if(coteLeft == 2)
            {
                pctBoxLeft.Image = Properties.Resources.Blue180;
                coteLeft++;
            }
            else if (coteLeft == 3)
            {
                pctBoxLeft.Image = Properties.Resources.Blue270;
                coteLeft++;
            }
            else if (coteLeft == 4)
            {
                pctBoxLeft.Image = Properties.Resources.Blue0;
                coteLeft = 1;
            }
        }

        private void pctBoxTryRight_Click(object sender, EventArgs e)
        {
            if (coteRight == 1)
            {
                pctBoxRight.Image = Properties.Resources.Blue270;
                coteRight++;
            }
            else if (coteRight == 2)
            {
                pctBoxRight.Image = Properties.Resources.Blue180;
                coteRight++;
            }
            else if (coteRight == 3)
            {
                pctBoxRight.Image = Properties.Resources.Blue90;
                coteRight++;
            }
            else if (coteRight == 4)
            {
                pctBoxRight.Image = Properties.Resources.Blue0;
                coteRight = 1;
            }
        }

        private void pctBoxTryForward_Click(object sender, EventArgs e)
        {
            pctBoxForward.Image = Properties.Resources.Bluedroit1;
            temps = 1;
            timerForward.Start();
        }

        private void timerForward_Tick(object sender, EventArgs e)
        {
            if(temps == 1)
            {
                pctBoxForward.Image = Properties.Resources.Bluedroit2;
                temps++;
            }
            else
            {
                timerForward.Stop();
            }
        }

        private void pctBoxTryBackward_Click(object sender, EventArgs e)
        {
            if (coteBack == 1)
            {
                pctBoxBackward.Image = Properties.Resources.Blue180;
                coteBack++;
            }
            else if(coteBack == 2)
            {
                pctBoxBackward.Image = Properties.Resources.Blue0;
                coteBack = 1;
            }
        }

        #endregion Action

        #region Mod
        int ExCani = 1, ExTele = 1;

        private void buttonEllipse1_Click(object sender, EventArgs e)
        {
            ExCani = 1;
            pctBoxCaniEx.Image = Properties.Resources.Cani1;
            timerCani.Start();
        }

        private void timerCani_Tick(object sender, EventArgs e)
        {
            if (ExCani == 1)
            {
                pctBoxCaniEx.Image = Properties.Resources.Cani2;
                ExCani++;
            }
            else if (ExCani == 2)
            {
                pctBoxCaniEx.Image = Properties.Resources.Cani3;
                ExCani++;
            }
            else if (ExCani == 3)
            {
                pctBoxCaniEx.Image = Properties.Resources.Cani4;
                ExCani++;
            }
            else if(ExCani==4)
            {
                pctBoxCaniEx.Image = Properties.Resources.Cani5;
                ExCani++;
            }
        }

        private void buttonEllipse2_Click(object sender, EventArgs e)
        {
            ExTele = 1;
            pctBoxTeleport.Image = Properties.Resources.Teleport1;
            timerTeleport.Start();
        }

        private void timerTeleport_Tick(object sender, EventArgs e)
        {
            if(ExTele == 1)
            {
                pctBoxTeleport.Image = Properties.Resources.Teleport2;
                ExTele++;
            }
            else if(ExTele == 2)
            {
                pctBoxTeleport.Image = Properties.Resources.Teleport3;
                ExTele++;
            }
            else if(ExTele == 3)
            {
                pctBoxTeleport.Image = Properties.Resources.Teleport4;
                ExTele++;
            }
            else if(ExTele == 4)
            {
                pctBoxTeleport.Image = Properties.Resources.Teleport5;
                timerTeleport.Stop();
            }
        }
        #endregion Mod

    }
}
