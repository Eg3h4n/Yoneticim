using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yoneticim.Controllers
{
    public class CoderController : Controller
    {
        public string GenerateSampleData()
        {
            try
            {
                YoneticimDBEntities db = new YoneticimDBEntities();
                InsertSampleYoneticiler(db);
                InsertSampleGorevliTurleri(db);
                InsertSampleSiteAndAparts(db);
                InsertSampleBlocks(db);
                InsertSampleFlats(db);
                InsertSampleStaff(db);
                InsertSampleFinancialItems(db);

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace;
            }
        }

        private void InsertSampleGorevliTurleri(YoneticimDBEntities db)
        {
            GorevliTurleri kapici = new GorevliTurleri()
            {
                Adi = "Kapıcı"
            };

            GorevliTurleri guvenlikci = new GorevliTurleri()
            {
                Adi = "Güvenlikçi"
            };

            GorevliTurleri temizlikci = new GorevliTurleri()
            {
                Adi = "Temizlikçi"
            };

            db.GorevliTurleris.Add(kapici);
            db.GorevliTurleris.Add(temizlikci);
            db.GorevliTurleris.Add(guvenlikci);

            db.SaveChanges();
        }

        private void InsertSampleFlats(YoneticimDBEntities db)
        {
            List<Mulkler> apartsBlokcs = db.Mulklers.Where(x => x.BlokSayisi == 1).ToList();

            foreach (Mulkler apartBlock in apartsBlokcs)
            {
                for (int k = 0; k < 8; k++)
                {
                    Daireler daire = new Daireler()
                    {
                        EPosta = FakeData.NetworkData.GetEmail(),
                        Kat = (byte)(k + 1),
                        KiraciAdi = FakeData.NameData.GetFirstName(),
                        KiraciSoyadi = FakeData.NameData.GetSurname(),
                        KiraciTel = FakeData.PhoneNumberData.GetPhoneNumber(),
                        No = (k * 100) + 1,
                        SahibiAdi = FakeData.NameData.GetFirstName(),
                        SahibiSoyadi = FakeData.NameData.GetSurname(),
                        SahibiTel = FakeData.PhoneNumberData.GetPhoneNumber(),
                        Sifre = "123"
                    };

                    apartBlock.Dairelers.Add(daire);
                }
            }

            db.SaveChanges();
        }

        private void InsertSampleFinancialItems(YoneticimDBEntities db)
        {
            foreach (Mulkler mulk in db.Mulklers)
            {
                // Sadece Mülk e ait olan kalemler..
                for (int i = 0; i < FakeData.NumberData.GetNumber(15, 30); i++)
                {
                    Kalemler kalem = new Kalemler()
                    {
                        Ay = FakeData.NumberData.GetNumber(1, 12),
                        Yıl = FakeData.NumberData.GetNumber(2014, 2015),
                        AidatMi = false,
                        GelirMi = FakeData.BooleanData.GetBoolean(),
                        Tutar = (decimal)FakeData.NumberData.GetDouble(),
                        Açıklama = "Açıklama : " + FakeData.TextData.GetAlphabetical(10)
                    };

                    kalem.Tutar = (kalem.GelirMi) ? kalem.Tutar : kalem.Tutar * (-1);

                    mulk.Kalemlers.Add(kalem);
                }

                // Daire Aidat kalemleri
                foreach (Daireler daire in mulk.Dairelers)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        Kalemler kalem1 = new Kalemler()
                        {
                            Ay = i,
                            Yıl = 2014,
                            AidatMi = true,
                            GelirMi = true,
                            Tutar = 150,
                            Açıklama = $"Aidat {i}/2014 : {daire.Kat}-{daire.No}",
                            Daireler = daire,
                            Mulkler = mulk
                        };

                        db.Kalemlers.Add(kalem1);

                        Kalemler kalem2 = new Kalemler()
                        {
                            Ay = i,
                            Yıl = 2015,
                            AidatMi = true,
                            GelirMi = true,
                            Tutar = 200,
                            Açıklama = $"Aidat {i}/2015 : {daire.Kat}-{daire.No}",
                            Daireler = daire,
                            Mulkler = mulk
                        };

                        db.Kalemlers.Add(kalem2);
                    }
                }

                foreach (Gorevliler gorevli in mulk.Gorevlilers)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        Kalemler kalem1 = new Kalemler()
                        {
                            Ay = i,
                            Yıl = 2014,
                            AidatMi = true,
                            GelirMi = false,
                            Tutar = 1500,
                            Açıklama = $"Maaş {i}/2014 : {gorevli.TcNo}",
                            Mulkler = mulk,
                            Gorevliler = gorevli
                        };

                        db.Kalemlers.Add(kalem1);

                        Kalemler kalem2 = new Kalemler()
                        {
                            Ay = i,
                            Yıl = 2015,
                            AidatMi = true,
                            GelirMi = false,
                            Tutar = 1750,
                            Açıklama = $"Maaş {i}/2015 : {gorevli.TcNo}",
                            Mulkler = mulk,
                            Gorevliler = gorevli
                        };

                        db.Kalemlers.Add(kalem2);
                    }
                }

            }

            db.SaveChanges();
        }

        private void InsertSampleStaff(YoneticimDBEntities db)
        {
            List<GorevliTurleri> turler = db.GorevliTurleris.ToList();

            foreach (Mulkler mulk in db.Mulklers)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1,3); i++)
                {
                    Gorevliler gorevli = new Gorevliler()
                    {
                        Adi = FakeData.NameData.GetFirstName(),
                        Soyadi = FakeData.NameData.GetSurname(),
                        GorevliTurleri = turler[FakeData.NumberData.GetNumber(0, turler.Count - 1)],
                        IsTanimi = FakeData.TextData.GetSentence(),
                        Maasi = FakeData.NumberData.GetNumber(1000, 2000),
                        SigortaNo = "S" + FakeData.NumberData.GetNumber(10000, 30000),
                        TcNo = FakeData.NumberData.GetNumber(20000, 30000).ToString() + FakeData.NumberData.GetNumber(20000, 30000).ToString(),
                    };

                    mulk.Gorevlilers.Add(gorevli);
                }
            }

            db.SaveChanges();
        }

        private void InsertSampleSiteAndAparts(YoneticimDBEntities db)
        {
            // Site ve apt olan kayıtlar atıldı.
            for (int i = 0; i < FakeData.NumberData.GetNumber(4, 6); i++)
            {
                Mulkler mulk = new Mulkler()
                {
                    Adres = FakeData.PlaceData.GetAddress(),
                    BlokSayisi = (byte)FakeData.NumberData.GetNumber(1, 5)
                };

                if (mulk.BlokSayisi > 1)
                    mulk.Adi = $"Site-{i}";
                else
                    mulk.Adi = $"Apt-{i}";

                db.Mulklers.Add(mulk);
            }

            db.SaveChanges();
        }

        private void InsertSampleBlocks(YoneticimDBEntities db)
        {
            List<Mulkler> siteler = db.Mulklers.Where(x => x.BlokSayisi > 1).ToList();

            foreach (Mulkler site in siteler)
            {
                for (int i = 0; i < site.BlokSayisi; i++)
                {
                    Mulkler mulk = new Mulkler()
                    {
                        Adi = $"Blok-{i}",
                        BlokSayisi = 1,
                        Mulkler2 = site
                    };

                    db.Mulklers.Add(mulk);
                }
            }

            db.SaveChanges();
        }

        private void InsertSampleYoneticiler(YoneticimDBEntities db)
        {
            Yoneticiler yonetici1 = new Yoneticiler()
            {
                Ad = "Egehan",
                Soyad = "Gür",
                EPosta = "egehan@mail.com",
                Sifre = "123"
            };

            Yoneticiler yonetici2 = new Yoneticiler()
            {
                Ad = "Murat",
                Soyad = "Başeren",
                EPosta = "murat@mail.com",
                Sifre = "123"
            };

            db.Yoneticilers.Add(yonetici1);
            db.Yoneticilers.Add(yonetici2);

            db.SaveChanges();
        }
    }
}