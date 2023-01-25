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
    public partial class Formname : Form
    {
        public Formname()
        {
            InitializeComponent();
        }

        private void btndone_Click(object sender, EventArgs e)
        {
            //les variable public retiennent le noms des joueurs choisit pour les fournir a celui principal si jamais un nom a etait introduit
            if(textBoxpl1.Text != "")
                Donnees_publics.player1 = textBoxpl1.Text + ":";
            if(textBoxpl2.Text != "")
                Donnees_publics.player2 = textBoxpl2.Text + ":";
            if(textBoxpl3.Text != "")
                Donnees_publics.player3 = textBoxpl3.Text + ":";
            if(textBoxpl4.Text != "")
                Donnees_publics.player4 = textBoxpl4.Text + ":";

            //puis fermeture du formulaire
            this.Close();
        }
    }
}
