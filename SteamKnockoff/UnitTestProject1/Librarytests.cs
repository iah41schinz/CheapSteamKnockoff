﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteamKnockoff;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class Librarytests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SpielHinzufügen_Spielattribut_ist_NULL()
        {
            Library ILibrary = new Library();
            ILibrary.SpielHinzufügen("Dead Island", "19.06.2017 10:30", "NA", "C:\\Games\\Dead Island Definitive Edition\\DeadIslandGame.exe", "Horror, RPG", null, 6);
        }

        [TestMethod]
        public void SpielHinzufügen_Spiel_ist_in_der_liste()
        {
            Library ILibrary = new Library();
            ILibrary.SpielHinzufügen("Dead Island", "19.06.2017 10:30", "NA", "C:\\Games\\Dead Island Definitive Edition\\DeadIslandGame.exe", "Horror, RPG", "THQ", 6);
            Assert.AreEqual("Dead Island", ILibrary.SpieleListe[0].Titel);
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SpielSpeichern_uebergebenes_Spiel_löst_Exception_aus()
        {
            Library ILibrary = new Library();
            Spiel ISpiel = null;
            Library.SpielSpeichern(ISpiel);
        }

        [TestMethod]
        public void XmlSpeichern_erstellt_XmlDocument()
        {
            Library ILibrary = new Library();
            ILibrary.XmlSpeichern();
            FileInfo Xmlfile = new FileInfo(@"..//Saves/XmlSave.xml");
            if (Xmlfile.Exists == false)
            {
                Assert.Fail();
            }
        }

    }
}
