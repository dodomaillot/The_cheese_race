using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using AudioSwitcher.AudioApi.CoreAudio;

namespace The_cheese_race
{
    public partial class FormMain : Form
    {
        //declaration d une variable qui nous permettre de modifier le son
        CoreAudioDevice arriereson;
        public SoundPlayer Son = new SoundPlayer();

        //initialisation d une liste qui permet de regroupe tous les fromages pour que par la suite ce soit plus facile
        PictureBox[] Fromage = new PictureBox[13];

        //initialisation d une liste qui permet de regrouper les 8 nombre de position left and top
        int[] nbpositionLeft = new int[8];
        int[] nbpositionTop = new int[8];

        public FormMain()
        {
            InitializeComponent();

            //creation de la listbox pour la souris bleu
            sourisbleu.Images.Add(Properties.Resources.blue_mouse0);
            sourisbleu.Images.Add(Properties.Resources.blue_mouse90);
            sourisbleu.Images.Add(Properties.Resources.blue_mouse180);
            sourisbleu.Images.Add(Properties.Resources.blue_mouse270);

            //creation de la listbox pour la souris vert
            sourisvert.Images.Add(Properties.Resources.green_mouse0);
            sourisvert.Images.Add(Properties.Resources.green_mouse90);
            sourisvert.Images.Add(Properties.Resources.green_mouse180);
            sourisvert.Images.Add(Properties.Resources.green_mouse270);

            //creation de la listbox pour la souris violet
            sourisviolet.Images.Add(Properties.Resources.purple_mouse0);
            sourisviolet.Images.Add(Properties.Resources.purple_mouse90);
            sourisviolet.Images.Add(Properties.Resources.purple_mouse180);
            sourisviolet.Images.Add(Properties.Resources.purple_mouse270);

            //creation de la listbox pour la souris rouge
            sourisrouge.Images.Add(Properties.Resources.red_mouse0);
            sourisrouge.Images.Add(Properties.Resources.red_mouse90);
            sourisrouge.Images.Add(Properties.Resources.red_mouse180);
            sourisrouge.Images.Add(Properties.Resources.red_mouse270);

            //creation de la liste pour facilite la tache lors de la fonction actionfro
            Fromage[1] = fro1;
            Fromage[2] = fro2;
            Fromage[3] = fro3;
            Fromage[4] = fro4;
            Fromage[5] = fro5;
            Fromage[6] = fro6;
            Fromage[7] = fro7;
            Fromage[8] = fro8;
            Fromage[9] = fro9;
            Fromage[10] = fro10;
            Fromage[11] = fro11;
            Fromage[12] = fro12;

            //creation de la liste qui permet de retenir les 8 nombre de position sur le plateau de jeu
            nbpositionLeft[0] = nbpositionTop[0] = 0;
            nbpositionLeft[1] = 88; nbpositionTop[1] = 85;
            nbpositionLeft[2] = 176; nbpositionTop[2] = 170;
            nbpositionLeft[3] = 264; nbpositionTop[3] = 255;
            nbpositionLeft[4] = 352; nbpositionTop[4] = 340;
            nbpositionLeft[5] = 440; nbpositionTop[5] = 425;
            nbpositionLeft[6] = 528; nbpositionTop[6] = 510;
            nbpositionLeft[7] = 616; nbpositionTop[7] = 595;

            //initialisation de timer pour les mouvement qui vont etre executer par chaque souris
            timersourisbleu.Tick += new EventHandler(movesourisbleu);
            timersourisvert.Tick += new EventHandler(movesourisvert);
            timersourisviolet.Tick += new EventHandler(movesourisviolet);
            timersourisrouge.Tick += new EventHandler(movesourisrouge);

            //initialisation d un evenement pour le timer: timerdice
            timerdice.Tick += new EventHandler(changenb);

            //On met le volume initial (quand l appli demarre) a 30
            arriereson = new CoreAudioController().DefaultPlaybackDevice;
            arriereson.Volume = 30;

            /*Son = new SoundPlayer();
            Son.Stream = Properties.Resources.musicjeu;
            Son.Play();*/
            
        }

        //initialisation de deux varible qui vont retenir la position x et y de la souris + l angle qu elle a
        int cordx = 0, cordy = 0;
        int angle = 0;

        //initialisation d une variable pour avoir si la musique est en marche + une variable Vol pour savoir le volume de l arriere plan
        bool playson = true;
        int Vol = 30;

        //declaration d un second formulaire, celui pour changer les noms + celui pour quitter + celui pour choisir la langue des regles
        Form formname = new Formname();
        Form formexit = new Formexit();
        Form formchooselang = new ChooseLanguage();
        Form annoncevainqueur = new AnnonceVainqueur();

        // player = a qui est le tour
        int nbjoueurs = 0, nbfro = 12, nbmouve = 0, player = 1, frobleu = 0, frovert = 0, froviolet = 0, frorouge = 0;

        //Etant donner que je n arrive pas a comparer deux image, je donne un angle specifique pour une image specifique
        int inclibleu = 0, inclivert = 180, incliviolet = 0, inclirouge = 180;

        //Etant donner que je n arrive pas a comparer deux image, je donne un chiffre specifique a chaque mouvement (ex: gauche=1, droite=2, etc) 
        int mouvement1 = 0, mouvement2 = 0, mouvement3 = 0, mouvement4 = 0;

        //savoir si un nb de joueur a ete choisi pour ne pas le changer(nb de joueur) pendant la parti + Savoir si une action a etait executer pour ne pas modifier les mode de jeu en pleine partie
        bool gameon = false;
        bool firstactdid = false;

        //la sequence est en cours d execution
        bool execcours = false;

        //initialisation du temps d affichage des erreur
        int temps = 3;

        //initialisation d un bool pour ne pas permettre au joueur de lancer le des autant de fois qu il veut + savoir si le des est en cours de lancement(quand les image arrete pas de s interchanger)
        bool nextplayer = true;
        bool DiceEnCour = false;

        bool ButonPeutAp = true;

        //initialisation du des grace a la fonction random pour obtenir un chiffre aleatoire
        Random nbdes = new Random();
        int nbdice;

        //initialisation d une variable qui nous permettra de savoir qu elle erreur c
        int erreur = 0;

        //initialisation de variable pour les modes extra
        bool secmode = false;
        bool canimode = false;
        bool teleportmode = false;

        private void ToolStripMenuItem2Players_Click(object sender, EventArgs e)
        {
            if (gameon == false && ToolStripMenuItem2Players.Checked ==false && ToolStripMenuItem2Players.Checked == false && ToolStripMenuItem2Players.Checked == false)
            {
                //mise en place des souris sur leur cases depart pour 2joueurs
                bluemouse.Image = Properties.Resources.blue_mouse0;
                bluemouse.Location = new System.Drawing.Point(0, 0);
                greenmouse.Image = Properties.Resources.green_mouse180;
                greenmouse.Location = new System.Drawing.Point(616, 0);
                nbjoueurs = 2;

                //les players 3 et 4 sont retirer du plan visuel
                lbpl3.Text = null;
                lbpl4.Text = null;

                //gameon est vrai, c a dire:la parti a commence
                gameon = true;

                ToolStripMenuItem2Players.Checked = true;
            }
        }

        private void ToolStripMenuItem3Players_Click(object sender, EventArgs e)
        {
            if (gameon == false && ToolStripMenuItem2Players.Checked == false && ToolStripMenuItem2Players.Checked == false && ToolStripMenuItem2Players.Checked == false)
            {
                //mise en place des souris sur leur cases depart pour 3joueurs
                bluemouse.Image = Properties.Resources.blue_mouse0;
                bluemouse.Location = new System.Drawing.Point(0, 0);
                greenmouse.Image = Properties.Resources.green_mouse180;
                greenmouse.Location = new System.Drawing.Point(616, 0);
                purplemouse.Image = Properties.Resources.purple_mouse0;
                purplemouse.Location = new System.Drawing.Point(0, 595);
                nbjoueurs = 3;

                //le player 4 est retirer du plan visuel
                lbpl4.Text = null;

                //gameon est vrai, c a dire:la parti a commence
                gameon = true;

                ToolStripMenuItem3Players.Checked = true;
            }
        }

        private void ToolStripMenuItem4Players_Click(object sender, EventArgs e)
        {
            if (gameon == false && ToolStripMenuItem2Players.Checked == false && ToolStripMenuItem2Players.Checked == false && ToolStripMenuItem2Players.Checked == false)
            {
                //mise en place des souris sur leur cases depart pour 4joueurs
                bluemouse.Image = Properties.Resources.blue_mouse0;
                bluemouse.Location = new System.Drawing.Point(0, 0);
                greenmouse.Image = Properties.Resources.green_mouse180;
                greenmouse.Location = new System.Drawing.Point(616, 0);
                purplemouse.Image = Properties.Resources.purple_mouse0;
                purplemouse.Location = new System.Drawing.Point(0, 595);
                redmouse.Image = Properties.Resources.red_mouse180;
                redmouse.Location = new System.Drawing.Point(616, 595);
                nbjoueurs = 4;

                //gameon est vrai, c a dire: la parti a commence
                gameon = true;

                ToolStripMenuItem4Players.Checked = true;
            }
        }

        private void ToolStripMenuItemNewGame_Click(object sender, EventArgs e)
        {
            //remise des compteurs a 0
            //les images des souris sont retirer
            bluemouse.Image = null;
            greenmouse.Image = null;
            purplemouse.Image = null;
            redmouse.Image = null;

            //les compteurs de fromage de chaque souris sont remis a zero
            frobleu = 0;
            frovert = 0;
            froviolet = 0;
            frorouge = 0;

            //le des s arrete et se remet a zero si jamais il etait lance
            btndice.Text = "Throw the dice";
            dicechoise.Image = null;
            timerdice.Stop();

            //nb de fromage et de joueur remis a zero + le joueur a qui est le tour revient a 1, c-a-dire la souris bleu
            nbfro = 12;
            nbjoueurs = 0;
            player = 1;

            //les inclinaison reviennent comme elles etaient au depart
            inclibleu = 0;
            inclivert = 180;
            incliviolet = 0;
            inclirouge = 180;

            //les fleche de la sequance sont retirer
            move1.Image = null;
            move2.Image = null;
            move3.Image = null;
            move4.Image = null;
            nbmouve = 0;

            //on remet les noms "player" a leur place
            lbpl1.Text = "Player 1:";
            lbpl2.Text = "Player 2:";
            lbpl3.Text = "Player 3:";
            lbpl4.Text = "Player 4:";
            lbGOGO.Text = null;
            ToolStripMenuItem2Players.Checked = false;
            ToolStripMenuItem3Players.Checked = false;
            ToolStripMenuItem4Players.Checked = false;

            //les "donnee public" sont remise a zero
            Donnees_publics.player1 = "Player 1:";
            Donnees_publics.player2 = "Player 2:";
            Donnees_publics.player3 = "Player 3:";
            Donnees_publics.player4 = "Player 4:";
            Donnees_publics.exit = false;
            Donnees_publics.Many = false;
            Donnees_publics.WantCompetition = false;
            Donnees_publics.blueWin = false;
            Donnees_publics.greenWin = false;
            Donnees_publics.purpleWin = false;
            Donnees_publics.redWin = false;
            Donnees_publics.HasWin = 1;

            //le des peux a nouveau etre lancer
            nextplayer = true;

            //la parti n est plus en cours + aucune action n a ete faite + la sequence d execution de mouvements n est plus en cours d execution
            gameon = false;
            firstactdid = false;
            execcours = false;

            //Au cas ou il n y avait plus de fromage sur le plateau e que le titre a ete remplace par le jeu est fini en anglais
            lbTitre.Text = "T\rH\rE\r\rC\rH\rE\rE\rS\rE\r\rR\rA\rC\rE";

            //Les fromages reapparaissent et sont amener en premier plan
            for (int i = 1; i <= 12; i++)
            {
                Fromage[i].Image = Properties.Resources.fromage;
                Fromage[i].BringToFront();
            }

            //si jamais le mode seconde chance a ete active, alors le bouton de rechoisir les mouvements disparait
            secmode = false;
            ToolStripMenuItemSecondChance.Checked = false;
            resetmouve.Image = null;
            resetmouve.BorderStyle = BorderStyle.None;

            //si jamais le mode cannibale a ete active
            canimode = false;
            ToolStripMenuItemCannibal.Checked = false;

            //si jamais le mode teleporting food a ete active, les fromages reprennent leurs places initales
            teleportmode = false;
            ToolStripMenuItemTeleportingFood.Checked = false;
            Fromage[1].Left = 176; Fromage[1].Top = 85;
            Fromage[2].Left = 352; Fromage[2].Top = 85;
            Fromage[3].Left = 0; Fromage[3].Top = 170;
            Fromage[4].Left = 616; Fromage[4].Top = 170;
            Fromage[5].Left = 264; Fromage[5].Top = 255;
            Fromage[6].Left = 440; Fromage[6].Top = 255;
            Fromage[7].Left = 0; Fromage[7].Top = 340;
            Fromage[8].Left = 616; Fromage[8].Top = 340;
            Fromage[9].Left = 176; Fromage[9].Top = 425;
            Fromage[10].Left = 352; Fromage[10].Top = 425;
            Fromage[11].Left = 440; Fromage[11].Top = 510;
            Fromage[12].Left = 264; Fromage[12].Top = 595;

            //le son est reactive
            arriereson.Mute(false);
        }

        private void ToolStripMenuItemPlayersName_Click(object sender, EventArgs e)
        {
            //dire que le formulaire "parent" du formulaire des noms est celui la(FormMain) + affichage de celui ci
            formname.Owner = this;
            formname.ShowDialog();

            //changement des noms apres fermeture du formulaire nom
            lbpl1.Text = Donnees_publics.player1;
            lbpl2.Text = Donnees_publics.player2;
            lbpl3.Text = Donnees_publics.player3;
            lbpl4.Text = Donnees_publics.player4;

            if(gameon == true)
            {
                if (nbjoueurs == 2)
                {
                    lbpl3.Text = null;
                    lbpl4.Text = null;
                    lbpl1.Text = Donnees_publics.player1 + frobleu;
                    lbpl2.Text = Donnees_publics.player2 + frovert;
                }
                else if (nbjoueurs == 3)
                {
                    lbpl4.Text = null;
                    lbpl1.Text = Donnees_publics.player1 + frobleu;
                    lbpl2.Text = Donnees_publics.player2 + frovert;
                    lbpl3.Text = Donnees_publics.player3 + froviolet;
                }
                else
                {
                    lbpl1.Text = Donnees_publics.player1 + frobleu;
                    lbpl2.Text = Donnees_publics.player2 + frovert;
                    lbpl3.Text = Donnees_publics.player3 + froviolet;
                    lbpl4.Text = Donnees_publics.player4 + frorouge;
                }
            }
        }

        private void theRulesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //le formulaire "parent" du formulaire pour choisir la langue des regles + ouverture de celle ci + fermeture de la form principale
            formchooselang.Owner = this;
            formchooselang.ShowDialog();
        }

        private void ToolStripMenuItemSecondChance_Click(object sender, EventArgs e)
        {
            //Si une action a ete effectuer, alors le(s) mode(s) de jeu ne peuvent plus etre modifie
            if (firstactdid == false && ToolStripMenuItemSecondChance.Checked == false)
            {
                //le mode seconde chance a ete activer
                secmode = true;
                ToolStripMenuItemSecondChance.Checked = true;
                resetmouve.Image = Properties.Resources.reset;
                resetmouve.BorderStyle = BorderStyle.Fixed3D;
            }
            else if(firstactdid == false && ToolStripMenuItemSecondChance.Checked == true)
            {
                //le mode seconde chance a ete desactive
                secmode = false;
                ToolStripMenuItemSecondChance.Checked = false;
                resetmouve.Image = null;
                resetmouve.BorderStyle = BorderStyle.None;
            }
        }

        private void ToolStripMenuItemCannibal_Click(object sender, EventArgs e)
        {
            //Si une action a ete effectuer, alors le(s) mode(s) de jeu ne peuvent plus etre modifie
            if(firstactdid == false && ToolStripMenuItemCannibal.Checked == false)
            {
                //le mode cannibale a ete active
                canimode = true;
                ToolStripMenuItemCannibal.Checked = true;
            }
            else if(firstactdid == false && ToolStripMenuItemCannibal.Checked == true)
            {
                //le mode cannibale a ete desactive
                canimode = false;
                ToolStripMenuItemCannibal.Checked = false;
            }
        }

        private void ToolStripMenuItemTeleportingFood_Click(object sender, EventArgs e)
        {
            //Si une action a ete effectuer, alors le(s) mode(s) de jeu ne peuvent plus etre modifie
            if (firstactdid == false && ToolStripMenuItemTeleportingFood.Checked == false)
            {
                //le mode teleportation a ete active
                teleportmode = true;
                ToolStripMenuItemTeleportingFood.Checked = true;
            }
            else if(firstactdid == false && ToolStripMenuItemTeleportingFood.Checked == true)
            {
                //le mode telportation a ete desactive
                teleportmode = false;
                ToolStripMenuItemTeleportingFood.Checked = false;
            }
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            //dire que le formulaire "parent" du formulaire de confirmation  est celui la(c a dire: le FormMain) + affichage de celui ci
            formexit.Owner = this;
            formexit.ShowDialog();

            //si la reponse est oui, alors l appli se ferme, sinon non
            if (Donnees_publics.exit == true)
                Application.Exit();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //si la variable exit est egal a false(faux), montrer le panneau de confirmation de sorti
            if (Donnees_publics.exit == false)
                formexit.ShowDialog();

            //si l utilisateur a choisi la reponde yes(oui), alors tout se ferme
            if (Donnees_publics.exit == true)
                Application.Exit();
            else if (Donnees_publics.exit == false)
                e.Cancel = true;
        }

        private void btndice_Click(object sender, EventArgs e)
        {
            if (nbjoueurs >= 2 && nbjoueurs <= 4 && gameon == true)
            {
                if (btndice.Text == "Throw the dice" && nextplayer == true)
                {
                    //une action a ete executer => les mode de jeu ne peuvent plus etre changer
                    firstactdid = true;

                    //demarage du timer pour le changement d image du des
                    timerdice.Start();
                    btndice.Text = "Stop the dice";
                    DiceEnCour = true;

                    if (execcours == true)
                        erreur = 5;
                }
                else
                {
                    //le timer "timerdice" s arrete
                    timerdice.Stop();

                    //Le if sert a faire en sorte que meme si le bouton "Throw the dice" est appuye, l image du nb fait ne change pas. De meme pour la valeur de nbdice
                    if (nextplayer == true)
                    {
                        //nbdice prend un nb aleatoire grace a la fonction random + le des n est plus en cours de lancement
                        nbdice = nbdes.Next(1, 5);
                        btndice.Text = "Throw the dice";
                        DiceEnCour = false;

                        //determination du chiffre fais par le des et affichage de celui ci
                        if (nbdice % 5 == 1)
                            dicechoise.Image = Properties.Resources.un;
                        else if (nbdice % 5 == 2)
                            dicechoise.Image = Properties.Resources.deux;
                        else if (nbdice % 5 == 3)
                            dicechoise.Image = Properties.Resources.trois;
                        else if (nbdice % 5 == 4)
                            dicechoise.Image = Properties.Resources.quatre;

                        ButonPeutAp = true;
                    }
                    else
                    {
                        //determination de l erreur + le timer "timererror" commence
                        erreur = 1;
                        timererror.Start();
                    }

                    //le des ne peux plus etre relancer
                    nextplayer = false;
                }
            }
        }

        private void changenb(object sender, EventArgs e)
        {
            //programmation du timer pour swapper 2 images en un intervalle de temps
            List<Bitmap> change = new List<Bitmap>();
            change.Add(Properties.Resources.pnt_intero);
            change.Add(Properties.Resources.pnt_excla);
            int index = DateTime.Now.Millisecond % 2;
            dicechoise.Image = change[index];
        }

        private void gauche_Click(object sender, EventArgs e)
        {
            //determination nb joueur pour savoir si la partie a commencer
            if (gameon == true && DiceEnCour == false && nbdice != 0)
            {
                //mise en place de la sequence en fonction de la direction choisi
                if (move1.Image == null && nbmouve < nbdice)
                {
                    move1.Image = Properties.Resources.gauche;
                    mouvement1 = 1;
                    nbmouve++;
                }
                else if (move2.Image == null && nbmouve < nbdice)
                {
                    move2.Image = Properties.Resources.gauche;
                    mouvement2 = 1;
                    nbmouve++;
                }
                else if (move3.Image == null && nbmouve < nbdice)
                {
                    move3.Image = Properties.Resources.gauche;
                    mouvement3 = 1;
                    nbmouve++;
                }
                else if (move4.Image == null && nbmouve < nbdice)
                {
                    move4.Image = Properties.Resources.gauche;
                    mouvement4 = 1;
                    nbmouve++;
                }
            }
        }

        private void droite_Click(object sender, EventArgs e)
        {
            //determination nb joueur pour savoir si la partie a commencer
            if (gameon == true && DiceEnCour == false && nbdice != 0)
            {
                //mise en place de la sequence en fonction de la direction choisi
                if (move1.Image == null && nbmouve < nbdice)
                {
                    move1.Image = Properties.Resources.droite;
                    mouvement1 = 2;
                    nbmouve++;
                }
                else if (move2.Image == null && nbmouve < nbdice)
                {
                    move2.Image = Properties.Resources.droite;
                    mouvement2 = 2;
                    nbmouve++;
                }
                else if (move3.Image == null && nbmouve < nbdice)
                {
                    move3.Image = Properties.Resources.droite;
                    mouvement3 = 2;
                    nbmouve++;
                }
                else if (move4.Image == null && nbmouve < nbdice)
                {
                    move4.Image = Properties.Resources.droite;
                    mouvement4 = 2;
                    nbmouve++;
                }
            }
        }

        private void ttdroit_Click(object sender, EventArgs e)
        {
            //determination nb joueur pour savoir si la partie a commencer
            if (gameon == true && DiceEnCour == false && nbdice != 0)
            {
                //mise en place de la sequence en fonction de la direction choisi
                if (move1.Image == null && nbmouve < nbdice)
                {
                    move1.Image = Properties.Resources.ttdroit;
                    mouvement1 = 3;
                    nbmouve++;
                }
                else if (move2.Image == null && nbmouve < nbdice)
                {
                    move2.Image = Properties.Resources.ttdroit;
                    mouvement2 = 3;
                    nbmouve++;
                }
                else if (move3.Image == null && nbmouve < nbdice)
                {
                    move3.Image = Properties.Resources.ttdroit;
                    mouvement3 = 3;
                    nbmouve++;
                }
                else if (move4.Image == null && nbmouve < nbdice)
                {
                    move4.Image = Properties.Resources.ttdroit;
                    mouvement4 = 3;
                    nbmouve++;
                }
            }
        }

        private void arriere_Click(object sender, EventArgs e)
        {
            //determination nb joueur pour savoir si la partie a commencer
            if (gameon == true && DiceEnCour == false && nbdice != 0)
            {
                //mise en place de la sequence en fonction de la direction choisi
                if (move1.Image == null && nbmouve < nbdice)
                {
                    move1.Image = Properties.Resources.arriere;
                    mouvement1 = 4;
                    nbmouve++;
                }
                else if (move2.Image == null && nbmouve < nbdice)
                {
                    move2.Image = Properties.Resources.arriere;
                    mouvement2 = 4;
                    nbmouve++;
                }
                else if (move3.Image == null && nbmouve < nbdice)
                {
                    move3.Image = Properties.Resources.arriere;
                    mouvement3 = 4;
                    nbmouve++;
                }
                else if (move4.Image == null && nbmouve < nbdice)
                {
                    move4.Image = Properties.Resources.arriere;
                    mouvement4 = 4;
                    nbmouve++;
                }
            }
        }

        private void resetmouve_Click(object sender, EventArgs e)
        {
            if(secmode == true)
            {
                //si le mode second chance a ete active et si l'execution de la sequence n'est pas en cours, alors la sequence peut etre modifier
                if (secmode == true && execcours == false)
                {
                    nbmouve = 0;

                    move1.Image = null;
                    move2.Image = null;
                    move3.Image = null;
                    move4.Image = null;

                    mouvement1 = 0;
                    mouvement2 = 0;
                    mouvement3 = 0;
                    mouvement4 = 0;
                }
                //si l'execution est en ccours et le bouton reset et appuyer, alors l'erreur 5 s affiche a l ecran
                else if (execcours == true)
                {
                    erreur = 5;
                    timererror.Start();
                }
            }
        }

        private void btnRUN_Click(object sender, EventArgs e)
        {
            if (gameon == true)
            {
                if (nbjoueurs >= 2 && nbjoueurs <= 4 && nbmouve == nbdice && DiceEnCour == false && firstactdid == true && ButonPeutAp == true)
                {
                    //l'execution de la sequence est en cours
                    execcours = true;

                    if (player == 1)
                    {
                        //on retient la position de depart de la souris bleu au cas ou elle sortirai du plateau + son inclinaison
                        cordx = bluemouse.Left;
                        cordy = bluemouse.Top;
                        angle = inclibleu;

                        timersourisbleu.Start();
                    }
                    else if (player == 2)
                    {
                        //on retient la position de depart de la souris verte au cas ou elle sortirai du plateau + son inclinaison
                        cordx = greenmouse.Left;
                        cordy = greenmouse.Top;
                        angle = inclivert;

                        timersourisvert.Start();
                    }
                    else if (player == 3)
                    {
                        //on retient la position de depart de la souris violette au cas ou elle sortirai du plateau + son inclinaison
                        cordx = purplemouse.Left;
                        cordy = purplemouse.Top;
                        angle = incliviolet;

                        timersourisviolet.Start();
                    }
                    else if (player == 4)
                    {
                        //on retient la position de depart de la souris rouge au cas ou elle sortirai du plateau + son inclinaison
                        cordx = redmouse.Left;
                        cordy = redmouse.Top;
                        angle = inclirouge;

                        timersourisrouge.Start();
                    }
                    nextplayer = false;
                }
                else
                {
                    //determination de l erreur plus affichage de celle ci
                    if (DiceEnCour == true)
                        erreur = 2;
                    else if (nbmouve != nbdice)
                        erreur = 3;
                    
                    timererror.Start();
                }
            }
        }

        private void movesourisbleu(object sender, EventArgs e)
        {
            if (nbmouve != 0)
            {
                //c le tour de la souris bleu, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
                bluemouse.BringToFront();

                //si le mouvement 1 a deja etait execute ou pas
                if (move1.Image != null)
                {
                    //determination du premier mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement1 == 1)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 2)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 3)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Left += 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Top -= 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Left -= 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Top += 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 4)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 2 a deja etait execute ou pas
                else if (move2.Image != null)
                {
                    //determination du deuxieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement2 == 1)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 2)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 3)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Left += 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Top -= 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Left -= 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Top += 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 4)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 3 a deja etait execute ou pas
                else if (move3.Image != null)
                {
                    //determination du troisieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement3 == 1)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 2)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 3)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Left += 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Top -= 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Left -= 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Top += 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 4)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 4 a deja etait execute ou pas
                else if (move4.Image != null)
                {
                    //determination du quatrieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement4 == 1)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 2)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 3)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Left += 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Top -= 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Left -= 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Top += 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 4)
                    {
                        if (inclibleu == 0)
                        {
                            bluemouse.Image = sourisbleu.Images[2];
                            inclibleu = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 90)
                        {
                            bluemouse.Image = sourisbleu.Images[3];
                            inclibleu = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 180)
                        {
                            bluemouse.Image = sourisbleu.Images[0];
                            inclibleu = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclibleu == 270)
                        {
                            bluemouse.Image = sourisbleu.Images[1];
                            inclibleu = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                }
            }
            if (nbmouve == 0)
            {
                //si jamais la souris a fait une sorti de map a la fin de la sequence, alors celle ci est remise a sa position qu elle aviat avant la sequence et l'erreur 4 s affiche
                if (bluemouse.Left < 0 || bluemouse.Left > 616 || bluemouse.Top < 0 || bluemouse.Top > 595)
                {
                    erreur = 4;
                    bluemouse.Left = cordx;
                    bluemouse.Top = cordy;
                    inclibleu = angle;
                    nbmouve = 0;
                    switch (angle)
                    {
                        case 0:
                            bluemouse.Image = sourisbleu.Images[0];
                            break;
                        case 90:
                            bluemouse.Image = sourisbleu.Images[1];
                            break;
                        case 180:
                            bluemouse.Image = sourisbleu.Images[2];
                            break;
                        case 270:
                            bluemouse.Image = sourisbleu.Images[3];
                            break;
                    }
                    execcours = false;
                    nbdice = 0;
                    timersourisbleu.Stop();
                    ButonPeutAp = false;
                    timererror.Start();
                }
                else if(Donnees_publics.WantCompetition == true && Donnees_publics.blueWin == true)
                {
                    nbdice = 0;
                    ButonPeutAp = false;
                    timersourisbleu.Stop();
                    WantComp();
                }
                else
                {
                    timersourisbleu.Stop();
                    ButonPeutAp = false;
                    nbdice = 0;
                    ActionFroCani();
                    execcours = false;
                    player = 2;
                    nextplayer = true;
                    dicechoise.Image = null;

                    //c le tour de la souris verta, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
                    greenmouse.BringToFront();
                }
            }

        }

        private void movesourisvert(object sender, EventArgs e)
        {
            if (nbmouve != 0)
            {
                //si le mouvement 1 a deja etait execute ou pas
                if (move1.Image != null)
                {
                    //determination du premier mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement1 == 1)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 2)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 3)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Left += 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Top -= 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Left -= 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Top += 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 4)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 2 a deja etait execute ou pas
                else if (move2.Image != null)
                {
                    //determination du deuxieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement2 == 1)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 2)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 3)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Left += 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Top -= 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Left -= 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Top += 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 4)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 3 a deja etait execute ou pas
                else if (move3.Image != null)
                {
                    //determination du troisieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement3 == 1)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 2)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 3)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Left += 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Top -= 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Left -= 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Top += 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 4)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 4 a deja etait execute ou pas
                else if (move4.Image != null)
                {
                    //determination du quatrieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement4 == 1)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 2)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 3)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Left += 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Top -= 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Left -= 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Top += 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 4)
                    {
                        if (inclivert == 0)
                        {
                            greenmouse.Image = sourisvert.Images[2];
                            inclivert = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 90)
                        {
                            greenmouse.Image = sourisvert.Images[3];
                            inclivert = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 180)
                        {
                            greenmouse.Image = sourisvert.Images[0];
                            inclivert = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclivert == 270)
                        {
                            greenmouse.Image = sourisvert.Images[1];
                            inclivert = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                }
            }
            if (nbmouve == 0)
            {
                //si jamais la souris a fait une sorti de map a la fin de la sequence, alors celle ci est remise a sa position qu elle aviat avant la sequence et l'erreur 4 s affiche
                if (greenmouse.Left < 0 || greenmouse.Left > 616 || greenmouse.Top < 0 || greenmouse.Top > 595)
                {
                    erreur = 4;
                    greenmouse.Left = cordx;
                    greenmouse.Top = cordy;
                    inclivert = angle;
                    nbmouve = 0;
                    switch (angle)
                    {
                        case 0:
                            greenmouse.Image = sourisvert.Images[0];
                            break;
                        case 90:
                            greenmouse.Image = sourisvert.Images[1];
                            break;
                        case 180:
                            greenmouse.Image = sourisvert.Images[2];
                            break;
                        case 270:
                            greenmouse.Image = sourisvert.Images[3];
                            break;
                    }
                    nbdice = 0;
                    execcours = false;
                    ButonPeutAp = false;
                    timererror.Start();
                }
                else if (Donnees_publics.WantCompetition == true && Donnees_publics.greenWin == true)
                {
                    timersourisvert.Stop();
                    nbdice = 0;
                    ButonPeutAp = false;
                    WantComp();
                }
                else
                {
                    if (nbjoueurs >= 3)
                    {
                        timersourisvert.Stop();
                        ButonPeutAp = false;
                        nbdice = 0;
                        ActionFroCani();
                        execcours = false;
                        player = 3;
                        nextplayer = true;
                        dicechoise.Image = null;

                        //c le tour de la souris violet, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
                        purplemouse.BringToFront();
                    }
                    else
                    {
                        timersourisvert.Stop();
                        ButonPeutAp = false;
                        nbdice = 0;
                        ActionFroCani();
                        execcours = false;
                        player = 1;
                        nextplayer = true;
                        dicechoise.Image = null;

                        //c le tour de la souris bleu, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
                        bluemouse.BringToFront();
                    }
                }
            }
        }

        private void movesourisviolet(object sender, EventArgs e)
        {
            //c le tour de la souris violet, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
            purplemouse.BringToFront();

            if (nbmouve != 0)
            {
                //si le mouvement 1 a deja etait execute ou pas
                if (move1.Image != null)
                {
                    //determination du premier mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement1 == 1)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 2)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 3)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Left += 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Top -= 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Left -= 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Top += 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 4)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 2 a deja etait execute ou pas
                else if (move2.Image != null)
                {
                    //determination du deuxieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement2 == 1)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 2)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 3)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Left += 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Top -= 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Left -= 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Top += 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 4)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 3 a deja etait execute ou pas
                else if (move3.Image != null)
                {
                    //determination du troisieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement3 == 1)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 2)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 3)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Left += 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Top -= 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Left -= 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Top += 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 4)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 4 a deja etait execute ou pas
                else if (move4.Image != null)
                {
                    //determination du quatrieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement4 == 1)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 2)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 3)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Left += 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Top -= 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Left -= 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Top += 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 4)
                    {
                        if (incliviolet == 0)
                        {
                            purplemouse.Image = sourisviolet.Images[2];
                            incliviolet = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 90)
                        {
                            purplemouse.Image = sourisviolet.Images[3];
                            incliviolet = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 180)
                        {
                            purplemouse.Image = sourisviolet.Images[0];
                            incliviolet = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (incliviolet == 270)
                        {
                            purplemouse.Image = sourisviolet.Images[1];
                            incliviolet = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                }
            }
            if (nbmouve == 0)
            {
                //si jamais la souris a fait une sorti de map a la fin de la sequence, alors celle ci est remise a sa position qu elle aviat avant la sequence et l'erreur 4 s affiche
                if (purplemouse.Left < 0 || purplemouse.Left > 616 || purplemouse.Top < 0 || purplemouse.Top > 595)
                {
                    erreur = 4;
                    purplemouse.Left = cordx;
                    purplemouse.Top = cordy;
                    incliviolet = angle;
                    nbmouve = 0;
                    switch (angle)
                    {
                        case 0:
                            purplemouse.Image = sourisviolet.Images[0];
                            break;
                        case 90:
                            purplemouse.Image = sourisviolet.Images[1];
                            break;
                        case 180:
                            purplemouse.Image = sourisviolet.Images[2];
                            break;
                        case 270:
                            purplemouse.Image = sourisviolet.Images[3];
                            break;
                    }
                    execcours = false;
                    nbdice = 0;
                    ButonPeutAp = false;
                    timererror.Start();
                }
                else if (Donnees_publics.WantCompetition == true && Donnees_publics.purpleWin == true)
                {
                    timersourisviolet.Stop();
                    ButonPeutAp = false;
                    nbdice = 0;
                    WantComp();
                }
                else
                {
                    if (nbjoueurs == 4)
                    {
                        timersourisviolet.Stop();
                        ButonPeutAp = false;
                        nbdice = 0;
                        ActionFroCani();
                        execcours = false;
                        player = 4;
                        nextplayer = true;
                        dicechoise.Image = null;

                        //c le tour de la souris rouge, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
                        redmouse.BringToFront();
                    }
                    else
                    {
                        timersourisviolet.Stop();
                        nbdice = 0;
                        ActionFroCani();
                        execcours = false;
                        player = 1;
                        nextplayer = true;
                        dicechoise.Image = null;

                        //c le tour de la souris bleu, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
                        bluemouse.BringToFront();
                    }
                }
            }
            

        }

        private void movesourisrouge(object sender, EventArgs e)
        {
            //c le tour de la souris rouge, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
            redmouse.BringToFront();

            if (nbmouve != 0)
            {
                //si le mouvement 1 a deja etait execute ou pas
                if (move1.Image != null)
                {
                    //determination du premier mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement1 == 1)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 2)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 3)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Left += 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Top -= 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Left -= 88;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Top += 85;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement1 == 4)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move1.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move1.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 2 a deja etait execute ou pas
                else if (move2.Image != null)
                {
                    //determination du deuxieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement2 == 1)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 2)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 3)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Left += 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Top -= 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Left -= 88;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Top += 85;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement2 == 4)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move2.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move2.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 3 a deja etait execute ou pas
                else if (move3.Image != null)
                {
                    //determination du troisieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement3 == 1)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 2)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 3)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Left += 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Top -= 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Left -= 88;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Top += 85;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement3 == 4)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move3.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move3.Image = null;
                            nbmouve--;
                        }
                    }
                }

                //si le mouvement 4 a deja etait execute ou pas
                else if (move4.Image != null)
                {
                    //determination du quatrieme mouvement (si c a droite ou a gauche ou etc) puis execution de celui ci
                    if (mouvement4 == 1)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 2)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 3)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Left += 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Top -= 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Left -= 88;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Top += 85;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                    else if (mouvement4 == 4)
                    {
                        if (inclirouge == 0)
                        {
                            redmouse.Image = sourisrouge.Images[2];
                            inclirouge = 180;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 90)
                        {
                            redmouse.Image = sourisrouge.Images[3];
                            inclirouge = 270;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 180)
                        {
                            redmouse.Image = sourisrouge.Images[0];
                            inclirouge = 0;
                            move4.Image = null;
                            nbmouve--;
                        }
                        else if (inclirouge == 270)
                        {
                            redmouse.Image = sourisrouge.Images[1];
                            inclirouge = 90;
                            move4.Image = null;
                            nbmouve--;
                        }
                    }
                }
            }
            if (nbmouve == 0)
            {
                //si jamais la souris a fait une sorti de map a la fin de la sequence, alors celle ci est remise a sa position qu elle aviat avant la sequence et l'erreur 4 s affiche
                if (redmouse.Left < 0 || redmouse.Left > 616 || redmouse.Top < 0 || redmouse.Top > 595)
                {
                    erreur = 4;
                    redmouse.Left = cordx;
                    redmouse.Top = cordy;
                    inclirouge = angle;
                    nbmouve = 0;
                    switch (angle)
                    {
                        case 0:
                            redmouse.Image = sourisrouge.Images[0];
                            break;
                        case 90:
                            redmouse.Image = sourisrouge.Images[1];
                            break;
                        case 180:
                            redmouse.Image = sourisrouge.Images[2];
                            break;
                        case 270:
                            redmouse.Image = sourisrouge.Images[3];
                            break;
                    }
                    execcours = false;
                    nbdice = 0;
                    ButonPeutAp = false;
                    timererror.Start();
                }
                else if (Donnees_publics.WantCompetition == true && Donnees_publics.redWin == true)
                {
                    timersourisrouge.Stop();
                    ButonPeutAp = false;
                    nbdice = 0;
                    WantComp();
                }
                else
                {
                    timersourisrouge.Stop();
                    ButonPeutAp = false;
                    nbdice = 0;
                    ActionFroCani();
                    execcours = false;
                    player = 1;
                    nextplayer = true;
                    dicechoise.Image = null;

                    //c le tour de la souris bleu, donc ce sera elle qui sera en premier plan(c celle qui sera visible si jamais deux picture box se superpose)
                    bluemouse.BringToFront();
                }
            }
        }

        private void ActionFroCani()
        {
            //on verifie si la souris est tomber sur un fromage, si oui, alors on lui rajoute un point et le fromage disparrait

            //initialisation d une variable qui permettra de savoir si la souris c est arrete sur un fromage
            bool etresurfro = false;

            for (int i = 1; i <= 12; i++)
            {
                if (Fromage[i].Top == bluemouse.Top && Fromage[i].Left == bluemouse.Left && Fromage[i].Image != null && player == 1)
                {
                    Fromage[i].Image = null;
                    nbfro--;
                    frobleu++;
                    lbpl1.Text = Donnees_publics.player1 + " " + Convert.ToString(frobleu);
                    etresurfro = true;
                    if (teleportmode == true)
                    {
                        TouchTeleport(a);
                        a = 1;
                    }
                    break;
                }
                else if (Fromage[i].Top == greenmouse.Top && Fromage[i].Left == greenmouse.Left && Fromage[i].Image != null && player == 2)
                {
                    Fromage[i].Image = null;
                    nbfro--;
                    frovert++;
                    lbpl2.Text = Donnees_publics.player2 + " " + Convert.ToString(frovert);
                    etresurfro = true;
                    if (teleportmode == true)
                    {
                        TouchTeleport(a);
                        a = 1;
                    }
                    break;
                }
                else if (Fromage[i].Top == purplemouse.Top && Fromage[i].Left == purplemouse.Left && Fromage[i].Image != null && player == 3)
                {
                    Fromage[i].Image = null;
                    nbfro--;
                    froviolet++;
                    lbpl3.Text = Donnees_publics.player3 + " " + Convert.ToString(froviolet);
                    etresurfro = true;
                    if (teleportmode == true)
                    {
                        TouchTeleport(a);
                        a = 1;
                    }
                    break;
                }
                else if (Fromage[i].Top == redmouse.Top && Fromage[i].Left == redmouse.Left && Fromage[i].Image != null && player == 4)
                {
                    Fromage[i].Image = null;
                    nbfro--;
                    frorouge++;
                    lbpl4.Text = Donnees_publics.player4 + " " + Convert.ToString(frorouge);
                    etresurfro = true;
                    if (teleportmode == true)
                    {
                        a = 1;
                        TouchTeleport(a);
                    }
                    break;
                }
            }

            if(canimode == true && etresurfro == false)
            {
                //si le mode cannibal a ete active, alors on verifie si on est pas tombe sur une souris, si si, alors la souris touche retourne a sa case depart
                if(player == 1)
                {
                    if (bluemouse.Top == greenmouse.Top && bluemouse.Left == greenmouse.Left)
                    {
                        greenmouse.Top = 0;
                        greenmouse.Left = 616;
                    }
                    else if(bluemouse.Top == purplemouse.Top && bluemouse.Left == purplemouse.Left)
                    {
                        purplemouse.Top = 595;
                        purplemouse.Left = 0;
                    }
                    else if(bluemouse.Top == redmouse.Top && bluemouse.Left == redmouse.Left)
                    {
                        redmouse.Top = 595;
                        redmouse.Left = 616;
                    }
                }
                else if(player == 2)
                {
                    if(greenmouse.Top == bluemouse.Top && greenmouse.Left == bluemouse.Left)
                    {
                        bluemouse.Top = 0;
                        bluemouse.Left = 0;
                    }
                    else if(greenmouse.Top == purplemouse.Top && greenmouse.Left == purplemouse.Left)
                    {
                        purplemouse.Top = 595;
                        purplemouse.Left = 0;
                    }
                    else if(greenmouse.Top == redmouse.Top && greenmouse.Left == redmouse.Left)
                    {
                        redmouse.Top = 595;
                        redmouse.Left = 616;
                    }
                }
                else if(player == 3)
                {
                    if(purplemouse.Top == bluemouse.Top && purplemouse.Left == bluemouse.Left)
                    {
                        bluemouse.Top = 0;
                        bluemouse.Left = 0;
                    }
                    else if(purplemouse.Top == greenmouse.Top && purplemouse.Left == greenmouse.Left)
                    {
                        greenmouse.Top = 0;
                        greenmouse.Left = 616;
                    }
                    else if(purplemouse.Top == redmouse.Top && purplemouse.Left == redmouse.Left)
                    {
                        redmouse.Top = 595;
                        redmouse.Left = 616;
                    }
                }
                else if(player == 4)
                {
                    if (redmouse.Top == bluemouse.Top && redmouse.Left == bluemouse.Left)
                    {
                        bluemouse.Top = 0;
                        bluemouse.Left = 0;
                    }
                    else if (redmouse.Top == greenmouse.Top && redmouse.Left == greenmouse.Left)
                    {
                        greenmouse.Top = 0;
                        greenmouse.Left = 616;
                    }
                    else if (redmouse.Top == purplemouse.Top && redmouse.Left == purplemouse.Left)
                    {
                        purplemouse.Top = 595;
                        purplemouse.Left = 0;
                    }
                }
            }

            //si il n y a plus de fromage, alors le jeu dit "fini"
            if (nbfro == 0)
            {
                lbTitre.Text = "T\rH\rE\r\rG\rA\rM\rE\r\rI\rS\r\rO\rV\rE\rR";
                WhoWin();
                Son.Stream = Properties.Resources.Roulements_de_tambour;
                Son.Play();
                gameon = false;
                timerTambour.Start();
            }
        }

        private void WhoWin()
        {
            if(nbjoueurs == 2)
            {
                if(frobleu == frovert)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.greenWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;

                }
                else if(frobleu > frovert)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.HasWin = 1;
                }
                else if(frovert > frobleu)
                {
                    Donnees_publics.greenWin = true;
                    Donnees_publics.HasWin = 1;
                }
            }
            else if(nbjoueurs == 3)
            {
                if (frobleu == frovert && frobleu == froviolet)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.greenWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 3;
                    Donnees_publics.Many = true;
                }
                else if(frobleu == frovert)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.greenWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if(frobleu == froviolet)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (frovert == froviolet)
                {
                    Donnees_publics.greenWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (frobleu > frovert && frobleu > froviolet)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.HasWin = 1;
                }
                else if (frovert > frobleu && frovert > froviolet)
                {
                    Donnees_publics.greenWin = true;
                    Donnees_publics.HasWin = 1;
                }
                else if (froviolet > frobleu && froviolet > frovert)
                {
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 1;
                }
            }
            else if(nbjoueurs == 4)
            {
                if(frobleu == frovert && frobleu == froviolet && frobleu == frorouge)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.greenWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 4;
                    Donnees_publics.Many = true;
                }
                else if (frobleu == frovert && frobleu == froviolet)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.greenWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 3;
                    Donnees_publics.Many = true;
                }
                else if (frobleu == frovert && frobleu == frorouge)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.greenWin = true;
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 3;
                    Donnees_publics.Many = true;
                }
                else if (frobleu == froviolet && frobleu == frorouge)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 3;
                    Donnees_publics.Many = true;
                }
                else if (frovert == froviolet && frovert == frorouge)
                {
                    Donnees_publics.greenWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 3;
                    Donnees_publics.Many = true;
                }
                else if (frobleu == frovert)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.greenWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (frobleu == froviolet)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (frobleu == frorouge)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (frovert == froviolet)
                {
                    Donnees_publics.greenWin = true;
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (frovert == frorouge)
                {
                    Donnees_publics.greenWin = true;
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (froviolet == frorouge)
                {
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 2;
                    Donnees_publics.Many = true;
                }
                else if (frobleu > frovert && frobleu > froviolet && frobleu > frorouge)
                {
                    Donnees_publics.blueWin = true;
                    Donnees_publics.HasWin = 1;
                }
                else if (frovert > frobleu && frovert > froviolet && frovert > frorouge)
                {
                    Donnees_publics.greenWin = true;
                    Donnees_publics.HasWin = 1;
                }
                else if (froviolet > frobleu && froviolet > frovert && froviolet > frorouge)
                {
                    Donnees_publics.purpleWin = true;
                    Donnees_publics.HasWin = 1;
                }
                else if (frorouge > frobleu && frorouge > frovert && frorouge > froviolet)
                {
                    Donnees_publics.redWin = true;
                    Donnees_publics.HasWin = 1;
                }
            }
        }

        private void timerTambour_Tick(object sender, EventArgs e)
        {
            timerTambour.Stop();
            Son.Stream = Properties.Resources.Tada;
            Son.Play();
            annoncevainqueur.ShowDialog();
            if(Donnees_publics.WantCompetition == true)
            {
                nbfro = 2;
                gameon = true;
                lbGOGO.Text = "GO! GO! GO! The first who get home win ☺";
            }
        }

        private void WantComp()
        {
            if (player == 1)
            {
                if (bluemouse.Left == 0 && bluemouse.Top == 0)
                {
                    Donnees_publics.Many = false;
                    lbGOGO.Text = null;
                    Donnees_publics.greenWin = false;
                    Donnees_publics.purpleWin = false;
                    Donnees_publics.redWin = false;
                    annoncevainqueur.ShowDialog();
                    gameon = false;
                }
                else
                {
                    execcours = false;
                    player = 2;
                    nextplayer = true;
                    dicechoise.Image = null;
                    greenmouse.BringToFront();
                }
            }
            else if (player == 2)
            {
                if (greenmouse.Left == 616 && greenmouse.Top == 0)
                {
                    Donnees_publics.Many = false;
                    lbGOGO.Text = null;
                    Donnees_publics.blueWin = false;
                    Donnees_publics.purpleWin = false;
                    Donnees_publics.redWin = false;
                    annoncevainqueur.ShowDialog();
                    gameon = false;
                }
                else
                {
                    if (nbjoueurs >= 3)
                    {
                        execcours = false;
                        player = 3;
                        nextplayer = true;
                        dicechoise.Image = null;
                        purplemouse.BringToFront();
                    }
                    else
                    {
                        execcours = false;
                        player = 1;
                        nextplayer = true;
                        dicechoise.Image = null;
                        bluemouse.BringToFront();
                    }
                }
            }
            else if (player == 3)
            {
                if (purplemouse.Left == 0 && purplemouse.Top == 595)
                {
                    Donnees_publics.Many = false;
                    lbGOGO.Text = null;
                    Donnees_publics.blueWin = false;
                    Donnees_publics.greenWin = false;
                    Donnees_publics.redWin = false;
                    annoncevainqueur.ShowDialog();
                    gameon = false;
                }
                else
                {
                    if (nbjoueurs == 4)
                    {
                        execcours = false;
                        player = 4;
                        nextplayer = true;
                        dicechoise.Image = null;
                        redmouse.BringToFront();
                    }
                    else
                    {
                        execcours = false;
                        player = 1;
                        nextplayer = true;
                        dicechoise.Image = null;
                        bluemouse.BringToFront();
                    }
                }
            }
            else if(player == 4)
            {
                if (redmouse.Left == 616 && redmouse.Top == 595)
                {
                    Donnees_publics.Many = false;
                    lbGOGO.Text = null;
                    Donnees_publics.blueWin = false;
                    Donnees_publics.greenWin = false;
                    Donnees_publics.purpleWin = false;
                    annoncevainqueur.ShowDialog();
                    gameon = false;
                }
                else
                {
                    execcours = false;
                    player = 1;
                    nextplayer = true;
                    dicechoise.Image = null;
                    bluemouse.BringToFront();
                }
            }
        }

        //initiation d une fonction A qui permettre si jamais la fonction random donne la meme position a deux fromage, on reprend le for a la picturebox A pour lui donner une nouvelle position
        int a = 1;

        //declaration d une fonction qui generera aleatoirement une nouvelle postion pour les fromages si la fonction teleport a ete choisi
        Random NewLocTeleport = new Random();
        int Left = 0, Top = 0;

        private void TouchTeleport(int a)
        {
            for(int i = a; i <= 12; i++)
            {
                //une nouvelle position sur le plateau de jeu est choisi pour les fromages
                Left = NewLocTeleport.Next(0, nbpositionLeft.Length);
                Top = NewLocTeleport.Next(0, nbpositionTop.Length);

                Fromage[i].Left = nbpositionLeft[Left];
                Fromage[i].Top = nbpositionTop[Top];

                //on verifie si la nouvelle position des fromages n'est pas la meme que celle d un autre fromage ou d une des souris presente sur le plateau de jeu
                for (int j = 1; j <= 12; j++)
                    if(i!=j && (Fromage[i].Left == Fromage[j].Left && Fromage[i].Top == Fromage[j].Top || Fromage[i].Left == bluemouse.Left && Fromage[i].Top == bluemouse.Top || Fromage[i].Left == greenmouse.Left && Fromage[i].Top == greenmouse.Top || Fromage[i].Left == purplemouse.Left && Fromage[i].Top == purplemouse.Top || Fromage[i].Left == redmouse.Left && Fromage[i].Top == redmouse.Top))
                    {
                        a = i;
                        SameLocTeleport(a);
                    }
                    else if(Fromage[i].Left == 0 && Fromage[i].Top == 0 || Fromage[i].Left == 616 && Fromage[i].Top == 0 || Fromage[i].Left == 0 && Fromage[i].Top == 595 || Fromage[i].Left == 616 && Fromage[i].Top == 595)
                    {
                        a = i;
                        SameLocTeleport(a);
                    }
            }
        }

        private void SameLocTeleport(int a)
        {
            //initiation d'une variable qui nous permettra de savoir si jamais la nouvelle location choisi tombe sur une autre picturebox
            bool SameToo = false;

            Left = NewLocTeleport.Next(0, nbpositionLeft.Length);
            Top = NewLocTeleport.Next(0, nbpositionTop.Length);

            Fromage[a].Left = nbpositionLeft[Left];
            Fromage[a].Top = nbpositionTop[Top];

            for (int i = 1; i <= 12; i++)
                if (a != i && (Fromage[a].Left == Fromage[i].Left && Fromage[a].Top == Fromage[i].Top || Fromage[a].Left == bluemouse.Left && Fromage[a].Top == bluemouse.Top || Fromage[a].Left == greenmouse.Left && Fromage[a].Top == greenmouse.Top || Fromage[a].Left == purplemouse.Left && Fromage[a].Top == purplemouse.Top || Fromage[a].Left == redmouse.Left && Fromage[a].Top == redmouse.Top))
                {
                    SameToo = true;
                    break;
                }
            if (Fromage[a].Left == 0 && Fromage[a].Top == 0 || Fromage[a].Left == 616 && Fromage[a].Top == 0 || Fromage[a].Left == 0 && Fromage[a].Top == 595 || Fromage[a].Left == 616 && Fromage[a].Top == 595)
                SameToo = true;

            if (SameToo == true)
                SameLocTeleport(a);
        }

        private void btnRUN_MouseMove(object sender, MouseEventArgs e)
        {
            //determination du joueur pour mettre la couleur de sa souris au fond ecran bouton Run quand entre sur le btn Run
            if (player == 1)
                btnRUN.BackgroundImage = Properties.Resources.blue_button1;
            else if (player == 2)
                btnRUN.BackgroundImage = Properties.Resources.green_button1;
            else if (player == 3)
                btnRUN.BackgroundImage = Properties.Resources.purple_button1;
            else if (player == 4)
                btnRUN.BackgroundImage = Properties.Resources.red_button1;
        }

        private void btnRUN_MouseLeave(object sender, EventArgs e)
        {
            //determination du joueur pour remettre la couleur de depart au fond ecran du bouton Run quand il ressort du btn Run
            if (player == 1)
                btnRUN.BackgroundImage = null;
            else if (player == 2)
                btnRUN.BackgroundImage = null;
            else if (player == 3)
                btnRUN.BackgroundImage = null;
            else if (player == 4)
                btnRUN.BackgroundImage = null;
        }

        private void PctBoxSon_Click(object sender, EventArgs e)
        {
            if(playson == true)
            {
                arriereson.Mute(true);
                PctBoxSon.Image = Properties.Resources.soncoupe;
                playson = false;
            }
            else
            {
                arriereson.Mute(false);
                PctBoxSon.Image = Properties.Resources.soncours;
                playson = true;
            }
        }

        private void error(object sender, EventArgs e)
        {
            if (temps == 0)
            {
                timererror.Stop();
                temps = 3;
                lbmeseror.Text = null;
            }
            else
            {
                //determination de l erreur, puis affichage de celle ci
                switch (erreur)
                {
                    case 1:

                        lbmeseror.Text = "! The dice has already\r\nbeen cast !";

                        break;

                    case 2:

                        lbmeseror.Text = "!  The dice hasn't stopped  !";

                        break;

                    case 3:

                        lbmeseror.Text = "!  The nomber of movements\r\nchosen doesn\'t correspond \r\nto that made by the dice !";

                        break;

                    case 4:

                        lbmeseror.Text = "!  This action is impossible  !";

                        break;
                    case 5:

                        lbmeseror.Text = "!  The sequence is running,\r   cannot be changed!";

                        break;
                }
                temps--;
            }
        }


        /*Les ERREURS:
             - erreur 1 = le des a deja etait lance par le joueur respectif(il ne peut pas le lancer autant de fois qu il veut, seulement 1 fois)
             - erreur 2 = le des est en train de se "melanger", donc le joueur ne peut ni jouer ni choisir des mouvements
             - erreur 3 = le nombre de mouvement ne correspond pas a ceux du des, donc la souris ne bougera pas
             - erreur 4 = la souris est sorti du plateau de jeu
             - erreur 5 = la chaine de mouvement est en train d etre executer, impossible de la reinitialiser
        */
    }
}
