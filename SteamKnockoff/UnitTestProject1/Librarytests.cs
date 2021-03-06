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
        [ExpectedException(typeof(FileNotFoundException))]
        public void SpielHinzufügen_Spielpfad_Existiert_nicht_Exception()
        {
            Library ILibrary = new Library();
            ILibrary.SpielHinzufügen("Dead Island", "19.06.2017 10:30", "NA", @"C:\Games\Dead Island Definitive Edition\NotDeadIslandGame.exe", "Horror, RPG", "THQ", 6);
        }
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
            ILibrary.SpieleListe.Clear();
            ILibrary.SpielHinzufügen("Dead Island", "19.06.2017 10:30", "NA", @"C:\Games\Dead Island Definitive Edition\DeadIslandGame.exe", "Horror, RPG", "THQ", 6);
            Assert.AreEqual("Dead Island", ILibrary.SpieleListe[0].Titel);
        }

        [TestMethod]
        public void XmlSpeichern_Schreibt_in_Xml_document()
        {
            FileInfo Xmlfile = new FileInfo(@"..\..\XmlSave.xml");
            Library ILibrary = new Library();
            Assert.IsTrue(ILibrary.XmlSpeichern(ILibrary.DefaultXmlPath));
        }

        [TestMethod]
        public void XmlLaden_auslesen_des_Dokuments_erfolgreich()
        {
            Library ILibrary = new Library();
            Assert.IsTrue(ILibrary.XmlLaden(ILibrary.DefaultXmlPath));
            File.Delete(ILibrary.DefaultXmlPath);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void SpielLöschen_indexOutOfRange()
        {
            int i = 0;
            Library ILibrary = new Library();
            ILibrary.SpielLöschen(i);
        }
    }
}
