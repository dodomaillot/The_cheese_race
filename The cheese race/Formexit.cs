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
    public partial class Formexit : Form
    {
        public Formexit()
        {
            InitializeComponent();
        }

        private void btnyes_Click(object sender, EventArgs e)
        {
            //la donne public exit est vrai
            Donnees_publics.exit = true;

            //this, c a dire que cette fenetre se ferme
            this.Close();
        }

        private void btnno_Click(object sender, EventArgs e)
        {
            //this, c a dire que cette fenetre se ferme
            this.Close();
        }

        private void btnyes_MouseMove(object sender, MouseEventArgs e)
        {
            //une petite souris aparrait au dessus du bouton yes lorsque le mouse passe sur le bouton
            Imgyes.Image = Properties.Resources.sourismur;
        }

        private void btnyes_MouseLeave(object sender, EventArgs e)
        {
            //l image disparait d au dessus du bouton yes lorsque le mouse quitte le bouton
            Imgyes.Image = null;
        }

        private void btnno_MouseMove(object sender, MouseEventArgs e)
        {
            //une petite souris aparrait au dessus du bouton no lorsque le mouse passe sur le bouton
            Imgno.Image = Properties.Resources.sourismur;
        }

        private void btnno_MouseLeave(object sender, EventArgs e)
        {
            //l image disparait d au dessus du bouton no lorsque le mouse quitte le bouton
            Imgno.Image = null;
        }
    }
}
