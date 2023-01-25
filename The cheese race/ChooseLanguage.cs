﻿using System;
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
    public partial class ChooseLanguage : Form
    {
        public ChooseLanguage()
        {
            InitializeComponent();
        }

        private void btnRomana_Click(object sender, EventArgs e)
        {
            //declaration de la nouvelle form, celle des regles en roumain + ouverture de celle ci + fermeture de la form pour choisir la langue
            Form RoRules = new RulesRO();
            RoRules.ShowDialog();
            this.Hide();
        }

        private void btnEnglish_MouseEnter(object sender, EventArgs e)
        {
            btnEnglish.BackgroundImage = Properties.Resources.ENdrapeau;
        }

        private void btnEnglish_MouseLeave(object sender, EventArgs e)
        {
            btnEnglish.BackgroundImage = null;
        }

        private void btnFrancais_MouseEnter(object sender, EventArgs e)
        {
            btnFrancais.BackgroundImage = Properties.Resources.FRdrapeau;
        }

        private void btnFrancais_MouseLeave(object sender, EventArgs e)
        {
            btnFrancais.BackgroundImage = null;
        }

        private void btnRomana_MouseEnter(object sender, EventArgs e)
        {
            btnRomana.BackgroundImage = Properties.Resources.ROdrapeau;
        }

        private void btnRomana_MouseLeave(object sender, EventArgs e)
        {
            btnRomana.BackgroundImage = null;
        }
    }
}
