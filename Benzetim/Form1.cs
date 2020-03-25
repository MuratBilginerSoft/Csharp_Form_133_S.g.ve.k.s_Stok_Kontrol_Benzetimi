using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Benzetim
{
    public partial class Form1 : Form
    {
        #region Metodlar

        #endregion

        #region Tanımlamalar
        Random r = new Random();
        ArrayList BenzetimZamanı = new ArrayList();
        ArrayList BaşlangıçStokPozisyonu = new ArrayList();
        ArrayList BaşlangıçStokSeviyesi = new ArrayList();
        ArrayList GelenSiparişVarmı = new ArrayList();
        ArrayList GelenSiparişMiktarı = new ArrayList();
        ArrayList Talep = new ArrayList();
        ArrayList SonStok = new ArrayList();
        ArrayList KayıpStok = new ArrayList();
        ArrayList SiparişVerildimi = new ArrayList();
        ArrayList TedarikSüresi = new ArrayList();
        ArrayList AçılanSipariş = new ArrayList();
        ArrayList BitişStokPozisyonu = new ArrayList();
        ArrayList E_B_M = new ArrayList();
        ArrayList KayıpSatışMaliyeti = new ArrayList();
        ArrayList SiparişMaliyeti = new ArrayList();
        ArrayList ToplamMaliyet = new ArrayList();


        #endregion

        #region Değişkenler

        int benzetimdöngüsü = 0;
        double taleprs = 0;
        int talepmiktarı = 0;
        int sonstok = 0;
        int tedariksüresi;
        double tedarikrs = 0;
        double eldetutmamaliyeti;
        double toplammaliyet;

        int a = 0;
        #endregion



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            benzetimdöngüsü = int.Parse(maskedTextBox1.Text);

            for (int i = 0; i < benzetimdöngüsü; i++)
            {
                BenzetimZamanı.Add(i + 1);
                taleprs = (r.Next(0, 100)) * 0.01;
                talepmiktarı = Convert.ToInt32(80 + (50 * taleprs));

                if (a == 0)
                {
                    #region İlkişlem
                    BaşlangıçStokPozisyonu.Add(320);
                    BaşlangıçStokSeviyesi.Add(320);
                    Talep.Add(talepmiktarı);
                    sonstok = Convert.ToInt32(BaşlangıçStokSeviyesi[i]) - Convert.ToInt32(Talep[i]);
                    SonStok.Add(sonstok);
                    if (sonstok < 0)
                    {
                        KayıpStok.Add(sonstok);
                        KayıpSatışMaliyeti.Add(-sonstok * 100);
                    }
                    else
                    {
                        KayıpStok.Add(0);
                        KayıpSatışMaliyeti.Add(0);
                    }

                    if (Convert.ToInt32(SonStok[i]) < 300)
                    {
                        SiparişVerildimi.Add(1);
                        tedarikrs = (r.Next(0, 100)) * 0.01;
                        if (tedarikrs < 0.75)
                        {
                            tedariksüresi = 2;
                            TedarikSüresi.Add(tedariksüresi);
                        }

                        else
                        {
                            tedariksüresi = 3;
                            TedarikSüresi.Add(tedariksüresi);
                        }
                    }

                    if (Convert.ToInt32(SiparişVerildimi[i]) == 1)
                    {
                        AçılanSipariş.Add(320);
                        BitişStokPozisyonu.Add(Convert.ToInt32(SonStok[i]) + 320);
                        SiparişMaliyeti.Add(50);

                    }


                    eldetutmamaliyeti = Convert.ToInt32(SonStok[i]) * 0.2;
                    E_B_M.Add(eldetutmamaliyeti);
                    toplammaliyet = Convert.ToDouble(E_B_M[i]) + Convert.ToInt32(KayıpSatışMaliyeti[i]) + Convert.ToInt32(SiparişMaliyeti[i]);
                    ToplamMaliyet.Add(toplammaliyet);

                    listBox1.Items.Add(Convert.ToInt32(BenzetimZamanı[i]));
                    listBox2.Items.Add(Convert.ToInt32(BaşlangıçStokPozisyonu[i]));
                    listBox3.Items.Add(Convert.ToInt32(BaşlangıçStokSeviyesi[i]));
                    listBox4.Items.Add("Hayır");
                    listBox5.Items.Add(0);
                    listBox6.Items.Add(Convert.ToInt32(Talep[i]));
                    listBox7.Items.Add(Convert.ToInt32(SonStok[i]));
                    listBox8.Items.Add(Convert.ToInt32(KayıpStok[i]));
                    listBox9.Items.Add("Evet");
                    listBox10.Items.Add(Convert.ToInt32(BitişStokPozisyonu[i]));
                    listBox11.Items.Add(Convert.ToInt32(TedarikSüresi[i]));
                    listBox12.Items.Add(Convert.ToInt32(E_B_M[i]));
                    listBox13.Items.Add(Convert.ToInt32(KayıpSatışMaliyeti[i]));
                    listBox14.Items.Add(Convert.ToInt32(SiparişMaliyeti[i]));
                    listBox15.Items.Add(Convert.ToInt32(ToplamMaliyet[i]));

                    a++;
                    #endregion
                }

                else
                {
                    listBox1.Items.Add(Convert.ToInt32(BenzetimZamanı[i]));

                    BaşlangıçStokPozisyonu.Add(Convert.ToInt32(BitişStokPozisyonu[i - 1]));
                    listBox2.Items.Add(Convert.ToInt32(BaşlangıçStokPozisyonu[i]));

                    if (tedariksüresi==0)
                    {
                        BaşlangıçStokSeviyesi.Add(320);
                    }

                    else
                    {
                        BaşlangıçStokSeviyesi.Add(SonStok[i - 1]);
                    }
                   
                    listBox3.Items.Add(Convert.ToInt32(BaşlangıçStokSeviyesi[i]));

                    tedariksüresi--;

                    if (tedariksüresi >= 0)
                    {
                        listBox4.Items.Add("Hayır");
                        listBox5.Items.Add(0);
                    }

                    else
                    {
                        listBox4.Items.Add("Evet");
                        listBox5.Items.Add(320);
                    }

                    

                    Talep.Add(talepmiktarı);
                    listBox6.Items.Add(Convert.ToInt32(Talep[i]));

                    sonstok = Convert.ToInt32(BaşlangıçStokSeviyesi[i]) - Convert.ToInt32(Talep[i]);

                    if (sonstok < 0)
                    {
                        KayıpStok.Add(-sonstok);
                        KayıpSatışMaliyeti.Add(-sonstok * 100);
                        SonStok.Add(0);
                    }
                    else
                    {
                        KayıpStok.Add(0);
                        KayıpSatışMaliyeti.Add(0);
                        SonStok.Add(sonstok);
                    }

                    listBox7.Items.Add(Convert.ToInt32(SonStok[i]));
                    listBox8.Items.Add(Convert.ToInt32(KayıpStok[i]));

                    if (tedariksüresi < 0 && Convert.ToInt32(SonStok[i]) < 300)
                    {

                        SiparişVerildimi.Add(1);
                        tedarikrs = (r.Next(0, 100)) * 0.01;
                        if (tedarikrs < 0.75)
                        {
                            tedariksüresi = 2;
                            TedarikSüresi.Add(tedariksüresi);
                            listBox11.Items.Add(Convert.ToInt32(TedarikSüresi[i]));
                        }

                        else
                        {
                            tedariksüresi = 3;
                            TedarikSüresi.Add(tedariksüresi);
                            listBox11.Items.Add(Convert.ToInt32(TedarikSüresi[i]));
                        }

                        listBox9.Items.Add("Evet");

                    }

                    else if (tedariksüresi >= 0)
                    {
                        TedarikSüresi.Add(0);
                        SiparişVerildimi.Add(0);
                        listBox9.Items.Add("Hayır");
                        listBox11.Items.Add("-");
                    }


                    if (Convert.ToInt32(SiparişVerildimi[i]) == 1)
                    {
                        AçılanSipariş.Add(320);
                        BitişStokPozisyonu.Add(Convert.ToInt32(SonStok[i]) + 320);
                        SiparişMaliyeti.Add(50);
                        listBox10.Items.Add(Convert.ToInt32(BitişStokPozisyonu[i]));
                    }

                    else if (Convert.ToInt32(SiparişVerildimi[i]) == 0)
                    {
                        AçılanSipariş.Add(0);
                        BitişStokPozisyonu.Add(Convert.ToInt32(SonStok[i]) + 320);
                        SiparişMaliyeti.Add(0);
                        listBox10.Items.Add(Convert.ToInt32(BitişStokPozisyonu[i]));
                    }

                    eldetutmamaliyeti = Convert.ToInt32(SonStok[i]) * 0.2;
                    E_B_M.Add(eldetutmamaliyeti);
                    toplammaliyet = Convert.ToDouble(E_B_M[i]) + Convert.ToInt32(KayıpSatışMaliyeti[i]) + Convert.ToInt32(SiparişMaliyeti[i]);
                    ToplamMaliyet.Add(toplammaliyet);

                    listBox12.Items.Add(Convert.ToInt32(E_B_M[i]));
                    listBox13.Items.Add(Convert.ToInt32(KayıpSatışMaliyeti[i]));
                    listBox14.Items.Add(Convert.ToInt32(SiparişMaliyeti[i]));
                    listBox15.Items.Add(Convert.ToInt32(ToplamMaliyet[i]));
                }

                a++;
            }

            toplammaliyet = 0;

            for (int i = 0; i < ToplamMaliyet.Count; i++)
            {
                toplammaliyet += Convert.ToInt32(ToplamMaliyet[i]);
            }

            label19.Text = toplammaliyet.ToString();
        }
    }
}